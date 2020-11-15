using System;
using System.IO;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Formatting;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Formatting;

namespace ch1seL.TonNet.ClientGenerator.Helpers
{
    internal static class UnitHelpers
    {
        public static void CreateUnit(string unitName, Func<string, NamespaceDeclarationSyntax> nsFactory, string filePathFactory,
            params string[] usings)
        {
            unitName = NamingConventions.Formatter(unitName);

            CompilationUnitSyntax cu = SyntaxFactory.CompilationUnit();
            cu = cu.AddUsings(usings.OrderBy(s => s)
                .Select(u => SyntaxFactory.UsingDirective(SyntaxFactory.ParseName(u)))
                .ToArray());

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
}