namespace DayTwo;

internal class Program
{
    static async Task Main(string[] args)
    {
        var timeToRun = TimeProvider.System.GetTimestamp();
        var reports = await ParseInputListAsync();
        var validReportsCount = reports.Count(IsValidReport);

        Console.WriteLine($"Valid reports: {validReportsCount}");
        Console.WriteLine($"Time to run: {TimeProvider.System.GetElapsedTime(timeToRun).TotalMilliseconds}");
    }

    private static bool IsValidReport(IEnumerable<int> elements) =>
        ValidateReport(elements, ascending: true) || ValidateReport(elements, ascending: false);

    private static bool ValidateReport(IEnumerable<int> elements, bool ascending) =>
        IsValidSequence(elements, ascending) || HasValidPermutation(elements, ascending);

    private static bool HasValidPermutation(IEnumerable<int> elements, bool ascending)
    {
        var validityList = GetElementsValidity(elements, ascending).ToList();
        var faultyIndex = validityList.FindIndex(e => !e);
        var leftPermutation = ExcludeAt(elements, faultyIndex);
        var rightPermutation = ExcludeAt(elements, faultyIndex + 1);

        return IsValidSequence(leftPermutation, ascending) || IsValidSequence(rightPermutation, ascending);
    }

    private static IEnumerable<int> ExcludeAt(IEnumerable<int> elements, int index) =>
        elements.Where((_, i) => i != index);

    private static bool IsValidSequence(IEnumerable<int> sequence, bool ascending) =>
        GetElementsValidity(sequence, ascending).All(x => x);

    private static IEnumerable<bool> GetElementsValidity(IEnumerable<int> elements, bool ascending) =>
        elements.Zip(elements.Skip(1), (f, s) => IsValidPair(f, s, ascending));

    private static bool IsValidPair(int f, int s, bool ascending) =>
        (ascending ? f < s : f > s) && Math.Abs(f - s) >= 1 && Math.Abs(f - s) <= 3;

    private static async Task<IEnumerable<IEnumerable<int>>> ParseInputListAsync()
    {
        var inputLines = await File.ReadAllLinesAsync("input.txt");

        return inputLines.Select(l => l.Split(' ').Select(i => Convert.ToInt32(i)));
    }
}
