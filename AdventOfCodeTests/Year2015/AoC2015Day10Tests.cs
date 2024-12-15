using System;
using AdventOfCode.Year2015;
using NUnit.Framework;

namespace AdventOfCodeTests.Year2015;

[TestFixture]
public class AoC2015Day10Tests {
    [TestCase("1", 6)]
    public void Solution1Example(string input, long expect) {
        var instance = new AoC2015Day10(input, 5);
        Assert.That(expect, Is.EqualTo(instance.Solution1()));
    }

    [Test]
    public void Solution1Test() {
        var instance = new AoC2015Day10(null, 40);
        var result = instance.Solution1();
        Assert.That(492982, Is.EqualTo(result));
        Console.WriteLine($"Result: {result}");
    }

    [Test]
    public void Solution2Test() {
        var instance = new AoC2015Day10(null, 50);
        var result = instance.Solution1();
        Assert.That(6989950, Is.EqualTo(result));
        Console.WriteLine($"Result: {result}");
    }
}