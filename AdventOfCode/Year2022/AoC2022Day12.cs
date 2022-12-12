using BenchmarkDotNet.Attributes;

namespace AdventOfCode.Year2022;

public class AoC2022Day12 {
    int gridWidth;
    int gridHeight;
    char[,] grid;
    (int x, int y) start;
    (int x, int y) end;

    public AoC2022Day12(string input) {
        var lines = input.Split(Environment.NewLine);
        gridWidth = lines[0].Length;
        gridHeight = lines.Length;
        grid = new char[gridWidth, gridHeight];
        for (var y = 0; y < gridHeight; y++) {
            for (var x = 0; x < gridWidth; x++) {
                var c = lines[y][x];
                if (c == 'S') {
                    start = (x, y);
                    c = 'a';
                } else if (c == 'E') {
                    end = (x, y);
                    c = 'z';
                }

                grid[x, y] = c;
            }
        }
    }

    [Benchmark]
    public long Solution1() {
        return Traverse(start);
    }

    [Benchmark]
    public long Solution2() {
        var min = long.MaxValue;
        for (var y = 0; y < gridHeight; y++) {
            for (var x = 0; x < gridWidth; x++) {
                if (grid[x, y] == 'a') {
                    min = Math.Min(min, Traverse((x, y)));
                }
            }
        }

        return min;
    }

    private long Traverse((int x, int y) start) {
        var queue = new Queue<(int x, int y, int steps)>();
        queue.Enqueue((start.x, start.y, 0));
        var visited = new HashSet<(int x, int y)>();
        while (queue.Count > 0) {
            var (x, y, steps) = queue.Dequeue();
            if (visited.Contains((x, y)))
                continue;
            visited.Add((x, y));
            if (x == end.x && y == end.y)
                return steps;
            if (x > 0 && grid[x - 1, y] - grid[x, y] <= 1)
                queue.Enqueue((x - 1, y, steps + 1));
            if (x < gridWidth - 1 && grid[x + 1, y] - grid[x, y] <= 1)
                queue.Enqueue((x + 1, y, steps + 1));
            if (y > 0 && grid[x, y - 1] - grid[x, y] <= 1)
                queue.Enqueue((x, y - 1, steps + 1));
            if (y < gridHeight - 1 && grid[x, y + 1] - grid[x, y] <= 1)
                queue.Enqueue((x, y + 1, steps + 1));
        }

        return long.MaxValue;
    }
}