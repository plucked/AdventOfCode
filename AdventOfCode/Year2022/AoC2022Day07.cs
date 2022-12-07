using System.Text.RegularExpressions;
using AdventOfCode.Utilities;
using BenchmarkDotNet.Attributes;

namespace AdventOfCode.Year2022;

public class AoC2022Day07 {
    private readonly Dir root;

    private class Dir {
        public string Name { get; set; }
        public Dictionary<string, Dir> SubDirs { get; } = new();
        public Dir Parent { get; set; }
        public long FileSize { get; set; }
        public long TotalSize => FileSize + SubDirs.Values.Sum(d => d.TotalSize);
    }

    public AoC2022Day07(string? customInput = null) {
        var input = (customInput ?? EmbeddedInput.ReadAllText("Year2022/2022_07_input.txt")).Split(Environment.NewLine);
        root = new Dir { Name = "/" };
        var current = root;

        var fileSizeRegex = new Regex(@"^\d+");
        foreach (var s in input) {
            if (s == "$ cd /") {
                current = root;
                continue;
            }

            if (s == "$ cd ..") {
                current = current.Parent;
                continue;
            }

            if (s.StartsWith("$ cd ")) {
                var subDirName = s.Substring("$ cd ".Length);
                if (current.SubDirs.TryGetValue(subDirName, out var subDir)) {
                    current = subDir;
                } else {
                    current = current.SubDirs[subDirName] = new Dir { Name = subDirName, Parent = current };
                }

                continue;
            }

            // only works when no file has a number in it
            var fileSizeMatch = fileSizeRegex.Match(s);
            if (fileSizeMatch.Success) {
                current.FileSize += long.Parse(fileSizeMatch.Value);
            }
        }
    }

    [Benchmark]
    public long Solution1() {
        var searchSize = 100000;
        long sum = 0;

        var queue = new Queue<Dir>();
        queue.Enqueue(root);
        while (queue.Count > 0) {
            var dir = queue.Dequeue();
            if (dir.TotalSize <= searchSize) {
                sum += dir.TotalSize;
            }

            foreach (var subDir in dir.SubDirs.Values) {
                queue.Enqueue(subDir);
            }
        }

        return sum;
    }

    [Benchmark]
    public long Solution2() {
        var totalDiskSize = 70_000_000;
        var requiredFreeSpace = 30_000_000;
        var freeSpace = totalDiskSize - root.TotalSize;
        var spaceToFree = requiredFreeSpace - freeSpace;

        var closest = long.MaxValue;

        var queue = new Queue<Dir>();
        queue.Enqueue(root);
        while (queue.Count > 0) {
            var dir = queue.Dequeue();
            if (dir.TotalSize >= spaceToFree) {
                if (dir.TotalSize < closest) {
                    closest = dir.TotalSize;
                }

                foreach (var subDir in dir.SubDirs.Values) {
                    queue.Enqueue(subDir);
                }
            }
        }

        return closest;
    }
}