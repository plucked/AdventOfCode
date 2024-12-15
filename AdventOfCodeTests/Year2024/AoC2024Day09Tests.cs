using AdventOfCode.Year2024;
using System;
using NUnit.Framework;

namespace AdventOfCodeTests.Year2024;

[TestFixture]
public class AoC2024Day09Tests {
    [Test]
    public void Solution1Examples()
    {
        string input = """
                       2333133121414131402
                       """;
        
        long expect = 1928;
        
        var instance = new AoC2024Day09(input);
        Assert.That(instance.Solution1(), Is.EqualTo(expect));
    }

    [Test]
    public void Solution1Test() {
        var instance = new AoC2024Day09();
        var result = instance.Solution1();
        Assert.That(result, Is.EqualTo(6435922584968));
        Console.WriteLine($"Result: {result}");
    }

    [Test]
    public void Solution2Examples() {
        string input = """
                       2333133121414131402
                       """;
        
        long expect = 2858;
        
        var instance = new AoC2024Day09(input);
        Assert.That(instance.Solution2(), Is.EqualTo(expect));
    }

    [Test]
    public void Solution2Test() {
        var instance = new AoC2024Day09();
        var result = instance.Solution2();
        Assert.That(result, Is.EqualTo(6469636832766));
        Console.WriteLine($"Result: {result}");
    }
}