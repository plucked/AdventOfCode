using System;
using AdventOfCode.Year2015;
using NUnit.Framework;

namespace AdventOfCodeTests.Year2015;

[TestFixture]
public class AoC2015Day05Tests {
    [TestCase("ugknbfddgicrmopn", 1)]
    [TestCase("aaa", 1)]
    [TestCase("jchzalrnumimnmhp", 0)]
    [TestCase("haegwjzuvuyypxyu", 0)]
    [TestCase("dvszwmarrgswjxmb", 0)]
    public void SamplesSolution1(string input, long expect) {
        var instance = new AoC2015Day05(new[] { input });
        Assert.AreEqual(expect, instance.Solution1());
    }

    [Test]
    public void Solution1Test() {
        var instance = new AoC2015Day05();
        var result = instance.Solution1();
        Assert.AreEqual(238, result);
        Console.WriteLine($"Result: {result}");
    }

    [TestCase("qjhvhtzxzqqjkmpb", 1)]
    [TestCase("xxyxx", 1)]
    [TestCase("uurcxstgmygtbstg", 0)]
    [TestCase("ieodomkazucvgmuy", 0)]
    public void SamplesSolution2(string input, long expect) {
        var instance = new AoC2015Day05(new[] { input });
        Assert.AreEqual(expect, instance.Solution2());
    }

    [Test]
    public void Solution2Test() {
        var instance = new AoC2015Day05();
        var result = instance.Solution2();
        Assert.AreEqual(69, result);
        Console.WriteLine($"Result: {result}");
    }
}