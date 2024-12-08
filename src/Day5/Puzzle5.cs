using System.Text;
using AOC24.Day2;

namespace AOC24.Day5;

public class Puzzle5 : IPuzzle<int>
{
    public class Input(IDictionary<int, List<int>> rules, IReadOnlyList<IReadOnlyList<int>> updates)
    {
        public IDictionary<int, List<int>> Rules { get; } = rules;
        public IReadOnlyList<IReadOnlyList<int>> Updates { get; } = updates;

        public static Input Parse(string rawInput)
        {
            var lines = rawInput.Split("\n");
            Dictionary<int, List<int>> rules = [];
            List<List<int>> updates = [];

            int i = 0;

            while (lines[i].Any())
            {
                var parts = lines[i].Split('|');
                var first = int.Parse(parts[0]);
                var second = int.Parse(parts[1]);

                if (rules.TryGetValue(first, out var existing))
                {
                    existing.Add(second);
                }
                else
                {
                    rules.Add(first, [second]);
                }

                i++;
            }

            i++;

            while (i < lines.Length)
            {
                var update = lines[i].Split(',')
                                     .Select(x => int.Parse(x ?? throw new NullReferenceException()))
                                     .ToList();

                updates.Add(update);
                i++;
            }

            return new Input(rules, updates);
        }
    }

    public int Day { get; } = 5;

    public int SolvePartOne(string rawInput)
    {
        var input = Input.Parse(rawInput);
        var valid = new List<List<int>>();

        return input.Updates.Where(update => IsUpdateCompliant(update, input))
                            .Sum(update => update[update.Count / 2]);
    }

    private static bool IsUpdateCompliant(IReadOnlyList<int> update, Input input)
    {
        List<int> seen = [];

        foreach (var element in update)
        {
            seen.Add(element);
                
            if (input.Rules.TryGetValue(element, out var rules))
            {
                if (!IsCompliantWithRules(rules, seen))
                {
                    return false;
                }
            }
        }

        return true;
    }

    private static bool IsCompliantWithRules(List<int> rules, List<int> seen)
    {
        foreach (var rule in rules)
        {
            if (seen.Contains(rule))
            {
                return false;
            }
        }

        return true;
    }

    public int SolvePartTwo(string rawInput)
    {
        throw new NotImplementedException();
    }
}