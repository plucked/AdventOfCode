using System.Numerics;
using AdventOfCode.Utilities;
using BenchmarkDotNet.Attributes;

namespace AdventOfCode.Year2024;

public class AoC2024Day07
{
    // input is negative when line is empty
    private readonly (long result, long[] values)[] tests;
    
    public AoC2024Day07(string? customInput = null)
    {
        var content = (customInput ?? EmbeddedInput.ReadAllText("Year2024/2024_07_input.txt"));
        var lines = content.Split('\n', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        tests = new (long, long[])[lines.Length];
        for (int i = 0; i < lines.Length; i++)
        {
            var colonIndex = lines[i].IndexOf(':');
            var testResult = long.Parse(lines[i][..colonIndex]);
            var test = lines[i][(colonIndex + 2)..].Split(' ').Select(long.Parse).ToArray();
            tests[i] = (testResult, test);
        }
    }

    [Benchmark]
    public long Solution1()
    {
        long correctSum = 0;

        foreach (var test in tests)
        {
            var testResult = test.result;
            var testValues = test.values;
            
            uint operatorFlags = 0; // bit field 0 = +, 1 = *
            while (BitOperations.PopCount(operatorFlags) < testValues.Length)
            {
                var result = testValues[0];
                for (int i = 1; i < testValues.Length; i++)
                {
                    if ((operatorFlags & (1u << i)) == 0)
                    {
                        result += testValues[i];
                    }
                    else
                    {
                        result *= testValues[i];
                    }
                }

                if (result == testResult)
                {
                    correctSum += testResult;
                    break;
                }
                
                operatorFlags++;
            }
        }
        
        return correctSum;
    }

    [Benchmark]
    public long Solution2()
    {
        long correctSum = 0;

        foreach (var test in tests)
        {
            var testResult = test.result;
            var testValues = test.values;
            
            uint operatorFlags = 0; // bit field 00 = +, 01 = *, 10 = concat, 11 = skip
            while (BitOperations.PopCount(operatorFlags) < testValues.Length * 2)
            {
                var result = testValues[0];
                bool skip = false;
                for (int i = 1; i < testValues.Length; i++)
                {
                    var op = (operatorFlags >> (i * 2)) & 3u;
                    if (op == 0)
                    {
                        result += testValues[i];
                    }
                    else if (op == 1)
                    {
                        result *= testValues[i];
                    }
                    else if (op == 2)
                    {
                        result = long.Parse(result.ToString() + testValues[i]);
                    }
                    else
                    {
                        skip = true;
                        break;
                    }
                }

                if (skip == false && result == testResult)
                {
                    correctSum += testResult;
                    break;
                }
                
                operatorFlags++;
            }
        }
        
        return correctSum;
    }
    
    
}