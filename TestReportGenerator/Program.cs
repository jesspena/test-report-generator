var builder = Host.CreateApplicationBuilder(args);
var services = builder.Services;

// Register services with proper DI
services.AddSingleton<IFileReader, FileReader>();
services.AddSingleton<IParserFactory, ParserFactory>();
services.AddSingleton<ICsvParser, CsvParser>();
services.AddSingleton<IJsonParser, JsonParser>();
services.AddSingleton<IXmlParser, XmlParser>();
services.AddSingleton<ITestAnalyzer, TestAnalyzer>();
services.AddSingleton<IReportGenerator, ConsoleReportGenerator>();
services.AddSingleton<ReportGeneratorCli>();

using var host = builder.Build();
host.Services.GetRequiredService<ReportGeneratorCli>().Run(args);