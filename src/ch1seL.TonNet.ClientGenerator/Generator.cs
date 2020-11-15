using System;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using ch1seL.TonNet.ClientGenerator.Helpers;
using ch1seL.TonNet.ClientGenerator.Models;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Formatting;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Formatting;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace ch1seL.TonNet.ClientGenerator
{
    internal static class Generator
    {
        private const string ApiFilePath = "Resources/api.json";
        private const string NameSpace = "ch1seL.TonNet.Client";
        private const string OutPath = @"C:\\Users\\ch1seL\\repos\\ton-actions\\ton-client-dotnet\\ch1seL.TonNet.Client\\";
        private static readonly string[] ModulesNamespaces = {"System", "System.Threading", "System.Threading.Tasks"};
        private static readonly string[] ModelsNamespaces = {"System", "System.Text.Json"};

        public static async Task GenerateModule()
        {
            var tonApi = await JsonSerializer.DeserializeAsync<TonApi>(File.OpenRead(ApiFilePath), TonModelSerializationOptions.Options);

            GeneratorHelpers.CreateUnit("ITonClient", unitName => CreateTonClientInterface(unitName, tonApi), Path.Combine(OutPath, "ITonClient.cs"));
            GeneratorHelpers.CreateUnit("TonClient", unitName => CreateTonClientClass(unitName, tonApi),  Path.Combine(OutPath, "TonClient.cs"), "ch1seL.TonNet.Abstract");

            foreach (Module module in tonApi!.Modules)
            {
                GeneratorHelpers.CreateUnit(module.Name, unitName=>CreateTonModuleInterface(unitName,module),Path.Combine(OutPath,"Modules", $"I{StringUtils.ToCamelCase(module.Name)}.cs"), ModulesNamespaces);
                GeneratorHelpers.CreateUnit(module.Name, unitName=>CreateTonModuleClass(unitName,module),Path.Combine(OutPath,"Modules", $"{StringUtils.ToCamelCase(module.Name)}.cs"), ModulesNamespaces.Append("ch1seL.TonNet.Abstract").ToArray());
                
                foreach (TypeElement typeElement in module.Types)
                {
                    GeneratorHelpers.CreateUnit(module.Name, unitName=>CreateTonModelClass(unitName,module, typeElement), Path.Combine(OutPath,"Models", $"{NamingConventions.CommonFormatterFormatter(typeElement.Name)}.cs"), ModulesNamespaces);    
                }
            }
        }

        private static NamespaceDeclarationSyntax CreateTonModelClass(string unitName, Module module, TypeElement typeElement)
        {
            ClassDeclarationSyntax item = ClassDeclaration(NamingConventions.CommonFormatterFormatter(typeElement.Name))
                .AddModifiers(Token(SyntaxKind.PublicKeyword));
            
            return NamespaceDeclaration(IdentifierName(NameSpace))
                .AddMembers(item);
        }

        private static NamespaceDeclarationSyntax CreateTonModuleClass(string unitName, Module module)
        {
            StatementSyntax statementSyntax = ParseStatement("_tonClientAdapter = tonClientAdapter;");
            
            VariableDeclarationSyntax variableDeclaration = VariableDeclaration(ParseTypeName("ITonClientAdapter"))
                .AddVariables(VariableDeclarator("_tonClientAdapter"));
            FieldDeclarationSyntax fieldDeclaration = FieldDeclaration(variableDeclaration)
                .AddModifiers(Token(SyntaxKind.PrivateKeyword));
            
            ConstructorDeclarationSyntax constructorDeclaration = ConstructorDeclaration(unitName)
                .AddParameterListParameters(
                    Parameter(Identifier("tonClientAdapter")).WithType(IdentifierName("ITonClientAdapter")))
                .WithModifiers(TokenList(Token(SyntaxKind.PublicKeyword)))
                .WithBody(Block(statementSyntax));
            
            var methods = module
                .Functions
                .Select(f => GeneratorHelpers.GetMethodDeclaration(module, f, true))
                .ToArray();
            
            ClassDeclarationSyntax item = ClassDeclaration(unitName)
                .AddModifiers(Token(SyntaxKind.PublicKeyword))
                .AddMembers(fieldDeclaration)
                .AddMembers(constructorDeclaration)
                .AddMembers(methods);
            
            return NamespaceDeclaration(IdentifierName(NameSpace))
                .AddMembers(item);
        }

        private static NamespaceDeclarationSyntax CreateTonModuleInterface(string unitName, Module module)
        {
            var methods = module.Functions.Select(function => GeneratorHelpers.GetMethodDeclaration(module, function, false))
                .ToArray();
            
            InterfaceDeclarationSyntax item = InterfaceDeclaration($"I{unitName}")
                .AddModifiers(Token(SyntaxKind.PublicKeyword))
                .AddMembers(methods);

            return NamespaceDeclaration(IdentifierName(NameSpace))
                .AddMembers(item);
        }

        private static NamespaceDeclarationSyntax CreateTonClientClass(string unitName, TonApi tonApi)
        {
            var propertyDeclarationSyntaxes = GeneratorHelpers.GetModuleProperties(tonApi);

            VariableDeclarationSyntax variableDeclaration = VariableDeclaration(ParseTypeName("ITonClientAdapter"))
                .AddVariables(VariableDeclarator("_tonClientAdapter"));
            FieldDeclarationSyntax fieldDeclaration = FieldDeclaration(variableDeclaration)
                .AddModifiers(Token(SyntaxKind.PrivateKeyword));

            StatementSyntax statementSyntax = ParseStatement("_tonClientAdapter = tonClientAdapter;");
            ConstructorDeclarationSyntax constructorDeclaration = ConstructorDeclaration(unitName)
                .AddParameterListParameters(
                    Parameter(Identifier("tonClientAdapter")).WithType(IdentifierName("ITonClientAdapter")))
                .WithModifiers(TokenList(Token(SyntaxKind.PublicKeyword)))
                .WithBody(Block(statementSyntax));

            ClassDeclarationSyntax item = ClassDeclaration(unitName)
                .AddModifiers(Token(SyntaxKind.PublicKeyword))
                .AddMembers(fieldDeclaration)
                .AddMembers(constructorDeclaration)
                .AddMembers(propertyDeclarationSyntaxes);

            return NamespaceDeclaration(IdentifierName(NameSpace))
                .AddMembers(item);
        }

        private static NamespaceDeclarationSyntax CreateTonClientInterface(string unitName, TonApi tonApi)
        {
            var propertyDeclarationSyntaxes = GeneratorHelpers.GetModuleProperties(tonApi);

            InterfaceDeclarationSyntax item = InterfaceDeclaration(unitName)
                .AddModifiers(Token(SyntaxKind.PublicKeyword))
                .AddMembers(propertyDeclarationSyntaxes);

            return NamespaceDeclaration(IdentifierName(NameSpace))
                .AddMembers(item);
        }
    }
}