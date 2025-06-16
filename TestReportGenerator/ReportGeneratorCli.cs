using System.Text.Json;
using System.Xml.Linq;
using TestReportGenerator.Models;

namespace TestReportGenerator;

// TODO: This class violates Single Responsibility Principle (SRP)
// It handles CLI logic, file reading, parsing, analysis, and report generation
// HINT: Extract responsibilities into separate services injected via constructor
public class ReportGeneratorCli
{
  // TODO: No constructor injection - violates Dependency Injection best practices
  // HINT: Add constructor with required dependencies

  public void Run(string[] args)
  {
    Console.WriteLine("Test Report Generator v1.0");
    Console.WriteLine("==========================\n");

    // TODO: Basic argument handling - could be improved with proper validation
    // HINT: Consider using a command-line parser library
    if (args.Length == 0)
    {
      Console.WriteLine("""
      Usage: TestReportGenerator <test_results_file>
      Supported formats: .csv, .json, .xml

      Example: TestReportGenerator test_results.csv
      """);

      return;
    }

    string inputFile = args[0];

    // TODO: File existence check before processing
    // HINT: This should be in a separate validation method
    if (!File.Exists(inputFile))
    {
      Console.WriteLine($"❌ Error: File not found: {inputFile}");
      return;
    }

    // TODO: Format detection based on extension - violates OCP
    // HINT: Consider using a more extensible approach
    string fileExtension = Path.GetExtension(inputFile).ToLower();
    string fileType = fileExtension switch
    {
      ".csv" => "CSV",
      ".json" => "JSON",
      ".xml" => "XML",
      _ => "UNKNOWN"
    };

    if (fileType == "UNKNOWN")
    {
      Console.WriteLine($"❌ Error: Unsupported file format: {fileExtension}");
      Console.WriteLine("Supported formats: .csv, .json, .xml");
      return;
    }

    Console.WriteLine($"Processing {fileType} file: {inputFile}");
    ProcessTestResults(inputFile, fileType);

    Console.WriteLine("\nPress any key to exit...");
    Console.ReadKey();
  }


  // TODO: This method is too complex and violates KISS (Keep It Simple, Stupid)
  // It also violates SRP by doing parsing, analysis, and report generation
  // HINT: Inject IParserFactory and use Strategy pattern for different formats
  private void ProcessTestResults(string fileName, string fileType)
  {
    List<TestResult> results = [];

    // TODO: This switch statement violates Open/Closed Principle
    // Adding new file formats requires modifying this method
    // HINT: Use Factory pattern or Abstract Factory pattern with DI
    switch (fileType)
    {
      case "CSV":
        // TODO: Direct file I/O violates SRP - should be in a separate service
        // HINT: Create IFileReader service
        string[] csvLines = File.ReadAllLines(fileName);

        // TODO: Parsing logic should be in a separate parser class
        // HINT: Create ICsvParser and inject it
        for (int i = 1; i < csvLines.Length; i++) // Skip header
        {
          string[] parts = csvLines[i].Split(',');

          if (parts.Length >= 5)
          {
            // TODO: Manual object creation - consider Builder pattern
            TestResult result = new TestResult
            {
              TestName = parts[0],
              Status = parts[1],
              Duration = double.Parse(parts[2]),
              Category = parts[3],
              Priority = parts[4]
            };
            results.Add(result);
          }
        }
        break;

      case "JSON":
        // TODO: Direct file I/O violates SRP - should be in a separate service
        // HINT: Create IFileReader service
        string jsonContent = File.ReadAllText(fileName);
        using (JsonDocument document = JsonDocument.Parse(jsonContent))
        {
          JsonElement root = document.RootElement;
          JsonElement tests = root.GetProperty("tests");

          foreach (JsonElement test in tests.EnumerateArray())
          {
            var result = new TestResult
            {
              TestName = test.GetProperty("testName").GetString(),
              Status = test.GetProperty("status").GetString(),
              Duration = test.GetProperty("duration").GetDouble(),
              Category = test.GetProperty("category").GetString(),
              Priority = test.GetProperty("priority").GetString()
            };
            results.Add(result);
          }
        }
        break;

      case "XML":
        // TODO: Direct file I/O violates SRP - should be in a separate service
        // HINT: Create IFileReader service
        XDocument xmlDoc = XDocument.Load(fileName);
        var testElements = xmlDoc.Descendants("test");

        foreach (var testElement in testElements)
        {
          var result = new TestResult
          {
            TestName = testElement.Element("testName")?.Value,
            Status = testElement.Element("status")?.Value,
            Duration = double.Parse(testElement.Element("duration")?.Value ?? "0"),
            Category = testElement.Element("category")?.Value,
            Priority = testElement.Element("priority")?.Value
          };
          results.Add(result);
        }
        break;

      default:
        Console.WriteLine($"Unsupported file type: {fileType}");
        return;
    }

    // TODO: Direct method call - should use injected service
    // HINT: Inject IReportGenerator
    // If you didn't finished and you reached here it's ok just create a service with the exisiting GenerateReport and use it with DI.
    GenerateReport(results, fileType);
  }

