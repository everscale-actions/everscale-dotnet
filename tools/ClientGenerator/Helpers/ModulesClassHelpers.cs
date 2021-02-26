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
            var requestParam = new {name = default(string), type = default(string)};
            var callbackParam = new {name = default(string), nameWithNull = default(string), type = default(string)};

            foreach (Param param in function.Params)
            {
                if (param.Type == ParamType.Generic && param.GenericName == ParamGenericName.Arc)
                {
                    GenericArg arcArg = param.GenericArgs[0];
                    if (arcArg.Type == GenericArgType.Ref && arcArg.RefName == "Request")
                    {
                        var name = StringUtils.EscapeReserved(function.Params[2].Name.GetEnumMemberValueOrString());
                        callbackParam = new
                        {
                            name,
                            nameWithNull = $"{name} = null",
                            type = string.Equals(module.Name, "net", StringComparison.OrdinalIgnoreCase)
                                ? "JsonElement"
                                : NamingConventions.EventFormatter(module.Name)
                        };
                    }
                }

                if (param.Type == ParamType.Generic && param.GenericName == ParamGenericName.AppObject)
                    callbackParam = new
                    {
                        name = "appObject",
                        nameWithNull = "appObject = null",
                        type = "JsonElement"
                    };

                if (param.Name == Name.Params)
                    requestParam = new
                    {
                        name = StringUtils.EscapeReserved(function.Params[1].Name.GetEnumMemberValueOrString()),
                        type = GetParamType(function.Params[1])
                    };
            }

            var functionSummary = function.Summary + (function.Description != null ? $"\n{function.Description}" : null);
            var modifiers = new List<SyntaxToken> {Token(SyntaxKind.PublicKeyword).WithLeadingTrivia(CommentsHelpers.BuildCommentTrivia(functionSummary))};
            if (withBody) modifiers.Add(Token(SyntaxKind.AsyncKeyword));

            var @params = new List<ParameterSyntax>();
            var methodDeclarationParams = new List<ParameterSyntax>();
            if (requestParam.name != default)
            {
                ParameterSyntax param = Parameter(Identifier(requestParam.name)).WithType(IdentifierName(requestParam.type));
                methodDeclarationParams.Add(param);
                @params.Add(param);
            }

            if (callbackParam.name != default)
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
                    StringUtils.GetGenericParametersDeclaration(requestParam?.type, responseType, callbackParam?.type);

                AwaitExpressionSyntax awaitExpression = AwaitExpression(
                    InvocationExpression(IdentifierName($"_tonClientAdapter.Request{genericParametersDeclaration}"))
                        .AddArgumentListArguments(arguments.ToArray()));


                StatementSyntax ex = responseType == null
                    ? ExpressionStatement(awaitExpression)
                    : ReturnStatement(awaitExpression);
                BlockSyntax blockSyntax = Block(ex);
                return method.WithBody(blockSyntax);
            }

            return method.WithSemicolonToken(Token(SyntaxKind.SemicolonToken));
        }

        private static string GetParamType(Param param)
        {
            return NamingConventions.Normalize(param.RefName);
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

            return NamespaceDeclaration(IdentifierName(ClientGenerator.NamespaceModules))
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

            return NamespaceDeclaration(IdentifierName(ClientGenerator.NamespaceAbstractModules))
                .AddMembers(item);
        }
    }
}