using System.Text;
using BenchmarkDotNet.Attributes;

namespace AdventOfCode.Year2015;

public class AoC2015Day11 {
    private byte[] input;

    [GlobalSetup(Targets = new[] { nameof(Solution1), nameof(Solution2) })]
    public void BenchmarkSetup() {
        Setup();
    }

    public void Setup(string? customInput = null) {
        input = Encoding.UTF8.GetBytes(customInput ?? "hxbxwxba");
    }

    [Benchmark]
    public string Solution1() {
        // filter out bad characters 
        for (int i = 0; i < input.Length; i++) {
            var c = input[i];
            if (c == 105 || c == 111 || c == 108) {
                input[i] = (byte)(c + 1);
                for (int j = i + 1; j < input.Length; j++) {
                    input[j] = 97;
                }
            }
        }

        while (true) {
            for (int i = input.Length - 1; i >= 0; i--) {
                var c = input[i];
                // check if incrementing will be past 'z'
                if (c + 1 <= 122) {
                    // skip over forbidden characters
                    if (c == 104 || c == 110 || c == 109) {
                        input[i] = (byte)(c + 2);
                    } else {
                        input[i] = (byte)(c + 1);
                    }

                    break;
                }

                // go back to a
                input[i] = 97;
            }

            var validation = Validate();
            if (validation != null) {
                return validation;
            }
        }
    }

    [Benchmark]
    public string Solution2() {
        Solution1();
        return Solution1();
    }

    public string? Validate() {
        bool hasStreight = false;
        bool hasDoublePair = false;
        byte? lastPair = null;
        for (int i = 0; i < input.Length; i++) {
            var a = input[i];
            byte? b = i < input.Length - 1 ? input[i + 1] : null;
            byte? c = i < input.Length - 2 ? input[i + 2] : null;

            if (!hasStreight && c != null && c - 1 == b && b - 1 == a) {
                hasStreight = true;
            }

            if (lastPair == null && b != null && a == b) {
                lastPair = a;
            }

            if (lastPair != null && lastPair != a && b != null && a == b) {
                hasDoublePair = true;
            }

            if (hasStreight && hasDoublePair) {
                return Encoding.UTF8.GetString(input);
            }
        }

        return null;
    }
}