using AdventOfCode.Utilities;
using AdventOfCode.Year2022;
using NUnit.Framework;

namespace AdventOfCodeTests.Year2022;

[TestFixture]
public class AoC2022Day16Tests {
    [TestCase("test_input", 1651)]
    [TestCase("input", 1737)]
    public void Solution1(string input, long expect) {
        if (input == "input") {
            input = EmbeddedInput.ReadAllText("Year2022/2022_16_input.txt");
        } else if (input == "test_input") {
            input = EmbeddedInput.ReadAllText("Year2022/2022_16_test_input.txt");
        }

        var instance = new AoC2022Day16(input);
        Assert.That(expect, Is.EqualTo(instance.Solution1()));
    }

    [TestCase("test_input", 1707)]
    [TestCase("input", 2216)]
    public void Solution2(string input, long expect) {
        if (input == "input") {
            input = EmbeddedInput.ReadAllText("Year2022/2022_16_input.txt");
        } else if (input == "test_input") {
            input = EmbeddedInput.ReadAllText("Year2022/2022_16_test_input.txt");
        }

        var instance = new AoC2022Day16(input);
        Assert.That(expect, Is.EqualTo(instance.Solution2()));
    }
}