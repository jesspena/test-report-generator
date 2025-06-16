using TestReportGenerator.Models;
using TestReportGenerator.Services;
using TestReportGenerator.Parsers;
using TestReportGenerator.Factories;
using TestReportGenerator.Reports;

namespace TestReportGenerator;

public class ReportGeneratorCli(
    IFileReader fileReader,
    IParserFactory parserFactory,
    IReportGenerator reportGenerator
)
{
    public void Run(string[] args)
    {
        if (args.Length == 0)
        {
            Console.WriteLine("❌ Usage: TestReportGenerator <filename>");
            return;
        }

        var path = args[0];
        if (!File.Exists(path))
        {
            Console.WriteLine("❌ File does not exist.");
            return;
        }

        var ext = Path.GetExtension(path);
        var parser = parserFactory.GetParser(ext);

        var results = parser.Parse(path).ToList();

        reportGenerator.Generate(results, ext);
    }
}