using BenchmarkDotNet.Attributes;

namespace AdventOfCode.Year2015;

/// <summary>
/// Basically https://en.wikipedia.org/wiki/Conway%27s_Game_of_Life
/// </summary>
public class AoC2015Day18 {
    private int width;
    private int height;
    private bool[,] field;

    [GlobalSetup(Targets = new[] { nameof(Solution1), nameof(Solution2) })]
    public void BenchmarkSetup() {
        Setup();
    }

    public void Setup(string[]? customInput = null) {
        var lines = customInput ?? File.ReadAllLines("Year2015/2015_18_input.txt");
        height = lines.Length;
        width = lines[0].Length;
        field = new bool[height + 2, width + 2]; // add some space above, right, left and below so we can skip some checks
        for (int y = 0; y < lines.Length; y++) {
            for (int x = 0; x < lines[y].Length; x++) {
                field[y + 1, x + 1] = lines[y][x] != '.';
            }
        }
    }

    [Benchmark]
    public long Solution1(int iterations = 100, bool fixedCornerLights = false) {
        var swap = new bool[field.GetLength(0), field.GetLength(1)];

        if (fixedCornerLights) {
            field[1, 1] = true;
            field[field.GetLength(0) - 2, 1] = true;
            field[1, field.GetLength(1) - 2] = true;
            field[field.GetLength(0) - 2, field.GetLength(1) - 2] = true;
        }

        var result = 0L;
        for (int i = 0; i < iterations; i++) {
            result = 0;
            for (int y = 1; y < height + 1; y++) {
                for (int x = 1; x < width + 1; x++) {
                    if (fixedCornerLights && (y == 1 && x == 1 || y == 1 && x == width || y == height && x == 1 || y == height && x == width)) {
                        swap[y, x] = true;
                        result++;
                    } else {
                        int trueNeighbors = 0;
                        for (int yy = y - 1; yy <= y + 1; yy++) {
                            for (int xx = x - 1; xx <= x + 1; xx++) {
                                if (xx == x && yy == y) {
                                    continue;
                                }

                                if (field[yy, xx]) {
                                    trueNeighbors++;
                                }
                            }
                        }

                        var current = field[y, x];
                        if (current) {
                            if (trueNeighbors == 2 || trueNeighbors == 3) {
                                swap[y, x] = true;
                                ++result;
                            } else {
                                swap[y, x] = false;
                            }
                        } else {
                            if (trueNeighbors == 3) {
                                swap[y, x] = true;
                                ++result;
                            } else {
                                swap[y, x] = false;
                            }
                        }
                    }
                }
            }

            (swap, field) = (field, swap);
        }

        return result;
    }

    [Benchmark]
    public long Solution2(int iterations = 100) {
        return Solution1(iterations, true);
    }
}