  // TODO: This method should be in a separate IReportGenerator service
  // It violates SRP and could benefit from Builder pattern
  private void GenerateReport(List<TestResult> results, string sourceType)
  {
    Console.WriteLine($"""

    ==========================================
             TEST EXECUTION REPORT
             Source: {sourceType}
    ==========================================

    """);

    // TODO: Create ITestAnalyzer service for these calculations
    // HINT: Use Decorator pattern to compose different metrics
    int totalTests = results.Count;
    int passedTests = results.Count(r => r.Status == "PASSED");
    int failedTests = results.Count(r => r.Status == "FAILED");
    double totalDuration = results.Sum(r => r.Duration);
    double passRate = totalTests > 0 ? (double)passedTests / totalTests * 100 : 0;

    Console.WriteLine($"""
    Total Tests: {totalTests}
    ✅Passed: {passedTests} ({passRate:F2}%)
    ❌Failed: {failedTests} ({100 - passRate:F2}%)
    Total Duration: {totalDuration:F2} seconds
    """);

    // TODO: Extract category analysis to separate service/strategy
    // HINT: Create ICategoryAnalyzer
    Console.WriteLine("\nBy Category:");
    var categories = results.GroupBy(r => r.Category);
    foreach (var category in categories)
    {
      int catTotal = category.Count();
      int catPassed = category.Count(r => r.Status == "PASSED");
      double catPassRate = catTotal > 0 ? (double)catPassed / catTotal * 100 : 0;
      Console.WriteLine($"- {category.Key}: {catPassed}/{catTotal} passed ({catPassRate:F2}%)");
    }

    // TODO: Extract failed test reporting to decorator or separate service
    // HINT: Use Decorator pattern for optional report sections
    var failedTests2 = results.Where(r => r.Status == "FAILED");
    if (failedTests2.Any())
    {
      Console.WriteLine("\nFailed Tests:");
      foreach (var test in failedTests2)
      {
        Console.WriteLine($"- {test.TestName} ({test.Duration}s) - {test.Category}");
      }
    }

    // TODO: Extract priority analysis - another candidate for Decorator
    // HINT: Create IPriorityAnalyzer or use Decorator pattern
    Console.WriteLine("\nBy Priority:");
    var priorities = results.GroupBy(r => r.Priority);
    foreach (var priority in priorities)
    {
      int priTotal = priority.Count();
      int priPassed = priority.Count(r => r.Status == "PASSED");
      Console.WriteLine($"- {priority.Key}: {priPassed}/{priTotal} tests passed");
    }

    // TODO: Consider using Template Method pattern for report structure
    // HINT: Create abstract BaseReportGenerator with customizable sections
    Console.WriteLine("==========================================\n");
  }
}
