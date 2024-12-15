using System;
using AdventOfCode.Year2015;
using NUnit.Framework;

namespace AdventOfCodeTests.Year2015;

[TestFixture]
public class AoC2015Day06Tests {
    [TestCase("turn on 0,0 through 999,999", 1_000_000)]
    [TestCase("toggle 0,0 through 999,0", 1_000)]
    public void SamplesSolution1(string input, long expect) {
        var instance = new AoC2015Day06(new[] { input });
        Assert.That(expect, Is.EqualTo(instance.Solution1()));
    }

    [Test]
    public void Solution1Test() {
        var instance = new AoC2015Day06();
        var result = instance.Solution1();
        Assert.That(569999, Is.EqualTo(result));
        Console.WriteLine($"Result: {result}");
    }

    [Test]
    public void Solution2Test() {
        var instance = new AoC2015Day06();
        var result = instance.Solution2();
        Assert.That(17836115, Is.EqualTo(result));
        Console.WriteLine($"Result: {result}");
    }
}