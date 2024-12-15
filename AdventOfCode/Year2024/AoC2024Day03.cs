using System.Text.RegularExpressions;
using AdventOfCode.Utilities;
using BenchmarkDotNet.Attributes;

namespace AdventOfCode.Year2024;

public class AoC2024Day03
{
    // input is negative when line is empty
    private readonly string input;

    public AoC2024Day03(string? customInput = null)
    {
        input = customInput ?? EmbeddedInput.ReadAllText("Year2024/2024_03_input.txt");
    }

    [Benchmark]
    public long Solution1()
    {
        var total = 0;
        var mulRegex = new Regex("mul\\((\\d{1,3})\\,(\\d{1,3})\\)");
        for (int i = 0; i < input.Length;)
        {
            var match = mulRegex.Match(input[i..]);
            if (match.Success)
            {
                total += int.Parse(match.Groups[1].Value) * int.Parse(match.Groups[2].Value);
                i += match.Index + match.Length;
            }
            else
            {
                break;
            }
        }

        return total;
    }
    
    

    [Benchmark]
    public long Solution2()
    {
        var total = 0;
        var mulRegex = new Regex("mul\\((\\d{1,3})\\,(\\d{1,3})\\)");
        var dontRegex = new Regex("don't\\(\\)");
        var doRegex = new Regex("do\\(\\)");
        var mulEnabled = true;
        for (int i = 0; i < input.Length;)
        {
            var mulMatch = mulRegex.Match(input[i..]);
            var dontMatch = dontRegex.Match(input[i..]);
            var doMatch = doRegex.Match(input[i..]);

            if (mulMatch.Success && CompareMatch(mulMatch, dontMatch) && CompareMatch(mulMatch, doMatch))
            {
                if (mulEnabled)
                {
                    total += int.Parse(mulMatch.Groups[1].Value) * int.Parse(mulMatch.Groups[2].Value);
                }
                
                i += mulMatch.Index + mulMatch.Length;
            } else if (dontMatch.Success && CompareMatch(dontMatch, mulMatch) && CompareMatch(dontMatch, doMatch))
            {
                mulEnabled = false;
                i += dontMatch.Index + dontMatch.Length;
            } else if (doMatch.Success && CompareMatch(doMatch, mulMatch) && CompareMatch(doMatch, dontMatch))
            {
                mulEnabled = true;
                i += doMatch.Index + doMatch.Length;
            }
            else
            {
                break;
            }
        }

        return total;
        
        bool CompareMatch(Match match1, Match match2)
        {
            return match1.Index < match2.Index || match2.Success == false;
        }
    }
}