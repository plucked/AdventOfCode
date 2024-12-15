using System;
using AdventOfCode.Year2015;
using NUnit.Framework;

namespace AdventOfCodeTests.Year2015;

[TestFixture]
public class AoC2015Day07Tests {
    [Test]
    public void Solution1Example() {
        var instance = new AoC2015Day07(
                @"123 -> x
456 -> y
x AND y -> d
x OR y -> e
x LSHIFT 2 -> f
y RSHIFT 2 -> g
NOT x -> h
NOT y -> i".Split(Environment.NewLine));
        var result = instance.Solution1();
        foreach (var v in result) {
            Console.WriteLine($"{v.Key}={v.Value}");
        }

        Assert.That(72, Is.EqualTo(result["d"]));
        Assert.That(507, Is.EqualTo(result["e"]));
        Assert.That(492, Is.EqualTo(result["f"]));
        Assert.That(114, Is.EqualTo(result["g"]));
        Assert.That(65412, Is.EqualTo(result["h"]));
        Assert.That(65079, Is.EqualTo(result["i"]));
        Assert.That(123, Is.EqualTo(result["x"]));
        Assert.That(456, Is.EqualTo(result["y"]));
    }

    [Test]
    public void Solution1Test() {
        var instance = new AoC2015Day07();
        var result = instance.Solution1()["a"];
        Assert.That(46065, Is.EqualTo(result));
        Console.WriteLine($"Result: {result}");
    }

    [Test]
    public void Solution2Test() {
        var instance = new AoC2015Day07();
        var result = instance.Solution2()["a"];
        Assert.That(14134, Is.EqualTo(result));
        Console.WriteLine($"Result: {result}");
    }
}