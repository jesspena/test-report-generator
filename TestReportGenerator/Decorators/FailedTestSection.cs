using TestReportGenerator.Models;

namespace TestReportGenerator.Decorators;

public class FailedTestsSection : BaseReportSection
{
    private readonly List<TestResult> _results;

    public FailedTestsSection(List<TestResult> results, IReportSection next) : base(next)
    {
        _results = results;
    }

    public override string Render()
    {
        ///test
    }
}
