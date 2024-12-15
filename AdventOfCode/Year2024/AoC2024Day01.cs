using AdventOfCode.Utilities;
using BenchmarkDotNet.Attributes;

namespace AdventOfCode.Year2024;

public class AoC2024Day01 {
    // input is negative when line is empty
    private readonly int[] inputLeft;
    private readonly int[] inputRight;

    public AoC2024Day01(string? customInput = null) {
        var content = (customInput ?? EmbeddedInput.ReadAllText("Year2024/2024_01_input.txt"));
        var lines = content.Split('\n', StringSplitOptions.RemoveEmptyEntries);
        
        /*
Input is:
3   4
4   3
2   5
1   3
3   9
3   3
         */
        
        inputLeft = new int[lines.Length];
        inputRight = new int[lines.Length];
        for (var i = 0; i < lines.Length; i++) {
            var line = lines[i];
            var parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            inputLeft[i] = int.Parse(parts[0]);
            inputRight[i] = int.Parse(parts[1]);
        }
    }

    [Benchmark]
    public long Solution1() {
        // sort inputs
        Array.Sort(inputLeft);
        Array.Sort(inputRight);

        int combinedDistance = 0;
        for (int i = 0; i < inputLeft.Length; i++)
        {
            combinedDistance += Math.Abs(inputLeft[i] - inputRight[i]);
        }
        
        return combinedDistance;
    }

    [Benchmark]
    public long Solution2()
    {
        int similarityScore = 0;
        for (int i = 0; i < inputLeft.Length; i++)
        {
            var occurences = 0;
            for (int j = 0; j < inputRight.Length; j++)
            {
                if (inputLeft[i] == inputRight[j])
                {
                    occurences++;
                }
            }

            similarityScore += occurences * inputLeft[i];
        }

        return similarityScore;
    }
}