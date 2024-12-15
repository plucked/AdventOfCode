using System;
using AdventOfCode.Year2015;
using NUnit.Framework;

namespace AdventOfCodeTests.Year2015;

[TestFixture]
public class AoC2015Day21Tests {
    [Test]
    public void Solution1Test() {
        var instance = new AoC2015Day21();
        var result = instance.Solution1();
        Assert.That(91, Is.EqualTo(result));
        Console.WriteLine($"Result: {result}");
    }

    [Test]
    public void Solution2Test() {
        var instance = new AoC2015Day21();
        var result = instance.Solution2();
        Assert.That(158, Is.EqualTo(result));
        Console.WriteLine($"Result: {result}");
    }
}