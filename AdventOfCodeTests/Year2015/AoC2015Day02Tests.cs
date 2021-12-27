using System;
using AdventOfCode.Year2015;
using NUnit.Framework;

namespace AdventOfCodeTests.Year2015;

[TestFixture]
public class AoC2015Day02Tests {
    [TestCase("2x3x4", 58)]
    [TestCase("1x1x10", 43)]
    public void SamplesDay1(string input, long expect) {
        var day2 = new AoC2015Day02();
        day2.Setup(new[] { input });
        Assert.AreEqual(expect, day2.Solution1());
    }

    [Test]
    public void Solution1Test() {
        var day2 = new AoC2015Day02();
        day2.Setup();
        Console.WriteLine(day2.Solution1());
    }

    [TestCase("2x3x4", 34)]
    [TestCase("1x1x10", 14)]
    public void SamplesDay2(string input, long expect) {
        var day2 = new AoC2015Day02();
        day2.Setup(new[] { input });
        Assert.AreEqual(expect, day2.Solution2());
    }

    [Test]
    public void Solution2Test() {
        var day2 = new AoC2015Day02();
        day2.Setup();
        Console.WriteLine(day2.Solution2());
    }
}