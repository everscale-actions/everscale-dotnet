namespace EverscaleNet.ClientGenerator.Helpers;

internal class ModelsClassHelpers {
	private readonly Dictionary<string, ApiType> _allTypes;
	private readonly IReadOnlyDictionary<string, string> _numberTypesMapping;

	public ModelsClassHelpers(IReadOnlyDictionary<string, string> numberTypesMapping, Dictionary<string, ApiType> allTypes) {
		_numberTypesMapping = numberTypesMapping;
		_allTypes = allTypes;
	}

	private static EnumDeclarationSyntax GenerateEnumOfConsts(TypeElement typeElement) {
		string summary = typeElement.Summary + (typeElement.Description != null ? $"\n{typeElement.Description}" : null);

		bool isValuedEnum = typeElement
		                    .EnumConsts.Any(e => e.Value is not null);

		EnumDeclarationSyntax declaration = EnumDeclaration(Identifier(NamingConventions.Normalize(typeElement.Name)));

		if (!isValuedEnum) {
			declaration = declaration.AddAttributeLists(AttributeList(SeparatedList(new List<AttributeSyntax> { Attribute(IdentifierName("JsonConverter(typeof(JsonStringEnumConverter))")) })));
		}
		return declaration
		       .AddMembers(typeElement
		                   .EnumConsts
		                   .Select(EnumSelector)
		                   .ToArray())
		       .AddModifiers(Token(SyntaxKind.PublicKeyword))
		       .WithLeadingTrivia(CommentsHelpers.BuildCommentTrivia(summary));
	}

	private static EnumMemberDeclarationSyntax EnumSelector(EnumConst e) {
		EnumMemberDeclarationSyntax enumMember = e.Value == null
			                                         ? EnumMemberDeclaration(e.Name)
			                                         : EnumMemberDeclaration(e.Name)
				                                         .WithEqualsValue(EqualsValueClause(
					                                                          LiteralExpression(
						                                                          SyntaxKind.NumericLiteralExpression,
						                                                          Literal(int.Parse(e.Value)))));
		return enumMember
			.WithLeadingTrivia(CommentsHelpers.BuildCommentTrivia(null));
	}

	private static MemberDeclarationSyntax CreatePropertyForPurpleTypeOptional(string name,
	                                                                           OptionalInnerOptionalInner optionalInner,
	                                                                           string description) {
		return optionalInner.Type switch {
			ApiType.BigInt => CreatePropertyDeclaration("BigInteger", name, description, true),
			ApiType.Boolean => CreatePropertyDeclaration("bool", name, description, true),
			ApiType.Number => CreatePropertyDeclaration(NumberUtils.ConvertToSharpNumeric(optionalInner.NumberType, optionalInner.NumberSize), name, description, true),
			ApiType.String => CreatePropertyDeclaration("string", name, description),
			_ => throw new ArgumentOutOfRangeException(nameof(optionalInner.Type), optionalInner.Type, "Not supported type detected")
		};
	}

	public NamespaceDeclarationSyntax CreateModelClass(TypeElement typeElement) {
		NamespaceDeclarationSyntax ns = NamespaceDeclaration(IdentifierName(ClientGenerator.NamespaceModels));

		return typeElement.Type switch {
			ApiType.EnumOfConsts => ns.AddMembers(GenerateEnumOfConsts(typeElement)),
			ApiType.EnumOfTypes => ns.AddMembers(GenerateEnumOfTypes(typeElement)),
			ApiType.Struct => ns.AddMembers(GenerateStruct(typeElement)),
			_ => throw new ArgumentOutOfRangeException(nameof(typeElement.Type), typeElement.Type,
			                                           "Not supported type")
		};
	}

