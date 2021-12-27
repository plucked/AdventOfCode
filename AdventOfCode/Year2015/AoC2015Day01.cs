using BenchmarkDotNet.Attributes;

namespace AdventOfCode.Year2015;

public class AoC2015Day01 {
    private byte[] input;

    [GlobalSetup(Targets = new[] { nameof(Solution1), nameof(Solution2) })]
    public void BenchmarkSetup() {
        Setup();
    }

    public void Setup(string? customInput = null) {
        input = (customInput ?? File.ReadAllText("Year2015/2015_01_input.txt")).Select(c => Convert.ToByte(c)).ToArray();
    }

    [Benchmark]
    public long Solution1() {
        var result = 0L;
        foreach (var c in input) {
            result += c == '(' ? 1 : -1;
        }

        return result;
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