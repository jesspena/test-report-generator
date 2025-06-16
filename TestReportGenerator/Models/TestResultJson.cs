namespace TestReportGenerator.Models;

public class TestResultJson
{
  public string? TestName { get; set; }
  public string? Status { get; set; }
  public double Duration { get; set; }
  public string? Category { get; set; }
  public string? Priority { get; set; }
  public string? ErrorMessage { get; set; }
}
