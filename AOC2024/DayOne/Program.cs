namespace DayOne;

public class Program
{
    static async Task Main(string[] args)
    {
        var timeToRun = TimeProvider.System.GetTimestamp();
        var (leftList, rightList) = await ParseInputListsAsync();
        int totalSum = SumDifferences(leftList, rightList);
        int similarityScore = CalculateSimilarity(leftList, rightList);

        Console.WriteLine($"Sum: {totalSum}");
        Console.WriteLine($"Similarity Score: {similarityScore}");
        Console.WriteLine($"Time to run: {TimeProvider.System.GetElapsedTime(timeToRun).TotalMilliseconds}");
    }

    private static async Task<(List<int> LeftList, List<int> RightList)> ParseInputListsAsync()
    {
        var inputLines = await File.ReadAllLinesAsync("input.txt");
        var (leftList, rightList) = (new List<int>(inputLines.Length), new List<int>(inputLines.Length));

        for (var index = 0; index < inputLines.Length; index++)
        {
            var numbers = inputLines[index].Split(',');

            leftList.Add(Convert.ToInt32(numbers[0]));
            rightList.Add(Convert.ToInt32(numbers[1]));
        }

        return (leftList, rightList);
    }

    private static int SumDifferences(List<int> leftList, List<int> rightList) =>
        leftList.Order()
            .Zip(rightList.Order(), (f, s) => Math.Abs(f - s))
            .Sum();

    private static int CalculateSimilarity(List<int> leftList, List<int> rightList)
    {
        var hashSet = new HashSet<int>(leftList);

        return rightList.Where(hashSet.Contains).Sum();
    }
}
