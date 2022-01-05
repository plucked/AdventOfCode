using System;
using AdventOfCode.Year2015;
using NUnit.Framework;

namespace AdventOfCodeTests.Year2015;

[TestFixture]
public class AoC2015Day24Tests {
    [TestCase(new long[] { 1, 2, 3, 4, 5, 7, 8, 9, 10, 11 }, 99)]
    public void Solution1Example(long[] input, long expected) {
        var instance = new AoC2015Day24(input);
        Assert.AreEqual(expected, instance.Solution1());
    }

    [Test]
    public void Solution1Test() {
        var instance = new AoC2015Day24();
        var result = instance.Solution1();
        Assert.AreEqual(11846773891, result);
        Console.WriteLine($"Result: {result}");
    }

    [TestCase(new long[] { 1, 2, 3, 4, 5, 7, 8, 9, 10, 11 }, 44)]
    public void Solution2Example(long[] input, long expected) {
        var instance = new AoC2015Day24(input);
        Assert.AreEqual(expected, instance.Solution2());
    }

    [Test]
    public void Solution2Test() {
        var instance = new AoC2015Day24();
        var result = instance.Solution2();
        Assert.AreEqual(80393059, result);
        Console.WriteLine($"Result: {result}");
    }
}