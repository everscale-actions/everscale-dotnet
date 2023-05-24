using EverscaleNet.ClientGenerator.Models;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;
using static EverscaleNet.ClientGenerator.Helpers.PropertyHelpers;
using Type = EverscaleNet.ClientGenerator.Models.Type;

namespace EverscaleNet.ClientGenerator.Helpers;

internal class ModelsClassHelpers {
	private readonly Dictionary<string, Type> _allTypes;
	private readonly IReadOnlyDictionary<string, string> _numberTypesMapping;

	public ModelsClassHelpers(IReadOnlyDictionary<string, string> numberTypesMapping, Dictionary<string, Type> allTypes) {
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
			Type.BigInt => CreatePropertyDeclaration("ulong", name, description, true),
			Type.Boolean => CreatePropertyDeclaration("bool", name, description, true),
			Type.Number => CreatePropertyDeclaration(NumberUtils.ConvertToSharpNumeric(optionalInner.NumberType, optionalInner.NumberSize), name, description, true),
			Type.String => CreatePropertyDeclaration("string", name, description),
			_ => throw new ArgumentOutOfRangeException(nameof(optionalInner.Type), optionalInner.Type, "Not supported type detected")
		};
	}

	public NamespaceDeclarationSyntax CreateModelClass(TypeElement typeElement) {
		NamespaceDeclarationSyntax ns = NamespaceDeclaration(IdentifierName(ClientGenerator.NamespaceModels));

		return typeElement.Type switch {
			Type.EnumOfConsts => ns.AddMembers(GenerateEnumOfConsts(typeElement)),
			Type.EnumOfTypes => ns.AddMembers(GenerateEnumOfTypes(typeElement)),
			Type.Struct => ns.AddMembers(GenerateStruct(typeElement)),
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
						Type.Ref => CreatePropertyForRef(subClass.RefName, subClass.Name, subClassSummary),
						Type.Struct => CreateClassForStruct(typeElement.Name, subClass.Name, subClass.StructFields, subClassSummary),
						_ => throw new ArgumentOutOfRangeException(nameof(subClass.Type), subClass.Type, "EnumOfTypes doesn't support this type")
					};
				})
				.ToArray();

		IEnumerable<SyntaxTrivia> polymorphicAttributes = typeElement.EnumTypes
		                                                             .Where(e => e.Type == Type.Struct)
		                                                             .SelectMany(e => new[] {
			                                                             DisabledText($"    [JsonDerivedType(typeof({e.Name}), nameof({e.Name}))]"), ElasticCarriageReturnLineFeed
		                                                             });

		return ClassDeclaration(NamingConventions.Normalize(typeElement.Name))
		       .AddModifiers(Token(SyntaxKind.PublicKeyword), Token(SyntaxKind.AbstractKeyword))
		       .AddMembers(enumTypes)
		       .WithLeadingTrivia(CommentsHelpers.BuildCommentTrivia(typeElementSummary)
		                                         .Add(DisabledText("#if NET6_0_OR_GREATER")).Add(ElasticCarriageReturnLineFeed)
		                                         .Add(DisabledText("    [JsonPolymorphic(TypeDiscriminatorPropertyName = \"type\")]")).Add(ElasticCarriageReturnLineFeed)
		                                         .AddRange(polymorphicAttributes)
		                                         .Add(DisabledText("#endif")).Add(ElasticCarriageReturnLineFeed));
	}

	private MemberDeclarationSyntax CreateClassForStruct(string baseName, string name, EnumType[] structFields, string subClassSummary) {
		var members = new List<MemberDeclarationSyntax>();
		baseName = NamingConventions.Normalize(baseName);
		name = NamingConventions.Normalize(name);
		members.AddRange(structFields
			                 .SelectMany(sf => {
				                 //useless value Struct
				                 if (sf.Type is Type.Struct && sf.Name == "value") {
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
		       .WithLeadingTrivia(CommentsHelpers.BuildCommentTrivia(subClassSummary)
		                                         .Add(DisabledText("#if !NET6_0_OR_GREATER"))
		                                         .Add(ElasticCarriageReturnLineFeed)
		                                         .Add(DisabledText($"        [Dahomey.Json.Attributes.JsonDiscriminator(\"{name}\")]"))
		                                         .Add(ElasticCarriageReturnLineFeed)
		                                         .Add(DisabledText("#endif"))
		                                         .Add(ElasticCarriageReturnLineFeed));
	}

	private MemberDeclarationSyntax CreatePropertyForRef(string typeName, string name, string description,
	                                                     bool addPostfix = false) {
		typeName = NamingConventions.Normalize(typeName);
		bool optional;

		if (_numberTypesMapping.ContainsKey(typeName)) {
			typeName = _numberTypesMapping[typeName];
			optional = false;
		} else {
			if (_allTypes.TryGetValue(typeName, out Type type)) {
				optional = type == Type.EnumOfConsts;
			} else {
				typeName = "JsonElement";
				optional = true;
			}
		}

		return CreatePropertyDeclaration(typeName, name, description, optional, addPostfix);
	}

	private MemberDeclarationSyntax CreatePropertyGenericArgs(Type type, string name, string refName,
	                                                          GenericArg optionalInner,
	                                                          string description, NumberType? numberType = null, long? numberSize = null, bool optional = false,
	                                                          bool addPostfix = false,
	                                                          ArrayItem arrayItem = null) {
		return type switch {
			Type.Boolean => CreatePropertyDeclaration("bool", name, description, optional, addPostfix),
			Type.Ref => CreatePropertyForRef(refName, name, description, addPostfix),
			Type.String => CreatePropertyDeclaration("string", name, description, addPostfix: addPostfix),
			Type.Optional => CreatePropertyGenericArgs(optionalInner.Type, name, optionalInner.RefName,
			                                           null, description, optional: true,
			                                           addPostfix: addPostfix),
			Type.Number => CreatePropertyDeclaration(NumberUtils.ConvertToSharpNumeric(numberType, numberSize), name, description, optional, addPostfix),
			Type.Array when arrayItem is not null => CreatePropertyForPurpleArrayItem(name, arrayItem.Type, arrayItem.RefName, null, description),
			Type.BigInt => CreatePropertyDeclaration("ulong", name, description, optional),
			_ => throw new ArgumentOutOfRangeException(nameof(Type), $"Name: {name} RefName: {refName} Type: {type.ToString()}")
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
			Type.Array => CreatePropertyForPurpleArrayItem(sf.Name, sf.ArrayItem.Type, sf.ArrayItem.RefName,
			                                               sf.ArrayItem.OptionalInner,
			                                               sfSummary),
			Type.BigInt => CreatePropertyDeclaration("ulong", sf.Name, sfSummary),
			Type.Boolean => CreatePropertyDeclaration("bool", sf.Name, sfSummary),
			Type.Number => CreatePropertyDeclaration(
				NumberUtils.ConvertToSharpNumeric(sf.NumberType, sf.NumberSize), sf.Name, sfSummary),
			Type.Ref => CreatePropertyForRef(sf.RefName, sf.Name, sfSummary),
			Type.String => CreatePropertyDeclaration("string", sf.Name, sfSummary),
			Type.Optional => CreateOptionalPropertyForPurple(sf.Name, sf.OptionalInner, sfSummary),
			_ => throw new ArgumentOutOfRangeException(nameof(sf.Type), sf.Type, "Not supported type detected")
		};
	}

	private MemberDeclarationSyntax CreateOptionalPropertyForPurple(string name,
	                                                                StructFieldOptionalInner optionalInner, string description) {
		return optionalInner.Type switch {
			Type.Array => CreatePropertyForPurpleArrayItem(name, optionalInner.ArrayItem.Type,
			                                               optionalInner.ArrayItem.RefName, null, description),
			Type.BigInt => CreatePropertyDeclaration("ulong", name, description, true),
			Type.Boolean => CreatePropertyDeclaration("bool", name, description, true),
			Type.Number => CreatePropertyDeclaration(
				NumberUtils.ConvertToSharpNumeric(optionalInner.NumberType, optionalInner.NumberSize), name,
				description, true),
			Type.Ref => CreatePropertyForRef(optionalInner.RefName, name, description),
			Type.String => CreatePropertyDeclaration("string", name, description),
			Type.Optional => CreatePropertyForPurpleTypeOptional(name, optionalInner.OptionalInner, description),
			_ => throw new ArgumentOutOfRangeException(nameof(optionalInner.Type), optionalInner.Type, "Not supported type detected")
		};
	}

	private MemberDeclarationSyntax CreatePropertyForPurpleArrayItem(string name, Type arrayType,
	                                                                 string arrayRefName,
	                                                                 GenericArg arrayItemOptionalInner, string description) {
		if (arrayType == Type.Optional)
			// ReSharper disable once TailRecursiveCall
		{
			return CreatePropertyForPurpleArrayItem(name, arrayItemOptionalInner.Type,
			                                        arrayItemOptionalInner.RefName, null, description);
		}

		string typeName = arrayType switch {
			Type.Ref => _allTypes.ContainsKey(NamingConventions.Normalize(arrayRefName))
				            ? NamingConventions.Normalize(arrayRefName)
				            : "JsonElement",
			Type.Boolean => "bool",
			Type.String => "string",
			_ => throw new ArgumentOutOfRangeException()
		};

		return CreatePropertyDeclaration($"{typeName}[]", name, description);
	}
}
