using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading;
using ch1seL.TonNet.ClientGenerator.Models;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Formatting;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;
using Formatter = Microsoft.CodeAnalysis.Formatting.Formatter;

namespace ch1seL.TonNet.ClientGenerator.Helpers
{
    internal static class GeneratorHelpers
    {
        private static readonly string[] GenericArgRefNames = {"ClientContext", "Request"};

        public static MemberDeclarationSyntax GetMethodDeclaration(Module module, Function function, bool body)
        {
            var parameters = function.Params
                .Where(p=>!p.IsClientContextParam())
                .Select(param => GetMethodParameter(module, param))
                .Append(Parameter(Identifier("cancellationToken"))
                    .WithType(IdentifierName(nameof(CancellationToken)))
                    .WithDefault(EqualsValueClause(IdentifierName("default"))))
                .ToArray();


            var modifiers = new List<SyntaxToken>(){Token(SyntaxKind.PublicKeyword)};
            if (body)
            {
                modifiers.Add(Token(SyntaxKind.AsyncKeyword));
            }

            MethodDeclarationSyntax method =
                MethodDeclaration(ParseTypeName(function.Result.GetMethodReturnType()), NamingConventions.CommonFormatterFormatter(function.Name))
                    .AddParameterListParameters(parameters)
                    .AddModifiers(modifiers.ToArray());

            if (body)
            {
                var arguments = new List<ArgumentSyntax>
                {
                    Argument(IdentifierName($"\"{module.Name}.{function.Name}\""))
                };
                arguments.AddRange(
                    parameters
                        .Select(p => Argument(IdentifierName(p.Identifier.Text))));

                BlockSyntax blockSyntax = Block(
                    ReturnStatement(AwaitExpression(InvocationExpression(IdentifierName("_tonClientAdapter.Request"))
                        .AddArgumentListArguments(arguments.ToArray())))
                );
                return method.WithBody(blockSyntax);
            }

            return method.WithSemicolonToken(Token(SyntaxKind.SemicolonToken));
        }

        private static ParameterSyntax GetMethodParameter(Module module, Param param)
        {
            var genericArg = param.GenericArgs?[0];
            string typeName = "Undefined";

            switch (genericArg?.RefName)
            {
                case GenericRefNames.Request:
                    typeName = $"Action<{NamingConventions.EventFormatter(module.Name)}>";
                    break;
                default:
                    switch (param.Type)
                    {
                        case ParamType.Ref:
                            typeName = NamingConventions.CommonFormatterFormatter(StringUtils.TypeFromRef(param.RefName));
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                    break;
            }
            
            return Parameter(Identifier(NamingConventions.EscapeReserved(param.Name.GetEnumMemberValueOrString()))).WithType(IdentifierName(typeName));
        }

        public static MemberDeclarationSyntax[] GetModuleProperties(TonApi tonApi)
        {
            MemberDeclarationSyntax[] propertyDeclarationSyntaxes = tonApi.Modules.Select(m => m.Name)
                .Select(moduleName =>
                {
                    var formattedName = NamingConventions.CommonFormatterFormatter(moduleName);
                    
                    return PropertyDeclaration(IdentifierName($"I{formattedName}"), formattedName)
                        .WithModifiers(TokenList(Token(SyntaxKind.PublicKeyword)))
                        .WithAccessorList(
                            AccessorList(
                                List(new[]
                                {
                                    AccessorDeclaration(
                                            SyntaxKind.GetAccessorDeclaration)
                                        .WithSemicolonToken(
                                            Token(SyntaxKind.SemicolonToken))
                                })));
                })
                .ToArray();
            return propertyDeclarationSyntaxes;
        }

        public static void CreateUnit(string unitName, Func<string, NamespaceDeclarationSyntax> nsFactory, string filePathFactory,
            params string[] usings)
        {
            unitName = StringUtils.ToCamelCase(unitName);

            CompilationUnitSyntax cu = CompilationUnit();
            cu = cu.AddUsings(usings.Select(u => UsingDirective(ParseName(u))).ToArray());

            var cw = new AdhocWorkspace();
            cw.Options.WithChangedOption(CSharpFormattingOptions.IndentBlock, true);
            SyntaxNode formattedNode = Formatter.Format(cu.AddMembers(nsFactory(unitName)), cw, cw.Options);

            var file = new FileInfo(filePathFactory);
            DirectoryInfo dir = file.Directory;
            if (!dir!.Exists) dir.Create();

            using FileStream fileStream = file.Exists ? file.Open(FileMode.Truncate) : file.Create();
            using var sw = new StreamWriter(fileStream);
            formattedNode.WriteTo(sw);
        }
    }

    internal static class EnumUtils
    {
        public static string GetEnumMemberValueOrString<T>(this T enumValue) where T : Enum {
            return enumValue.FindAttributeOfType<EnumMemberAttribute>()?.Value ?? enumValue.ToString();
        }

        private static T FindAttributeOfType<T>(this Enum enumValue, Func<T, bool> filter = null) {
            return enumValue.GetType()
                .GetField(enumValue.ToString())!
                .GetCustomAttributes(false)
                .OfType<T>()
                .SingleOrDefault(a => filter?.Invoke(a) ?? true);
        }
    }
}