using System;
using AdventOfCode.Year2022;
using NUnit.Framework;

namespace AdventOfCodeTests.Year2022;

[TestFixture]
public class AoC2022Day07Tests {
    [TestCase(
            "$ cd /\n$ ls\ndir a\n14848514 b.txt\n8504156 c.dat\ndir d\n$ cd a\n$ ls\ndir e\n29116 f\n2557 g\n62596 h.lst\n$ cd e\n$ ls\n584 i\n$ cd ..\n$ cd ..\n$ cd d\n$ ls\n4060174 j\n8033020 d.log\n5626152 d.ext\n7214296 k",
            95437)]
    public void Solution1Examples(string input, long expect) {
        var instance = new AoC2022Day07(input);
        Assert.AreEqual(expect, instance.Solution1());
    }

    [Test]
    public void Solution1Test() {
        var instance = new AoC2022Day07();
        var result = instance.Solution1();
        Assert.AreEqual(1334506, result);
        Console.WriteLine($"Result: {result}");
    }

    [TestCase(
            "$ cd /\n$ ls\ndir a\n14848514 b.txt\n8504156 c.dat\ndir d\n$ cd a\n$ ls\ndir e\n29116 f\n2557 g\n62596 h.lst\n$ cd e\n$ ls\n584 i\n$ cd ..\n$ cd ..\n$ cd d\n$ ls\n4060174 j\n8033020 d.log\n5626152 d.ext\n7214296 k",
            24933642)]
    public void Solution2Examples(string input, long expect) {
        var instance = new AoC2022Day07(input);
        Assert.AreEqual(expect, instance.Solution2());
    }

    [Test]
    public void Solution2Test() {
        var instance = new AoC2022Day07();
        var result = instance.Solution2();
        Assert.AreEqual(7421137, result);
        Console.WriteLine($"Result: {result}");
    }
}