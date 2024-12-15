using System;
using AdventOfCode.Year2022;
using NUnit.Framework;

namespace AdventOfCodeTests.Year2022;

[TestFixture]
public class AoC2022Day05Tests {
    [TestCase(
            @"    [D]    
[N] [C]    
[Z] [M] [P]
 1   2   3 

move 1 from 2 to 1
move 3 from 1 to 3
move 2 from 2 to 1
move 1 from 1 to 2",
            "CMZ")]
    public void Solution1Examples(string input, string expect) {
        var instance = new AoC2022Day05(input);
        Assert.That(expect, Is.EqualTo(instance.Solution1()));
    }

    [Test]
    public void Solution1Test() {
        var instance = new AoC2022Day05();
        var result = instance.Solution1();
        Assert.That("QNHWJVJZW", Is.EqualTo(result));
        Console.WriteLine($"Result: {result}");
    }

    [TestCase(
            @"    [D]    
[N] [C]    
[Z] [M] [P]
 1   2   3 

move 1 from 2 to 1
move 3 from 1 to 3
move 2 from 2 to 1
move 1 from 1 to 2",
            "MCD")]
    public void Solution2Examples(string input, string expect) {
        var instance = new AoC2022Day05(input);
        Assert.That(expect, Is.EqualTo(instance.Solution2()));
    }

    [Test]
    public void Solution2Test() {
        var instance = new AoC2022Day05();
        var result = instance.Solution2();
        Assert.That("BPCZJLFJW", Is.EqualTo(result));
        Console.WriteLine($"Result: {result}");
    }
}