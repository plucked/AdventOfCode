using AdventOfCode.Utilities;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Helpers;

namespace AdventOfCode.Year2022;

public class AoC2022Day03 {
    private readonly string[] input;

    public AoC2022Day03(string? customInput = null) {
        var content = (customInput ?? EmbeddedInput.ReadAllText("Year2022/2022_03_input.txt"));
        input = content.Split("\n");
    }

    [Benchmark]
    public long Solution1() {
        long sum = 0;
        var tempSet = new HashSet<char>();
        foreach (var s in input) {
            tempSet.Clear();
            for (int i = 0; i < s.Length; i++) {
                var c = s[i];
                if (i < s.Length / 2) {
                    tempSet.Add(c);
                } else {
                    if (tempSet.Contains(c)) {
                        sum += c >= 'a' ? c - 'a' + 1 : c - 'A' + 26 + 1;
                        tempSet.Remove(c);
                    }
                }
            }
        }

        return sum;
    }

    [Benchmark]
    public long Solution2() {
        long sum = 0;

        for (int i = 0; i < input.Length; i += 3) {
            var h = new[] { new HashSet<char>(input[i].ToCharArray()), new HashSet<char>(input[i + 1].ToCharArray()), new HashSet<char>(input[i + 2].ToCharArray()) };
            foreach (var c in h[0]) {
                var matches = true;
                for (int j = 1; j < h.Length; j++) {
                    if (h[j].Contains(c) == false) {
                        matches = false;
                        break;
                    }
                }

                if (matches) {
                    sum += c >= 'a' ? c - 'a' + 1 : c - 'A' + 26 + 1;
                    break;
                }
            }
        }

        return sum;
    }
}