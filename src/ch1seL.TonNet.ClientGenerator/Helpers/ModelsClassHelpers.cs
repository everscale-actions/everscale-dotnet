using System;
using System.Collections.Generic;
using System.Linq;
using ch1seL.TonNet.ClientGenerator.Models;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;
using static ch1seL.TonNet.ClientGenerator.Helpers.PropertyHelpers;

namespace ch1seL.TonNet.ClientGenerator.Helpers
{
    internal class ModelsClassHelpers
    {
        private readonly string[] _allTypes;
        private readonly IReadOnlyDictionary<string, string> _numberTypesMapping;

        public ModelsClassHelpers(IReadOnlyDictionary<string, string> numberTypesMapping, string[] allTypes)
        {
            _numberTypesMapping = numberTypesMapping;
            _allTypes = allTypes;
        }

        public NamespaceDeclarationSyntax CreateTonModelClass(TypeElement typeElement)
        {
            NamespaceDeclarationSyntax ns = NamespaceDeclaration(IdentifierName(Generator.NameSpaceModels));

            return typeElement.Type switch
            {
                TypeType.EnumOfConsts => ns.AddMembers(GenerateEnumOfConsts(typeElement)),
                TypeType.EnumOfTypes => ns.AddMembers(GenerateEnumOfTypes(typeElement)),
                TypeType.Struct => ns.AddMembers(GenerateStruct(typeElement)),
                _ => throw new ArgumentOutOfRangeException(nameof(typeElement.Type), typeElement.Type, "Not supported type")
            };
        }

        private static EnumDeclarationSyntax GenerateEnumOfConsts(TypeElement typeElement)
        {
            return EnumDeclaration(Identifier(NamingConventions.Normalize(typeElement.Name)))
                .AddMembers(typeElement
                    .EnumConsts
                    .Select(e => EnumMemberDeclaration(e.Name))
                    .ToArray())
                .AddModifiers(Token(SyntaxKind.PublicKeyword)
                    .WithLeadingTrivia(CommentsHelpers.BuildCommentTrivia(typeElement.Description)));
        }

        private ClassDeclarationSyntax GenerateEnumOfTypes(TypeElement typeElement)
        {
            var enumTypes = typeElement
                .EnumTypes
                .Select(subClass =>
                {
                    switch (subClass.Type)
                    {
                        case GenericArgType.Ref:
                            return CreatePropertyForRef(subClass.RefName, subClass.Name, subClass.Description);
                        case GenericArgType.Struct:
                            var properties = new List<MemberDeclarationSyntax>();
                            properties.AddRange(subClass.StructFields.Select(sf =>
                            {
                                var addPostfix = NamingConventions.Normalize(sf.Name) == NamingConventions.Normalize(subClass.Name);
                                return CreatePropertyGenericArgs(sf.Type, sf.Name, sf.RefName, sf.OptionalInner, subClass.Description, addPostFix: addPostfix);
                            }));
                            return (MemberDeclarationSyntax) ClassDeclaration(NamingConventions.Normalize(subClass.Name))
                                .AddModifiers(Token(SyntaxKind.PublicKeyword)
                                    .WithLeadingTrivia(CommentsHelpers.BuildCommentTrivia(subClass.Description)))
                                .AddBaseListTypes(
                                    SimpleBaseType(IdentifierName(NamingConventions.Normalize(typeElement.Name))))
                                .AddMembers(properties.ToArray());
                        default:
                            throw new ArgumentOutOfRangeException(nameof(subClass.Type), subClass.Type, "EnumOfTypes doesn't support this type");
                    }
                })
                .ToArray();

            return ClassDeclaration(NamingConventions.Normalize(typeElement.Name))
                .AddModifiers(Token(SyntaxKind.PublicKeyword)
                        .WithLeadingTrivia(CommentsHelpers.BuildCommentTrivia(typeElement.Description))
                    , Token(SyntaxKind.AbstractKeyword))
                .AddMembers(enumTypes);
        }

        private MemberDeclarationSyntax CreatePropertyForRef(string typeName, string name, string description, bool optional = false, bool nullable = false,
            bool addPostfix = false)
        {
            typeName = NamingConventions.Normalize(typeName);
            typeName = !_allTypes.Contains(typeName) ? "JsonElement" : typeName;

            typeName = _numberTypesMapping.ContainsKey(typeName)
                ? _numberTypesMapping[typeName]
                : typeName;

            return CreatePropertyDeclaration(typeName, name, description, optional, nullable, addPostfix);
        }


