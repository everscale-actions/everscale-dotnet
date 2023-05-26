using Microsoft.CodeAnalysis.CSharp.Formatting;
using Microsoft.CodeAnalysis.Formatting;

namespace EverscaleNet.ClientGenerator.Helpers;

internal static class UnitHelpers {
	public static void CreateUnit(string unitName, Func<string, NamespaceDeclarationSyntax> nsFactory, string filePathFactory,
	                              params string[] usings) {
		unitName = NamingConventions.Normalize(unitName);

		CompilationUnitSyntax cu = CompilationUnit();
		cu = cu.AddUsings(usings.OrderBy(s => s)
		                        .Select(u => UsingDirective(ParseName(u)))
		                        .ToArray());

		var cw = new AdhocWorkspace();
		cw.Options.WithChangedOption(CSharpFormattingOptions.IndentBlock, true);
		SyntaxNode formattedNode = Formatter.Format(cu.AddMembers(nsFactory(unitName)), cw, cw.Options);

		var file = new FileInfo(filePathFactory);
		DirectoryInfo dir = file.Directory;
		if (!dir!.Exists) {
			dir.Create();
		}

		using FileStream fileStream = file.Exists ? file.Open(FileMode.Truncate) : file.Create();
		using var sw = new StreamWriter(fileStream);
		formattedNode.WriteTo(sw);
	}
}
