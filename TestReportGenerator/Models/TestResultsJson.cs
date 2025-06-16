namespace TestReportGenerator.Models;

public class TestResultsJson
{
  public string? TestSuite { get; set; }
  public string? ExecutionDate { get; set; }
  public string? Environment { get; set; }
  public List<TestResultJson> Tests { get; set; } = [];
}
