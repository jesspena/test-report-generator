namespace TestReportGenerator.Models;

// TODO: This class should implement an interface (Interface Segregation Principle)
// HINT: Create ITestResult interface in Models folder
public class TestResult : ITestResult
{
    public required string TestName { get; init; }
    public required TestStatusEnum Status { get; init; }
    public required double Duration { get; init; }
    public required string Category { get; init; }
    public required string Priority { get; init; }

  // TODO: No validation - consider adding validation in setters or using Builder pattern
  // HINT: Status should be an enum, Duration should be positive
}
