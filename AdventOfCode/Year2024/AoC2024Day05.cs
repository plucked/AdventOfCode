using AdventOfCode.Utilities;
using BenchmarkDotNet.Attributes;

namespace AdventOfCode.Year2024;

public class AoC2024Day05
{
    // input is negative when line is empty
    private readonly (int, int)[] orderRules;
    private readonly int[][] orders;
    
    public AoC2024Day05(string? customInput = null)
    {
        var content = (customInput ?? EmbeddedInput.ReadAllText("Year2024/2024_05_input.txt"));
        var lines = content.Split('\n', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        orderRules = new (int, int)[lines.Count(l => l.Contains('|'))];
        orders = new int[lines.Length - orderRules.Length][];
        for (int i = 0; i < orderRules.Length; i++)
        {
            var separatorIndex = lines[i].IndexOf('|');
            orderRules[i] = (int.Parse(lines[i][..separatorIndex]), int.Parse(lines[i][(separatorIndex + 1)..]));
        }

        for (int i = 0; i < orders.Length; i++)
        {
            var line = lines[i + orderRules.Length];
            var parts = line.Split(',', StringSplitOptions.RemoveEmptyEntries);
            orders[i] = parts.Select(int.Parse).ToArray();
        }
    }

    [Benchmark]
    public long Solution1()
    {
        int sum = 0;
        foreach (var order in orders)
        {
            var orderSortedCorrectly = new int[order.Length];
            Array.Copy(order, orderSortedCorrectly, order.Length);
            Array.Sort(orderSortedCorrectly, (a, b) =>
            {
                foreach (var rule in orderRules)
                {
                    if (rule.Item1 == a && rule.Item2 == b)
                    {
                        return -1;
                    }
                    else if (rule.Item1 == b && rule.Item2 == a)
                    {
                        return 1;
                    }
                }

                return 0;
            });
            
            if (order.SequenceEqual(orderSortedCorrectly))
            {
                var middle = order.Length / 2;
                var mid = order[middle];
                sum += mid;
            }
        }

        return sum;
    }

    

    [Benchmark]
    public long Solution2()
    {
        int sum = 0;
        foreach (var order in orders)
        {
            var orderSortedCorrectly = new int[order.Length];
            Array.Copy(order, orderSortedCorrectly, order.Length);
            Array.Sort(orderSortedCorrectly, (a, b) =>
            {
                foreach (var rule in orderRules)
                {
                    if (rule.Item1 == a && rule.Item2 == b)
                    {
                        return -1;
                    }
                    else if (rule.Item1 == b && rule.Item2 == a)
                    {
                        return 1;
                    }
                }

                return 0;
            });
            
            if (!order.SequenceEqual(orderSortedCorrectly))
            {
                var middle = orderSortedCorrectly.Length / 2;
                var mid = orderSortedCorrectly[middle];
                sum += mid;
            }
        }

        return sum;
    }
}