namespace TestReportGenerator.Analysis;

public interface ITestAnalyzer
{
    int Total(List<ITestResult> results);
    int Passed(List<ITestResult> results);
    int Failed(List<ITestResult> results);
    double TotalDuration(List<ITestResult> results);
}
