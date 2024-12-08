using System.Data;
using System.Text;
using AOC24.Day2;

namespace AOC24.Day6;

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

        return input.Updates.Where(update => IsUpdateCompliant(update, input.Rules))
                    .Sum(update => update[update.Count / 2]);
    }

    public int SolvePartTwo(string rawInput)
    {
        var input = Input.Parse(rawInput);
        var valid = new List<List<int>>();

        return input.Updates.Where(update => !IsUpdateCompliant(update, input.Rules))
                    .Select(update => ReorderToComply(update, input.Rules))
                    .Sum(update => update[update.Count / 2]);
    }

    private static bool IsUpdateCompliant(IEnumerable<int> update, IDictionary<int, List<int>> rules)
    {
        List<int> seen = [];

        foreach (var element in update)
        {
            seen.Add(element);

            if (rules.TryGetValue(element, out var ruleSet))
            {
                if (!IsCompliantWithRules(ruleSet, seen))
                {
                    return false;
                }
            }
        }

        return true;
    }

    private static bool IsCompliantWithRules(IEnumerable<int> ruleSet, IEnumerable<int> seen)
    {
        foreach (var rule in ruleSet)
        {
            if (seen.Contains(rule))
            {
                return false;
            }
        }

        return true;
    }

    private static IReadOnlyList<int> ReorderToComply(IEnumerable<int> update, IDictionary<int, List<int>> rules)
    {
        LinkedList<int> seen = [];

        foreach (var element in update)
        {
            seen.AddLast(element);

            if (rules.TryGetValue(element, out var ruleSet))
            {
                if (!IsCompliantWithRules(ruleSet, seen))
                {
                    // Find index of first rule for this ruleSet in seen, then place the element there. curry this until we are compliant

                    LinkedListNode<int> node = new(-1);

                    for (int i = 0; i < seen.Count; i++)
                    {
                        node = seen.Find(seen.ElementAt(i)) ?? throw new NullReferenceException();

                        if (ruleSet.Contains(node.Value))
                        {
                            break;
                        }
                    }

                    seen.RemoveLast();
                    seen.AddBefore(node, element);

                    ReorderToComply(seen, rules);
                }
            }
        }

        return seen.ToList();
    }
}