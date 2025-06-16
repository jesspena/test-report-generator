namespace TestReportGenerator.Parsers;

using TestReportGenerator.Models;

public class XmlParser : ITestResultParser
{
    public List<ITestResult> Parse(string fileName)
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
}