        private MemberDeclarationSyntax CreatePropertyGenericArgs(GenericArgType type, string name, string refName, GenericArg optionalInner,
            string description,
            bool optional = false, bool addPostFix = false)
        {
            return type switch
            {
                GenericArgType.Boolean => CreatePropertyDeclaration("bool", name, description, optional, addPostfix: addPostFix),
                GenericArgType.Ref => CreatePropertyForRef(refName, name, description, addPostfix: addPostFix),
                GenericArgType.String => CreatePropertyDeclaration("string", name, description, addPostfix: addPostFix),
                GenericArgType.Optional => CreatePropertyGenericArgs(optionalInner.Type, name, optionalInner.RefName, null, description, addPostFix),
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        private ClassDeclarationSyntax GenerateStruct(TypeElement typeElement)
        {
            var className = typeElement.Name;

            var properties = typeElement.StructFields.Select(CreatePropertyStructFields).ToArray();

            return ClassDeclaration(NamingConventions.Normalize(className))
                .AddModifiers(Token(SyntaxKind.PublicKeyword)
                    .WithLeadingTrivia(CommentsHelpers.BuildCommentTrivia(typeElement.Description)))
                .AddMembers(properties);
        }

        private MemberDeclarationSyntax CreatePropertyStructFields(StructField sf)
        {
            return sf.Type switch
            {
                PurpleType.Array => CreatePropertyForPurpleArrayItem(sf.Name, sf.ArrayItem.Type, sf.ArrayItem.RefName, sf.ArrayItem.OptionalInner,
                    sf.Description),
                PurpleType.BigInt => CreatePropertyDeclaration("BigInteger", sf.Name, sf.Description),
                PurpleType.Boolean => CreatePropertyDeclaration("bool", sf.Name, sf.Description),
                PurpleType.Number => CreatePropertyDeclaration(NumberUtils.ConvertToSharpNumeric(sf.NumberType, sf.NumberSize), sf.Name, sf.Description),
                PurpleType.Ref => CreatePropertyForRef(sf.RefName, sf.Name, sf.Description),
                PurpleType.String => CreatePropertyDeclaration("string", sf.Name, sf.Description),
                PurpleType.Optional => CreateOptionalPropertyForPurple(sf.Name, sf.OptionalInner, sf.Description),
                _ => throw new ArgumentOutOfRangeException(nameof(sf.Type), sf.Type, "Not supported type detected")
            };
        }

        private MemberDeclarationSyntax CreateOptionalPropertyForPurple(string name, StructFieldOptionalInner optionalInner, string description)
        {
            return optionalInner.Type switch
            {
                PurpleType.Array => CreatePropertyForPurpleArrayItem(name, optionalInner.ArrayItem.Type, optionalInner.ArrayItem.RefName, null, description,
                    true),
                PurpleType.BigInt => CreatePropertyDeclaration("BigInteger", name, description, true, true),
                PurpleType.Boolean => CreatePropertyDeclaration("bool", name, description, true, true),
                PurpleType.Number => CreatePropertyDeclaration(NumberUtils.ConvertToSharpNumeric(optionalInner.NumberType, optionalInner.NumberSize), name,
                    description, true),
                PurpleType.Ref => CreatePropertyForRef(optionalInner.RefName, name, description),
                PurpleType.String => CreatePropertyDeclaration("string", name, description),
                PurpleType.Optional => CreatePropertyForPurpleTypeOptionalOptional(name, optionalInner.OptionalInner, description),
                _ => throw new ArgumentOutOfRangeException(nameof(optionalInner.Type), optionalInner.Type, "Not supported type detected")
            };
        }

        private static MemberDeclarationSyntax CreatePropertyForPurpleTypeOptionalOptional(string name, OptionalInnerOptionalInner optionalInner,
            string description)
        {
            return optionalInner.Type switch
            {
                PurpleType.BigInt => CreatePropertyDeclaration("BigInteger", name, description, true, true),
                PurpleType.Boolean => CreatePropertyDeclaration("bool", name, description, true, true),
                PurpleType.Number => CreatePropertyDeclaration(NumberUtils.ConvertToSharpNumeric(optionalInner.NumberType, optionalInner.NumberSize), name,
                    description,
                    true, true),
                PurpleType.String => CreatePropertyDeclaration("string", name, description, true),
                _ => throw new ArgumentOutOfRangeException(nameof(optionalInner.Type), optionalInner.Type, "Not supported type detected")
            };
        }

        private MemberDeclarationSyntax CreatePropertyForPurpleArrayItem(string name, GenericArgType arrayType, string arrayRefName,
            GenericArg arrayItemOptionalInner, string description, bool nullable = false)
        {
            if (arrayType == GenericArgType.Optional)
                // ReSharper disable once TailRecursiveCall
                return CreatePropertyForPurpleArrayItem(name, arrayItemOptionalInner.Type, arrayItemOptionalInner.RefName, null, description, true);

            var typeName = arrayType switch
            {
                GenericArgType.Ref => _allTypes.Contains(arrayRefName) ? NamingConventions.Normalize(arrayRefName) : "JsonElement",
                GenericArgType.Boolean => "bool",
                GenericArgType.String => "string",
                _ => throw new ArgumentOutOfRangeException()
            };

            return CreatePropertyDeclaration($"{typeName}[]", name, description, nullable: nullable);
        }
    }
}