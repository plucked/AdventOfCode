using AdventOfCode.Year2024;
using System;
using NUnit.Framework;

namespace AdventOfCodeTests.Year2024;

[TestFixture]
public class AoC2024Day01Tests {
    [Test]
    public void Solution1Examples()
    {
        string input = """
                       3   4
                       4   3
                       2   5
                       1   3
                       3   9
                       3   3
                       """;
        
        long expect = 11;
        
        var instance = new AoC2024Day01(input);
        Assert.That(instance.Solution1(), Is.EqualTo(expect));
    }

    [Test]
    public void Solution1Test() {
        var instance = new AoC2024Day01();
        var result = instance.Solution1();
        Assert.That(result, Is.EqualTo(1319616));
        Console.WriteLine($"Result: {result}");
    }

    [Test]
    public void Solution2Examples() {
        string input = """
                       3   4
                       4   3
                       2   5
                       1   3
                       3   9
                       3   3
                       """;
        
        long expect = 31;
        
        var instance = new AoC2024Day01(input);
        Assert.That(expect, Is.EqualTo(instance.Solution2()));
    }

    [Test]
    public void Solution2Test() {
        var instance = new AoC2024Day01();
        var result = instance.Solution2();
        Assert.That(result, Is.EqualTo(27267728));
        Console.WriteLine($"Result: {result}");
    }
}