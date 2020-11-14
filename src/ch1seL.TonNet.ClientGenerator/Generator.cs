using System.IO;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Formatting;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Formatting;

namespace Generator
{
    public static class Generator
    {
        public static void GenerateModule()
        {
            CompilationUnitSyntax cu = SyntaxFactory.CompilationUnit()
                .AddUsings(
                    SyntaxFactory.UsingDirective(SyntaxFactory.IdentifierName("System")),
                    SyntaxFactory.UsingDirective(SyntaxFactory.IdentifierName("System.Collections.Generic")),
                    SyntaxFactory.UsingDirective(SyntaxFactory.IdentifierName("System.Linq")),
                    SyntaxFactory.UsingDirective(SyntaxFactory.IdentifierName("System.Text")),
                    SyntaxFactory.UsingDirective(SyntaxFactory.IdentifierName("System.Threading.Tasks")));

            NamespaceDeclarationSyntax localNamespace = SyntaxFactory.NamespaceDeclaration(SyntaxFactory.IdentifierName("test"));
            ClassDeclarationSyntax localClass = SyntaxFactory
                .ClassDeclaration("test")
                .AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword));

            localNamespace = localNamespace.AddMembers(localClass);


            cu = cu.AddMembers(localNamespace);
            var cw = new AdhocWorkspace();
            cw.Options.WithChangedOption(CSharpFormattingOptions.IndentBraces, true);
            SyntaxNode formattedNode = Formatter.Format(cu, cw, cw.Options);


            using FileStream fileStream = new FileInfo("test.cs").OpenWrite();
            using var sw = new StreamWriter(fileStream);
            formattedNode.WriteTo(sw);
        }
    }
}