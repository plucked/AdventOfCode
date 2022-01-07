using AdventOfCode.Utilities;
using BenchmarkDotNet.Attributes;

namespace AdventOfCode.Year2016;

public class AoC2016Day02 {
    private char[][] input;

    public AoC2016Day02(string[]? customInput = null) {
        input = (customInput ?? EmbeddedInput.ReadAllLines("Year2016/2016_02_input.txt")).Select(l => l.ToArray()).ToArray();
    }

    [Benchmark]
    public long Solution1() {
        var grid = new[,] { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 } };
        var x = 1;
        var y = 1;
        var result = 0;
        for (int line = 0; line < input.GetLength(0); line++) {
            foreach (var c in input[line]) {
                switch (c) {
                    case 'U':
                        y -= 1;
                        break;
                    case 'D':
                        y += 1;
                        break;
                    case 'L':
                        x -= 1;
                        break;
                    case 'R':
                        x += 1;
                        break;
                }

                x = Math.Max(0, Math.Min(2, x));
                y = Math.Max(0, Math.Min(2, y));
            }

            result *= 10;
            result += grid[y, x];
        }

        return result;
    }

    [Benchmark]
    public string Solution2() {
        var grid = @"0010002340567890ABC000D00".ToArray();
        var width = 5;
        var result = "";
        var x = 0;
        var y = 3;
        var index = 0;

        for (int line = 0; line < input.GetLength(0); line++) {
            foreach (var c in input[line]) {
                switch (c) {
                    case 'U':
                        if (TryGetIndex(x, y - 1, out index) && grid[index] != '0') {
                            y -= 1;
                        }

                        break;
                    case 'D':
                        if (TryGetIndex(x, y + 1, out index) && grid[index] != '0') {
                            y += 1;
                        }

                        break;
                    case 'L':
                        if (TryGetIndex(x - 1, y, out index) && grid[index] != '0') {
                            x -= 1;
                        }

                        break;
                    case 'R':
                        if (TryGetIndex(x + 1, y, out index) && grid[index] != '0') {
                            x += 1;
                        }

                        break;
                }
            }

            result += grid[y * width + x];
        }

        return result;

        bool TryGetIndex(int x, int y, out int index) {
            if (x < 0 || x >= width || y < 0 || y >= width) {
                index = 0;
                return false;
            }

            index = y * width + x;
            return true;
        }
    }
}