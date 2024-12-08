using System.Collections;
using System.Diagnostics;
using System.Text;
using Xunit.Abstractions;

namespace AOC24.Day8;

public class Puzzle8 : IPuzzle<long>
{
    public int Day { get; } = 8;
    public bool IsRenderingEnabled { get; set; }

    private readonly ITestOutputHelper _output;

    public Puzzle8(ITestOutputHelper output)
    {
        _output = output;
    }

    public long SolvePartOne(string rawInput)
    {
        var input = CharMapInput.Parse(rawInput);
        var sum = 0;

        var antiNodes = new List<Position>();

        for (int y = 0; y < input.YBoundary; y++)
        {
            for (int x = 0; x < input.XBoundary; x++)
            {
                var ch = input.Map[x, y];

                if (ch == '.' || ch == '#')
                {
                    continue;
                }

                var nodes = GenerateAntiNodes(input, new(x, y), antiNodes);
                sum += nodes;
            }
        }

        RenderMap(input);

        return sum;
    }

    public long SolvePartTwo(string rawInput)
    {
        var input = CharMapInput.Parse(rawInput);
        var sum = 0;

        var antiNodes = new List<Position>();

        for (int y = 0; y < input.YBoundary; y++)
        {
            for (int x = 0; x < input.XBoundary; x++)
            {
                var ch = input.Map[x, y];

                if (ch == '.' || ch == '#')
                {
                    continue;
                }

                var nodes = GenerateExtendedNodes(input, new(x, y), antiNodes);
                sum += nodes;
            }
        }

        RenderMap(input);

        return sum;
    }

    private int GenerateExtendedNodes(CharMapInput input, Position antennaPosition, List<Position> antiNodes)
    {
        var nodes = 0;
        var antenna = input.At(antennaPosition);


        for (int y = 0; y < input.YBoundary; y++)
        {
            for (int x = 0; x < input.XBoundary; x++)
            {
                var scanPosition = new Position(x, y);

                if (antennaPosition == scanPosition)
                {
                    continue;
                }

                var scan = input.At(scanPosition);

                if (scan == antenna)
                {
                    var xDistance = antennaPosition.X - scanPosition.X;
                    var yDistance = antennaPosition.Y - scanPosition.Y;

                    // positive direction
                    int gain = 0;
                    Position antiNode;

                    do
                    {
                        antiNode = new Position(antennaPosition.X + xDistance * gain, antennaPosition.Y + yDistance * gain);

                        if (IsValidAntiNode(input, antiNode, antiNodes))
                        {
                            Debug.WriteLine($"[{antenna}] Anti node found at: {antiNode}");
                            nodes++;
                        }

                        gain++;
                    } while (input.IsInBoundary(antiNode));


                    // negative direction
                    gain = 0;

                    do
                    {
                        antiNode = new Position(antennaPosition.X - xDistance * gain, antennaPosition.Y - yDistance * gain);

                        if (IsValidAntiNode(input, antiNode, antiNodes))
                        {
                            Debug.WriteLine($"[{antenna}] Anti node found at: {antiNode}");
                            nodes++;
                        }

                        gain++;
                    } while (input.IsInBoundary(antiNode));
                }
            }
        }

        return nodes;
    }

    private int GenerateAntiNodes(CharMapInput input, Position antennaPosition, List<Position> antiNodes)
    {
        var nodes = 0;
        var antenna = input.At(antennaPosition);


        for (int y = 0; y < input.YBoundary; y++)
        {
            for (int x = 0; x < input.XBoundary; x++)
            {
                var scanPosition = new Position(x, y);

                if (antennaPosition == scanPosition)
                {
                    continue;
                }

                var scan = input.At(scanPosition);

                if (scan == antenna)
                {
                    // calculate distance
                    var xDistance = antennaPosition.X - scanPosition.X;
                    var yDistance = antennaPosition.Y - scanPosition.Y;

                    // antenna side anti node
                    var antennaAntiNode = new Position(antennaPosition.X + xDistance, antennaPosition.Y + yDistance);

                    if (IsValidAntiNode(input, antennaAntiNode, antiNodes))
                    {
                        Debug.WriteLine($"[{antenna}] Anti node found at: {antennaAntiNode}");
                        nodes++;
                    }

                    // scan side anti node
                    var scanAntiNode = new Position(scanPosition.X - xDistance, scanPosition.Y - yDistance);

                    if (IsValidAntiNode(input, scanAntiNode, antiNodes))
                    {
                        Debug.WriteLine($"[{antenna}] Anti node found at: {scanAntiNode}");
                        nodes++;
                    }
                }
            }
        }

        return nodes;
    }

    private static bool IsValidAntiNode(CharMapInput input, Position antiNodePosition, List<Position> antiNodes)
    {
        if (!input.IsInBoundary(antiNodePosition))
        {
            return false;
        }

        var antiNode = input.At(antiNodePosition);

        if (antiNode == '#' || antiNodes.Contains(antiNodePosition))
        {
            return false;
        }

        antiNodes.Add(antiNodePosition);

        if (antiNode == '.')
        {
            input.Set(antiNodePosition, '#');
        }

        return true;
    }

    private void RenderMap(CharMapInput input)
    {
        if (!IsRenderingEnabled)
        {
            return;
        }

        _output.WriteLine(input.RenderMap());
    }
}