using AdventOfCode.Year2024;
using System;
using NUnit.Framework;

namespace AdventOfCodeTests.Year2024;

[TestFixture]
public class AoC2024Day05Tests {
    [Test]
    public void Solution1Examples()
    {
        string input = """
                       47|53
                       97|13
                       97|61
                       97|47
                       75|29
                       61|13
                       75|53
                       29|13
                       97|29
                       53|29
                       61|53
                       97|53
                       61|29
                       47|13
                       75|47
                       97|75
                       47|61
                       75|61
                       47|29
                       75|13
                       53|13
                       
                       75,47,61,53,29
                       97,61,53,29,13
                       75,29,13
                       75,97,47,61,53
                       61,13,29
                       97,13,75,29,47
                       """;
        
        long expect = 143;
        
        var instance = new AoC2024Day05(input);
        Assert.That(instance.Solution1(), Is.EqualTo(expect));
    }

    [Test]
    public void Solution1Test() {
        var instance = new AoC2024Day05();
        var result = instance.Solution1();
        Assert.That(result, Is.EqualTo(5639));
        Console.WriteLine($"Result: {result}");
    }

    [Test]
    public void Solution2Examples() {
        string input = """
                       47|53
                       97|13
                       97|61
                       97|47
                       75|29
                       61|13
                       75|53
                       29|13
                       97|29
                       53|29
                       61|53
                       97|53
                       61|29
                       47|13
                       75|47
                       97|75
                       47|61
                       75|61
                       47|29
                       75|13
                       53|13
                       
                       75,47,61,53,29
                       97,61,53,29,13
                       75,29,13
                       75,97,47,61,53
                       61,13,29
                       97,13,75,29,47
                       """;
        
        long expect = 123;
        
        var instance = new AoC2024Day05(input);
        Assert.That(instance.Solution2(), Is.EqualTo(expect));
    }

    [Test]
    public void Solution2Test() {
        var instance = new AoC2024Day05();
        var result = instance.Solution2();
        Assert.That(result, Is.EqualTo(5273));
        Console.WriteLine($"Result: {result}");
    }
}