namespace TestReportGenerator.Factories;

using TestReportGenerator.Constants;
using TestReportGenerator.Parsers;

public class ParserFactory : IParserFactory
{
    public ITestResultParser CreateParser(string fileExtension)
    {
        return fileExtension.ToLower() switch
        {
            Constants.CsvExtension => new CsvParser(),
            Constants.JsonExtension => new JsonParser(),
            Constants.XmlExtension => new XmlParser(),
            _ => throw new NotSupportedException($"Unsupported file extension: {fileExtension}")
        };
    }
}