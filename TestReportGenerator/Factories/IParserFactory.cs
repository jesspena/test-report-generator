namespace TestReportGenerator.Factories;

using TestReportGenerator.Parsers;

public interface IParserFactory
{
    ITestResultParser CreateParser(string extension);
}
