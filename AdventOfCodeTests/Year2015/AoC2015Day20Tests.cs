using System;
using AdventOfCode.Year2015;
using NUnit.Framework;

namespace AdventOfCodeTests.Year2015;

[TestFixture]
public class AoC2015Day20Tests {
    [TestCase(10, 1)]
    [TestCase(30, 2)]
    [TestCase(40, 3)]
    [TestCase(60, 5)]
    [TestCase(70, 4)]
    [TestCase(80, 7)]
    [TestCase(120, 6)]
    [TestCase(130, 9)]
    [TestCase(140, 13)]
    [TestCase(150, 8)]
    [TestCase(180, 10)]
    [TestCase(200, 19)]
    [TestCase(240, 14)]
    [TestCase(280, 12)]
    [TestCase(300, 29)]
    [TestCase(310, 16)]
    [TestCase(320, 21)]
    [TestCase(360, 22)]
    [TestCase(380, 37)]
    [TestCase(390, 18)]
    [TestCase(400, 27)]
    [TestCase(420, 20)]
    [TestCase(440, 43)]
    [TestCase(480, 33)]
    [TestCase(540, 34)]
    [TestCase(560, 28)]
    [TestCase(570, 49)]
    [TestCase(600, 24)]
    [TestCase(630, 32)]
    [TestCase(720, 30)]
    [TestCase(780, 45)]
    [TestCase(840, 44)]
    [TestCase(900, 40)]
    [TestCase(910, 36)]
    [TestCase(930, 50)]
    [TestCase(960, 42)]
    [TestCase(1240, 48)]
    public void Solution1Example(int presents, long expected) {
        var instance = new AoC2015Day20();
        Assert.AreEqual(expected, instance.Solution1(presents, true));
    }

    [Test]
    public void Solution1Test() {
        var instance = new AoC2015Day20();
        var result = instance.Solution1();
        Assert.AreEqual(665280, result);
        Console.WriteLine($"Result: {result}");
    }

    [Test]
    public void Solution2Test() {
        var instance = new AoC2015Day20();
        instance.Setup();
        var result = instance.Solution2();
        Assert.AreEqual(705600, result);
        Console.WriteLine($"Result: {result}");
    }
}