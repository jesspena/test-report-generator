# Test Report Generator - Refactoring Exercise

## Overview
This console application reads test results from different file formats and generates reports. It uses the modern .NET hosting model with Microsoft.Extensions.Hosting and DependencyInjection, but the current implementation has numerous code smells and violations of SOLID principles, DRY, and KISS principles.

## How you can run

```shell
dotnet run --project TestReportGenerator test_results.csv

dotnet run --project TestReportGenerator test_results.json

dotnet run --project TestReportGenerator test_results.xml

```

## Your Task
Refactor this application to follow best practices and implement design patterns where appropriate don't care about validations just throw if needed.

> Note: Use explicit names for each design pattern.

## Code Smell Inventory

### 1. SOLID Violations
- **Single Responsibility Principle (SRP)**: The Program class handles file reading, parsing, analysis, and report generation
- **Open/Closed Principle (OCP)**: Adding new file formats or report types requires modifying existing code
- **Liskov Substitution Principle (LSP)**: No abstractions to substitute
- **Interface Segregation Principle (ISP)**: No interfaces defined
- **Dependency Inversion Principle (DIP)**: Concrete dependencies instead of abstractions

### 2. DRY Violations
- Repeated file existence checking logic
- Similar parsing structure for different file types
- Duplicated calculation logic

### 3. KISS Violations
- Complex methods doing too many things
- Nested logic that could be simplified

## Required Refactoring Tasks

### Priority 1: Extract Responsibilities 
1. Create separate classes for:
   - File reading (`IFileReader` interface)
   - Parsing (`ITestResultParser` interface with implementations)
   - Report generation (`IReportGenerator` interface)
   - Test analysis (`ITestAnalyzer` interface)

### Priority 2: Implement Patterns 

#### Factory Pattern 
- Create `IParserFactory` to create appropriate parsers based on file type
- Remove the switch statement in `ProcessTestResults`

#### Strategy Pattern 
- Implement different parsing strategies for CSV, JSON, and XML
- Each strategy should implement `ITestResultParser`

#### Builder Pattern 
- Create `ReportBuilder` to construct reports step by step
- Allow fluent interface for report customization

#### Decorator Pattern 
- Create decorators for report sections (Summary, CategoryAnalysis, FailedTests, PriorityAnalysis)
- Allow dynamic composition of report sections

### Priority 3: Apply Principles 

#### Template Method Pattern (Optional)
- Create abstract `BaseReportGenerator` with template method
- Allow different report formats (Console, HTML, Markdown)

#### Facade Pattern (Optional)
- Create `TestReportFacade` to simplify the API
- Hide complexity of parsing and report generation

## Expected Structure After Refactoring (suggestion)

```
TestReportGenerator/
├── Program.cs (clean DI setup)
├── ReportGeneratorCli.cs (simplified with injected dependencies)
├── Models/
│   ├── ITestResult.cs
│   └── TestResult.cs
├── Services/
│   ├── IFileReader.cs
│   └── FileReader.cs
├── Parsers/
│   ├── ITestResultParser.cs
│   ├── CsvParser.cs
│   ├── JsonParser.cs
│   └── XmlParser.cs
├── Factories/
│   ├── IParserFactory.cs
│   └── ParserFactory.cs
├── Reports/
│   ├── IReportGenerator.cs
│   ├── ConsoleReportGenerator.cs
│   └── Builders/
│       └── ReportBuilder.cs
├── Decorators/
│   ├── IReportSection.cs
│   ├── SummarySection.cs
│   ├── CategorySection.cs
│   └── FailedTestsSection.cs
└── Analysis/
    ├── ITestAnalyzer.cs
    └── TestAnalyzer.cs
```

## Sample Refactored Program.cs
```csharp
var builder = Host.CreateApplicationBuilder(args);
var services = builder.Services;

// Register services with proper DI
services.AddSingleton<IFileReader, FileReader>();
services.AddSingleton<IParserFactory, ParserFactory>();
services.AddSingleton<ICsvParser, CsvParser>();
services.AddSingleton<IJsonParser, JsonParser>();
services.AddSingleton<IXmlParser, XmlParser>();
services.AddSingleton<ITestAnalyzer, TestAnalyzer>();
services.AddSingleton<IReportGenerator, ConsoleReportGenerator>();
services.AddSingleton<ReportGeneratorCli>();

using var host = builder.Build();
host.Services.GetRequiredService<ReportGeneratorCli>().Run(args);
```

## Evaluation Criteria
1. **SOLID Principles Applied** (25 points)
2. **Design Patterns Implemented** (35 points)
3. **DRY/KISS Principles** (20 points)
4. **Code Organization** (10 points)
5. **Functionality Maintained** (10 points)

## Tips
- Start with extracting interfaces
- Focus on one pattern at a time
- Ensure the application still works after each refactoring
- Use meaningful names for interfaces and classes
- Don't over-engineer - apply patterns where they add value

Good luck!