using System;
using System.Collections.Generic;
using System.Linq;
using ch1seL.TonNet.ClientGenerator.Models;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;
using static ch1seL.TonNet.ClientGenerator.Helpers.GeneratorHelpers;

namespace ch1seL.TonNet.ClientGenerator.Helpers
{
    internal class ModelsClassHelpers
    {
        private readonly IReadOnlyCollection<string> _moduleTypes;
        private readonly IReadOnlyDictionary<string, string> _numberTypesMapping;
        private readonly string[] _allTypes;

        public ModelsClassHelpers(IReadOnlyCollection<string> moduleTypes, IReadOnlyDictionary<string, string> numberTypesMapping, string[] allTypes)
        {
            _moduleTypes = moduleTypes;
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
            return EnumDeclaration(Identifier(NamingConventions.Formatter(typeElement.Name)))
                .AddMembers(typeElement
                    .EnumConsts
                    .Select(e => EnumMemberDeclaration(e.Name))
                    .ToArray())
                .AddModifiers(Token(SyntaxKind.PublicKeyword));
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
                            return CreatePropertyForRef(subClass.RefName, subClass.Name);
                        case GenericArgType.Struct:
                            var properties = new List<MemberDeclarationSyntax>();
                            properties.AddRange(subClass.StructFields.Select(sf =>
                            {
                                var addPostfix = NamingConventions.Formatter(sf.Name) == NamingConventions.Formatter(subClass.Name);
                                return CreatePropertyGenericArgs(sf.Type, sf.Name, sf.RefName, sf.OptionalInner, addPostFix: addPostfix);
                            }));
                            return (MemberDeclarationSyntax)ClassDeclaration(NamingConventions.Formatter(subClass.Name))
                                .AddModifiers(Token(SyntaxKind.PublicKeyword))
                                .AddBaseListTypes(
                                    SimpleBaseType(IdentifierName(NamingConventions.Formatter(typeElement.Name))))
                                .AddMembers(properties.ToArray());
                        default:
                            throw new ArgumentOutOfRangeException(nameof(subClass.Type), subClass.Type, "EnumOfTypes doesn't support this type");
                    }
                })
                .ToArray();

            return ClassDeclaration(NamingConventions.Formatter(typeElement.Name))
                .AddModifiers(Token(SyntaxKind.PublicKeyword), Token(SyntaxKind.AbstractKeyword))
                .AddMembers(enumTypes);
        }

        private MemberDeclarationSyntax CreatePropertyForRef(string typeName, string name, bool optional = false, bool nullable = false, bool addPostfix = false)
        {
            typeName = NamingConventions.Formatter(typeName);
            typeName = !_allTypes.Contains(typeName) ? "object" : typeName;
           
            typeName = _numberTypesMapping.ContainsKey(typeName)
                ? _numberTypesMapping[typeName]
                : typeName;

            return CreatePropertyDeclaration(typeName, name, optional, nullable, addPostfix);
        }


        private MemberDeclarationSyntax CreatePropertyGenericArgs(GenericArgType type, string name, string refName, GenericArg optionalInner, bool optional = false, bool addPostFix = false)
        {
            return type switch
            {
                GenericArgType.Boolean => CreatePropertyDeclaration("bool", name, optional, addPostfix: addPostFix),
                GenericArgType.Ref => CreatePropertyForRef(refName, name, addPostfix: addPostFix),
                GenericArgType.String => CreatePropertyDeclaration("string", name, addPostfix: addPostFix),
                GenericArgType.Optional => CreatePropertyGenericArgs(optionalInner.Type, name, optionalInner.RefName, null, addPostFix),
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        private ClassDeclarationSyntax GenerateStruct(TypeElement typeElement)
        {
            var className = typeElement.Name;

            var properties = typeElement.StructFields.Select(CreatePropertyStructFields).ToArray();

            return ClassDeclaration(NamingConventions.Formatter(className))
                .AddModifiers(Token(SyntaxKind.PublicKeyword))
                .AddMembers(properties);
        }

        private MemberDeclarationSyntax CreatePropertyStructFields(StructField sf)
        {
            return sf.Type switch
            {
                PurpleType.Array => CreatePropertyForPurpleArrayItem(sf.Name, sf.ArrayItem.Type, sf.ArrayItem.RefName, sf.ArrayItem.OptionalInner),
                PurpleType.BigInt => CreatePropertyDeclaration("BigInteger", sf.Name),
                PurpleType.Boolean => CreatePropertyDeclaration("bool", sf.Name),
                PurpleType.Number => CreatePropertyDeclaration(NumberUtils.ConvertToSharpNumeric(sf.NumberType, sf.NumberSize), sf.Name),
                PurpleType.Ref => CreatePropertyForRef(sf.RefName, sf.Name),
                PurpleType.String => CreatePropertyDeclaration("string", sf.Name),
                PurpleType.Optional => CreateOptionalPropertyForPurple(sf.Name, sf.OptionalInner),
                _ => throw new ArgumentOutOfRangeException(nameof(sf.Type), sf.Type,"Not supported type detected")
            };
        }

        private MemberDeclarationSyntax CreateOptionalPropertyForPurple(string name, StructFieldOptionalInner optionalInner)
        {
            return optionalInner.Type switch
            {
                PurpleType.Array => CreatePropertyForPurpleArrayItem(name, optionalInner.ArrayItem.Type, optionalInner.ArrayItem.RefName, null, true),
                PurpleType.BigInt => CreatePropertyDeclaration("BigInteger", name, true, true),
                PurpleType.Boolean => CreatePropertyDeclaration("bool", name, true, true),
                PurpleType.Number => CreatePropertyDeclaration(NumberUtils.ConvertToSharpNumeric(optionalInner.NumberType, optionalInner.NumberSize), name,
                    true),
                PurpleType.Ref => CreatePropertyForRef(optionalInner.RefName, name),
                PurpleType.String => CreatePropertyDeclaration("string", name),
                PurpleType.Optional => CreatePropertyForPurpleTypeOptionalOptional(name, optionalInner.OptionalInner),
                _ => throw new ArgumentOutOfRangeException(nameof(optionalInner.Type), optionalInner.Type,"Not supported type detected")
            };
        }

        private static MemberDeclarationSyntax CreatePropertyForPurpleTypeOptionalOptional(string name, OptionalInnerOptionalInner optionalInner)
        {
            return optionalInner.Type switch
            {
                PurpleType.BigInt => CreatePropertyDeclaration("BigInteger", name, true, true),
                PurpleType.Boolean => CreatePropertyDeclaration("bool", name, true, true),
                PurpleType.Number => CreatePropertyDeclaration(NumberUtils.ConvertToSharpNumeric(optionalInner.NumberType, optionalInner.NumberSize), name,
                    true, true),
                PurpleType.String => CreatePropertyDeclaration("string", name, true),
                _ => throw new ArgumentOutOfRangeException(nameof(optionalInner.Type), optionalInner.Type,"Not supported type detected")
            };
        }
        
        private  MemberDeclarationSyntax CreatePropertyForPurpleArrayItem(string name, GenericArgType arrayType, string arrayRefName,
            GenericArg arrayItemOptionalInner, bool nullable = false)
        {
            if (arrayType == GenericArgType.Optional)
                // ReSharper disable once TailRecursiveCall
                return CreatePropertyForPurpleArrayItem(name, arrayItemOptionalInner.Type, arrayItemOptionalInner.RefName, null, true);

            var typeName = arrayType switch
            {
                GenericArgType.Ref => _allTypes.Contains(arrayRefName)?NamingConventions.Formatter(arrayRefName):"object",
                GenericArgType.Boolean => "bool",
                GenericArgType.String => "string",
                _ => throw new ArgumentOutOfRangeException()
            };

            return CreatePropertyDeclaration($"{typeName}[]", name, nullable: nullable);
        }
    }
}