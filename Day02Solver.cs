namespace AdventOfCode.Day02;

public class Day02Tests
{
    private readonly ITestOutputHelper _output;
    public Day02Tests(ITestOutputHelper output) => _output = output;

    [Fact] public void Step1WithExample() => new Day02Solver().ExecuteExample1("2");
        
    [Fact] public void Step2WithExample() => new Day02Solver().ExecuteExample2("4");

    [Fact] public void Step1WithPuzzleInput() => _output.WriteLine(new Day02Solver().ExecutePuzzle1());
        
    [Fact] public void Step2WithPuzzleInput() => _output.WriteLine(new Day02Solver().ExecutePuzzle2());
}

public class Day02Solver : SolverBase
{
    // List<??> _data;

    private List<List<int>> _reports;

    protected override void Parse(List<string> data)
    {
        // _data = new();

        _reports = new List<List<int>>();

        foreach (var line in data)
        {
            var report = line.Split(' ')
                             .Select(int.Parse)
                             .ToList();
            _reports.Add(report);
        }
    }

    protected override object Solve1()
    {
        int safeReportCount = 0;

        foreach (var report in _reports)
        {
            if (IsSafeReport(report))
            {
                safeReportCount++;
            }
        }

        return safeReportCount;
    }

    // Check if the report is safe
    private bool IsSafeReport(List<int> report)
    {
        bool isIncreasing = true;
        bool isDecreasing = true;

        for (int i = 0; i < report.Count - 1; i++)
        {
            int diff = report[i + 1] - report[i];

            // Check the difference constraint: must be between 1 and 3
            if (Math.Abs(diff) < 1 || Math.Abs(diff) > 3)
            {
                return false; // If difference is outside the range [1, 3], it's unsafe
            }

            // Check if the report is strictly increasing or decreasing
            if (diff > 0)
                isDecreasing = false;
            if (diff < 0)
                isIncreasing = false;
        }

        // Return true if the report is either strictly increasing or strictly decreasing
        return isIncreasing || isDecreasing;
    }

    protected override object Solve2()
    {
        int safeReportCount = 0;

        foreach (var report in _reports)
        {
            // Check if the report is safe by removing a single level
            if (IsSafeReport(report) || CanBeMadeSafeByRemovingOneLevel(report))
            {
                safeReportCount++;
            }
        }

        return safeReportCount;
    }

    // Check if the report can be made safe by removing one level
    private bool CanBeMadeSafeByRemovingOneLevel(List<int> report)
    {
        // Try removing each level one at a time and check if the resulting report is safe
        for (int i = 0; i < report.Count; i++)
        {
            var modifiedReport = report.Where((_, index) => index != i).ToList();

            if (IsSafeReport(modifiedReport))
            {
                return true; // If any modification makes it safe, return true
            }
        }

        return false; // If no modification makes it safe, return false
    }
}
