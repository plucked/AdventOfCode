using System;
using AdventOfCode.Year2015;
using NUnit.Framework;

namespace AdventOfCodeTests.Year2015;

[TestFixture]
public class AoC2015Day13Tests {
    [TestCase(
            new[] {
                    "Alice would gain 54 happiness units by sitting next to Bob.",
                    "Alice would lose 79 happiness units by sitting next to Carol.",
                    "Alice would lose 2 happiness units by sitting next to David.",
                    "Bob would gain 83 happiness units by sitting next to Alice.",
                    "Bob would lose 7 happiness units by sitting next to Carol.",
                    "Bob would lose 63 happiness units by sitting next to David.",
                    "Carol would lose 62 happiness units by sitting next to Alice.",
                    "Carol would gain 60 happiness units by sitting next to Bob.",
                    "Carol would gain 55 happiness units by sitting next to David.",
                    "David would gain 46 happiness units by sitting next to Alice.",
                    "David would lose 7 happiness units by sitting next to Bob.",
                    "David would gain 41 happiness units by sitting next to Carol.",
            },
            330)]
    public void Solution1Example(string[] input, long expect) {
        var instance = new AoC2015Day13(input);
        Assert.That(expect, Is.EqualTo(instance.Solution1()));
    }

    [Test]
    public void Solution1Test() {
        var instance = new AoC2015Day13();
        var result = instance.Solution1();
        Assert.That(709, Is.EqualTo(result));
        Console.WriteLine($"Result: {result}");
    }

    [Test]
    public void Solution2Test() {
        var instance = new AoC2015Day13();
        var result = instance.Solution2();
        Assert.That(668, Is.EqualTo(result));
        Console.WriteLine($"Result: {result}");
    }
}