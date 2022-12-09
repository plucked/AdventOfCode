using AdventOfCode.Utilities;
using BenchmarkDotNet.Attributes;

namespace AdventOfCode.Year2022;

public class AoC2022Day09 {
    private List<(char direction, int distance)> instructions = new();

    public AoC2022Day09(string? customInput = null) {
        var input = customInput ?? EmbeddedInput.ReadAllText("Year2022/2022_09_input.txt");
        instructions = input.Split(Environment.NewLine).Select(x => (x[0], int.Parse(x[1..]))).ToList();
    }

    private struct Vector {
        public int X;
        public int Y;
        public long EncodeToLong() => (long)X << 32 | (uint)Y;
        public bool IsAdjacent(Vector other) => Math.Abs(X - other.X) <= 1 && Math.Abs(Y - other.Y) <= 1;
    }

    [Benchmark]
    public long Solution1() {
        return CalculateVisited(2);
    }

    [Benchmark]
    public long Solution2() {
        return CalculateVisited(10);
    }

    private long CalculateVisited(int robeLength) {
        var knotPositions = new Vector[robeLength];
        var visited = new HashSet<long>();
        var lastKnotIndex = knotPositions.Length - 1;
        visited.Add(knotPositions[lastKnotIndex].EncodeToLong());

        foreach (var instruction in instructions) {
            var incrX = instruction.direction == 'R' ? 1 : instruction.direction == 'L' ? -1 : 0;
            var incrY = instruction.direction == 'U' ? 1 : instruction.direction == 'D' ? -1 : 0;

            for (var i = 0; i < instruction.distance; i++) {
                knotPositions[0].X += incrX;
                knotPositions[0].Y += incrY;

                for (var j = 1; j < knotPositions.Length; j++) {
                    var knotPos = knotPositions[j];
                    var prevKnotPos = knotPositions[j - 1];

                    if (knotPos.IsAdjacent(prevKnotPos)) {
                        break;
                    }

                    if (prevKnotPos.X == knotPos.X && Math.Abs(prevKnotPos.Y - knotPos.Y) == 2) {
                        knotPos.Y += prevKnotPos.Y > knotPos.Y ? 1 : -1;
                    } else if (prevKnotPos.Y == knotPos.Y && Math.Abs(prevKnotPos.X - knotPos.X) == 2) {
                        knotPos.X += prevKnotPos.X > knotPos.X ? 1 : -1;
                    } else {
                        knotPos.X += prevKnotPos.X > knotPos.X ? 1 : -1;
                        knotPos.Y += prevKnotPos.Y > knotPos.Y ? 1 : -1;
                    }

                    knotPositions[j] = knotPos;
                }

                visited.Add(knotPositions[lastKnotIndex].EncodeToLong());
            }
        }

        return visited.Count;
    }
}