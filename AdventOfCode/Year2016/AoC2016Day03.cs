using AdventOfCode.Utilities;
using BenchmarkDotNet.Attributes;

namespace AdventOfCode.Year2016;

public class AoC2016Day03 {
    private int[] columnA;
    private int[] columnB;
    private int[] columnC;

    public AoC2016Day03(string[]? customInput = null) {
        var lines = (customInput ?? EmbeddedInput.ReadAllLines("Year2016/2016_03_input.txt"));
        columnA = new int[lines.Length];
        columnB = new int[lines.Length];
        columnC = new int[lines.Length];

        for (var i = 0; i < lines.Length; i++) {
            var split = lines[i].Split(" ", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            columnA[i] = int.Parse(split[0]);
            columnB[i] = int.Parse(split[1]);
            columnC[i] = int.Parse(split[2]);
        }
    }

    [Benchmark]
    public long Solution1() {
        return columnA.Where((t, i) => t + columnB[i] > columnC[i] && t + columnC[i] > columnB[i] && columnB[i] + columnC[i] > t).Count();
    }

    [Benchmark]
    public unsafe long Solution2() {
        long result = 0;

        for (var i = 0; i < columnA.Length; i += 3) {
            if (columnA[i] + columnA[i + 1] > columnA[i + 2] && columnA[i] + columnA[i + 2] > columnA[i + 1] && columnA[i + 1] + columnA[i + 2] > columnA[i + 0]) {
                result++;
            }

            if (columnB[i] + columnB[i + 1] > columnB[i + 2] && columnB[i] + columnB[i + 2] > columnB[i + 1] && columnB[i + 1] + columnB[i + 2] > columnB[i + 0]) {
                result++;
            }

            if (columnC[i] + columnC[i + 1] > columnC[i + 2] && columnC[i] + columnC[i + 2] > columnC[i + 1] && columnC[i + 1] + columnC[i + 2] > columnC[i + 0]) {
                result++;
            }
        }

        return result;
    }
}