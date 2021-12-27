using System;
using AdventOfCode.Year2015;
using NUnit.Framework;

namespace AdventOfCodeTests.Year2015;

[TestFixture]
public class AoC2015Day02Tests {
    [TestCase("2x3x4", 58)]
    [TestCase("1x1x10", 43)]
    public void SamplesDay1(string input, long expect) {
        var instance = new AoC2015Day02();
        instance.Setup(new[] { input });
        Assert.AreEqual(expect, instance.Solution1());
    }

    [Test]
    public void Solution1Test() {
        var instance = new AoC2015Day02();
        instance.Setup();
        var result = instance.Solution1();
        Assert.AreEqual(1606483, result);
        Console.WriteLine($"Result: {result}");
    }

    [TestCase("2x3x4", 34)]
    [TestCase("1x1x10", 14)]
    public void SamplesDay2(string input, long expect) {
        var instance = new AoC2015Day02();
        instance.Setup(new[] { input });
        Assert.AreEqual(expect, instance.Solution2());
    }

    [Test]
    public void Solution2Test() {
        var instance = new AoC2015Day02();
        instance.Setup();
        var result = instance.Solution2();
        Assert.AreEqual(3842356, result);
        Console.WriteLine($"Result: {result}");
    }
}