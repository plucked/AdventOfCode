using System;
using AdventOfCode.Year2015;
using NUnit.Framework;

namespace AdventOfCodeTests.Year2015;

[TestFixture]
public class AoC2015Day08Tests {
    [Test]
    public void Solution1Test() {
        var instance = new AoC2015Day08();
        var result = instance.Solution1();
        Assert.That(1371, Is.EqualTo(result));
        Console.WriteLine($"Result: {result}");
    }

    [TestCase(new[] { @"""""", "\"abc\"", "\"aaa\\\"aaa\"", "\"\\x27\"" }, 19)]
    public void Solution2Example(string[] input, long expect) {
        var instance = new AoC2015Day08(input);
        Assert.That(expect, Is.EqualTo(instance.Solution2()));
    }

    [Test]
    public void Solution2Test() {
        var instance = new AoC2015Day08();
        var result = instance.Solution2();
        Assert.That(2117, Is.EqualTo(result));
        Console.WriteLine($"Result: {result}");
    }
}