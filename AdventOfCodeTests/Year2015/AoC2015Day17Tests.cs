using System;
using AdventOfCode.Year2015;
using NUnit.Framework;

namespace AdventOfCodeTests.Year2015;

[TestFixture]
public class AoC2015Day17Tests {
    [TestCase(new[] { 20, 15, 10, 5, 5 }, 4)]
    public void Solution1Example(int[] buckets, long expected) {
        var instance = new AoC2015Day17();
        instance.Setup(buckets);
        Assert.AreEqual(expected, instance.Solution1(25));
    }

    [Test]
    public void Solution1Test() {
        var instance = new AoC2015Day17();
        instance.Setup();
        var result = instance.Solution1();
        Assert.AreEqual(1638, result);
        Console.WriteLine($"Result: {result}");
    }

    [TestCase(new[] { 20, 15, 10, 5, 5 }, 3)]
    public void Solution2Example(int[] buckets, long expected) {
        var instance = new AoC2015Day17();
        instance.Setup(buckets);
        Assert.AreEqual(expected, instance.Solution2(25));
    }

    [Test]
    public void Solution2Test() {
        var instance = new AoC2015Day17();
        instance.Setup();
        var result = instance.Solution2();
        Assert.AreEqual(17, result);
        Console.WriteLine($"Result: {result}");
    }
}