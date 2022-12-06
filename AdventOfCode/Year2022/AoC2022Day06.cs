using System.Text.RegularExpressions;
using AdventOfCode.Utilities;
using BenchmarkDotNet.Attributes;

namespace AdventOfCode.Year2022;

public class AoC2022Day06 {
    private readonly string input;

    public AoC2022Day06(string? customInput = null) {
        input = customInput ?? EmbeddedInput.ReadAllText("Year2022/2022_06_input.txt");
    }

    [Benchmark]
    public long Solution1() {
        return Run(4);
    }

    [Benchmark]
    public long Solution2() {
        return Run(14);
    }

    private long Run(int searchLen) {
        var s = new HashSet<char>();
        for (var i = 0; i < input.Length - searchLen; i++) {
            for (var j = i; j < i + searchLen; j++) {
                var c = s.Count;
                s.Add(input[j]);
                // little break out if already know element was added
                if (c == s.Count) {
                    break;
                }
            }

            if (s.Count == searchLen) {
                return i + searchLen;
            }

            s.Clear();
        }

        return 0;
    }
}