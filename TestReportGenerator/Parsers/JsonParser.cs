namespace TestReportGenerator.Parsers;

using System.Text.Json;
using TestReportGenerator.Models;

public class JsonParser : ITestResultParser
{
    public List<ITestResult> Parse(string fileName)
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
}