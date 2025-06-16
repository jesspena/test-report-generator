using TestReportGenerator.Models;

namespace TestReportGenerator.Parsers;

public class CsvParser : ITestResultParser
{
    public List<ITestResult> Parse(string filePath)
    {
        var lines = File.ReadAllLines(filePath).Skip(1);

        foreach (var line in lines)
        {
            var parts = line.Split(',');

            yield return new TestResult
            {
                TestName = parts[0],
                Status = parts[1],
                Duration = double.Parse(parts[2], CultureInfo.InvariantCulture),
                Category = parts[3],
                Priority = parts[4]
            };
        }
    }
}