using System;
using AdventOfCode.Year2015;
using NUnit.Framework;

namespace AdventOfCodeTests.Year2015;

[TestFixture]
public class AoC2015Day11Tests {
    [TestCase("abcdefgh", "abcdffaa")]
    [TestCase("ghijklmn", "ghjaabcc")]
    public void Solution1Example(string input, string expect) {
        var instance = new AoC2015Day11(input);
        Assert.AreEqual(expect, instance.Solution1());
    }

    [Test]
    public void Solution1Test() {
        var instance = new AoC2015Day11();
        var result = instance.Solution1();
        Assert.AreEqual("hxbxxyzz", result);
        Console.WriteLine($"Result: {result}");
    }

    [Test]
    public void Solution2Test() {
        var instance = new AoC2015Day11();
        var result = instance.Solution2();
        Assert.AreEqual("hxcaabcc", result);
        Console.WriteLine($"Result: {result}");
    }
}