using AdventOfCode.Year2024;
using System;
using NUnit.Framework;

namespace AdventOfCodeTests.Year2024;

[TestFixture]
public class AoC2024Day07Tests {
    [Test]
    public void Solution1Examples()
    {
        string input = """
                       190: 10 19
                       3267: 81 40 27
                       83: 17 5
                       156: 15 6
                       7290: 6 8 6 15
                       161011: 16 10 13
                       192: 17 8 14
                       21037: 9 7 18 13
                       292: 11 6 16 20
                       """;
        
        long expect = 3749;
        
        var instance = new AoC2024Day07(input);
        Assert.That(instance.Solution1(), Is.EqualTo(expect));
    }

    [Test]
    public void Solution1Test() {
        var instance = new AoC2024Day07();
        var result = instance.Solution1();
        Assert.That(result, Is.EqualTo(7710205485870));
        Console.WriteLine($"Result: {result}");
    }

    [Test]
    public void Solution2Examples() {
        string input = """
                       190: 10 19
                       3267: 81 40 27
                       83: 17 5
                       156: 15 6
                       7290: 6 8 6 15
                       161011: 16 10 13
                       192: 17 8 14
                       21037: 9 7 18 13
                       292: 11 6 16 20
                       """;
        
        long expect = 11387;
        
        var instance = new AoC2024Day07(input);
        Assert.That(instance.Solution2(), Is.EqualTo(expect));
    }

    [Test]
    public void Solution2Test() {
        var instance = new AoC2024Day07();
        var result = instance.Solution2();
        Assert.That(result, Is.EqualTo(20928985450275));
        Console.WriteLine($"Result: {result}");
    }
}