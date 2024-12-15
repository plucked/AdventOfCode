using System;
using AdventOfCode.Year2015;
using NUnit.Framework;

namespace AdventOfCodeTests.Year2015;

[TestFixture]
public class AoC2015Day23Tests {
    [TestCase(new[] { "inc a", "jio a, +2", "tpl a", "inc a" }, 2)]
    public void Solution1Example(string[] input, long expected) {
        var instance = new AoC2015Day23(input, 0);
        Assert.That(expected, Is.EqualTo(instance.Solution1()));
    }

    [Test]
    public void Solution1Test() {
        var instance = new AoC2015Day23();
        var result = instance.Solution1();
        Assert.That(184, Is.EqualTo(result));
        Console.WriteLine($"Result: {result}");

        // 412 too low
    }

    [Test]
    public void Solution2Test() {
        var instance = new AoC2015Day23();
        var result = instance.Solution2();
        Assert.That(231, Is.EqualTo(result));
        Console.WriteLine($"Result: {result}");
    }
}