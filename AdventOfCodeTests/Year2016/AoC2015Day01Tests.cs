using System;
using AdventOfCode.Year2016;
using NUnit.Framework;

namespace AdventOfCodeTests.Year2016;

[TestFixture]
public class AoC2016Day01Tests {
    [TestCase("R2, L3", 5)]
    [TestCase("R2, R2, R2", 2)]
    [TestCase("R5, L5, R5, R3", 12)]
    public void Solution1Examples(string input, long expect) {
        var instance = new AoC2016Day01(input);
        Assert.AreEqual(expect, instance.Solution1());
    }

    [Test]
    public void Solution1Test() {
        var instance = new AoC2016Day01();
        var result = instance.Solution1();
        Assert.AreEqual(300, result);
        Console.WriteLine($"Result: {result}");
    }

    [TestCase("R8, R4, R4, R8", 4)]
    public void Solution2Examples(string input, long expect) {
        var instance = new AoC2016Day01(input);
        Assert.AreEqual(expect, instance.Solution2());
    }

    [Test]
    public void Solution2Test() {
        var instance = new AoC2016Day01();
        var result = instance.Solution2();
        Assert.AreEqual(159, result);
        Console.WriteLine($"Result: {result}");
    }
}