	private ClassDeclarationSyntax GenerateEnumOfTypes(TypeElement typeElement) {
		string typeElementSummary = typeElement.Summary +
		                            (typeElement.Description != null ? $"\n{typeElement.Description}" : null);

		MemberDeclarationSyntax[] enumTypes =
			typeElement
				.EnumTypes
				.Select(subClass => {
					string subClassSummary = subClass.Summary +
					                         (subClass.Description != null ? $"\n{subClass.Description}" : null);

					return subClass.Type switch {
						ApiType.Ref => CreatePropertyForRef(subClass.RefName, subClass.Name, subClassSummary),
						ApiType.Struct => CreateClassForStruct(typeElement.Name, subClass.Name, subClass.StructFields, subClassSummary),
						_ => throw new ArgumentOutOfRangeException(nameof(subClass.Type), subClass.Type, "EnumOfTypes doesn't support this type")
					};
				})
				.ToArray();

		IEnumerable<SyntaxTrivia> polymorphicAttributes = typeElement.EnumTypes
		                                                             .Where(e => e.Type == ApiType.Struct)
		                                                             .SelectMany(e => new[] {
			                                                             DisabledText($"    [JsonDerivedType(typeof({e.Name}), nameof({e.Name}))]"), ElasticCarriageReturnLineFeed
		                                                             });

		return ClassDeclaration(NamingConventions.Normalize(typeElement.Name))
		       .AddModifiers(Token(SyntaxKind.PublicKeyword), Token(SyntaxKind.AbstractKeyword))
		       .AddMembers(enumTypes)
		       .WithLeadingTrivia(CommentsHelpers.BuildCommentTrivia(typeElementSummary)
		                                         .Add(DisabledText("    [JsonPolymorphic(TypeDiscriminatorPropertyName = \"type\")]")).Add(ElasticCarriageReturnLineFeed)
		                                         .AddRange(polymorphicAttributes));
	}

	private MemberDeclarationSyntax CreateClassForStruct(string baseName, string name, EnumType[] structFields, string subClassSummary) {
		var members = new List<MemberDeclarationSyntax>();
		baseName = NamingConventions.Normalize(baseName);
		name = NamingConventions.Normalize(name);
		members.AddRange(structFields
			                 .SelectMany(sf => {
				                 //useless value Struct
				                 if (sf.Type is ApiType.Struct && sf.Name == "value") {
					                 return sf.StructFields
					                          .Select(sfVal => CreatePropertyGenericArgs(sfVal.Type, sfVal.Name, sfVal.RefName, sfVal.OptionalInner,
					                                                                     sfVal.Summary, sfVal.NumberType,
					                                                                     sfVal.NumberSize, addPostfix: NamingConventions.Normalize(sfVal.Name) == name, arrayItem: sfVal.ArrayItem));
				                 }
				                 return new[] {
					                 CreatePropertyGenericArgs(sf.Type, sf.Name, sf.RefName, sf.OptionalInner,
					                                           sf.Summary, sf.NumberType,
					                                           sf.NumberSize, addPostfix: NamingConventions.Normalize(sf.Name) == name, arrayItem: sf.ArrayItem)
				                 };
			                 }));
		return ClassDeclaration(name)
		       .AddModifiers(Token(SyntaxKind.PublicKeyword))
		       .AddBaseListTypes(
			       SimpleBaseType(IdentifierName(baseName)))
		       .AddMembers(members.ToArray())
		       .WithLeadingTrivia(CommentsHelpers.BuildCommentTrivia(subClassSummary));
	}

	private MemberDeclarationSyntax CreatePropertyForRef(string typeName, string name, string description,
	                                                     bool addPostfix = false) {
		typeName = NamingConventions.Normalize(typeName);
		bool optional;

		if (_numberTypesMapping.ContainsKey(typeName)) {
			typeName = _numberTypesMapping[typeName];
			optional = false;
		} else {
			if (_allTypes.TryGetValue(typeName, out ApiType type)) {
				optional = type == ApiType.EnumOfConsts;
			} else {
				typeName = "JsonElement";
				optional = true;
			}
		}

		return CreatePropertyDeclaration(typeName, name, description, optional, addPostfix);
	}

	private MemberDeclarationSyntax CreatePropertyGenericArgs(ApiType type, string name, string refName,
	                                                          GenericArg optionalInner,
	                                                          string description, NumberType? numberType = null, long? numberSize = null, bool optional = false,
	                                                          bool addPostfix = false,
	                                                          ArrayItem arrayItem = null) {
		return type switch {
			ApiType.Boolean => CreatePropertyDeclaration("bool", name, description, optional, addPostfix),
			ApiType.Ref => CreatePropertyForRef(refName, name, description, addPostfix),
			ApiType.String => CreatePropertyDeclaration("string", name, description, addPostfix: addPostfix),
			ApiType.Optional => CreatePropertyGenericArgs(optionalInner.Type, name, optionalInner.RefName,
			                                              null, description, optional: true,
			                                              addPostfix: addPostfix),
			ApiType.Number => CreatePropertyDeclaration(NumberUtils.ConvertToSharpNumeric(numberType, numberSize), name, description, optional, addPostfix),
			ApiType.Array when arrayItem is not null => CreatePropertyForPurpleArrayItem(name, arrayItem.Type, arrayItem.RefName, null, description),
			ApiType.BigInt => CreatePropertyDeclaration("BigInteger", name, description, optional),
			_ => throw new ArgumentOutOfRangeException(nameof(ApiType), $"Name: {name} RefName: {refName} Type: {type.ToString()}")
		};
	}

