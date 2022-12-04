using System.Text.RegularExpressions;
using AdventOfCode.Utilities;
using BenchmarkDotNet.Attributes;

namespace AdventOfCode.Year2022;

public class AoC2022Day04 {
    private struct Range {
        public int From;
        public int To;

        public Range(int from, int to) {
            From = from;
            To = to;
        }

        public bool Contains(Range other) {
            return From <= other.From && To >= other.To;
        }

        public bool Overlaps(Range other) {
            return From <= other.From && To >= other.From;
        }
    }

    private readonly Range[][] input;

    public AoC2022Day04(string? customInput = null) {
        var regex = new Regex(@"(\d*)-(\d*),(\d*)-(\d*)", RegexOptions.Compiled);
        var content = (customInput ?? EmbeddedInput.ReadAllText("Year2022/2022_04_input.txt"));
        input = content.Split("\n")
                       .Select(
                               s => {
                                   var match = regex.Match(s);
                                   var rangeA = new Range(int.Parse(match.Groups[1].Value), int.Parse(match.Groups[2].Value));
                                   var rangeB = new Range(int.Parse(match.Groups[3].Value), int.Parse(match.Groups[4].Value));
                                   return new[] { rangeA, rangeB };
                               })
                       .ToArray();
    }

    [Benchmark]
    public long Solution1() {
        long sum = 0;

        foreach (var ranges in input) {
            if (ranges[0].Contains(ranges[1]) || ranges[1].Contains(ranges[0])) {
                ++sum;
            }
        }

        return sum;
    }

    [Benchmark]
    public long Solution2() {
        long sum = 0;

        foreach (var ranges in input) {
            if (ranges[0].Overlaps(ranges[1]) || ranges[1].Overlaps(ranges[0])) {
                ++sum;
            }
        }

        return sum;
    }
}