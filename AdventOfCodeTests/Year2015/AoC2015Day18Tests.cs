using System;
using AdventOfCode.Year2015;
using NUnit.Framework;

namespace AdventOfCodeTests.Year2015;

[TestFixture]
public class AoC2015Day18Tests {
    [TestCase(
            @".#.#.#
...##.
#....#
..#...
#.#..#
####..",
            4)]
    public void Solution1Example(string input, long expected) {
        var instance = new AoC2015Day18(input.Split(Environment.NewLine), 4);
        Assert.AreEqual(expected, instance.Solution1());
    }

    [Test]
    public void Solution1Test() {
        var instance = new AoC2015Day18();
        var result = instance.Solution1();
        Assert.AreEqual(821, result);
        Console.WriteLine($"Result: {result}");
    }

    [TestCase(
            @".#.#.#
...##.
#....#
..#...
#.#..#
####..",
            17)]
    public void Solution2Example(string input, long expected) {
        var instance = new AoC2015Day18(input.Split(Environment.NewLine), 5);
        Assert.AreEqual(expected, instance.Solution2());
    }

    [Test]
    public void Solution2Test() {
        var instance = new AoC2015Day18();
        var result = instance.Solution2();
        Assert.AreEqual(886, result);
        Console.WriteLine($"Result: {result}");
    }
}