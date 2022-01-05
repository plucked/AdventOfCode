using AdventOfCode.Utilities;
using BenchmarkDotNet.Attributes;

namespace AdventOfCode.Year2015;

public class AoC2015Day02 {
    private Box[] input;

    private struct Box {
        public long Length;
        public long Width;
        public long Height;

        public Box(long length, long width, long height) {
            Length = length;
            Width = width;
            Height = height;
        }
    }

    public AoC2015Day02(string[]? customInput = null) {
        var lines = customInput ?? EmbeddedInput.ReadAllLines("Year2015/2015_02_input.txt");
        input = lines.Select(
                             line => {
                                 var split = line.Split('x');
                                 return new Box(long.Parse(split[0]), long.Parse(split[1]), long.Parse(split[2]));
                             })
                     .ToArray();
    }

    [Benchmark]
    public long Solution1() {
        var total = 0L;
        foreach (var t in input) {
            var side1 = 2 * t.Length * t.Width;
            var side2 = 2 * t.Width * t.Height;
            var side3 = 2 * t.Height * t.Length;
            var area = side1 + side2 + side3;
            var extra = Math.Min(side1, Math.Min(side2, side3));
            area += extra / 2;
            total += area;
        }

        return total;
    }

    [Benchmark]
    public long Solution2() {
        var total = 0L;
        var sides = new long[3];
        foreach (var t in input) {
            sides[0] = t.Length;
            sides[1] = t.Width;
            sides[2] = t.Height;
            Array.Sort(sides);
            total += sides[0] * 2 + sides[1] * 2;
            total += sides[0] * sides[1] * sides[2];
        }

        return total;
    }
}