namespace TestReportGenerator.Services;

public class FileReader : IFileReader
{
    public string ReadAllText(string path) => File.ReadAllText(path);
    public string[] ReadAllLines(string path) => File.ReadAllLines(path);
}