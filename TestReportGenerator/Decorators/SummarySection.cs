using TestReportGenerator.Models;

namespace TestReportGenerator.Decorators;

public class SummarySection : BaseReportSection
{
    private readonly List<TestResult> _results;

    public SummarySection(List<TestResult> results, IReportSection next) : base(next)
    {
        _results = results;
    }

    public override string Render()
    {
        var total = _results.Count();
        var passed = _results.Count(r => r.Passed);
        var failed = total - passed;
        var summary = $"Summary: Total={total}, Passed={passed}, Failed={failed}\n";
        return summary + base.Render();
    }
}
