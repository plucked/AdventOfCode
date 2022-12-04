using System;
using AdventOfCode.Year2022;
using NUnit.Framework;

namespace AdventOfCodeTests.Year2022;

[TestFixture]
public class AoC2022Day04Tests {
    [TestCase("2-4,6-8\n2-3,4-5\n5-7,7-9\n2-8,3-7\n6-6,4-6\n2-6,4-8", 2)]
    public void Solution1Examples(string input, long expect) {
        var instance = new AoC2022Day04(input);
        Assert.AreEqual(expect, instance.Solution1());
    }

    [Test]
    public void Solution1Test() {
        var instance = new AoC2022Day04();
        var result = instance.Solution1();
        Assert.AreEqual(515, result);
        Console.WriteLine($"Result: {result}");
    }

    [TestCase("2-4,6-8\n2-3,4-5\n5-7,7-9\n2-8,3-7\n6-6,4-6\n2-6,4-8", 4)]
    public void Solution2Examples(string input, long expect) {
        var instance = new AoC2022Day04(input);
        Assert.AreEqual(expect, instance.Solution2());
    }

    [Test]
    public void Solution2Test() {
        var instance = new AoC2022Day04();
        var result = instance.Solution2();
        Assert.AreEqual(883, result);
        Console.WriteLine($"Result: {result}");
    }
}