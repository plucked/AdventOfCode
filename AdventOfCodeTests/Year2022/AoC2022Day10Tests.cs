using System;
using AdventOfCode.Utilities;
using AdventOfCode.Year2022;
using NUnit.Framework;

namespace AdventOfCodeTests.Year2022;

[TestFixture]
public class AoC2022Day10Tests {
    [TestCase("test_input", 13140)]
    [TestCase("input", 17180)]
    public void Solution1(string input, long expect) {
        if (input == "input") {
            input = EmbeddedInput.ReadAllText("Year2022/2022_10_input.txt");
        } else if (input == "test_input") {
            input = EmbeddedInput.ReadAllText("Year2022/2022_10_test_input.txt");
        }

        var instance = new AoC2022Day10(input);
        Assert.AreEqual(expect, instance.Solution1());
    }

    const string ExpectedTestOutput = @"
##..##..##..##..##..##..##..##..##..##..
###...###...###...###...###...###...###.
####....####....####....####....####....
#####.....#####.....#####.....#####.....
######......######......######......####
#######.......#######.......#######.....";

    private const string ExpectedOutput = @"
###..####.#..#.###..###..#....#..#.###..
#..#.#....#..#.#..#.#..#.#....#..#.#..#.
#..#.###..####.#..#.#..#.#....#..#.###..
###..#....#..#.###..###..#....#..#.#..#.
#.#..#....#..#.#....#.#..#....#..#.#..#.
#..#.####.#..#.#....#..#.####..##..###..";

    [TestCase("test_input", ExpectedTestOutput)]
    [TestCase("input", ExpectedOutput)]
    public void Solution2(string input, string expect) {
        if (input == "input") {
            input = EmbeddedInput.ReadAllText("Year2022/2022_10_input.txt");
        } else if (input == "test_input") {
            input = EmbeddedInput.ReadAllText("Year2022/2022_10_test_input.txt");
        }

        var instance = new AoC2022Day10(input);
        Assert.AreEqual(expect, Environment.NewLine + instance.Solution2());
    }
}