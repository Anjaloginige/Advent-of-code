namespace AdventOfCode.Day03;

public class Day03Tests
{
    private readonly ITestOutputHelper _output;
    public Day03Tests(ITestOutputHelper output) => _output = output;

    [Fact] public void Step1WithExample() => new Day03Solver().ExecuteExample1("161");

    [Fact] public void Step2WithExample() => new Day03Solver().ExecuteExample2("48");

    [Fact] public void Step1WithPuzzleInput() => _output.WriteLine(new Day03Solver().ExecutePuzzle1());
        
    [Fact] public void Step2WithPuzzleInput() => _output.WriteLine(new Day03Solver().ExecutePuzzle2());
}

public partial class Day03Solver : SolverBase
{
    private bool isMulEnabled = true; // Initially, multiplication is enabled
    private int totalSum = 0;


    protected override void Parse(List<string> data)
    {
        foreach (string line in data)
        {
            // Scan each line and process instructions
            ProcessLine(line);
        }
    }

    // Process each line of input
    private void ProcessLine(string line)
    {
        int i = 0;
        while (i < line.Length)
        {
            if (line.Substring(i).StartsWith("mul("))
            {
                // Process a mul instruction
                i = ProcessMul(line, i);
            }
            else if (line.Substring(i).StartsWith("do()"))
            {
                // Process do() instruction
                isMulEnabled = true;
                i += 4; // Skip past "do()"
            }
            else if (line.Substring(i).StartsWith("don't()"))
            {
                // Process don't() instruction
                isMulEnabled = false;
                i += 7; // Skip past "don't()"
            }
            else
            {
                // Skip invalid characters or instructions
                i++;
            }
        }
    }

    // Process the mul(X, Y) instruction
    private int ProcessMul(string line, int i)
    {
        int start = i + 4; // Skip the "mul("
        int end = line.IndexOf(')', start);

        if (end == -1) return line.Length;  // No valid closing parenthesis

        string mulContent = line.Substring(start, end - start);
        string[] parts = mulContent.Split(',');

        if (parts.Length == 2 && int.TryParse(parts[0], out int X) && int.TryParse(parts[1], out int Y))
        {
            if (isMulEnabled)
            {
                int product = X * Y;
                totalSum += product;
            }
        }

        return end + 1; // Move past the closing parenthesis of mul(X, Y)
    }

    // Implementing Solve1 for the first part (sum of all valid multiplications)
    protected override object Solve1()
    {
        return totalSum.ToString();  // Return the total sum
    }


    // Implementing Solve2 to handle the logic for part 2
    protected override object Solve2()
    {
        // Reset total sum and isMulEnabled for Part 2
        totalSum = 0;
        isMulEnabled = true;

        // Load and process input data for Part 2
        var inputData = Load("input.txt");
        Parse(inputData);

        return totalSum.ToString();
    }
}