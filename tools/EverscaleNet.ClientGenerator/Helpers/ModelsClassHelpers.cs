using EverscaleNet.ClientGenerator.Models;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;
using static EverscaleNet.ClientGenerator.Helpers.PropertyHelpers;

namespace EverscaleNet.ClientGenerator.Helpers;

internal class ModelsClassHelpers {
	private readonly Dictionary<string, TypeType> _allTypes;
	private readonly IReadOnlyDictionary<string, string> _numberTypesMapping;

	public ModelsClassHelpers(IReadOnlyDictionary<string, string> numberTypesMapping, Dictionary<string, TypeType> allTypes) {
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

	private static MemberDeclarationSyntax CreatePropertyForPurpleTypeOptionalOptional(string name,
	                                                                                   OptionalInnerOptionalInner optionalInner,
	                                                                                   string description) {
		return optionalInner.Type switch {
			PurpleType.BigInt => CreatePropertyDeclaration("ulong", name, description, true),
			PurpleType.Boolean => CreatePropertyDeclaration("bool", name, description, true),
			PurpleType.Number => CreatePropertyDeclaration(
				NumberUtils.ConvertToSharpNumeric(optionalInner.NumberType, optionalInner.NumberSize), name,
				description,
				true),
			PurpleType.String => CreatePropertyDeclaration("string", name, description),
			_ => throw new ArgumentOutOfRangeException(nameof(optionalInner.Type), optionalInner.Type,
			                                           "Not supported type detected")
		};
	}

	public NamespaceDeclarationSyntax CreateModelClass(TypeElement typeElement) {
		NamespaceDeclarationSyntax ns = NamespaceDeclaration(IdentifierName(ClientGenerator.NamespaceModels));

		return typeElement.Type switch {
			TypeType.EnumOfConsts => ns.AddMembers(GenerateEnumOfConsts(typeElement)),
			TypeType.EnumOfTypes => ns.AddMembers(GenerateEnumOfTypes(typeElement)),
			TypeType.Struct => ns.AddMembers(GenerateStruct(typeElement)),
			_ => throw new ArgumentOutOfRangeException(nameof(typeElement.Type), typeElement.Type,
			                                           "Not supported type")
		};
	}

	private ClassDeclarationSyntax GenerateEnumOfTypes(TypeElement typeElement) {
		string typeElementSummary = typeElement.Summary +
		                            (typeElement.Description != null ? $"\n{typeElement.Description}" : null);

		MemberDeclarationSyntax[] enumTypes = typeElement
		                                      .EnumTypes
		                                      .SelectMany(subClass => {
			                                      string subClassSummary = subClass.Summary +
			                                                               (subClass.Description != null ? $"\n{subClass.Description}" : null);

			                                      switch (subClass.Type) {
				                                      case GenericArgType.Ref:
					                                      return new[] { CreatePropertyForRef(subClass.RefName, subClass.Name, subClassSummary) };

				                                      case GenericArgType.Struct:
					                                      var properties = new List<MemberDeclarationSyntax>();
					                                      properties.AddRange(subClass.StructFields.Select(sf => {
						                                      bool addPostfix = NamingConventions.Normalize(sf.Name) ==
						                                                        NamingConventions.Normalize(subClass.Name);
						                                      return CreatePropertyGenericArgs(sf.Type, sf.Name, sf.RefName, sf.OptionalInner,
						                                                                       subClassSummary, sf.NumberType,
						                                                                       sf.NumberSize, addPostfix: addPostfix, arrayItem: sf.ArrayItem);
					                                      }));
					                                      return new[] {
						                                      ClassDeclaration(NamingConventions.Normalize(subClass.Name))
							                                      .AddModifiers(Token(SyntaxKind.PublicKeyword))
							                                      .AddBaseListTypes(
								                                      SimpleBaseType(IdentifierName(NamingConventions.Normalize(typeElement.Name))))
							                                      .AddMembers(properties.ToArray())
							                                      .WithLeadingTrivia(CommentsHelpers.BuildCommentTrivia(subClassSummary)
							                                                                        .Add(DisabledText("#if !NET6_0_OR_GREATER"))
							                                                                        .Add(ElasticCarriageReturnLineFeed)
							                                                                        .Add(DisabledText($"        [Dahomey.Json.Attributes.JsonDiscriminator(\"{subClass.Name}\")]"))
							                                                                        .Add(ElasticCarriageReturnLineFeed)
							                                                                        .Add(DisabledText("#endif"))
							                                                                        .Add(ElasticCarriageReturnLineFeed))
					                                      };

				                                      default:
					                                      throw new ArgumentOutOfRangeException(nameof(subClass.Type), subClass.Type,
					                                                                            "EnumOfTypes doesn't support this type");
			                                      }
		                                      })
		                                      .ToArray();

		IEnumerable<SyntaxTrivia> polymAttributes = typeElement.EnumTypes
		                                                       .Where(e => e.Type == GenericArgType.Struct)
		                                                       .SelectMany(e => new[] { DisabledText($"    [JsonDerivedType(typeof({e.Name}), nameof({e.Name}))]"), ElasticCarriageReturnLineFeed });

		return ClassDeclaration(NamingConventions.Normalize(typeElement.Name))
		       .AddModifiers(Token(SyntaxKind.PublicKeyword), Token(SyntaxKind.AbstractKeyword))
		       .AddMembers(enumTypes)
		       .WithLeadingTrivia(CommentsHelpers.BuildCommentTrivia(typeElementSummary)
		                                         .Add(DisabledText("#if NET6_0_OR_GREATER")).Add(ElasticCarriageReturnLineFeed)
		                                         .Add(DisabledText("    [JsonPolymorphic(TypeDiscriminatorPropertyName = \"type\")]")).Add(ElasticCarriageReturnLineFeed)
		                                         .AddRange(polymAttributes)
		                                         .Add(DisabledText("#endif")).Add(ElasticCarriageReturnLineFeed));
	}

	private MemberDeclarationSyntax CreatePropertyForRef(string typeName, string name, string description,
	                                                     bool addPostfix = false) {
		typeName = NamingConventions.Normalize(typeName);
		bool optional;

		if (_numberTypesMapping.ContainsKey(typeName)) {
			typeName = _numberTypesMapping[typeName];
			optional = false;
		} else {
			if (_allTypes.TryGetValue(typeName, out TypeType type)) {
				optional = type == TypeType.EnumOfConsts;
			} else {
				typeName = "JsonElement";
				optional = true;
			}
		}

		return CreatePropertyDeclaration(typeName, name, description, optional, addPostfix);
	}

	private MemberDeclarationSyntax CreatePropertyGenericArgs(GenericArgType type, string name, string refName,
	                                                          GenericArg optionalInner,
	                                                          string description, NumberType? numberType = null, long? numberSize = null, bool optional = false,
	                                                          bool addPostfix = false,
	                                                          ArrayItem arrayItem = null) {
		if (type == GenericArgType.Array && arrayItem == null) {
			throw new ArgumentNullException(nameof(arrayItem));
		}

		return type switch {
			GenericArgType.Boolean => CreatePropertyDeclaration("bool", name, description, optional, addPostfix),
			GenericArgType.Ref => CreatePropertyForRef(refName, name, description, addPostfix: addPostfix),
			GenericArgType.String => CreatePropertyDeclaration("string", name, description, addPostfix: addPostfix),
			GenericArgType.Optional => CreatePropertyGenericArgs(optionalInner.Type, name, optionalInner.RefName,
			                                                     null, description,
			                                                     addPostfix: addPostfix),
			GenericArgType.Number => CreatePropertyDeclaration(
				NumberUtils.ConvertToSharpNumeric(numberType, numberSize), name, description,
				addPostfix: addPostfix),
			// ReSharper disable once PossibleNullReferenceException
			GenericArgType.Array => CreatePropertyForPurpleArrayItem(name, arrayItem.Type, arrayItem.RefName, null,
			                                                         description),
			GenericArgType.BigInt => CreatePropertyDeclaration("ulong", name, description),
			_ => throw new ArgumentOutOfRangeException()
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
			PurpleType.Array => CreatePropertyForPurpleArrayItem(sf.Name, sf.ArrayItem.Type, sf.ArrayItem.RefName,
			                                                     sf.ArrayItem.OptionalInner,
			                                                     sfSummary),
			PurpleType.BigInt => CreatePropertyDeclaration("ulong", sf.Name, sfSummary),
			PurpleType.Boolean => CreatePropertyDeclaration("bool", sf.Name, sfSummary),
			PurpleType.Number => CreatePropertyDeclaration(
				NumberUtils.ConvertToSharpNumeric(sf.NumberType, sf.NumberSize), sf.Name, sfSummary),
			PurpleType.Ref => CreatePropertyForRef(sf.RefName, sf.Name, sfSummary),
			PurpleType.String => CreatePropertyDeclaration("string", sf.Name, sfSummary),
			PurpleType.Optional => CreateOptionalPropertyForPurple(sf.Name, sf.OptionalInner, sfSummary),
			_ => throw new ArgumentOutOfRangeException(nameof(sf.Type), sf.Type, "Not supported type detected")
		};
	}

	private MemberDeclarationSyntax CreateOptionalPropertyForPurple(string name,
	                                                                StructFieldOptionalInner optionalInner, string description) {
		return optionalInner.Type switch {
			PurpleType.Array => CreatePropertyForPurpleArrayItem(name, optionalInner.ArrayItem.Type,
			                                                     optionalInner.ArrayItem.RefName, null, description),
			PurpleType.BigInt => CreatePropertyDeclaration("ulong", name, description, true),
			PurpleType.Boolean => CreatePropertyDeclaration("bool", name, description, true),
			PurpleType.Number => CreatePropertyDeclaration(
				NumberUtils.ConvertToSharpNumeric(optionalInner.NumberType, optionalInner.NumberSize), name,
				description, true),
			PurpleType.Ref => CreatePropertyForRef(optionalInner.RefName, name, description),
			PurpleType.String => CreatePropertyDeclaration("string", name, description),
			PurpleType.Optional => CreatePropertyForPurpleTypeOptionalOptional(name, optionalInner.OptionalInner,
			                                                                   description),
			_ => throw new ArgumentOutOfRangeException(nameof(optionalInner.Type), optionalInner.Type, "Not supported type detected")
		};
	}

	private MemberDeclarationSyntax CreatePropertyForPurpleArrayItem(string name, GenericArgType arrayType,
	                                                                 string arrayRefName,
	                                                                 GenericArg arrayItemOptionalInner, string description) {
		if (arrayType == GenericArgType.Optional)
			// ReSharper disable once TailRecursiveCall
		{
			return CreatePropertyForPurpleArrayItem(name, arrayItemOptionalInner.Type,
			                                        arrayItemOptionalInner.RefName, null, description);
		}

		string typeName = arrayType switch {
			GenericArgType.Ref => _allTypes.ContainsKey(NamingConventions.Normalize(arrayRefName))
				                      ? NamingConventions.Normalize(arrayRefName)
				                      : "JsonElement",
			GenericArgType.Boolean => "bool",
			GenericArgType.String => "string",
			_ => throw new ArgumentOutOfRangeException()
		};

		return CreatePropertyDeclaration($"{typeName}[]", name, description);
	}
}
