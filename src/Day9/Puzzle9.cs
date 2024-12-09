using System.Collections;
using System.Diagnostics;
using System.Text;
using Xunit.Abstractions;

namespace AOC24.Day9;

public class Puzzle9 : IPuzzle<long>
{
    public class Input(IReadOnlyList<int> memory)
    {
        public IReadOnlyList<int> Memory = memory;

        public static Input Parse(string rawInput) => new Input(rawInput.Select(x => x - '0').ToList()); // cheeky ascii char hack
    }

    public int Day { get; } = 9;

    public long SolvePartOne(string rawInput)
    {
        var input = Input.Parse(rawInput);
        long sum = 0;

        var id = 0;
        List<int> diskMap = [];

        for (int i = 0; i < input.Memory.Count; i++)
        {
            if (i % 2 == 0)
            {
                diskMap.AddRange(Enumerable.Repeat(id, input.Memory[i]));
                id++;
            }
            else
            {
                diskMap.AddRange(Enumerable.Repeat(-1, input.Memory[i]));
            }
        }

        List<int> sorted = [];
        var map = new LinkedList<int>(diskMap);

        while (map.Count > 0)
        {
            var back = map.Last();
            map.RemoveLast();

            if (back == -1)
            {
                continue;
            }

            if (map.Count <= 0)
            {
                sorted.Add(back);
                break;
            }

            var front = map.First();
            map.RemoveFirst();

            while (front != -1)
            {
                sorted.Add(front);

                if (map.Count <= 0)
                {
                    break;
                }


                front = map.First();
                map.RemoveFirst();
            }

            sorted.Add(back);
        }

        for (int i = 0; i < sorted.Count; i++)
        {
            sum += sorted[i] > 0 ? i * sorted[i] : 0;
        }

        return sum;
    }

    public long SolvePartTwo(string rawInput)
    {
        var input = Input.Parse(rawInput);
        long sum = 0;

        var id = 0;
        List<int> diskMap = [];

        for (int i = 0; i < input.Memory.Count; i++)
        {
            if (i % 2 == 0)
            {
                diskMap.AddRange(Enumerable.Repeat(id, input.Memory[i]));
                id++;
            }
            else
            {
                diskMap.AddRange(Enumerable.Repeat(-1, input.Memory[i]));
            }
        }

        List<int> sorted = [];
        var map = new LinkedList<int>(diskMap);

        while (map.Count > 0)
        {
            List<int> backBlock = [];
            var back = map.Last();
            
            if (back == -1)
            {
                map.RemoveLast();
                continue;
            }
            

            
            if (map.Count <= 0)
            {
                sorted.Add(back);
                break;
            }

            var front = map.First();
            map.RemoveFirst();

            while (front != -1)
            {
                sorted.Add(front);

                if (map.Count <= 0)
                {
                    break;
                }


                front = map.First();
                map.RemoveFirst();
            }

            sorted.Add(back);
        }

        for (int i = 0; i < sorted.Count; i++)
        {
            sum += sorted[i] > 0 ? i * sorted[i] : 0;
        }

        return sum;
    }
}