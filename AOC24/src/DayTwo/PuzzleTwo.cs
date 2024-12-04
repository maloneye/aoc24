namespace AOC24.DayTwo;

public class PuzzleTwo : IPuzzle<int>
{
    public class Input
    {
        public IReadOnlyList<IReadOnlyList<int>> Reports { get; }

        public Input(IReadOnlyList<IReadOnlyList<int>> reports)
        {
            Reports = reports;
        }

        public static Input Parse(StreamReader reader)
        {
            List<List<int>> reports = [];
            
            while (!reader.EndOfStream)
            {
                List<int> report = [];
                var line = reader.ReadLine().AsSpan();
                
                int start = 0;
                while (start < line.Length)
                {
                    int splitIndex = line[start..].IndexOf(' ');
                    if (splitIndex == -1)
                    {
                        report.Add(int.Parse(line[start..]));
                        break;
                    }

                    report.Add(int.Parse(line.Slice(start, splitIndex)));
                    start += splitIndex + 1;
                }

                reports.Add(report);
            }

            return new Input(reports);
        }
    }

    private readonly Input _input;

    public PuzzleTwo(Input input)
    {
        _input = input;
    }

    public int SolvePartOne()
    {
        int safeReports = 0;

        foreach (var report in _input.Reports)
        {
            if (IsReportSafe(report))
            {
                safeReports++;
            }
        }

        return safeReports;
    }

    public int SolvePartTwo()
    {
        int safeReports = 0;

        foreach (var report in _input.Reports)
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