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
        var instance = new AoC2015Day19(input.Split(Environment.NewLine));
        Assert.That(expected, Is.EqualTo(instance.Solution1()));
    }

    [Test]
    public void Solution1Test() {
        var instance = new AoC2015Day19();
        var result = instance.Solution1();
        Assert.That(509, Is.EqualTo(result));
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
        var instance = new AoC2015Day19(input.Split(Environment.NewLine));
        Assert.That(expected, Is.EqualTo(instance.Solution2()));
    }

    [Test]
    public void Solution2Test() {
        var instance = new AoC2015Day19();
        var result = instance.Solution2();
        Assert.That(195, Is.EqualTo(result));
        Console.WriteLine($"Result: {result}");
    }
}