using System;
using AdventOfCode.Year2022;
using NUnit.Framework;

namespace AdventOfCodeTests.Year2022;

[TestFixture]
public class AoC2022Day08Tests {
    [TestCase("30373\n25512\n65332\n33549\n35390", 21)]
    public void Solution1Examples(string input, long expect) {
        var instance = new AoC2022Day08(input);
        Assert.AreEqual(expect, instance.Solution1());
    }

    [Test]
    public void Solution1Test() {
        var instance = new AoC2022Day08();
        var result = instance.Solution1();
        Assert.AreEqual(1835, result);
        Console.WriteLine($"Result: {result}");
    }

    [TestCase("30373\n25512\n65332\n33549\n35390", 8)]
    public void Solution2Examples(string input, long expect) {
        var instance = new AoC2022Day08(input);
        Assert.AreEqual(expect, instance.Solution2());
    }

    [Test]
    public void Solution2Test() {
        var instance = new AoC2022Day08();
        var result = instance.Solution2();
        Assert.AreEqual(263670, result);
        Console.WriteLine($"Result: {result}");
    }
}