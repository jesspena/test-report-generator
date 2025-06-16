namespace TestReportGenerator.Models;

// TODO: This class should implement an interface (Interface Segregation Principle)
// HINT: Create ITestResult interface in Models folder
public class TestResult
{
  public string? TestName { get; set; }
  public string? Status { get; set; }
  public double Duration { get; set; }
  public string? Category { get; set; }
  public string? Priority { get; set; }

  // TODO: No validation - consider adding validation in setters or using Builder pattern
  // HINT: Status should be an enum, Duration should be positive
}
