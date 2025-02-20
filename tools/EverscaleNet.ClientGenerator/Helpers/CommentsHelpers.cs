namespace EverscaleNet.ClientGenerator.Helpers;

internal static class CommentsHelpers {
	private static readonly SyntaxToken XmlNewline = XmlTextNewLine(Environment.NewLine);

	public static SyntaxTriviaList BuildCommentTrivia(string comment) {
		if (string.IsNullOrWhiteSpace(comment)) {
			comment = "Not described yet..";
		}

		XmlNodeSyntax[] commentNodes = GetLines(comment)
		                               .SelectMany(l => new XmlNodeSyntax[] {
			                               XmlParaElement(XmlText(l)),
			                               XmlText(XmlNewline)
		                               }).ToArray();

		// add a newline after the summary element
		XmlNodeSyntax[] formattedCommentNodes = new XmlNodeSyntax[] { XmlText(XmlNewline) }.Concat(commentNodes).ToArray();

		return TriviaList(
			Trivia(DocumentationComment(XmlSummaryElement(formattedCommentNodes))),
			ElasticCarriageReturnLineFeed
		);
	}

	private static IEnumerable<string> GetLines(string comment) {
		if (comment == null) {
			throw new ArgumentNullException(nameof(comment));
		}

		return comment.Split(['\r', '\n'], StringSplitOptions.RemoveEmptyEntries);
	}
}
