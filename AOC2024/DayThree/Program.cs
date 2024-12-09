using System.Text.RegularExpressions;

namespace DayThree;

internal class Program
{
    static async Task Main(string[] args)
    {
        var timeToRun = TimeProvider.System.GetTimestamp();
        var matchesList = await ParseInputListAsync();
        var sum = matchesList.SelectMany(SelectProducts).Sum();

        Console.WriteLine($"Sum: {sum}");
        Console.WriteLine($"Time to run: {TimeProvider.System.GetElapsedTime(timeToRun).TotalMilliseconds}");
    }

    private static IEnumerable<int> SelectProducts(MatchCollection matches) =>
        matches.Select(m => Convert.ToInt32(m.Groups[1].Value) * Convert.ToInt32(m.Groups[2].Value));

    private static async Task<IEnumerable<MatchCollection>> ParseInputListAsync()
    {
        var inputLines = await File.ReadAllLinesAsync("input.txt");

        return inputLines.Select(l => Regex.Matches(l, @"mul\((\d{1,3}),(\d{1,3})\)", RegexOptions.Compiled, TimeSpan.FromMilliseconds(1000)));
    }
}
