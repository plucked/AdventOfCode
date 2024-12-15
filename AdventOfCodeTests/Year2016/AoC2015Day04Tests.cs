using System;
using AdventOfCode.Year2016;
using NUnit.Framework;

namespace AdventOfCodeTests.Year2016;

[TestFixture]
public class AoC2015Day04Tests {
    [Test]
    public void Solution1Test() {
        var instance = new AoC2016Day04();
        var result = instance.Solution1();
        Assert.That(278221, Is.EqualTo(result));
        Console.WriteLine($"Result: {result}");
    }

    [Test]
    public void Solution2Test() {
        var instance = new AoC2016Day04();
        var result = instance.Solution2();
        Assert.That(267, Is.EqualTo(result));
        Console.WriteLine($"Result: {result}");
    }
}