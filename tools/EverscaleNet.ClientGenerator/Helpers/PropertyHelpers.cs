namespace EverscaleNet.ClientGenerator.Helpers;

internal static class PropertyHelpers {
	private static readonly AccessorListSyntax GetSetAccessorListSyntax = AccessorList(
		List(new[] {
			AccessorDeclaration(
					SyntaxKind.GetAccessorDeclaration)
				.WithSemicolonToken(
					Token(SyntaxKind.SemicolonToken)),
			AccessorDeclaration(
					SyntaxKind.SetAccessorDeclaration)
				.WithSemicolonToken(
					Token(SyntaxKind.SemicolonToken))
		}));

	private static readonly AccessorListSyntax GetInitAccessorListSyntax = AccessorList(
		List(new[] {
			AccessorDeclaration(
					SyntaxKind.GetAccessorDeclaration)
				.WithSemicolonToken(
					Token(SyntaxKind.SemicolonToken)),
			AccessorDeclaration(
					SyntaxKind.SetAccessorDeclaration)
				.WithSemicolonToken(
					Token(SyntaxKind.SemicolonToken))
		}));

	public static PropertyDeclarationSyntax CreatePropertyDeclaration(string typeName, string name,
	                                                                  string description, bool optional = false,
	                                                                  bool addPostfix = false, bool onlyInit = false) {
		AccessorListSyntax accessors = onlyInit ? GetInitAccessorListSyntax : GetSetAccessorListSyntax;

		var attributes = new List<AttributeSyntax> {
			Attribute(IdentifierName($"JsonPropertyName(\"{name}\")"))
		};

		return PropertyDeclaration(IdentifierName($"{typeName}{(optional ? "?" : null)}"),
		                           NamingConventions.Normalize($"{name}{(addPostfix ? "Accessor" : null)}"))
		       .AddAttributeLists(AttributeList(SeparatedList(attributes))
			                          .WithLeadingTrivia(CommentsHelpers.BuildCommentTrivia(description)))
		       .WithModifiers(TokenList(Token(SyntaxKind.PublicKeyword)))
		       .WithAccessorList(accessors);
	}
}
