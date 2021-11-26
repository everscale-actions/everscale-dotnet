using System.Linq;
using ch1seL.TonNet.ClientGenerator.Models;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace ch1seL.TonNet.ClientGenerator.Helpers;

internal static class ClientClassHelpers {
	public static NamespaceDeclarationSyntax CreateTonClientClass(string unitName, TonApi tonApi) {
		MemberDeclarationSyntax[] propertyDeclarationSyntaxes = GetProperties(tonApi);
		string[] moduleNames = tonApi.Modules.Select(m => m.Name).ToArray();

		VariableDeclarationSyntax variableDeclaration = VariableDeclaration(ParseTypeName("ITonClientAdapter"))
			.AddVariables(VariableDeclarator("_tonClientAdapter"));
		FieldDeclarationSyntax fieldDeclaration = FieldDeclaration(variableDeclaration)
			.AddModifiers(Token(SyntaxKind.PrivateKeyword), Token(SyntaxKind.ReadOnlyKeyword));

		StatementSyntax[] statementSyntax =
			new[] {
					ParseStatement("_tonClientAdapter = tonClientAdapter;")
						.WithTrailingTrivia(LineFeed)
				}
				.Union(moduleNames
					       .Select(m => ParseStatement(
							               $"{NamingConventions.Normalize(m)} = new {NamingConventions.Normalize(m)}Module(tonClientAdapter);")
						               .WithTrailingTrivia(LineFeed)))
				.ToArray();

		ConstructorDeclarationSyntax constructorDeclaration = ConstructorDeclaration(unitName)
		                                                      .AddParameterListParameters(Parameter(Identifier("tonClientAdapter"))
			                                                                                  .WithType(IdentifierName("ITonClientAdapter")))
		                                                      .WithModifiers(TokenList(Token(SyntaxKind.PublicKeyword)))
		                                                      .WithBody(Block(statementSyntax));

		ClassDeclarationSyntax item = ClassDeclaration(unitName)
		                              .AddModifiers(Token(SyntaxKind.PublicKeyword))
		                              .AddBaseListTypes(SimpleBaseType(IdentifierName("ITonClient")))
		                              .AddMembers(fieldDeclaration)
		                              .AddMembers(constructorDeclaration)
		                              .AddMembers(propertyDeclarationSyntaxes);

		return NamespaceDeclaration(IdentifierName(ClientGenerator.Namespace))
			.AddMembers(item);
	}

	public static NamespaceDeclarationSyntax CreateTonClientInterface(string unitName, TonApi tonApi) {
		MemberDeclarationSyntax[] propertyDeclarationSyntaxes = GetProperties(tonApi);

		InterfaceDeclarationSyntax item = InterfaceDeclaration(unitName)
		                                  .AddModifiers(Token(SyntaxKind.PublicKeyword))
		                                  .AddMembers(propertyDeclarationSyntaxes);

		return NamespaceDeclaration(IdentifierName(ClientGenerator.NamespaceAbstract))
			.AddMembers(item);
	}

	private static MemberDeclarationSyntax[] GetProperties(TonApi tonApi) {
		return tonApi.Modules
		             .Select(module => {
			             string formattedName = NamingConventions.Normalize(module.Name);

			             string summary = module.Summary + (module.Description != null ? $"\n{module.Description}" : null);

			             return PropertyDeclaration(IdentifierName($"I{formattedName}Module"), formattedName)
			                    .WithModifiers(TokenList(Token(SyntaxKind.PublicKeyword)
				                                             .WithLeadingTrivia(CommentsHelpers.BuildCommentTrivia(summary))))
			                    .WithAccessorList(
				                    AccessorList(
					                    List(new[] {
						                    AccessorDeclaration(
								                    SyntaxKind.GetAccessorDeclaration)
							                    .WithSemicolonToken(
								                    Token(SyntaxKind.SemicolonToken))
					                    })));
		             })
		             .Cast<MemberDeclarationSyntax>()
		             .ToArray();
	}
}