using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using EverscaleNet.Client.Models;
using EverscaleNet.Serialization;
using Microsoft.Build.Framework;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Formatting;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Formatting;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;
using Task = Microsoft.Build.Utilities.Task;

namespace EverscaleNet.ContractGenerator;

public class Generate : Task {
	public override bool Execute() {
		var contract = new FileInfo(SourceFile);
		AbiContract abi = GetAbiContract(contract).GetAwaiter().GetResult();
		GenerateContract(contract.Name, abi!.Functions);

		return true;
	}

	private void GenerateContract(string contractName, AbiFunction[] functions) {
		NamespaceDeclarationSyntax ns = NamespaceDeclaration(IdentifierName("Test"));

		MemberDeclarationSyntax[] methods = functions
		                                    .Select(f => GetMethodDeclaration(module, f, true))
		                                    .ToArray();

		ClassDeclarationSyntax cs = ClassDeclaration(contractName)
		                            .AddModifiers(Token(SyntaxKind.PublicKeyword))
		                            .AddMembers();

		CompilationUnitSyntax cu = CompilationUnit()
		                           .AddMembers(ns)
		                           .AddMembers(cs);

		var cw = new AdhocWorkspace();
		cw.Options.WithChangedOption(CSharpFormattingOptions.IndentBlock, true);
		SyntaxNode formattedNode = Formatter.Format(cu, cw, cw.Options);

		var file = new FileInfo(DestinationFile);
		DirectoryInfo dir = file.Directory;
		if (!dir!.Exists) {
			dir.Create();
		}

		using FileStream fileStream = file.Exists ? file.Open(FileMode.Truncate) : file.Create();
		using var sw = new StreamWriter(fileStream);
		formattedNode.WriteTo(sw);
	}

	private static async Task<AbiContract?> GetAbiContract(FileInfo fileInfo) {
		await using FileStream fs = fileInfo.OpenRead();
		return await JsonSerializer.DeserializeAsync<AbiContract>(fs, JsonOptionsProvider.JsonSerializerOptions);
	}

	[Required]
	public string SourceFile { get; set; }

	[Required]
	public string DestinationFile { get; set; }
}
