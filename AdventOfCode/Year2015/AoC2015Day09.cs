using System.Numerics;
using BenchmarkDotNet.Attributes;

namespace AdventOfCode.Year2015;

/// <summary>
/// This is the Travelling Salesman Problem
/// https://en.wikipedia.org/wiki/Travelling_salesman_problem
/// </summary>
public class AoC2015Day09 {
    private Edge[] input;
    private uint locationCount;

    private struct Edge {
        public int LocationA;
        public int LocationB;
        public int Distance;
    }

    [GlobalSetup(Targets = new[] { nameof(Solution1), nameof(Solution2) })]
    public void BenchmarkSetup() {
        Setup();
    }

    public void Setup(string[]? customInput = null) {
        var uniqueId = new Dictionary<string, int>();
        int nextIndex = 1;
        var lines = customInput ?? File.ReadAllLines("Year2015/2015_09_input.txt");
        input = lines.Select(
                             line => {
                                 var split1 = line.Split(" to ");
                                 var split2 = split1[1].Split(" = ");
                                 var locA = split1[0];
                                 var locB = split2[0];
                                 int locAIdx = 0;
                                 int locBIdx = 0;

                                 if (uniqueId.TryGetValue(locA, out locAIdx) == false) {
                                     locAIdx = nextIndex;
                                     uniqueId[locA] = locAIdx;
                                     nextIndex <<= 1;
                                 }

                                 if (uniqueId.TryGetValue(locB, out locBIdx) == false) {
                                     locBIdx = nextIndex;
                                     uniqueId[locB] = locBIdx;
                                     nextIndex <<= 1;
                                 }

                                 return new Edge { LocationA = locAIdx, LocationB = locBIdx, Distance = int.Parse(split2[1]) };
                             })
                     .ToArray();

        locationCount = (uint)uniqueId.Count;
    }

    [Benchmark]
    public long Solution1() {
        return Run(true);
    }

    [Benchmark]
    public long Solution2() {
        return Run(false);
    }

    public unsafe int Run(bool returnShortestDistance, bool travelToStart = false) {
        int bestDistanceTravelled = returnShortestDistance ? int.MaxValue : int.MinValue;

        // need to create a thread, because the default max stack size of the main thread is 1MB and
        // this solution needs about 64MB because of the recursion (o:
        var thread = new Thread(
                () => {
                    // create a graph to look up distances (faster than a dict lookup)
                    // the index in the table is ids of two cities combined
                    // the puzzle input has 7 cities, so the biggest index will be 1100000 = 96
                    var distances = stackalloc int[11000001];
                    foreach (var edge in input) {
                        distances[edge.LocationA | edge.LocationB] = edge.Distance;
                    }

                    for (int i = 0; i < locationCount; i++) {
                        int location = 1 << i;
                        Travel(location, location, 0, travelToStart ? location : null);
                    }

                    void Travel(int travelled, int from, int distanceTravelled, int? start) {
                        if (BitOperations.PopCount((uint)travelled) == locationCount) {
                            // this is needed for the day 13 solution, ignored in day 9
                            if (start != null) {
                                distanceTravelled += distances[@from | start.Value];
                            }

                            if (returnShortestDistance) {
                                bestDistanceTravelled = Math.Min(bestDistanceTravelled, distanceTravelled);
                            } else {
                                bestDistanceTravelled = Math.Max(bestDistanceTravelled, distanceTravelled);
                            }
                        }

                        for (int i = 0; i < locationCount; i++) {
                            int location = 1 << i;
                            if ((location & travelled) != 0) {
                                continue;
                            }

                            Travel(travelled | location, location, distanceTravelled + distances[@from | location], start);
                        }
                    }
                },
                maxStackSize:1024 * 1024 * 64);

        thread.Start();
        thread.Join();
        return bestDistanceTravelled;
    }
}