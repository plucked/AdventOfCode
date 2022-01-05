using BenchmarkDotNet.Attributes;

namespace AdventOfCode.Year2015;

public class AoC2015Day25 {
    [Benchmark]
    public long Solution1() {
        ulong row = 3010;
        ulong column = 3019;
        ulong first = 20151125;
        ulong firstOp = 252533;
        ulong secondOp = 33554393;

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