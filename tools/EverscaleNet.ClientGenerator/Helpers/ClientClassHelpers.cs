using System.Linq;
using EverscaleNet.ClientGenerator.Models;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace EverscaleNet.ClientGenerator.Helpers;

internal static class ClientClassHelpers {
	public static NamespaceDeclarationSyntax CreateClientClass(string unitName, EverApi everApi) {
		MemberDeclarationSyntax[] propertyDeclarationSyntaxes = GetProperties(everApi);
		string[] moduleNames = everApi.Modules.Select(m => m.Name).ToArray();

		VariableDeclarationSyntax variableDeclaration = VariableDeclaration(ParseTypeName("IEverClientAdapter"))
			.AddVariables(VariableDeclarator("_everClientAdapter"));
		FieldDeclarationSyntax fieldDeclaration = FieldDeclaration(variableDeclaration)
			.AddModifiers(Token(SyntaxKind.PrivateKeyword), Token(SyntaxKind.ReadOnlyKeyword));

		StatementSyntax[] statementSyntax =
			new[] {
					ParseStatement("_everClientAdapter = everClientAdapter;")
						.WithTrailingTrivia(LineFeed)
				}
				.Union(moduleNames
					       .Select(m => ParseStatement(
							               $"{NamingConventions.Normalize(m)} = new {NamingConventions.Normalize(m)}Module(everClientAdapter);")
						               .WithTrailingTrivia(LineFeed)))
				.ToArray();

		ConstructorDeclarationSyntax constructorDeclaration = ConstructorDeclaration(unitName)
		                                                      .AddParameterListParameters(Parameter(Identifier("everClientAdapter"))
			                                                                                  .WithType(IdentifierName("IEverClientAdapter")))
		                                                      .WithModifiers(TokenList(Token(SyntaxKind.PublicKeyword)))
		                                                      .WithBody(Block(statementSyntax));

		ClassDeclarationSyntax item = ClassDeclaration(unitName)
		                              .AddModifiers(Token(SyntaxKind.PublicKeyword))
		                              .AddBaseListTypes(SimpleBaseType(IdentifierName("IEverClient")))
		                              .AddMembers(fieldDeclaration)
		                              .AddMembers(constructorDeclaration)
		                              .AddMembers(propertyDeclarationSyntaxes);

		return NamespaceDeclaration(IdentifierName(ClientGenerator.Namespace))
			.AddMembers(item);
	}

	public static NamespaceDeclarationSyntax CreateClientInterface(string unitName, EverApi everApi) {
		MemberDeclarationSyntax[] propertyDeclarationSyntaxes = GetProperties(everApi);

		InterfaceDeclarationSyntax item = InterfaceDeclaration(unitName)
		                                  .AddModifiers(Token(SyntaxKind.PublicKeyword))
		                                  .AddMembers(propertyDeclarationSyntaxes);

		return NamespaceDeclaration(IdentifierName(ClientGenerator.NamespaceAbstract))
			.AddMembers(item);
	}

	private static MemberDeclarationSyntax[] GetProperties(EverApi everApi) {
		return everApi.Modules
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