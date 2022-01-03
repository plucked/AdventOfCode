using BenchmarkDotNet.Attributes;

namespace AdventOfCode.Year2015;

public class AoC2015Day25 {
    public ulong row = 3010;
    public ulong column = 3019;
    public ulong first = 20151125;
    public ulong firstOp = 252533;
    public ulong secondOp = 33554393;

    [GlobalSetup(Targets = new[] { nameof(Solution1) })]
    public void BenchmarkSetup() {
        Setup();
    }

    public void Setup() {
    }

    [Benchmark]
    public long Solution1() {
        var targetRow = row - 1 + column;
        // see https://en.wikipedia.org/wiki/Lazy_caterer%27s_sequence
        var end = targetRow * (targetRow - 1) / 2ul + 1ul;
        end += column - 2;
        ulong prevRow = first;
        for (ulong i = 1; i <= end; i++) {
            prevRow = prevRow * firstOp % secondOp;
        }

        return (long)prevRow;
    }
}