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
        Assert.That(expect, Is.EqualTo(instance.Solution1()));
    }

    [Test]
    public void Solution1Test() {
        var instance = new AoC2015Day11();
        var result = instance.Solution1();
        Assert.That("hxbxxyzz", Is.EqualTo(result));
        Console.WriteLine($"Result: {result}");
    }

    [Test]
    public void Solution2Test() {
        var instance = new AoC2015Day11();
        var result = instance.Solution2();
        Assert.That("hxcaabcc", Is.EqualTo(result));
        Console.WriteLine($"Result: {result}");
    }
}