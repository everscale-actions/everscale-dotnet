using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace EverscaleNet.ClientGenerator.Helpers;

internal static class CommentsHelpers {
	private static readonly SyntaxToken XmlNewline = XmlTextNewLine(Environment.NewLine);

	public static SyntaxTriviaList BuildCommentTrivia(string comment) {
		if (string.IsNullOrEmpty(comment)) {
			comment = "Not described yet..";
		}

		IReadOnlyCollection<string> commentLines = GetLines(comment);
		XmlNodeSyntax[] commentNodes = commentLines.Count > 1
			                               ? commentLines.SelectMany(l => new XmlNodeSyntax[] { XmlParaElement(XmlText(l)), XmlText(XmlNewline) }).ToArray()
			                               : new XmlNodeSyntax[] { XmlText(XmlTextLiteral(comment), XmlNewline) };
		// add a newline after the summary element
		XmlNodeSyntax[] formattedCommentNodes = new XmlNodeSyntax[] { XmlText(XmlNewline) }.Concat(commentNodes).ToArray();

		return TriviaList(
			Trivia(
				DocumentationComment(
					XmlSummaryElement(formattedCommentNodes))),
			ElasticCarriageReturnLineFeed
		);
	}

	private static IReadOnlyCollection<string> GetLines(string comment) {
		if (comment == null) {
			throw new ArgumentNullException(nameof(comment));
		}

		return comment.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
	}
}