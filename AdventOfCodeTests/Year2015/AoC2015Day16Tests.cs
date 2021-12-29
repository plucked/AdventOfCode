using System;
using AdventOfCode.Year2015;
using NUnit.Framework;

namespace AdventOfCodeTests.Year2015;

[TestFixture]
public class AoC2015Day16Tests {
    [Test]
    public void Solution1Test() {
        var instance = new AoC2015Day16();
        instance.Setup();
        var result = instance.Solution1();
        Assert.AreEqual(213, result);
        Console.WriteLine($"Result: {result}");
    }

    [Test]
    public void Solution2Test() {
        var instance = new AoC2015Day16();
        instance.Setup();
        var result = instance.Solution2();
        Assert.AreEqual(323, result);
        Console.WriteLine($"Result: {result}");
    }
}