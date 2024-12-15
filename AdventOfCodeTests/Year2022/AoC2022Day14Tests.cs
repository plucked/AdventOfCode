using AdventOfCode.Utilities;
using AdventOfCode.Year2022;
using NUnit.Framework;

namespace AdventOfCodeTests.Year2022;

[TestFixture]
public class AoC2022Day14Tests {
    [TestCase("test_input", 24)]
    [TestCase("input", 592)]
    public void Solution1(string input, long expect) {
        if (input == "input") {
            input = EmbeddedInput.ReadAllText("Year2022/2022_14_input.txt");
        } else if (input == "test_input") {
            input = EmbeddedInput.ReadAllText("Year2022/2022_14_test_input.txt");
        }

        var instance = new AoC2022Day14(input);
        Assert.That(expect, Is.EqualTo(instance.Solution1()));
    }

    [TestCase("test_input", 93)]
    [TestCase("input", 30367)]
    public void Solution2(string input, long expect) {
        if (input == "input") {
            input = EmbeddedInput.ReadAllText("Year2022/2022_14_input.txt");
        } else if (input == "test_input") {
            input = EmbeddedInput.ReadAllText("Year2022/2022_14_test_input.txt");
        }

        var instance = new AoC2022Day14(input);
        Assert.That(expect, Is.EqualTo(instance.Solution2()));
    }
}