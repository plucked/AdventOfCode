using System;
using AdventOfCode.Year2022;
using NUnit.Framework;

namespace AdventOfCodeTests.Year2022;

[TestFixture]
public class AoC2022Day02Tests {
    [TestCase("A Y\nB X\nC Z", 15)]
    public void Solution1Examples(string input, long expect) {
        var instance = new AoC2022Day02(input);
        Assert.That(expect, Is.EqualTo(instance.Solution1()));
    }

    [Test]
    public void Solution1Test() {
        var instance = new AoC2022Day02();
        var result = instance.Solution1();
        Assert.That(13009, Is.EqualTo(result));
        Console.WriteLine($"Result: {result}");
    }

    [TestCase("A Y\nB X\nC Z", 12)]
    public void Solution2Examples(string input, long expect) {
        var instance = new AoC2022Day02(input);
        Assert.That(expect, Is.EqualTo(instance.Solution2()));
    }

    [Test]
    public void Solution2Test() {
        var instance = new AoC2022Day02();
        var result = instance.Solution2();
        Assert.That(10398, Is.EqualTo(result));
        Console.WriteLine($"Result: {result}");
    }
}