namespace TestReportGenerator.Models;

public interface ITestResult
{
    string TestName { get; }
    TestStatusEnum Status { get; }
    double Duration { get; }
    string Category { get; }
    string Priority { get; }
}