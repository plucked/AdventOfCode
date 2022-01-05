using System;
using AdventOfCode.Year2015;
using NUnit.Framework;

namespace AdventOfCodeTests.Year2015;

[TestFixture]
public class AoC2015Day03Tests {
    [TestCase(">", 2)]
    [TestCase("^>v<", 4)]
    [TestCase("^v^v^v^v^v", 2)]
    public void SamplesSolution1(string input, long expect) {
        var instance = new AoC2015Day03(input);
        Assert.AreEqual(expect, instance.Solution1());
    }

    [Test]
    public void Solution1Test() {
        var instance = new AoC2015Day03();
        var result = instance.Solution1();
        Assert.AreEqual(2081, result);
        Console.WriteLine($"Result: {result}");
    }

    [TestCase("^v", 3)]
    [TestCase("^>v<", 3)]
    [TestCase("^v^v^v^v^v", 11)]
    public void SamplesSolution2(string input, long expect) {
        var instance = new AoC2015Day03(input);
        Assert.AreEqual(expect, instance.Solution2());
    }

    [Test]
    public void Solution2Test() {
        var instance = new AoC2015Day03();
        var result = instance.Solution2();
        Assert.AreEqual(2341, result);
        Console.WriteLine($"Result: {result}");
    }
}