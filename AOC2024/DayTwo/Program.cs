namespace DayTwo
{
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

        private static bool IsValidReport(IEnumerable<int> elements) //Output: 383
        {
            var ascendingCheck = elements.Zip(elements.Skip(1), (f, s) => IsValidPair(f, s, ascending: true));
            var descendingCheck = elements.Zip(elements.Skip(1), (f, s) => IsValidPair(f, s, ascending: false));

            return ascendingCheck.All(x => x) || descendingCheck.All(x => x);
        }

        private static bool IsValidPair(int f, int s, bool ascending) =>
            (ascending ? f < s : f > s) && Math.Abs(f - s) >= 1 && Math.Abs(f - s) <= 3;

        private static async Task<IEnumerable<IEnumerable<int>>> ParseInputListAsync()
        {
            var inputLines = await File.ReadAllLinesAsync("input.txt");

            return inputLines.Select(l => l.Split(' ').Select(i => Convert.ToInt32(i)));
        }
    }
}
