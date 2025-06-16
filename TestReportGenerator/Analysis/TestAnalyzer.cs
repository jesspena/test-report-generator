using TestReportGenerator.Models;

namespace TestReportGenerator.Analysis;

public class TestAnalyzer : ITestAnalyzer
{
    public int Total(List<ITestResult> results) => results.Count();
    public int Passed(List<ITestResult> results) => results.Count(r => r.Status == TestStatusEnum.Passed);
    public int Failed(List<ITestResult> results) => results.Count(r => r.Status == TestStatusEnum.Failed);
    public double TotalDuration(List<ITestResult> results) => results.Sum(r => r.Duration);
}