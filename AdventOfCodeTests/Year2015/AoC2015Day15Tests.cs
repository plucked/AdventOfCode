using System;
using AdventOfCode.Year2015;
using NUnit.Framework;

namespace AdventOfCodeTests.Year2015;

[TestFixture]
public class AoC2015Day15Tests {
    [TestCase(
            new[] { "Butterscotch: capacity -1, durability -2, flavor 6, texture 3, calories 8", "Cinnamon: capacity 2, durability 3, flavor -2, texture -1, calories 3" },
            62842880)]
    public void Solution1Example(string[] input, long expect) {
        var instance = new AoC2015Day15(input);
        Assert.AreEqual(expect, instance.Solution1());
    }

    [Test]
    public void Solution1Test() {
        var instance = new AoC2015Day15();
        var result = instance.Solution1();
        Assert.AreEqual(222870, result);
        Console.WriteLine($"Result: {result}");
    }

    [TestCase(
            new[] { "Butterscotch: capacity -1, durability -2, flavor 6, texture 3, calories 8", "Cinnamon: capacity 2, durability 3, flavor -2, texture -1, calories 3" },
            57600000)]
    public void Solution2Example(string[] input, long expect) {
        var instance = new AoC2015Day15(input);
        Assert.AreEqual(expect, instance.Solution2());
    }

    [Test]
    public void Solution2Test() {
        var instance = new AoC2015Day15();
        var result = instance.Solution2();
        Assert.AreEqual(117936, result);
        Console.WriteLine($"Result: {result}");
    }
}