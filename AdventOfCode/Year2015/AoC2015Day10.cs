using BenchmarkDotNet.Attributes;

namespace AdventOfCode.Year2015;

public class AoC2015Day10 {
    private byte[] input;
    private int iterations;

    public AoC2015Day10(string? input = null, int iterations = 50) {
        this.input = (input ?? "1321131112").Select(c => (byte)(c - '0')).ToArray();
        this.iterations = iterations;
    }

    [Benchmark]
    public long Solution1() {
        var a = new List<byte>(input);
        var b = new List<byte>();
        for (int i = 0; i < iterations; i++) {
            b.Clear();
            byte last = a[0];
            byte count = 1;
            for (int j = 1; j < a.Count; j++) {
                if (a[j] == last) {
                    ++count;
                    continue;
                }

                b.Add(count);
                b.Add(last);
                last = a[j];
                count = 1;
            }

            b.Add(count);
            b.Add(last);
            (a, b) = (b, a);
        }

        return a.Count;
    }
}