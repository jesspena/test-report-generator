using TestReportGenerator.Models;

namespace TestReportGenerator.Reports;

public interface IReportGenerator
{
    void Generate(IEnumerable<TestResult> results, string sourceType);
}