using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using ch1seL.TonNet.ClientGenerator.Models;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace ch1seL.TonNet.ClientGenerator.Helpers
{
    internal static class ModulesClassHelpers
    {
        private static MemberDeclarationSyntax GetMethodDeclaration(Module module, Function function, bool withBody)
        {
            var responseType = function.Result.GetMethodReturnType();
            var responseDeclaration = responseType == null ? "Task" : $"Task<{responseType}>";

            // request parameter
            var requestParam = function.Params.Length >= 2
                ? new
                {
                    name = StringUtils.EscapeReserved(function.Params[1].Name.GetEnumMemberValueOrString()),
                    type = NamingConventions.Normalize(function.Params[1].RefName)
                }
                : null;

            //callback parameter
            var callbackParam = function.Params.Length >= 3
                ? new
                {
                    name = StringUtils.EscapeReserved(function.Params[2].Name.GetEnumMemberValueOrString()),
                    nameWithNull = $"{StringUtils.EscapeReserved(function.Params[2].Name.GetEnumMemberValueOrString())} = null",
                    type = string.Equals(module.Name, "net", StringComparison.OrdinalIgnoreCase)
                        ? "JsonElement"
                        : NamingConventions.EventFormatter(module.Name)
                }
                : null;

            var modifiers = new List<SyntaxToken> {Token(SyntaxKind.PublicKeyword).WithLeadingTrivia(CommentsHelpers.BuildCommentTrivia(function.Description))};
            if (withBody) modifiers.Add(Token(SyntaxKind.AsyncKeyword));

            var @params = new List<ParameterSyntax>();
            var methodDeclarationParams = new List<ParameterSyntax>();
            if (requestParam != null)
            {
                ParameterSyntax param = Parameter(Identifier(requestParam.name)).WithType(IdentifierName(requestParam.type));
                methodDeclarationParams.Add(param);
                @params.Add(param);
            }

            if (callbackParam != null)
            {
                methodDeclarationParams.Add(Parameter(Identifier(callbackParam.nameWithNull)).WithType(IdentifierName($"Action<{callbackParam.type},uint>")));
                @params.Add(Parameter(Identifier(callbackParam.name)).WithType(IdentifierName($"Action<{callbackParam.type},uint>")));
            }

            MethodDeclarationSyntax method =
                MethodDeclaration(ParseTypeName(responseDeclaration), NamingConventions.Normalize(function.Name))
                    .AddParameterListParameters(methodDeclarationParams.ToArray())
                    .AddParameterListParameters(Parameter(Identifier("cancellationToken"))
                        .WithType(IdentifierName(nameof(CancellationToken)))
                        .WithDefault(EqualsValueClause(IdentifierName("default"))))
                    .AddModifiers(modifiers.ToArray());

            if (withBody)
            {
                var arguments = new List<ArgumentSyntax>
                {
                    Argument(IdentifierName($"\"{module.Name}.{function.Name}\""))
                };
                arguments.AddRange(
                    @params
                        .Select(p => Argument(IdentifierName(p.Identifier.Text))));
                arguments.Add(Argument(IdentifierName("cancellationToken")));

                var genericParametersDeclaration =
                    StringUtils.GetGenericParametersDeclaration(@params.FirstOrDefault()?.Type?.ToString(), responseType, callbackParam?.type);

                AwaitExpressionSyntax awaitExpression = AwaitExpression(
                    InvocationExpression(IdentifierName($"_tonClientAdapter.Request{genericParametersDeclaration}"))
                        .AddArgumentListArguments(arguments.ToArray()));


                StatementSyntax ex = responseType == null
                    ? (StatementSyntax) ExpressionStatement(awaitExpression)
                    : ReturnStatement(awaitExpression);
                BlockSyntax blockSyntax = Block(ex);
                return method.WithBody(blockSyntax);
            }

            return method.WithSemicolonToken(Token(SyntaxKind.SemicolonToken));
        }

        private static string GetCallbackActionType(string callBackType)
        {
            return $"Action<{callBackType}, uint>";
        }

        public static NamespaceDeclarationSyntax CreateTonModuleClass(string unitName, Module module)
        {
            var moduleName = $"{unitName}Module";

            StatementSyntax statementSyntax = ParseStatement("_tonClientAdapter = tonClientAdapter;");

            VariableDeclarationSyntax variableDeclaration = VariableDeclaration(ParseTypeName("ITonClientAdapter"))
                .AddVariables(VariableDeclarator("_tonClientAdapter"));
            FieldDeclarationSyntax fieldDeclaration = FieldDeclaration(variableDeclaration)
                .AddModifiers(Token(SyntaxKind.PrivateKeyword), Token(SyntaxKind.ReadOnlyKeyword));

            ConstructorDeclarationSyntax constructorDeclaration = ConstructorDeclaration(moduleName)
                .AddParameterListParameters(
                    Parameter(Identifier("tonClientAdapter")).WithType(IdentifierName("ITonClientAdapter")))
                .WithModifiers(TokenList(Token(SyntaxKind.PublicKeyword)))
                .WithBody(Block(statementSyntax));

            var methods = module
                .Functions
                .Select(f => GetMethodDeclaration(module, f, true))
                .ToArray();

            ClassDeclarationSyntax item = ClassDeclaration(moduleName)
                .AddModifiers(Token(SyntaxKind.PublicKeyword))
                .AddBaseListTypes(SimpleBaseType(IdentifierName(NamingConventions.ToInterfaceName(moduleName))))
                .AddMembers(fieldDeclaration)
                .AddMembers(constructorDeclaration)
                .AddMembers(methods);

            return NamespaceDeclaration(IdentifierName(Generator.NameSpaceModules))
                .AddMembers(item);
        }

        public static NamespaceDeclarationSyntax CreateTonModuleInterface(string unitName, Module module)
        {
            var moduleName = $"{unitName}Module";

            var methods = module
                .Functions
                .Select(function => GetMethodDeclaration(module, function, false))
                .ToArray();

            InterfaceDeclarationSyntax item = InterfaceDeclaration($"I{moduleName}")
                .AddModifiers(Token(SyntaxKind.PublicKeyword))
                .AddBaseListTypes(SimpleBaseType(IdentifierName("ITonModule")))
                .AddMembers(methods);

            return NamespaceDeclaration(IdentifierName(Generator.NameSpaceModules))
                .AddMembers(item);
        }
    }
}