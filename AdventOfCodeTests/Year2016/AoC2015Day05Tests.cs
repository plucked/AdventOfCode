using System;
using AdventOfCode.Year2016;
using NUnit.Framework;

namespace AdventOfCodeTests.Year2016;

[TestFixture]
public class AoC2015Day05Tests {
    [TestCase("abc", "18f47a30")]
    public void Solution1Example(string input, string expected) {
        var instance = new AoC2016Day05(input);
        var result = instance.Solution1();
        Assert.That(expected, Is.EqualTo(result));
        Console.WriteLine($"Result: {result}");
    }

    [Test]
    public void Solution1Test() {
        var instance = new AoC2016Day05();
        var result = instance.Solution1();
        Assert.That("f97c354d", Is.EqualTo(result));
        Console.WriteLine($"Result: {result}");
    }

    [TestCase("abc", "05ace8e3")]
    public void Solution2Example(string input, string expected) {
        var instance = new AoC2016Day05(input);
        var result = instance.Solution2();
        Assert.That(expected, Is.EqualTo(result));
        Console.WriteLine($"Result: {result}");
    }

    [Test]
    public void Solution2Test() {
        var instance = new AoC2016Day05();
        var result = instance.Solution2();
        Assert.That("863dde27", Is.EqualTo(result));
        Console.WriteLine($"Result: {result}");
    }
}