using System;
using AdventOfCode.Year2015;
using NUnit.Framework;

namespace AdventOfCodeTests.Year2015;

[TestFixture]
public class AoC2015Day07Tests {
    [Test]
    public void Solution1Example() {
        var instance = new AoC2015Day07();
        instance.Setup(
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

        Assert.AreEqual(72, result["d"]);
        Assert.AreEqual(507, result["e"]);
        Assert.AreEqual(492, result["f"]);
        Assert.AreEqual(114, result["g"]);
        Assert.AreEqual(65412, result["h"]);
        Assert.AreEqual(65079, result["i"]);
        Assert.AreEqual(123, result["x"]);
        Assert.AreEqual(456, result["y"]);
    }

    [Test]
    public void Solution1Test() {
        var instance = new AoC2015Day07();
        instance.Setup();
        var result = instance.Solution1()["a"];
        Assert.AreEqual(46065, result);
        Console.WriteLine($"Result: {result}");
    }

    [Test]
    public void Solution2Test() {
        var instance = new AoC2015Day07();
        instance.Setup();
        var result = instance.Solution2()["a"];
        Assert.AreEqual(14134, result);
        Console.WriteLine($"Result: {result}");
    }
}