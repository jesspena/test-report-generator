namespace TestReportGenerator.Services;

public interface IFileReader
{
    string ReadAllText(string path);
    string[] ReadAllLines(string path);
}