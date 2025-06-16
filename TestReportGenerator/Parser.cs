using System.Text.Json;
using System.Xml.Linq;
using TestReportGenerator.Models;

namespace TestReportGenerator;

// IMPORTANT: This is an extra logic you can use if you want to process json, xml or csv
// You can delete this file is not really part of the project those are suggestions
public static class Parser
{
  // JSON Parsing Implementation
  public static List<TestResult> ParseJsonFile(string fileName)
  {
    var results = new List<TestResult>();

    // Read the entire JSON file
    string jsonContent = File.ReadAllText(fileName);

    // Parse the JSON document
    using (JsonDocument document = JsonDocument.Parse(jsonContent))
    {
      JsonElement root = document.RootElement;

      // Get the tests array
      if (root.TryGetProperty("tests", out JsonElement tests))
      {
        // Iterate through each test
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
    }

    return results;
  }

  // XML Parsing Implementation
  public static List<TestResult> ParseXmlFile(string fileName)
  {
    var results = new List<TestResult>();

    // Load the XML document
    XDocument xmlDoc = XDocument.Load(fileName);

    // Find all test elements
    var testElements = xmlDoc.Descendants("test");

    // Parse each test element
    foreach (var testElement in testElements)
    {
      var result = new TestResult
      {
        TestName = testElement.Element("testName")?.Value ?? "",
        Status = testElement.Element("status")?.Value ?? "",
        Duration = double.TryParse(testElement.Element("duration")?.Value, out double duration) ? duration : 0,
        Category = testElement.Element("category")?.Value ?? "",
        Priority = testElement.Element("priority")?.Value ?? ""
      };

      results.Add(result);
    }

    return results;
  }

  // Alternative: Using System.Text.Json deserialization for JSON (cleaner approach)
  public static List<TestResult> ParseJsonFileWithDeserialization(string fileName)
  {
    var results = new List<TestResult>();

    string jsonContent = File.ReadAllText(fileName);

    // Define a wrapper class for the JSON structure
    var options = new JsonSerializerOptions
    {
      PropertyNameCaseInsensitive = true
    };

    var testData = JsonSerializer.Deserialize<TestResultsJson>(jsonContent, options);

    if (testData?.Tests != null)
    {
      results = testData.Tests.Select(t => new TestResult
      {
        TestName = t.TestName,
        Status = t.Status,
        Duration = t.Duration,
        Category = t.Category,
        Priority = t.Priority
      }).ToList();
    }

    return results;
  }
}
