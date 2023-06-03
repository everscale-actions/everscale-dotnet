namespace EverscaleNet.ClientGenerator.Helpers;

internal static class ModulesClassHelpers {
	public static NamespaceDeclarationSyntax CreateModuleClass(string unitName, Module module) {
		var moduleName = $"{unitName}Module";

		StatementSyntax statementSyntax = ParseStatement("_everClientAdapter = everClientAdapter;");

		VariableDeclarationSyntax variableDeclaration = VariableDeclaration(ParseTypeName("IEverClientAdapter"))
			.AddVariables(VariableDeclarator("_everClientAdapter"));
		FieldDeclarationSyntax fieldDeclaration = FieldDeclaration(variableDeclaration)
			.AddModifiers(Token(SyntaxKind.PrivateKeyword), Token(SyntaxKind.ReadOnlyKeyword));

		ConstructorDeclarationSyntax constructorDeclaration = ConstructorDeclaration(moduleName)
		                                                      .AddParameterListParameters(
			                                                      Parameter(Identifier("everClientAdapter")).WithType(IdentifierName("IEverClientAdapter")))
		                                                      .WithModifiers(TokenList(Token(SyntaxKind.PublicKeyword)))
		                                                      .WithBody(Block(statementSyntax))
		                                                      .WithLeadingTrivia(CommentsHelpers.BuildCommentTrivia(".ctor"));

		MemberDeclarationSyntax[] methods = module
		                                    .Functions
		                                    .Select(f => GetMethodDeclaration(module, f, true))
		                                    .ToArray();

		ClassDeclarationSyntax item = ClassDeclaration(moduleName)
		                              .AddModifiers(Token(SyntaxKind.PublicKeyword))
		                              .AddBaseListTypes(SimpleBaseType(IdentifierName(NamingConventions.ToInterfaceName(moduleName))))
		                              .AddMembers(fieldDeclaration)
		                              .AddMembers(constructorDeclaration)
		                              .AddMembers(methods)
		                              .WithLeadingTrivia(CommentsHelpers.BuildCommentTrivia($"{unitName} Module"));

		return NamespaceDeclaration(IdentifierName(ClientGenerator.NamespaceModules))
			.AddMembers(item);
	}

	public static NamespaceDeclarationSyntax CreateModuleInterface(string unitName, Module module) {
		var moduleName = $"{unitName}Module";

		MemberDeclarationSyntax[] methods = module
		                                    .Functions
		                                    .Select(function => GetMethodDeclaration(module, function, false))
		                                    .ToArray();

		InterfaceDeclarationSyntax item = InterfaceDeclaration($"I{moduleName}")
		                                  .AddModifiers(Token(SyntaxKind.PublicKeyword))
		                                  .AddBaseListTypes(SimpleBaseType(IdentifierName("IEverModule")))
		                                  .AddMembers(methods)
		                                  .WithLeadingTrivia(CommentsHelpers.BuildCommentTrivia($"{unitName} Module"));

		return NamespaceDeclaration(IdentifierName(ClientGenerator.NamespaceAbstractModules))
			.AddMembers(item);
	}

	private static MemberDeclarationSyntax GetMethodDeclaration(Module module, Function function, bool withBody) {
		string responseType = function.Result.GetMethodReturnType();
		string responseDeclaration = responseType == null ? "Task" : $"Task<{responseType}>";
		var requestParam = new { name = default(string), type = default(string) };
		var callbackParam = new { name = default(string), nameWithNull = default(string), type = default(string) };

		foreach (Param param in function.Params) {
			if (param.Type == ApiType.Generic && param.GenericName == ParamGenericName.Arc) {
				GenericArg arcArg = param.GenericArgs[0];
				if (arcArg.Type == ApiType.Ref && arcArg.RefName == "Request") {
					string name = StringUtils.EscapeReserved(function.Params[2].Name.GetEnumMemberValueOrString());
					callbackParam = new {
						name,
						nameWithNull = $"{name} = null",
						type = string.Equals(module.Name, "net", StringComparison.OrdinalIgnoreCase)
							       ? "JsonElement"
							       : NamingConventions.EventFormatter(module.Name)
					};
				}
			}

			if (param.Type == ApiType.Generic && param.GenericName == ParamGenericName.AppObject) {
				callbackParam = new {
					name = "appObject",
					nameWithNull = "appObject = null",
					type = "JsonElement"
				};
			}

			if (param.Name == Name.Params) {
				requestParam = new {
					name = StringUtils.EscapeReserved(function.Params[1].Name.GetEnumMemberValueOrString()),
					type = GetParamType(function.Params[1])
				};
			}
		}

		string functionSummary =
			function.Summary + (function.Description != null ? $"\n{function.Description}" : null);
		var modifiers = new List<SyntaxToken> {
			Token(SyntaxKind.PublicKeyword).WithLeadingTrivia(CommentsHelpers.BuildCommentTrivia(functionSummary))
		};
		if (withBody) {
			modifiers.Add(Token(SyntaxKind.AsyncKeyword));
		}

		var @params = new List<ParameterSyntax>();
		var methodDeclarationParams = new List<ParameterSyntax>();
		if (requestParam.name != default) {
			ParameterSyntax param = Parameter(Identifier(requestParam.name))
				.WithType(IdentifierName(requestParam.type));
			methodDeclarationParams.Add(param);
			@params.Add(param);
		}

		if (callbackParam.name != default) {
			methodDeclarationParams.Add(Parameter(Identifier(callbackParam.nameWithNull))
				                            .WithType(IdentifierName($"Func<{callbackParam.type}, uint, CancellationToken, Task>")));
			@params.Add(Parameter(Identifier(callbackParam.name))
				            .WithType(IdentifierName($"Func<{callbackParam.type}, uint, CancellationToken, Task>")));
		}

		MethodDeclarationSyntax method =
			MethodDeclaration(ParseTypeName(responseDeclaration), NamingConventions.Normalize(function.Name))
				.AddParameterListParameters(methodDeclarationParams.ToArray())
				.AddParameterListParameters(Parameter(Identifier("cancellationToken"))
				                            .WithType(IdentifierName(nameof(CancellationToken)))
				                            .WithDefault(EqualsValueClause(IdentifierName("default"))))
				.AddModifiers(modifiers.ToArray());

		if (withBody) {
			var arguments = new List<ArgumentSyntax> {
				Argument(IdentifierName($"\"{module.Name}.{function.Name}\""))
			};
			arguments.AddRange(
				@params
					.Select(p => Argument(IdentifierName(p.Identifier.Text))));
			arguments.Add(Argument(IdentifierName("cancellationToken")));

			string genericParametersDeclaration =
				StringUtils.GetGenericParametersDeclaration(requestParam.type, responseType, callbackParam.type);

			AwaitExpressionSyntax awaitExpression = AwaitExpression(
				InvocationExpression(IdentifierName($"_everClientAdapter.Request{genericParametersDeclaration}"))
					.AddArgumentListArguments(arguments.ToArray()));

			StatementSyntax ex = responseType == null
				                     ? ExpressionStatement(awaitExpression)
				                     : ReturnStatement(awaitExpression);
			BlockSyntax blockSyntax = Block(ex);
			return method.WithBody(blockSyntax);
		}

		return method.WithSemicolonToken(Token(SyntaxKind.SemicolonToken));
	}

	private static string GetParamType(Param param) {
		return NamingConventions.Normalize(param.RefName);
	}
}
