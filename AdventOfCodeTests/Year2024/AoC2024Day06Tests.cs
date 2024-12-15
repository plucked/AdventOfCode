using AdventOfCode.Year2024;
using System;
using NUnit.Framework;

namespace AdventOfCodeTests.Year2024;

[TestFixture]
public class AoC2024Day06Tests {
    [Test]
    public void Solution1Examples()
    {
        string input = """
                       ....#.....
                       .........#
                       ..........
                       ..#.......
                       .......#..
                       ..........
                       .#..^.....
                       ........#.
                       #.........
                       ......#...
                       """;
        
        long expect = 41;
        
        var instance = new AoC2024Day06(input);
        Assert.That(instance.Solution1(), Is.EqualTo(expect));
    }

    [Test]
    public void Solution1Test() {
        var instance = new AoC2024Day06();
        var result = instance.Solution1();
        Assert.That(result, Is.EqualTo(4454));
        Console.WriteLine($"Result: {result}");
    }

    [Test]
    public void Solution2Examples() {
        string input = """
                       ....#.....
                       .........#
                       ..........
                       ..#.......
                       .......#..
                       ..........
                       .#..^.....
                       ........#.
                       #.........
                       ......#...
                       """;
        
        long expect = 6;
        
        var instance = new AoC2024Day06(input);
        Assert.That(instance.Solution2(), Is.EqualTo(expect));
    }

    [Test]
    public void Solution2Test() {
        var instance = new AoC2024Day06();
        var result = instance.Solution2();
        Assert.That(result, Is.EqualTo(1503));
        Console.WriteLine($"Result: {result}");
    }
}