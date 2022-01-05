using System;
using AdventOfCode.Year2015;
using NUnit.Framework;

namespace AdventOfCodeTests.Year2015;

[TestFixture]
public class AoC2015Day10Tests {
    [TestCase("1", 6)]
    public void Solution1Example(string input, long expect) {
        var instance = new AoC2015Day10(input, 5);
        Assert.AreEqual(expect, instance.Solution1());
    }

    [Test]
    public void Solution1Test() {
        var instance = new AoC2015Day10(null, 40);
        var result = instance.Solution1();
        Assert.AreEqual(492982, result);
        Console.WriteLine($"Result: {result}");
    }

    [Test]
    public void Solution2Test() {
        var instance = new AoC2015Day10(null, 50);
        var result = instance.Solution1();
        Assert.AreEqual(6989950, result);
        Console.WriteLine($"Result: {result}");
    }
}