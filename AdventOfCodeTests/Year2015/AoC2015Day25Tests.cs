using System;
using AdventOfCode.Year2015;
using NUnit.Framework;

namespace AdventOfCodeTests.Year2015;

[TestFixture]
public class AoC2015Day25Tests {
    [Test]
    public void Solution1Test() {
        var instance = new AoC2015Day25();
        var result = instance.Solution1();
        Assert.AreEqual(8997277, result);
        Console.WriteLine($"Result: {result}");
    }
}