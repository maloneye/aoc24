namespace AOC24.DayTwo;

public class PuzzleTwo : IPuzzle<int>
{
    
    public class Input(IReadOnlyList<IReadOnlyList<int>> reports)
    {
        public IReadOnlyList<IReadOnlyList<int>> Reports { get; } = reports;

        public static Input Parse(string rawData)
        {
            var lines = rawData.Split("\n");
            List<List<int>> reports = [];

            foreach (var line in lines)
            {
                List<int> report = [];


                int start = 0;

                while (start < line.Length)
                {
                    int splitIndex = line[start..].IndexOf(' ');

                    if (splitIndex == -1)
                    {
                        report.Add(int.Parse(line[start..]));
                        break;
                    }

                    report.Add(int.Parse(line.Substring(start, splitIndex)));
                    start += splitIndex + 1;
                }

                reports.Add(report);
            }

            return new Input(reports);
        }
    }
    
    public int Day { get; } = 2;
    
    public int SolvePartOne(string rawInput)
    {
        var input =  Input.Parse(rawInput);
        int safeReports = 0;

        foreach (var report in input.Reports)
        {
            if (IsReportSafe(report))
            {
                safeReports++;
            }
        }

        return safeReports;
    }

    public int SolvePartTwo(string rawInput)
    {
        var input =  Input.Parse(rawInput);
        int safeReports = 0;

        foreach (var report in input.Reports)
        {
            if (IsReportSafe(report))
            {
                safeReports++;
            }
            else
            {
                for (int i = 0; i < report.Count; i++)
                {
                    var dampedReport = report.ToList();
                    dampedReport.RemoveAt(i);

                    if (IsReportSafe(dampedReport))
                    {
                        safeReports++;
                        break;
                    }
                }
            }
        }

        return safeReports;
    }

    private static bool IsReportSafe(IReadOnlyList<int> report)
    {
        // check first 2 numbers this sets direction
        var initalDiff = report[0] - report[1];

        if (IsUnsafeDiff(initalDiff))
        {
            return false;
        }

        for (var i = 1; i < report.Count - 1; i++)
        {
            var diff = report[i] - report[i + 1];

            if (diff * initalDiff < 0 || IsUnsafeDiff(diff))
            {
                return false;
            }
        }

        return true;
    }

    private static bool IsUnsafeDiff(int diff) => Math.Abs(diff) is < 1 or > 3;
}