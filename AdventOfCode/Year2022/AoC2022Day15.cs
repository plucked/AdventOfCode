using System.Text.RegularExpressions;
using BenchmarkDotNet.Attributes;

namespace AdventOfCode.Year2022;

public class AoC2022Day15 {
    private readonly Sensor[] sensors;

    private class Sensor {
        public long X { get; set; }
        public long Y { get; set; }
        public long BeaconX { get; set; }
        public long BeaconY { get; set; }

        private long? distanceBeacon;
        public long DistanceBeacon => distanceBeacon ??= Math.Abs(X - BeaconX) + Math.Abs(Y - BeaconY);

        public long MinX {
            get => X - DistanceBeacon;
        }
        public long MaxX {
            get => X + DistanceBeacon;
        }

        public bool IsWithinBeaconRange(long x, long y) {
            return Math.Abs(x - X) + Math.Abs(y - Y) <= DistanceBeacon;
        }
    }

    public AoC2022Day15(string input) {
        var lines = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

        var regex = new Regex(@"x=(?<x>-?\d+), y=(?<y>-?\d+): closest beacon is at x=(?<bx>-?\d+), y=(?<by>-?\d+)", RegexOptions.Compiled);
        sensors = lines.Select(
                               line => {
                                   var match = regex.Match(line);
                                   return new Sensor {
                                           X = long.Parse(match.Groups["x"].Value),
                                           Y = long.Parse(match.Groups["y"].Value),
                                           BeaconX = long.Parse(match.Groups["bx"].Value),
                                           BeaconY = long.Parse(match.Groups["by"].Value)
                                   };
                               })
                       .ToArray();
    }

    [Benchmark]
    public long Solution1(long row) {
        List<(long lineStart, long lineEnd)> lines = new();

        foreach (var sensor in sensors) {
            var rowDistance = Math.Abs(sensor.Y - row);
            if (rowDistance > sensor.DistanceBeacon) {
                continue;
            }

            (long lineStart, long lineEnd) newLine = (sensor.MinX + rowDistance, sensor.MaxX - rowDistance);

            for (var i = 0; i < lines.Count; i++) {
                var line = lines[i];

                if (newLine.lineStart >= line.lineStart && newLine.lineEnd <= line.lineEnd) {
                    newLine = (-1, -1);
                    break;
                }

                if (newLine.lineStart <= line.lineEnd && newLine.lineEnd >= line.lineStart) {
                    newLine = (Math.Min(newLine.lineStart, line.lineStart), Math.Max(newLine.lineEnd, line.lineEnd));
                    lines.RemoveAt(i);
                    i--;
                    continue;
                }

                if (newLine.lineStart <= line.lineStart && newLine.lineEnd >= line.lineStart) {
                    newLine = (newLine.lineStart, line.lineEnd);
                    lines.RemoveAt(i);
                    i--;
                    continue;
                }

                if (newLine.lineEnd >= line.lineEnd && newLine.lineStart <= line.lineEnd) {
                    newLine = (line.lineStart, newLine.lineEnd);
                    lines.RemoveAt(i);
                    i--;
                }
            }

            if (newLine.lineStart != -1 && newLine.lineStart < newLine.lineEnd) {
                lines.Add(newLine);
            }
        }

        long count = 0;
        foreach (var c in lines) {
            count += c.lineEnd - c.lineStart;
        }

        return count;
    }

    [Benchmark]
    public long Solution2(long searchAreaSize) {
        List<(long lineStart, long lineEnd)> lines = new();

        for (int y = 0; y <= searchAreaSize; y++) {
            lines.Clear();
            foreach (var sensor in sensors) {
                var rowDistance = Math.Abs(sensor.Y - y);
                if (rowDistance > sensor.DistanceBeacon) {
                    continue;
                }

                (long lineStart, long lineEnd) newLine = (Math.Max(sensor.MinX + rowDistance, 0), Math.Min(sensor.MaxX - rowDistance, searchAreaSize));

                for (var i = 0; i < lines.Count; i++) {
                    var line = lines[i];

                    if (newLine.lineStart >= line.lineStart && newLine.lineEnd <= line.lineEnd) {
                        newLine = (-1, -1);
                        break;
                    }

                    if (newLine.lineStart <= line.lineEnd && newLine.lineEnd >= line.lineStart) {
                        newLine = (Math.Min(newLine.lineStart, line.lineStart), Math.Max(newLine.lineEnd, line.lineEnd));
                        lines.RemoveAt(i);
                        i--;
                        continue;
                    }

                    if (newLine.lineStart <= line.lineStart && newLine.lineEnd >= line.lineStart) {
                        newLine = (newLine.lineStart, line.lineEnd);
                        lines.RemoveAt(i);
                        i--;
                        continue;
                    }

                    if (newLine.lineEnd >= line.lineEnd && newLine.lineStart <= line.lineEnd) {
                        newLine = (line.lineStart, newLine.lineEnd);
                        lines.RemoveAt(i);
                        i--;
                    }
                }

                if (newLine.lineStart != -1 && newLine.lineStart < newLine.lineEnd) {
                    lines.Add(newLine);
                }
            }

            if (lines.Count > 1) {
                lines.Sort((a, b) => a.lineStart.CompareTo(b.lineStart));
                return (lines[0].lineEnd + 1) * 4000000 + y;
            }
        }

        return 0;
    }
}