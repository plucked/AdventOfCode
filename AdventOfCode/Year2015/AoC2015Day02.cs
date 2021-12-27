using BenchmarkDotNet.Attributes;

namespace AdventOfCode.Year2015;

public unsafe class AoC2015Day02 {
    private (long l, long w, long h)[] input = null!;

    [GlobalSetup(Targets = new[] { nameof(Solution1), nameof(Solution2) })]
    public void BenchmarkSetup() {
        Setup();
    }

    public void Setup(string[]? customInput = null) {
        var lines = (customInput ?? File.ReadAllLines("Year2015/2015_02_input.txt"));
        input = lines.Select(
                             line => {
                                 var split = line.Split('x');
                                 return (long.Parse(split[0]), long.Parse(split[1]), long.Parse(split[2]));
                             })
                     .ToArray();
    }

    [Benchmark]
    public long Solution1() {
        var total = 0L;
        foreach (var t in input) {
            var side1 = 2 * t.l * t.w;
            var side2 = 2 * t.w * t.h;
            var side3 = 2 * t.h * t.l;
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
            sides[0] = t.l;
            sides[1] = t.w;
            sides[2] = t.h;
            Array.Sort(sides);
            total += sides[0] * 2 + sides[1] * 2;
            total += sides[0] * sides[1] * sides[2];
        }

        return total;
    }
}