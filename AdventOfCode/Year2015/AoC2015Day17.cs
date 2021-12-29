using System.Numerics;
using BenchmarkDotNet.Attributes;

namespace AdventOfCode.Year2015;

public class AoC2015Day17 {
    private int[] buckets;
    [GlobalSetup(Targets = new[] { nameof(Solution1), nameof(Solution2) })]
    public void BenchmarkSetup() {
        Setup();
    }

    public void Setup(int[]? customInput = null) {
        buckets = customInput ?? File.ReadAllLines("Year2015/2015_17_input.txt").Select(line => int.Parse(line)).ToArray();
        // sort from biggest to smallest
        Array.Sort(buckets, (a, b) => -a.CompareTo(b));
    }

    [Benchmark]
    public long Solution1(long liters = 150) {
        long result = 0;
        // amount of possible combinations
        int max = 1 << buckets.Length;
        for (int i = 0; i < max; i++) {
            var t = i;
            var current = 0;
            var index = 0;
            // iterate through all the indices of the current combination
            while (t != 0) {
                if ((t & 1) == 1) {
                    current += buckets[index];
                }

                // stop when we exceeded the expected liters match
                if (current > liters) {
                    break;
                }

                index++;
                t >>= 1;
            }

            if (current == liters) {
                ++result;
            }
        }
        
        return result;
    }

    [Benchmark]
    public long Solution2(long liters = 150) {
        long result = 0;
        uint fewestBuckets = uint.MaxValue;
        // amount of possible combinations
        int max = 1 << buckets.Length;
        for (uint i = 0; i < max; i++) {
            // skip when this combination already has more 
            // we simply count the bits which are set
            var combinations = (uint)BitOperations.PopCount(i);
            if (combinations > fewestBuckets) {
                continue;
            }

            var t = i;
            var current = 0;
            var index = 0;
            // iterate through all the indices of the current combination
            while (t != 0) {
                if ((t & 1) == 1) {
                    current += buckets[index];
                }

                // stop when we exceeded the expected liters match
                if (current > liters) {
                    break;
                }

                index++;
                t >>= 1;
            }

            if (current == liters) {
                if (fewestBuckets > combinations) {
                    result = 1;
                    fewestBuckets = combinations;
                } else {
                    ++result;
                }
            }
        }
        
        return result;
    }
}