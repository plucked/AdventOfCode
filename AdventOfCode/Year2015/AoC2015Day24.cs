using System.Numerics;
using AdventOfCode.Utilities;
using BenchmarkDotNet.Attributes;

namespace AdventOfCode.Year2015;

public class AoC2015Day24 {
    public long[] packages;

    public AoC2015Day24(long[]? customInput = null) {
        packages = customInput ?? EmbeddedInput.ReadAllLines("Year2015/2015_24_input.txt").Select(l => long.Parse(l)).ToArray();
        Array.Sort(packages);
    }

    [Benchmark]
    public long Solution1() {
        return Run(3);
    }

    [Benchmark]
    public long Solution2() {
        return Run(4);
    }

    public long Run(int containerAmount) {
        var totalWeight = packages.Sum();
        var weightPerContainer = totalWeight / containerAmount;
        var fullContainerSets = new HashSet<uint>();
        // get all combinations where a container is full
        Pack(0, 0, 0, fullContainerSets);

        var minPackSize = long.MaxValue;
        var minEntanglement = long.MaxValue;
        // run each combination and find our result
        foreach (var combination in fullContainerSets) {
            var packSize = BitOperations.PopCount(combination);
            if (packSize < minPackSize) {
                minPackSize = packSize;
                minEntanglement = long.MaxValue;
            }

            if (packSize == minPackSize) {
                var c = combination;
                long entanglement = 1;
                // iterate only through the set bits 
                while (c != 0) {
                    // get index of first set bit
                    var index = BitOperations.TrailingZeroCount(c);
                    entanglement *= packages[index];
                    // remove set bit from 'c'
                    c = ~(~c ^ (1u << index));
                }

                minEntanglement = Math.Min(minEntanglement, entanglement);
            }
        }

        return minEntanglement;

        void Pack(int startIndex, long weight, uint usedPackages, HashSet<uint> usedPackageTracker) {
            for (int i = startIndex; i < packages.Length; i++) {
                if ((usedPackages & (1u << 1)) != 0) {
                    continue;
                }

                var p = packages[i];

                if (p + weight == weightPerContainer) {
                    usedPackageTracker.Add(usedPackages | (1u << i));
                } else if (p + weight < weightPerContainer) {
                    Pack(i + 1, weight + p, usedPackages | (1u << i), usedPackageTracker);
                } else {
                    break;
                }
            }
        }
    }
}