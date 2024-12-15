using AdventOfCode.Utilities;
using AdventOfCode.Year2022;
using NUnit.Framework;

namespace AdventOfCodeTests.Year2022;

[TestFixture]
public class AoC2022Day12Tests {
    [TestCase("test_input", 31)]
    [TestCase("input", 497)]
    public void Solution1(string input, long expect) {
        if (input == "input") {
            input = EmbeddedInput.ReadAllText("Year2022/2022_12_input.txt");
        } else if (input == "test_input") {
            input = EmbeddedInput.ReadAllText("Year2022/2022_12_test_input.txt");
        }

        var instance = new AoC2022Day12(input);
        Assert.That(expect, Is.EqualTo(instance.Solution1()));
    }

    [TestCase("test_input", 29)]
    [TestCase("input", 492)]
    public void Solution2(string input, long expect) {
        if (input == "input") {
            input = EmbeddedInput.ReadAllText("Year2022/2022_12_input.txt");
        } else if (input == "test_input") {
            input = EmbeddedInput.ReadAllText("Year2022/2022_12_test_input.txt");
        }

        var instance = new AoC2022Day12(input);
        Assert.That(expect, Is.EqualTo(instance.Solution2()));
    }
}