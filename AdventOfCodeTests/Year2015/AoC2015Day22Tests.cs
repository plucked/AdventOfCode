using System;
using AdventOfCode.Year2015;
using NUnit.Framework;

namespace AdventOfCodeTests.Year2015;

[TestFixture]
public class AoC2015Day22Tests {
    [Test]
    public void Solution1Test() {
        var instance = new AoC2015Day22();
        var result = instance.Solution1();
        Assert.That(1269, Is.EqualTo(result));
        Console.WriteLine($"Result: {result}");

        // 412 too low
    }

    [Test]
    public void Solution2Test() {
        var instance = new AoC2015Day22();
        var result = instance.Solution2();
        Assert.That(1309, Is.EqualTo(result));
        Console.WriteLine($"Result: {result}");
    }
}