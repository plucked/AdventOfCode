using AdventOfCode.Utilities;
using BenchmarkDotNet.Attributes;

namespace AdventOfCode.Year2024;

public class AoC2024Day04
{
    // input is negative when line is empty
    private readonly string[] input;

    public AoC2024Day04(string? customInput = null)
    {
        var content = (customInput ?? EmbeddedInput.ReadAllText("Year2024/2024_04_input.txt"));
        input = content.Split('\n', StringSplitOptions.RemoveEmptyEntries);
    }

    [Benchmark]
    public long Solution1()
    {
        var searchKernels = new[]
        {
            new[,] { { 'X', 'M', 'A', 'S' } },
            new[,] { { 'S', 'A', 'M', 'X' } },
            new[,]
            {
                { 'X' },
                { 'M' },
                { 'A' },
                { 'S' },
            },
            new[,]
            {
                { 'S' },
                { 'A' },
                { 'M' },
                { 'X' },
            },
            new[,]
            {
                { 'X', '0', '0', '0' },
                { '0', 'M', '0', '0' },
                { '0', '0', 'A', '0' },
                { '0', '0', '0', 'S' },
            },
            new[,]
            {
                { 'S', '0', '0', '0' },
                { '0', 'A', '0', '0' },
                { '0', '0', 'M', '0' },
                { '0', '0', '0', 'X' },
            },
            new[,]
            {
                { '0', '0', '0', 'X' },
                { '0', '0', 'M', '0' },
                { '0', 'A', '0', '0' },
                { 'S', '0', '0', '0' },
            },
            new[,]
            {
                { '0', '0', '0', 'S' },
                { '0', '0', 'A', '0' },
                { '0', 'M', '0', '0' },
                { 'X', '0', '0', '0' },
            },
        }; // '0' is a wildcard

        var sum = 0;
        foreach (var kernel in searchKernels)
        {
            sum += CountKernelOccurrences(kernel);
        }

        return sum;
    }

    private int CountKernelOccurrences(char[,] kernel)
    {
        int count = 0;
        for (int i = 0; i < input.Length; i++)
        {
            for (int j = 0; j < input[i].Length; j++)
            {
                if (MatchKernel(i, j, kernel))
                {
                    count++;
                }
            }
        }

        return count;
    }

    private bool MatchKernel(int i, int j, char[,] kernel)
    {
        for (int k = 0; k < kernel.GetLength(0); k++)
        {
            for (int l = 0; l < kernel.GetLength(1); l++)
            {
                if (i + k >= input.Length || j + l >= input[i].Length)
                {
                    return false;
                }

                if (kernel[k, l] != '0' && kernel[k, l] != input[i + k][j + l])
                {
                    return false;
                }
            }
        }

        return true;
    }

    [Benchmark]
    public long Solution2()
    {
        var searchKernels = new[]
        {
            new[,]
            {
                { 'M', '0', 'M' },
                { '0', 'A', '0' },
                { 'S', '0', 'S' },
            },
            new[,]
            {
                { 'M', '0', 'S' },
                { '0', 'A', '0' },
                { 'M', '0', 'S' },
            },
            new[,]
            {
                { 'S', '0', 'S' },
                { '0', 'A', '0' },
                { 'M', '0', 'M' },
            },
            new[,]
            {
                { 'S', '0', 'M' },
                { '0', 'A', '0' },
                { 'S', '0', 'M' },
            },
        }; // '0' is a wildcard

        var sum = 0;
        foreach (var kernel in searchKernels)
        {
            sum += CountKernelOccurrences(kernel);
        }

        return sum;
    }
}