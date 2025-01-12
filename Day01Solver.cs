namespace AdventOfCode.Day01;

public class Day01Tests
{
    private readonly ITestOutputHelper _output;
    public Day01Tests(ITestOutputHelper output) => _output = output;

    [Fact] public void Step1WithExample() => new Day01Solver().ExecuteExample1("11");
        
    [Fact] public void Step2WithExample() => new Day01Solver().ExecuteExample2("31");

    [Fact] public void Step1WithPuzzleInput() => _output.WriteLine(new Day01Solver().ExecutePuzzle1());
        
    [Fact] public void Step2WithPuzzleInput() => _output.WriteLine(new Day01Solver().ExecutePuzzle2());
}

public class Day01Solver : SolverBase
{
    private List<int> _leftList;
    private List<int> _rightList;

    // Parse the input into two separate lists of integers
    protected override void Parse(List<string> data)
    {
        _leftList = new List<int>();
    
        _rightList = new List<int>();

        foreach (var line in data)
        {
            var trimmedLine = line.Trim();
            if (string.IsNullOrEmpty(trimmedLine)) continue;

            var numbers = trimmedLine.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            if (numbers.Length == 2)
            {
                try
                {
                    _leftList.Add(int.Parse(numbers[0]));
                    _rightList.Add(int.Parse(numbers[1]));
                }
                catch (FormatException)
                {
                    Console.WriteLine($"Error parsing line: {line}");
                    continue;
                }
            }
            else
            {
                Console.WriteLine($"Skipping invalid line: {line}");
            }
        }
    }


    // Solve for Step 1: Calculate the total distance between the two lists
    protected override object Solve1()
    {
        // Sort both lists
        _leftList.Sort();
        _rightList.Sort();

        
        // Calculate the total distance by summing the absolute differences
        int totalDistance = 0;
        for (int i = 0; i < _leftList.Count; i++)
        {
            int diff = Math.Abs(_leftList[i] - _rightList[i]);
            // Debug: Print the distance for each pair
            Console.WriteLine($"Difference between {_leftList[i]} and {_rightList[i]}: {diff}");
            totalDistance += diff;
        }

        // Final total distance (debugging the final result)
        Console.WriteLine($"\nTotal Distance: {totalDistance}");

        return totalDistance;
    }

    protected override object Solve2()
    {
        int totalSimilarityScore = 0;

        // For each number in the left list, find how many times it appears in the right list
        foreach (var leftNum in _leftList)
        {
            int countInRightList = _rightList.Count(x => x == leftNum); // Count occurrences of leftNum in right list
            totalSimilarityScore += leftNum * countInRightList; // Multiply by the count and add to the similarity score
        }

        return totalSimilarityScore;
    }
}
