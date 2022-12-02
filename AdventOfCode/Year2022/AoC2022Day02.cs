using AdventOfCode.Utilities;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Helpers;

namespace AdventOfCode.Year2022;

public class AoC2022Day02 {
    private enum Tool {
        Rock = 1,
        Paper = 2,
        Scissor = 3
    }

    private struct Round {
        public Tool Opponent;
        public Tool Me;

        public Round(Tool opponent, Tool me) {
            Opponent = opponent;
            Me = me;
        }

        public int Points() {
            switch (Opponent) {
                case Tool.Rock:
                    return Me switch {
                            Tool.Paper => (int)Me + 6,
                            Tool.Rock => (int)Me + 3,
                            _ => (int)Me
                    };
                case Tool.Paper:
                    return Me switch {
                            Tool.Scissor => (int)Me + 6,
                            Tool.Paper => (int)Me + 3,
                            _ => (int)Me
                    };
                case Tool.Scissor:
                    return Me switch {
                            Tool.Rock => (int)Me + 6,
                            Tool.Scissor => (int)Me + 3,
                            _ => (int)Me
                    };
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }

    // input is negative when line is empty
    private readonly Round[] input;

    public AoC2022Day02(string? customInput = null) {
        var content = (customInput ?? EmbeddedInput.ReadAllText("Year2022/2022_02_input.txt"));
        input = content.Split("\n")
                       .Select(
                               s => {
                                   var firstTool = s[0] switch {
                                           'A' => Tool.Rock,
                                           'B' => Tool.Paper,
                                           'C' => Tool.Scissor,
                                           _ => throw new ArgumentException()
                                   };

                                   var secondTool = s[2] switch {
                                           'X' => Tool.Rock,
                                           'Y' => Tool.Paper,
                                           'Z' => Tool.Scissor,
                                           _ => throw new ArgumentException()
                                   };

                                   return new Round(firstTool, secondTool);
                               })
                       .ToArray();
    }

    [Benchmark]
    public long Solution1() {
        return Run(input);
    }

    [Benchmark]
    public long Solution2() {
        // transforming the input
        for (int i = 0; i < input.Length; i++) {
            switch (input[i].Me) {
                case Tool.Rock:
                    input[i].Me = input[i].Opponent switch {
                            Tool.Rock => Tool.Scissor,
                            Tool.Paper => Tool.Rock,
                            Tool.Scissor => Tool.Paper,
                            _ => throw new ArgumentException()
                    };
                    break;
                case Tool.Paper:
                    input[i].Me = input[i].Opponent;
                    break;
                case Tool.Scissor:
                    input[i].Me = input[i].Opponent switch {
                            Tool.Rock => Tool.Paper,
                            Tool.Paper => Tool.Scissor,
                            Tool.Scissor => Tool.Rock,
                            _ => throw new ArgumentException()
                    };
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        return Run(input);
    }

    private long Run(Round[] rounds) {
        long sum = 0;
        foreach (var round in rounds) {
            sum += round.Points();
        }

        return sum;
    }
}