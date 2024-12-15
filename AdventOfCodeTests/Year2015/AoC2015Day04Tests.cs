using System;
using AdventOfCode.Year2015;
using NUnit.Framework;

namespace AdventOfCodeTests.Year2015;

[TestFixture]
public class AoC2015Day04Tests {
    [TestCase("abcdef", 609043)]
    [TestCase("pqrstuv", 1048970)]
    public void SamplesSolution1(string input, long expect) {
        var instance = new AoC2015Day04(input);
        Assert.That(expect, Is.EqualTo(instance.Solution1()));
    }

    [Test]
    public void Solution1Test() {
        var instance = new AoC2015Day04();
        var result = instance.Solution1();
        Assert.That(346386, Is.EqualTo(result));
        Console.WriteLine($"Result: {result}");
    }

    [Test]
    public void Solution2Test() {
        var instance = new AoC2015Day04();
        var result = instance.Solution2();
        Assert.That(9958218, Is.EqualTo(result));
        Console.WriteLine($"Result: {result}");
    }
}