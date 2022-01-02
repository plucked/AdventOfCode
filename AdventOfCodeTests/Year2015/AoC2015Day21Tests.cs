using System;
using AdventOfCode.Year2015;
using NUnit.Framework;

namespace AdventOfCodeTests.Year2015;

[TestFixture]
public class AoC2015Day21Tests {
    [Test]
    public void Solution1Test() {
        var instance = new AoC2015Day21();
        var result = instance.Solution1();
        Assert.AreEqual(91, result);
        Console.WriteLine($"Result: {result}");
    }

    [Test]
    public void Solution2Test() {
        var instance = new AoC2015Day21();
        instance.Setup();
        var result = instance.Solution2();
        Assert.AreEqual(158, result);
        Console.WriteLine($"Result: {result}");
    }
}