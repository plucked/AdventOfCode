using System.Numerics;
using AdventOfCode.Utilities;
using BenchmarkDotNet.Attributes;

namespace AdventOfCode.Year2024;

public class AoC2024Day08
{
    // input is negative when line is empty
    private readonly char[,] map;

    public AoC2024Day08(string? customInput = null)
    {
        var content = (customInput ?? EmbeddedInput.ReadAllText("Year2024/2024_08_input.txt"));
        var lines = content.Split('\n', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        map = new char[lines.Length, lines[0].Length];
        for (int i = 0; i < lines.Length; i++)
        {
            var line = lines[i];
            for (int j = 0; j < line.Length; j++)
            {
                map[i, j] = line[j];
            }
        }
    }

    [Benchmark]
    public long Solution1()
    {
        var antennasPerType = new Dictionary<char, List<(int x, int y)>>();
        for (int x = 0; x < map.GetLength(0); x++)
        {
            for (int y = 0; y < map.GetLength(1); y++)
            {
                var antennaType = map[x, y];
                if (antennaType == '.')
                {
                    continue;
                }

                if (!antennasPerType.TryGetValue(antennaType, out var antennas))
                {
                    antennas = new List<(int x, int y)>();
                    antennasPerType[antennaType] = antennas;
                }

                antennas.Add((x, y));
            }
        }

        var antiNodes = new HashSet<(int x, int y)>();

        foreach (var (_, coords) in antennasPerType)
        {
            foreach (var coordA in coords)
            {
                foreach (var coordB in coords)
                {
                    if (coordA == coordB)
                    {
                        continue;
                    }

                    var diffX = coordA.x - coordB.x;
                    var diffY = coordA.y - coordB.y;
                    var antiNode = (x: coordA.x + diffX, y: coordA.y + diffY);


                    if (antiNode.x < 0 || antiNode.x >= map.GetLength(0) || antiNode.y < 0 ||
                        antiNode.y >= map.GetLength(1))
                    {
                        continue;
                    }

                    antiNodes.Add(antiNode);
                }
            }
        }

        return antiNodes.Count;
    }

    [Benchmark]
    public long Solution2()
    {
        var antennasPerType = new Dictionary<char, List<(int x, int y)>>();
        for (int x = 0; x < map.GetLength(0); x++)
        {
            for (int y = 0; y < map.GetLength(1); y++)
            {
                var antennaType = map[x, y];
                if (antennaType == '.')
                {
                    continue;
                }

                if (!antennasPerType.TryGetValue(antennaType, out var antennas))
                {
                    antennas = new List<(int x, int y)>();
                    antennasPerType[antennaType] = antennas;
                }

                antennas.Add((x, y));
            }
        }

        var antiNodes = new HashSet<(int x, int y)>();
        foreach (var (_, coords) in antennasPerType)
        {
            if (coords.Count == 1)
            {
                continue;
            }
            
            foreach (var coordA in coords)
            {
                antiNodes.Add(coordA);
                foreach (var coordB in coords)
                {
                    if (coordA == coordB)
                    {
                        continue;
                    }

                    var diffX = coordA.x - coordB.x;
                    var diffY = coordA.y - coordB.y;

                    var antiNode = (x: coordA.x + diffX, y: coordA.y + diffY);
                    
                    while (antiNode.x >= 0 && antiNode.x < map.GetLength(0) && antiNode.y >= 0 &&
                           antiNode.y < map.GetLength(1))
                    {
                        antiNodes.Add(antiNode);
                        antiNode = (x: antiNode.x + diffX, y: antiNode.y + diffY);
                    }
                }
            }

        }
       
        return antiNodes.Count;
    }
}