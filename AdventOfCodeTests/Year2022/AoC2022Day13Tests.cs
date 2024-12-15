using AdventOfCode.Utilities;
using AdventOfCode.Year2022;
using NUnit.Framework;

namespace AdventOfCodeTests.Year2022;

[TestFixture]
public class AoC2022Day13Tests {
    [TestCase("test_input", 13)]
    [TestCase("input", 5196)]
    public void Solution1(string input, long expect) {
        if (input == "input") {
            input = EmbeddedInput.ReadAllText("Year2022/2022_13_input.txt");
        } else if (input == "test_input") {
            input = EmbeddedInput.ReadAllText("Year2022/2022_13_test_input.txt");
        }

        var instance = new AoC2022Day13(input);
        Assert.That(expect, Is.EqualTo(instance.Solution1()));
    }

    [TestCase("test_input", 140)]
    [TestCase("input", 22134)]
    public void Solution2(string input, long expect) {
        if (input == "input") {
            input = EmbeddedInput.ReadAllText("Year2022/2022_13_input.txt");
        } else if (input == "test_input") {
            input = EmbeddedInput.ReadAllText("Year2022/2022_13_test_input.txt");
        }

        var instance = new AoC2022Day13(input);
        Assert.That(expect, Is.EqualTo(instance.Solution2()));
    }
}