using System;
using AdventOfCode.Year2015;
using NUnit.Framework;

namespace AdventOfCodeTests.Year2015;

[TestFixture]
public class AoC2015Day19Tests {
    [TestCase(
            @"H => HO
H => OH
O => HH

HOH",
            4)]
    [TestCase(
            @"H => HO
H => OH
O => HH

HOHOHO",
            7)]
    public void Solution1Example(string input, long expected) {
        var instance = new AoC2015Day19();
        instance.Setup(input.Split(Environment.NewLine));
        Assert.AreEqual(expected, instance.Solution1());
    }

    [Test]
    public void Solution1Test() {
        var instance = new AoC2015Day19();
        instance.Setup();
        var result = instance.Solution1();
        Assert.AreEqual(509, result);
        Console.WriteLine($"Result: {result}");
    }
    
    [TestCase(
            @"e => H
e => O
H => HO
H => OH
O => HH

HOH",
            3)]
    [TestCase(
            @"e => H
e => O
H => HO
H => OH
O => HH

HOHOHO",
            6)]
    public void Solution2Example(string input, long expected) {
        var instance = new AoC2015Day19();
        instance.Setup(input.Split(Environment.NewLine));
        Assert.AreEqual(expected, instance.Solution2());
    }

    [Test]
    public void Solution2Test() {
        var instance = new AoC2015Day19();
        instance.Setup();
        var result = instance.Solution2();
        Assert.AreEqual(195, result);
        Console.WriteLine($"Result: {result}");
    }
}