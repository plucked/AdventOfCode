using System;
using AdventOfCode.Year2015;
using NUnit.Framework;

namespace AdventOfCodeTests.Year2015;

[TestFixture]
public class AoC2015Day17Tests {
    [TestCase(new[] { 20, 15, 10, 5, 5 }, 4)]
    public void Solution1Example(int[] buckets, long expected) {
        var instance = new AoC2015Day17(buckets, 25);
        Assert.That(expected, Is.EqualTo(instance.Solution1()));
    }

    [Test]
    public void Solution1Test() {
        var instance = new AoC2015Day17();
        var result = instance.Solution1();
        Assert.That(1638, Is.EqualTo(result));
        Console.WriteLine($"Result: {result}");
    }

    [TestCase(new[] { 20, 15, 10, 5, 5 }, 3)]
    public void Solution2Example(int[] buckets, long expected) {
        var instance = new AoC2015Day17(buckets, 25);
        Assert.That(expected, Is.EqualTo(instance.Solution2()));
    }

    [Test]
    public void Solution2Test() {
        var instance = new AoC2015Day17();
        var result = instance.Solution2();
        Assert.That(17, Is.EqualTo(result));
        Console.WriteLine($"Result: {result}");
    }
}