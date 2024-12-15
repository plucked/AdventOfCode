using AdventOfCode.Utilities;
using AdventOfCode.Year2022;
using NUnit.Framework;

namespace AdventOfCodeTests.Year2022;

[TestFixture]
public class AoC2022Day15Tests {
    [TestCase("test_input", 10, 26)]
    [TestCase("input", 2000000, 4582667)]
    public void Solution1(string input, long row, long expect) {
        if (input == "input") {
            input = EmbeddedInput.ReadAllText("Year2022/2022_15_input.txt");
        } else if (input == "test_input") {
            input = EmbeddedInput.ReadAllText("Year2022/2022_15_test_input.txt");
        }

        var instance = new AoC2022Day15(input);
        Assert.That(expect, Is.EqualTo(instance.Solution1(row)));
    }

    [TestCase("test_input", 20, 56000011)]
    [TestCase("input", 4000000, 10961118625406)]
    public void Solution2(string input, long searchAreaSize, long expect) {
        if (input == "input") {
            input = EmbeddedInput.ReadAllText("Year2022/2022_15_input.txt");
        } else if (input == "test_input") {
            input = EmbeddedInput.ReadAllText("Year2022/2022_15_test_input.txt");
        }

        var instance = new AoC2022Day15(input);
        Assert.That(expect, Is.EqualTo(instance.Solution2(searchAreaSize)));
    }
}