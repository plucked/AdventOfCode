using AdventOfCode.Utilities;
using BenchmarkDotNet.Attributes;

namespace AdventOfCode.Year2024;

public class AoC2024Day09
{
    // input is negative when line is empty
    private readonly List<int> disk = new();

    public AoC2024Day09(string? customInput = null)
    {
        var content = (customInput ?? EmbeddedInput.ReadAllText("Year2024/2024_09_input.txt"));
        bool file = true;
        var nextId = 0;
        foreach (var c in content)
        {
            var count = c - '0';
            if (file)
            {
                disk.AddRange(Enumerable.Range(0, count).Select(_ => nextId));
                nextId++;
            }
            else
            {
                disk.AddRange(Enumerable.Range(0, count).Select(_ => -1));
            }
            file = !file;
        }
    }

    [Benchmark]
    public long Solution1()
    {
        var lastInsertIndex = -1;
        for (int i = disk.Count - 1; i > 0; i--)
        {
            var v = disk[i];
            if (v == -1)
            {
                continue;
            }

            for (int j = lastInsertIndex + 1; j < disk.Count && j < i; j++)
            {
                if (disk[j] == -1)
                {
                    disk[j] = v;
                    disk[i] = -1;
                    lastInsertIndex = j;
                    break;
                }
            }
        }

        var checkSum = 0L;
        for (int i = 0; i < disk.Count; i++)
        {
            if (disk[i] != -1)
            {
                checkSum += i * disk[i];
            }
        }
  
        return checkSum;
    }

    [Benchmark]
    public long Solution2()
    {
        var lastInsertIndex = -1;
        var moved = new HashSet<int>();
        for (int i = disk.Count - 1; i > 0; i--)
        {
            var v = disk[i];
            if (v == -1)
            {
                continue;
            }
            
            var length = 1;
            for (int j = i - 1; j >= 0; j--)
            {
                if (disk[j] == disk[i])
                {
                    length++;
                }
                else
                {
                    break;
                }
            }

            if (moved.Add(disk[i]) == false)
            {
                continue;
            }

            for (int j = lastInsertIndex + 1; j < disk.Count && j < i; j++)
            {
                if (disk[j] != -1)
                {
                    continue;
                } else if (disk[j] == disk[i])
                {
                    break;
                }

                var enoughSpace = true;
                for (int k = j + 1; k < j + length; k++)
                {
                    if (disk[k] != -1)
                    {
                        enoughSpace = false;
                        break;
                    }
                }

                if (enoughSpace == false)
                {
                    continue;
                }
                
                for (int k = 0; k < length; k++)
                {
                    disk[j + k] = disk[i - length + 1 + k];
                    disk[i - length + 1 + k] = -1;
                }
            }
        }

        var checkSum = 0L;
        for (int i = 0; i < disk.Count; i++)
        {
            if (disk[i] != -1)
            {
                checkSum += i * disk[i];
            }
        }
        
        return checkSum;
    }
}