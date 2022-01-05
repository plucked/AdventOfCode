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
        Assert.AreEqual(1269, result);
        Console.WriteLine($"Result: {result}");

        // 412 too low
    }

    [Test]
    public void Solution2Test() {
        var instance = new AoC2015Day22();
        var result = instance.Solution2();
        Assert.AreEqual(1309, result);
        Console.WriteLine($"Result: {result}");
    }
}