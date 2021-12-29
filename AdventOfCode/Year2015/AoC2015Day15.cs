using System.Text.RegularExpressions;
using BenchmarkDotNet.Attributes;
using Microsoft.Diagnostics.Tracing.Parsers.ApplicationServer;

namespace AdventOfCode.Year2015;

public class AoC2015Day15 {
    private Ingredient[] input;

    private unsafe struct Ingredient {
        public fixed long Attributes[4];
        public long Calories;

        public Ingredient(long capacity, long durability, long flavor, long texture, long calories) {
            Attributes[0] = capacity;
            Attributes[1] = durability;
            Attributes[2] = flavor;
            Attributes[3] = texture;
            Calories = calories;
        }
    }

    [GlobalSetup(Targets = new[] { nameof(Solution1), nameof(Solution2) })]
    public void BenchmarkSetup() {
        Setup();
    }

    public void Setup(string[]? lines = null) {
        lines ??= File.ReadAllLines("Year2015/2015_15_input.txt");
        var regex = new Regex("^\\w+: capacity (\\-?\\d+), durability (\\-?\\d+), flavor (\\-?\\d+), texture (\\-?\\d+), calories (\\-?\\d+)", RegexOptions.Compiled);
        input = new Ingredient[lines.Length];
        var index = 0;
        foreach (var line in lines) {
            var match = regex.Match(line);
            input[index++] = new Ingredient(
                    long.Parse(match.Groups[1].Value),
                    long.Parse(match.Groups[2].Value),
                    long.Parse(match.Groups[3].Value),
                    long.Parse(match.Groups[4].Value),
                    long.Parse(match.Groups[5].Value));
        }
    }

    [Benchmark]
    public unsafe long Solution1() {
        return Run(null);
    }

    [Benchmark]
    public long Solution2() {
        return Run(500);
    }

    private long Run(long? expectedCalories) {
        var bestScore = 0L;
        var maxSpoons = 100;
        var balance = new int[4];
        for (int i = 0; i <= 100; i++) {
            balance[0] = i;
            Score(maxSpoons - i, 1);
        }

        return bestScore;

        unsafe void Score(int spoonsLeft, int index) {
            if (spoonsLeft == 0) {
                var score = 1L;
                if (expectedCalories != null) {
                    var totalCalories = 0L;
                    for (int j = 0; j < input.Length; j++) {
                        if (balance[j] == 0) {
                            continue;
                        }

                        totalCalories += input[j].Calories * balance[j];
                    }

                    if (totalCalories != expectedCalories) {
                        return;
                    }
                }
                
                for (int i = 0; i < 4; i++) {
                    long attributeScore = 0;
                    for (int j = 0; j < input.Length; j++) {
                        if (balance[j] == 0) {
                            continue;
                        }

                        attributeScore += input[j].Attributes[i] * balance[j];
                    }

                    score *= Math.Max(0, attributeScore);
                }

                bestScore = Math.Max(score, bestScore);
                return;
            }

            if (index == input.Length - 1) {
                balance[index] = spoonsLeft;
                Score(0, index + 1);
            } else {
                for (int i = spoonsLeft; i >= 0; i--) {
                    balance[index] = i;
                    Score(spoonsLeft - i, index + 1);
                }
            }

            balance[index] = 0;
        }
    }
}