using AdventOfCode.Utilities;
using BenchmarkDotNet.Attributes;

namespace AdventOfCode.Year2022;

public class AoC2022Day01 {
    // input is negative when line is empty
    private readonly int[] input;

    public AoC2022Day01(string? customInput = null) {
        var content = (customInput ?? EmbeddedInput.ReadAllText("Year2022/2022_01_input.txt"));
        input = content.Split("\n").Select(s => int.TryParse(s, out var r) ? r : -1).ToArray();
    }

    [Benchmark]
    public long Solution1() {
        var max = 0;
        var current = 0;
        foreach (var cal in input) {
            if (cal == -1) {
                max = current > max ? current : max;
                current = 0;
            } else {
                current += cal;
            }
        }

        max = current > max ? current : max;
        return max;
    }

    [Benchmark]
    public long Solution2() {
        List<int> bags = new(100);
        var current = 0;
        foreach (var cal in input) {
            if (cal == -1) {
                bags.Add(current);
                current = 0;
            } else {
                current += cal;
            }
        }

        bags.Add(current);

        return bags.OrderByDescending(i => i).Take(3).Sum();
    }
}