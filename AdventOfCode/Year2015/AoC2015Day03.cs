using BenchmarkDotNet.Attributes;

namespace AdventOfCode.Year2015;

public class AoC2015Day03 {
    private const byte MOVE_UP = 1;
    private const byte MOVE_DOWN = 2;
    private const byte MOVE_LEFT = 3;
    private const byte MOVE_RIGHT = 4;
    private const long OFFSET = 1000000;
    private byte[] input;

    [GlobalSetup(Targets = new[] { nameof(Solution1), nameof(Solution2) })]
    public void BenchmarkSetup() {
        Setup();
    }

    public void Setup(string? customInput = null) {
        var lines = customInput ?? File.ReadAllText("Year2015/2015_03_input.txt");
        input = lines.Select(
                             c => {
                                 switch (c) {
                                     case '^':
                                         return MOVE_UP;
                                     case '>':
                                         return MOVE_RIGHT;
                                     case 'v':
                                         return MOVE_DOWN;
                                     case '<':
                                         return MOVE_LEFT;
                                     default:
                                         throw new Exception($"Unexpected char '{c}'");
                                 }
                             })
                     .ToArray();
    }

    [Benchmark]
    public long Solution1() {
        var visited = new HashSet<long>();
        var x = 0;
        var y = 0;
        visited.Add(ToIndex(x, y));

        foreach (var move in input) {
            switch (move) {
                case MOVE_RIGHT:
                    ++x;
                    break;
                case MOVE_LEFT:
                    --x;
                    break;
                case MOVE_UP:
                    ++y;
                    break;
                case MOVE_DOWN:
                    --y;
                    break;
            }

            visited.Add(ToIndex(x, y));
        }

        return visited.Count;
    }

    [Benchmark]
    public unsafe long Solution2() {
        var visited = new HashSet<long>();
        var santa = 0;
        var x = stackalloc long[2];
        var y = stackalloc long[2];
        visited.Add(ToIndex(0, 0));

        foreach (var move in input) {
            switch (move) {
                case MOVE_RIGHT:
                    ++x[santa];
                    break;
                case MOVE_LEFT:
                    --x[santa];
                    break;
                case MOVE_UP:
                    ++y[santa];
                    break;
                case MOVE_DOWN:
                    --y[santa];
                    break;
            }

            visited.Add(ToIndex(x[santa], y[santa]));
            santa += 1;
            santa = santa > 1 ? 0 : santa;
        }

        return visited.Count;
    }

    private long ToIndex(long x, long y) {
        return ((x + OFFSET) << 32) | (y + OFFSET);
    }
}