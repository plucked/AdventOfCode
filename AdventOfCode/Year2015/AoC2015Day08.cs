using AdventOfCode.Utilities;
using BenchmarkDotNet.Attributes;

namespace AdventOfCode.Year2015;

public class AoC2015Day08 {
    private string[] input;

    public AoC2015Day08(string[]? customInput = null) {
        input = customInput ?? EmbeddedInput.ReadAllLines("Year2015/2015_08_input.txt");
    }

    [Benchmark]
    public long Solution1() {
        var total = 0L;
        foreach (var line in input) {
            var length = line.Length;
            var remove = 0;
            for (int i = 0; i < line.Length; i++) {
                var c = line[i];
                char? next = i + 1 < line.Length ? line[i + 1] : null;
                if (c == '"') {
                    remove++;
                } else if (c == '\\' && next != null) {
                    switch (next) {
                        case '"':
                        case '\\':
                            ++i;
                            remove++;
                            break;
                        case 'x':
                            i += 3;
                            remove += 3;
                            break;
                    }
                }
            }

            total += length - (length - remove);
        }

        return total;
    }

    [Benchmark]
    public long Solution2() {
        var total = 0L;
        foreach (var line in input) {
            var add = 0L;
            for (int i = 0; i < line.Length; i++) {
                var c = line[i];
                if (c == '"') {
                    add += i == 0 || i == line.Length - 1 ? 2 : 1;
                } else if (c == '\\') {
                    add += 1;
                }
            }

            total += add;
        }

        return total;
    }
}