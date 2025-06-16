namespace TestReportGenerator.Parsers;

using TestReportGenerator.Models;

public interface ITestResultParser
{
    List<ITestResult> Parse(string filePath);
}