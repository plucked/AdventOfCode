using System;
using AdventOfCode.Year2015;
using NUnit.Framework;

namespace AdventOfCodeTests.Year2015;

[TestFixture]
public class AoC2015Day09Tests {
    [TestCase(new[] { "London to Dublin = 464", "London to Belfast = 518", "Dublin to Belfast = 141" }, 605)]
    public void Solution1Example(string[] input, long expect) {
        var instance = new AoC2015Day09(input);
        Assert.That(expect, Is.EqualTo(instance.Solution1()));
    }

    [Test]
    public void Solution1Test() {
        var instance = new AoC2015Day09();
        var result = instance.Solution1();
        Assert.That(251, Is.EqualTo(result));
        Console.WriteLine($"Result: {result}");
    }

    [Test]
    public void Solution2Test() {
        var instance = new AoC2015Day09();
        var result = instance.Solution2();
        Assert.That(898, Is.EqualTo(result));
        Console.WriteLine($"Result: {result}");
    }
}