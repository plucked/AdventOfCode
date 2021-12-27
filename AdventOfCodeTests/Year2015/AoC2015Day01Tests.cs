using System;
using AdventOfCode.Year2015;
using NUnit.Framework;

namespace AdventOfCodeTests.Year2015;

[TestFixture]
public class AoC2015Day01Tests {
    [TestCase("(())", 0)]
    [TestCase("()()", 0)]
    [TestCase("(((", 3)]
    [TestCase("(()(()(", 3)]
    [TestCase("))(((((", 3)]
    [TestCase("())", -1)]
    [TestCase("))(", -1)]
    [TestCase(")))", -3)]
    [TestCase(")())())", -3)]
    public void Samples(string input, long expect) {
        var day1 = new AoC2015Day01();
        day1.Setup(input);
        Assert.AreEqual(expect, day1.Solution1());
    }

    [Test]
    public void Solution1Test() {
        var day1 = new AoC2015Day01();
        day1.Setup();
        Console.WriteLine(day1.Solution1());
    }

    [Test]
    public void Solution2Test() {
        var day1 = new AoC2015Day01();
        day1.Setup();
        Console.WriteLine(day1.Solution2());
    }
}