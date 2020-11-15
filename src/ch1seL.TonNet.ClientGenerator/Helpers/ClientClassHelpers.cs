using System.Linq;
using ch1seL.TonNet.ClientGenerator.Models;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace ch1seL.TonNet.ClientGenerator.Helpers

{
    internal static class ClientClassHelpers
    {
        public static MemberDeclarationSyntax[] GetProperties(TonApi tonApi)
        {
            MemberDeclarationSyntax[] propertyDeclarationSyntaxes = tonApi.Modules.Select(m => m.Name)
                .Select(moduleName =>
                {
                    var formattedName = NamingConventions.Formatter(moduleName);

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

        public static NamespaceDeclarationSyntax CreateTonClientClass(string unitName, TonApi tonApi)
        {
            var propertyDeclarationSyntaxes = GetProperties(tonApi);

            VariableDeclarationSyntax variableDeclaration = VariableDeclaration(ParseTypeName("ITonClientAdapter"))
                .AddVariables(VariableDeclarator("_tonClientAdapter"));
            FieldDeclarationSyntax fieldDeclaration = FieldDeclaration(variableDeclaration)
                .AddModifiers(Token(SyntaxKind.PrivateKeyword));

            StatementSyntax statementSyntax = ParseStatement("_tonClientAdapter = tonClientAdapter;");
            ConstructorDeclarationSyntax constructorDeclaration = ConstructorDeclaration(unitName)
                .AddParameterListParameters(
                    Parameter(Identifier("tonClientAdapter")).WithType(IdentifierName("ITonClientAdapter")))
                .WithModifiers(TokenList(Token(SyntaxKind.PublicKeyword)))
                .WithBody(Block(statementSyntax));

            ClassDeclarationSyntax item = ClassDeclaration(unitName)
                .AddModifiers(Token(SyntaxKind.PublicKeyword))
                .AddMembers(fieldDeclaration)
                .AddMembers(constructorDeclaration)
                .AddMembers(propertyDeclarationSyntaxes);

            return NamespaceDeclaration(IdentifierName(Generator.NameSpace))
                .AddMembers(item);
        }

        public static NamespaceDeclarationSyntax CreateTonClientInterface(string unitName, TonApi tonApi)
        {
            var propertyDeclarationSyntaxes = GetProperties(tonApi);

            InterfaceDeclarationSyntax item = InterfaceDeclaration(unitName)
                .AddModifiers(Token(SyntaxKind.PublicKeyword))
                .AddMembers(propertyDeclarationSyntaxes);

            return NamespaceDeclaration(IdentifierName(Generator.NameSpace))
                .AddMembers(item);
        }
    }
}