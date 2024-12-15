using AdventOfCode.Year2024;
using System;
using NUnit.Framework;

namespace AdventOfCodeTests.Year2024;

[TestFixture]
public class AoC2024Day02Tests {
    [Test]
    public void Solution1Examples()
    {
        string input = """
                       7 6 4 2 1
                       1 2 7 8 9
                       9 7 6 2 1
                       1 3 2 4 5
                       8 6 4 4 1
                       1 3 6 7 9
                       """;
        
        long expect = 2;
        
        var instance = new AoC2024Day02(input);
        Assert.That(instance.Solution1(), Is.EqualTo(expect));
    }

    [Test]
    public void Solution1Test() {
        var instance = new AoC2024Day02();
        var result = instance.Solution1();
        Assert.That(result, Is.EqualTo(379));
        Console.WriteLine($"Result: {result}");
    }

    [Test]
    public void Solution2Examples() {
        string input = """
                       7 6 4 2 1
                       1 2 7 8 9
                       9 7 6 2 1
                       1 3 2 4 5
                       8 6 4 4 1
                       1 3 6 7 9
                       """;
        
        long expect = 4;
        
        var instance = new AoC2024Day02(input);
        Assert.That(instance.Solution2(), Is.EqualTo(expect));
    }

    [Test]
    public void Solution2Test() {
        var instance = new AoC2024Day02();
        var result = instance.Solution2();
        Assert.That(result, Is.EqualTo(430));
        Console.WriteLine($"Result: {result}");
    }
}