using System;
using AdventOfCode.Year2015;
using NUnit.Framework;

namespace AdventOfCodeTests.Year2015;

[TestFixture]
public class AoC2015Day14Tests {
    [TestCase(
            new[] { "Comet can fly 14 km/s for 10 seconds, but then must rest for 127 seconds.", "Dancer can fly 16 km/s for 11 seconds, but then must rest for 162 seconds." },
            1120)]
    public void Solution1Example(string[] input, long expect) {
        var instance = new AoC2015Day14(input, 1000);
        Assert.That(expect, Is.EqualTo(instance.Solution1()));
    }

    [Test]
    public void Solution1Test() {
        var instance = new AoC2015Day14();
        var result = instance.Solution1();
        Assert.That(2655, Is.EqualTo(result));
        Console.WriteLine($"Result: {result}");
    }

    [TestCase(
            new[] { "Comet can fly 14 km/s for 10 seconds, but then must rest for 127 seconds.", "Dancer can fly 16 km/s for 11 seconds, but then must rest for 162 seconds." },
            689)]
    public void Solution2Example(string[] input, long expect) {
        var instance = new AoC2015Day14(input, 1000);
        Assert.That(expect, Is.EqualTo(instance.Solution2()));
    }

    [Test]
    public void Solution2Test() {
        var instance = new AoC2015Day14();
        var result = instance.Solution2();
        Assert.That(1059, Is.EqualTo(result));
        Console.WriteLine($"Result: {result}");
    }
}