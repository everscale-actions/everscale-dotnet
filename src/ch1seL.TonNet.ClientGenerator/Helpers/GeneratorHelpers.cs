using System.Collections.Generic;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace ch1seL.TonNet.ClientGenerator.Helpers
{
    internal static class GeneratorHelpers
    {
        public static PropertyDeclarationSyntax CreatePropertyDeclaration(string typeName, string name, bool optional = false, bool nullable = false,
            bool addPostfix=false)
        {
            nullable = optional || nullable;

            var attributes = new List<AttributeSyntax>
            {
                Attribute(IdentifierName($"JsonPropertyName(\"{name}\")"))
            };

            if (nullable) attributes.Add(Attribute(IdentifierName("JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)")));

            AccessorListSyntax accessorListSyntax = AccessorList(
                List(new[]
                {
                    AccessorDeclaration(
                            SyntaxKind.GetAccessorDeclaration)
                        .WithSemicolonToken(
                            Token(SyntaxKind.SemicolonToken)),
                    AccessorDeclaration(
                            SyntaxKind.SetAccessorDeclaration)
                        .WithSemicolonToken(
                            Token(SyntaxKind.SemicolonToken))
                }));

            return PropertyDeclaration(IdentifierName($"{typeName}{(optional ? "?" : null)}"), NamingConventions.Formatter($"{name}{(addPostfix?"Accessor":null)}"))
                .AddAttributeLists(AttributeList(SeparatedList(attributes)))
                .WithModifiers(TokenList(Token(SyntaxKind.PublicKeyword)))
                .WithAccessorList(accessorListSyntax);
        }
    }
}