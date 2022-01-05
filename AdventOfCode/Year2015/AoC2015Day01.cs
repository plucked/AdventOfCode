using System.Reflection;
using AdventOfCode.Utilities;
using BenchmarkDotNet.Attributes;

namespace AdventOfCode.Year2015;

public class AoC2015Day01 {
    private readonly byte[] input;

    public AoC2015Day01(string? customInput = null) {
        input = (customInput ?? EmbeddedInput.ReadAllText("Year2015/2015_01_input.txt")).Select(Convert.ToByte).ToArray();
    }

    [Benchmark]
    public long Solution1() {
        return input.Count(c => c == '(') - input.Count(c => c == ')');
    }

    [Benchmark]
    public long Solution2() {
        var result = 0L;
        for (var i = 0; i < input.Length; i++) {
            var c = input[i];
            result += c == '(' ? 1 : -1;
            if (result == -1) {
                return i + 1;
            }
        }

        throw new Exception("Unexpected solution ending");
    }
}