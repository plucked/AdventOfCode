using System;
using AdventOfCode.Year2015;
using NUnit.Framework;

namespace AdventOfCodeTests.Year2015;

[TestFixture]
public class AoC2015Day08Tests {
    [Test]
    public void Solution1Test() {
        var instance = new AoC2015Day08();
        instance.Setup();
        var result = instance.Solution1();
        Assert.AreEqual(1371, result);
        Console.WriteLine($"Result: {result}");
    }

    [TestCase(new[] { @"""""", "\"abc\"", "\"aaa\\\"aaa\"", "\"\\x27\"" }, 19)]
    public void Solution2Example(string[] input, long expect) {
        var instance = new AoC2015Day08();
        instance.Setup(input);
        Assert.AreEqual(expect, instance.Solution2());
    }

    [Test]
    public void Solution2Test() {
        var instance = new AoC2015Day08();
        instance.Setup();
        var result = instance.Solution2();
        Assert.AreEqual(2117, result);
        Console.WriteLine($"Result: {result}");
    }
}