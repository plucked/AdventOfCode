using AdventOfCode.Utilities;
using BenchmarkDotNet.Attributes;

namespace AdventOfCode.Year2016;

public class AoC2016Day01 {
    // input is positive when 'Right' and negative when 'Left'
    private readonly int[] input;

    public AoC2016Day01(string? customInput = null) {
        var content = (customInput ?? EmbeddedInput.ReadAllText("Year2016/2016_01_input.txt"));
        input = content.Split(", ")
                       .Select(
                               s => {
                                   var r = int.Parse(s.Substring(1));
                                   if (s.StartsWith("L")) {
                                       r = -r;
                                   }

                                   return r;
                               })
                       .ToArray();
    }

    [Benchmark]
    public long Solution1() {
        var x = 0;
        var y = 0;

        Run(
                (newX, newY) => {
                    x = newX;
                    y = newY;
                    return true;
                });

        return Math.Abs(x) + Math.Abs(y);
    }

    [Benchmark]
    public long Solution2() {
        var visited = new HashSet<ulong>();
        var x = 0;
        var y = 0;

        Run(
                (newX, newY) => {
                    x = newX;
                    y = newY;
                    ulong h = (ulong)x << 32 | (uint)y;
                    if (visited.Contains(h)) {
                        return false;
                    }

                    visited.Add(h);
                    return true;
                });

        return Math.Abs(x) + Math.Abs(y);
    }

    void Run(Func<int, int, bool> cb) {
        var x = 0;
        var y = 0;

        var direction = 0;
        for (var i = 0; i < input.Length; i++) {
            var d = input[i];
            if (d > 0) {
                direction++;
            } else {
                d = -d;
                direction--;
            }

            direction = direction switch {
                    < 0 => 3,
                    > 3 => 0,
                    _ => direction
            };

            while (d > 0) {
                switch (direction) {
                    case 0:
                        y++;
                        break;
                    case 1:
                        x++;
                        break;
                    case 2:
                        y--;
                        break;
                    case 3:
                        x--;
                        break;
                }

                if (cb(x, y) == false) {
                    return;
                }

                --d;
            }
        }
    }
}