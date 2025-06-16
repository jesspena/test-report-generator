using TestReportGenerator.Models;

namespace TestReportGenerator.Reports;

public class ConsoleReportGenerator : IReportGenerator
{
    public void Generate(IEnumerable<TestResult> results, string sourceType)
    {
        Console.WriteLine($"\n======= Report Source: {sourceType} =======");
        Console.WriteLine($"Total Tests: {results.Count()}");
        Console.WriteLine($"✅ Passed: {results.Count(r => r.Status == "PASSED")}");
        Console.WriteLine($"❌ Failed: {results.Count(r => r.Status == "FAILED")}");
        Console.WriteLine("===========================================");
    }
}