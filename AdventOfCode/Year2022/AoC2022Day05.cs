using System.Text.RegularExpressions;
using AdventOfCode.Utilities;
using BenchmarkDotNet.Attributes;

namespace AdventOfCode.Year2022;

public class AoC2022Day05 {
    private readonly string[] lines;

    public AoC2022Day05(string? customInput = null) {
        lines = (customInput ?? EmbeddedInput.ReadAllText("Year2022/2022_05_input.txt")).Split(Environment.NewLine);
    }

    [Benchmark]
    public string Solution1() {
        return Run(true);
    }

    [Benchmark]
    public string Solution2() {
        return Run(false);
    }

    string Run(bool reverse) {
        var stacks = new List<char>[(lines[0].Length + 1) / 4];
        for (var i = 0; i < stacks.Length; i++) {
            stacks[i] = new();
        }

        var lineIndex = 0;
        for (; lineIndex < lines.Length; lineIndex++) {
            var line = lines[lineIndex];
            var pos = 0;
            var index = line.IndexOf('[', pos);
            if (index < 0) {
                break;
            }

            while (index >= 0) {
                var arrayIndex = index / 4;
                var c = line.Substring(index + 1)[0];
                stacks[arrayIndex].Insert(0, c);

                pos = index + 1;
                index = line.IndexOf('[', pos);
            }
        }

        lineIndex += 2;
        var regex = new Regex(@"move (\d*) from (\d*) to (\d*)", RegexOptions.Compiled);
        for (; lineIndex < lines.Length; lineIndex++) {
            var line = lines[lineIndex];
            var match = regex.Match(line);

            var amount = int.Parse(match.Groups[1].Value);
            var from = int.Parse(match.Groups[2].Value) - 1;
            var to = int.Parse(match.Groups[3].Value) - 1;

            if (reverse) {
                stacks[to].AddRange(stacks[from].TakeLast(amount).Reverse());
            } else {
                stacks[to].AddRange(stacks[from].TakeLast(amount));
            }

            stacks[from].RemoveRange(stacks[from].Count - amount, amount);
        }

        return string.Join("", stacks.Select(s => s.Last()));
    }
}