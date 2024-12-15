using AdventOfCode.Utilities;
using AdventOfCode.Year2022;
using NUnit.Framework;

namespace AdventOfCodeTests.Year2022;

[TestFixture]
public class AoC2022Day11Tests {
    [TestCase("test_input", 10605)]
    [TestCase("input", 50172)]
    public void Solution1(string input, long expect) {
        if (input == "input") {
            input = EmbeddedInput.ReadAllText("Year2022/2022_11_input.txt");
        } else if (input == "test_input") {
            input = EmbeddedInput.ReadAllText("Year2022/2022_11_test_input.txt");
        }

        var instance = new AoC2022Day11(input);
        Assert.That(expect, Is.EqualTo(instance.Solution1()));
    }

    [TestCase("test_input", 2713310158)]
    [TestCase("input", 11614682178)]
    public void Solution2(string input, long expect) {
        if (input == "input") {
            input = EmbeddedInput.ReadAllText("Year2022/2022_11_input.txt");
        } else if (input == "test_input") {
            input = EmbeddedInput.ReadAllText("Year2022/2022_11_test_input.txt");
        }

        var instance = new AoC2022Day11(input);
        Assert.That(expect, Is.EqualTo(instance.Solution2()));
    }
}