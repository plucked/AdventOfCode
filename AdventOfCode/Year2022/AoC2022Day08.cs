using AdventOfCode.Utilities;
using BenchmarkDotNet.Attributes;

namespace AdventOfCode.Year2022;

public class AoC2022Day08 {
    private int[][] trees;

    public AoC2022Day08(string? customInput = null) {
        var input = customInput ?? EmbeddedInput.ReadAllText("Year2022/2022_08_input.txt");
        var lines = input.Split(Environment.NewLine);
        trees = lines.Select(l => l.Select(c => c - '0').ToArray()).ToArray();
    }

    [Benchmark]
    public long Solution1() {
        bool[][] marked = new bool[trees.Length][];
        for (int i = 0; i < trees.Length; i++) {
            marked[i] = new bool[trees[i].Length];
        }

        int width = trees[0].Length;
        int height = trees.Length;

        for (var x = 0; x < width; ++x) {
            int highest = -1;
            for (var y = 0; y < height; ++y) {
                var tree = trees[y][x];

                if (highest == -1 || tree > highest) {
                    highest = trees[y][x];
                    marked[y][x] = true;
                }
            }

            highest = -1;
            for (var y = height - 1; y >= 0; --y) {
                var tree = trees[y][x];

                if (highest == -1 || tree > highest) {
                    highest = trees[y][x];
                    marked[y][x] = true;
                }
            }
        }

        for (var y = 0; y < height; ++y) {
            int highest = -1;
            for (var x = 0; x < width; ++x) {
                var tree = trees[y][x];

                if (highest == -1 || tree > highest) {
                    highest = trees[y][x];
                    marked[y][x] = true;
                }
            }

            highest = -1;
            for (var x = width - 1; x >= 0; --x) {
                var tree = trees[y][x];

                if (highest == -1 || tree > highest) {
                    highest = trees[y][x];
                    marked[y][x] = true;
                }
            }
        }

        return marked.SelectMany(b => b).Count(b => b);
    }

    [Benchmark]
    public long Solution2() {
        long[][] scores = new long[trees.Length][];
        for (int i = 0; i < trees.Length; i++) {
            scores[i] = new long[trees[i].Length];
        }

        int width = trees[0].Length;
        int height = trees.Length;

        for (var x = 0; x < width; ++x) {
            for (var y = 0; y < height; ++y) {
                var tree = trees[y][x];

                var scoreUp = 0;
                for (int yy = y - 1; yy >= 0; yy--) {
                    var treeUp = trees[yy][x];
                    scoreUp++;

                    if (treeUp >= tree) {
                        break;
                    }
                }

                var scoreDown = 0;
                for (int yy = y + 1; yy < height; yy++) {
                    var treeDown = trees[yy][x];
                    scoreDown++;

                    if (treeDown >= tree) {
                        break;
                    }
                }

                var scoreLeft = 0;
                for (int xx = x - 1; xx >= 0; xx--) {
                    var treeLeft = trees[y][xx];
                    scoreLeft++;

                    if (treeLeft >= tree) {
                        break;
                    }
                }

                var scoreRight = 0;
                for (int xx = x + 1; xx < width; xx++) {
                    var treeRight = trees[y][xx];
                    scoreRight++;

                    if (treeRight >= tree) {
                        break;
                    }
                }

                var score = scoreUp * scoreDown * scoreLeft * scoreRight;
                scores[y][x] = score;
            }
        }

        return scores.SelectMany(b => b).Max();
    }
}