	private ClassDeclarationSyntax GenerateStruct(TypeElement typeElement) {
		string className = typeElement.Name;
		string typeElementSummary = typeElement.Summary +
		                            (typeElement.Description != null ? $"\n{typeElement.Description}" : null);

		MemberDeclarationSyntax[] properties = typeElement.StructFields.Select(CreatePropertyStructFields).ToArray();

		return ClassDeclaration(NamingConventions.Normalize(className))
		       .AddModifiers(Token(SyntaxKind.PublicKeyword)
			                     .WithLeadingTrivia(CommentsHelpers.BuildCommentTrivia(typeElementSummary)))
		       .AddMembers(properties);
	}

	private MemberDeclarationSyntax CreatePropertyStructFields(StructField sf) {
		string sfSummary = sf.Summary + (sf.Description != null ? $"\n{sf.Description}" : null);

		return sf.Type switch {
			ApiType.Array => CreatePropertyForPurpleArrayItem(sf.Name, sf.ArrayItem.Type, sf.ArrayItem.RefName,
			                                                  sf.ArrayItem.OptionalInner,
			                                                  sfSummary),
			ApiType.BigInt => CreatePropertyDeclaration("BigInteger", sf.Name, sfSummary),
			ApiType.Boolean => CreatePropertyDeclaration("bool", sf.Name, sfSummary),
			ApiType.Number => CreatePropertyDeclaration(
				NumberUtils.ConvertToSharpNumeric(sf.NumberType, sf.NumberSize), sf.Name, sfSummary),
			ApiType.Ref => CreatePropertyForRef(sf.RefName, sf.Name, sfSummary),
			ApiType.String => CreatePropertyDeclaration("string", sf.Name, sfSummary),
			ApiType.Optional => CreateOptionalPropertyForPurple(sf.Name, sf.OptionalInner, sfSummary),
			_ => throw new ArgumentOutOfRangeException(nameof(sf.Type), sf.Type, "Not supported type detected")
		};
	}

	private MemberDeclarationSyntax CreateOptionalPropertyForPurple(string name,
	                                                                StructFieldOptionalInner optionalInner, string description) {
		return optionalInner.Type switch {
			ApiType.Array => CreatePropertyForPurpleArrayItem(name, optionalInner.ArrayItem.Type,
			                                                  optionalInner.ArrayItem.RefName, null, description),
			ApiType.BigInt => CreatePropertyDeclaration("BigInteger", name, description, true),
			ApiType.Boolean => CreatePropertyDeclaration("bool", name, description, true),
			ApiType.Number => CreatePropertyDeclaration(
				NumberUtils.ConvertToSharpNumeric(optionalInner.NumberType, optionalInner.NumberSize), name,
				description, true),
			ApiType.Ref => CreatePropertyForRef(optionalInner.RefName, name, description),
			ApiType.String => CreatePropertyDeclaration("string", name, description),
			ApiType.Optional => CreatePropertyForPurpleTypeOptional(name, optionalInner.OptionalInner, description),
			_ => throw new ArgumentOutOfRangeException(nameof(optionalInner.Type), optionalInner.Type, "Not supported type detected")
		};
	}

	private MemberDeclarationSyntax CreatePropertyForPurpleArrayItem(string name, ApiType arrayType,
	                                                                 string arrayRefName,
	                                                                 GenericArg arrayItemOptionalInner, string description) {
		if (arrayType == ApiType.Optional)
			// ReSharper disable once TailRecursiveCall
		{
			return CreatePropertyForPurpleArrayItem(name, arrayItemOptionalInner.Type,
			                                        arrayItemOptionalInner.RefName, null, description);
		}

		string typeName = arrayType switch {
			ApiType.Ref => _allTypes.ContainsKey(NamingConventions.Normalize(arrayRefName))
				               ? NamingConventions.Normalize(arrayRefName)
				               : "JsonElement",
			ApiType.Boolean => "bool",
			ApiType.String => "string",
			_ => throw new ArgumentOutOfRangeException()
		};

		return CreatePropertyDeclaration($"{typeName}[]", name, description);
	}
}
