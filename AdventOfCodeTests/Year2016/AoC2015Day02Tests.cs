using System;
using AdventOfCode.Year2016;
using NUnit.Framework;

namespace AdventOfCodeTests.Year2016;

[TestFixture]
public class AoC2015Day02Tests {
    [TestCase(
            @"ULL
RRDDD
LURDL
UUUUD",
            1985)]
    public void Solution1Example(string input, long expected) {
        var instance = new AoC2016Day02(input.Split(Environment.NewLine));
        var result = instance.Solution1();
        Assert.AreEqual(expected, result);
        Console.WriteLine($"Result: {result}");
    }

    [Test]
    public void Solution1Test() {
        var instance = new AoC2016Day02();
        var result = instance.Solution1();
        Assert.AreEqual(24862, result);
        Console.WriteLine($"Result: {result}");
    }

    [TestCase(
            @"ULL
RRDDD
LURDL
UUUUD",
            "5DB3")]
    public void Solution2Example(string input, string expected) {
        var instance = new AoC2016Day02(input.Split(Environment.NewLine));
        var result = instance.Solution2();
        Assert.AreEqual(expected, result);
        Console.WriteLine($"Result: {result}");
    }

    [Test]
    public void Solution2Test() {
        var instance = new AoC2016Day02();
        var result = instance.Solution2();
        Assert.AreEqual("46C91", result);
        Console.WriteLine($"Result: {result}");
    }
}