﻿using System.Linq;
using ch1seL.TonNet.ClientGenerator.Models;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace ch1seL.TonNet.ClientGenerator.Helpers

{
    internal static class ClientClassHelpers
    {
        private static MemberDeclarationSyntax[] GetProperties(TonApi tonApi)
        {
            MemberDeclarationSyntax[] propertyDeclarationSyntaxes = tonApi.Modules
                .Select(module =>
                {
                    var formattedName = NamingConventions.Normalize(module.Name);

                    var summary = module.Summary + (module.Description != null ? $"\n{module.Description}" : null);

                    return PropertyDeclaration(IdentifierName($"I{formattedName}Module"), formattedName)
                        .WithModifiers(TokenList(Token(SyntaxKind.PublicKeyword).WithLeadingTrivia(CommentsHelpers.BuildCommentTrivia(summary))))
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

        public static NamespaceDeclarationSyntax CreateTonClientClass(string unitName, TonApi tonApi)
        {
            var propertyDeclarationSyntaxes = GetProperties(tonApi);
            var moduleNames = tonApi.Modules.Select(m => m.Name).ToArray();

            VariableDeclarationSyntax variableDeclaration = VariableDeclaration(ParseTypeName("ITonClientAdapter"))
                .AddVariables(VariableDeclarator("_tonClientAdapter"));
            FieldDeclarationSyntax fieldDeclaration = FieldDeclaration(variableDeclaration)
                .AddModifiers(Token(SyntaxKind.PrivateKeyword), Token(SyntaxKind.ReadOnlyKeyword));

            var statementSyntax =
                new[]
                    {
                        ParseStatement("_tonClientAdapter = tonClientAdapter;")
                            .WithTrailingTrivia(LineFeed)
                    }
                    .Union(moduleNames
                        .Select(m => ParseStatement(
                            $"{NamingConventions.Normalize(m)} = new {NamingConventions.Normalize(m)}Module(tonClientAdapter);").WithTrailingTrivia(LineFeed)))
                    .ToArray();

            MethodDeclarationSyntax disposeMethod = MethodDeclaration(ParseTypeName("void"), "Dispose")
                .AddModifiers(Token(SyntaxKind.PublicKeyword))
                .AddBodyStatements(ParseStatement("_tonClientAdapter?.Dispose();"));

            ConstructorDeclarationSyntax constructorDeclaration = ConstructorDeclaration(unitName)
                .AddParameterListParameters(Parameter(Identifier("tonClientAdapter")).WithType(IdentifierName("ITonClientAdapter")))
                .WithModifiers(TokenList(Token(SyntaxKind.PublicKeyword)))
                .WithBody(Block(statementSyntax));

            ClassDeclarationSyntax item = ClassDeclaration(unitName)
                .AddModifiers(Token(SyntaxKind.PublicKeyword))
                .AddBaseListTypes(SimpleBaseType(IdentifierName("ITonClient")), SimpleBaseType(IdentifierName("IDisposable")))
                .AddMembers(fieldDeclaration)
                .AddMembers(constructorDeclaration)
                .AddMembers(propertyDeclarationSyntaxes)
                .AddMembers(disposeMethod);

            return NamespaceDeclaration(IdentifierName(ClientGenerator.Namespace))
                .AddMembers(item);
        }

        public static NamespaceDeclarationSyntax CreateTonClientInterface(string unitName, TonApi tonApi)
        {
            var propertyDeclarationSyntaxes = GetProperties(tonApi);

            InterfaceDeclarationSyntax item = InterfaceDeclaration(unitName)
                .AddModifiers(Token(SyntaxKind.PublicKeyword))
                .AddMembers(propertyDeclarationSyntaxes);

            return NamespaceDeclaration(IdentifierName(ClientGenerator.NamespaceAbstract))
                .AddMembers(item);
        }
    }
}