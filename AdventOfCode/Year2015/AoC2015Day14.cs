using System.Text.RegularExpressions;
using BenchmarkDotNet.Attributes;

namespace AdventOfCode.Year2015;

public class AoC2015Day14 {
    private Reindeer[] input;

    private struct Reindeer {
        public string Name;
        public int Speed;
        public int MoveTime;
        public int RestTime;
    }

    [GlobalSetup(Targets = new[] { nameof(Solution1), nameof(Solution2) })]
    public void BenchmarkSetup() {
        Setup();
    }

    public void Setup(string[]? lines = null) {
        lines ??= File.ReadAllLines("Year2015/2015_14_input.txt");
        var regex = new Regex("(\\w*) can fly (\\d+) km\\/s for (\\d+) seconds, but then must rest for (\\d+) seconds.", RegexOptions.Compiled);
        input = new Reindeer[lines.Length];
        var index = 0;
        foreach (var line in lines) {
            var match = regex.Match(line);
            input[index++] = new Reindeer {
                    Name = match.Groups[1].Value, Speed = int.Parse(match.Groups[2].Value), MoveTime = int.Parse(match.Groups[3].Value), RestTime = int.Parse(match.Groups[4].Value)
            };
        }
    }

    [Benchmark]
    public long Solution1(int seconds = 2503) {
        var distance = new int[input.Length];

        for (int reindeerIdx = 0; reindeerIdx < input.Length; reindeerIdx++) {
            var secondsPast = 0;
            while (secondsPast < seconds) {
                var moveTime = input[reindeerIdx].MoveTime;
                moveTime = (secondsPast + moveTime <= seconds) ? moveTime : seconds - secondsPast;
                distance[reindeerIdx] += moveTime * input[reindeerIdx].Speed;
                secondsPast += moveTime + input[reindeerIdx].RestTime;
            }
        }

        Array.Sort(distance);
        return distance[^1];
    }

    [Benchmark]
    public long Solution2(int seconds = 2503) {
        var distance = new int[input.Length];
        var points = new int[input.Length];
        var state = new int[input.Length];

        for (int i = 0; i < state.Length; i++) {
            state[i] = input[i].MoveTime;
        }

        for (int second = 0; second < seconds; second++) {
            for (int i = 0; i < input.Length; i++) {
                if (state[i] == -input[i].RestTime) {
                    state[i] = input[i].MoveTime;
                }

                if (state[i] > 0) {
                    distance[i] += input[i].Speed;
                }

                state[i]--;
            }

            // award the leading reindeer with a point
            var largestDistance = -1;
            for (int i = 0; i < distance.Length; i++) {
                var d = distance[i];
                if (d > largestDistance) {
                    largestDistance = d;
                }
            }

            for (int i = 0; i < distance.Length; i++) {
                if (distance[i] == largestDistance) {
                    points[i]++;
                }
            }
        }

        Array.Sort(points);
        return points[^1];
    }
}