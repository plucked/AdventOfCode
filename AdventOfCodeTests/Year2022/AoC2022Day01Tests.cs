using System;
using AdventOfCode.Year2022;
using NUnit.Framework;

namespace AdventOfCodeTests.Year2022;

[TestFixture]
public class AoC2022Day01Tests {
    [TestCase("1000\n2000\n3000\n\n4000\n\n5000\n6000\n\n7000\n8000\n9000\n\n10000", 24000)]
    public void Solution1Examples(string input, long expect) {
        var instance = new AoC2022Day01(input);
        Assert.AreEqual(expect, instance.Solution1());
    }

    [Test]
    public void Solution1Test() {
        var instance = new AoC2022Day01();
        var result = instance.Solution1();
        Assert.AreEqual(74394, result);
        Console.WriteLine($"Result: {result}");
    }

    [TestCase("1000\n2000\n3000\n\n4000\n\n5000\n6000\n\n7000\n8000\n9000\n\n10000", 45000)]
    public void Solution2Examples(string input, long expect) {
        var instance = new AoC2022Day01(input);
        Assert.AreEqual(expect, instance.Solution2());
    }

    [Test]
    public void Solution2Test() {
        var instance = new AoC2022Day01();
        var result = instance.Solution2();
        Assert.AreEqual(212836, result);
        Console.WriteLine($"Result: {result}");
    }
}