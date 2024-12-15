using AdventOfCode.Year2024;
using System;
using NUnit.Framework;

namespace AdventOfCodeTests.Year2024;

[TestFixture]
public class AoC2024Day03Tests {
    [Test]
    public void Solution1Examples()
    {
        string input = """
                       xmul(2,4)%&mul[3,7]!@^do_not_mul(5,5)+mul(32,64]then(mul(11,8)mul(8,5))
                       """;
        
        long expect = 161;
        
        var instance = new AoC2024Day03(input);
        Assert.That(instance.Solution1(), Is.EqualTo(expect));
    }

    [Test]
    public void Solution1Test() {
        var instance = new AoC2024Day03();
        var result = instance.Solution1();
        Assert.That(result, Is.EqualTo(173419328));
        Console.WriteLine($"Result: {result}");
    }

    [Test]
    public void Solution2Examples() {
        string input = """
                       xmul(2,4)&mul[3,7]!^don't()_mul(5,5)+mul(32,64](mul(11,8)undo()?mul(8,5))
                       """;
        
        long expect = 48;
        
        var instance = new AoC2024Day03(input);
        Assert.That(instance.Solution2(), Is.EqualTo(expect));
    }

    [Test]
    public void Solution2Test() {
        var instance = new AoC2024Day03();
        var result = instance.Solution2();
        Assert.That(result, Is.EqualTo(90669332));
        Console.WriteLine($"Result: {result}");
    }
}