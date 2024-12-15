using AdventOfCode.Utilities;
using BenchmarkDotNet.Attributes;

namespace AdventOfCode.Year2024;

public class AoC2024Day02
{
    // input is negative when line is empty
    private readonly int[][] input;

    public AoC2024Day02(string? customInput = null)
    {
        var content = (customInput ?? EmbeddedInput.ReadAllText("Year2024/2024_02_input.txt"));
        var lines = content.Split('\n', StringSplitOptions.RemoveEmptyEntries);
        input = new int[lines.Length][];
        for (int i = 0; i < lines.Length; i++)
        {
            var line = lines[i];
            input[i] = line.Split(' ').Select(int.Parse).ToArray();
        }
    }

    [Benchmark]
    public long Solution1()
    {
        var safeLevels = 0;
        foreach (var level in input)
        {
            if (IsSafe(level))
            {
                safeLevels++;
            }
        }
        
        return safeLevels;
    }
    
    private bool IsSafe(int[] level)
    {
        var diff = level[0] - level[1];
        if (diff < -3 || diff > 3 || diff == 0)
        {
            return false;
        }
        for (int i = 1; i < level.Length - 1; i++)
        {
            var newDiff = level[i] - level[i + 1];
            if (newDiff < -3 || newDiff > 3 || newDiff == 0 || Math.Sign(newDiff) != Math.Sign(diff))
            {
                return false;
            }
            diff = newDiff;
        }

        return true;
    }

    [Benchmark]
    public long Solution2()
    {
        var safeLevels = 0;
        foreach (var level in input)
        {
            if (IsSafe(level))
            {
                safeLevels++;
            }
            else
            {
                for (int i = 0; i < level.Length; i++)
                {
                    var cpy = new int[level.Length - 1];
                    Array.Copy(level, 0, cpy, 0, i);
                    Array.Copy(level, i + 1, cpy, i, level.Length - i - 1);
                    
                    if (IsSafe(cpy))
                    {
                        safeLevels++;
                        break;
                    }
                }
            }
        }
        
        return safeLevels;
    }
}