using AdventOfCode.Year2024;
using System;
using NUnit.Framework;

namespace AdventOfCodeTests.Year2024;

[TestFixture]
public class AoC2024Day04Tests {
    [Test]
    public void Solution1Examples()
    {
        string input = """
                       MMMSXXMASM
                       MSAMXMSMSA
                       AMXSXMAAMM
                       MSAMASMSMX
                       XMASAMXAMM
                       XXAMMXXAMA
                       SMSMSASXSS
                       SAXAMASAAA
                       MAMMMXMMMM
                       MXMXAXMASX
                       """;
        
        long expect = 18;
        
        var instance = new AoC2024Day04(input);
        Assert.That(instance.Solution1(), Is.EqualTo(expect));
    }

    [Test]
    public void Solution1Test() {
        var instance = new AoC2024Day04();
        var result = instance.Solution1();
        Assert.That(result, Is.EqualTo(2557));
        Console.WriteLine($"Result: {result}");
    }

    [Test]
    public void Solution2Examples() {
        string input = """
                       .M.S......
                       ..A..MSMS.
                       .M.S.MAA..
                       ..A.ASMSM.
                       .M.S.M....
                       ..........
                       S.S.S.S.S.
                       .A.A.A.A..
                       M.M.M.M.M.
                       ..........
                       """;
        
        long expect = 9;
        
        var instance = new AoC2024Day04(input);
        Assert.That(instance.Solution2(), Is.EqualTo(expect));
    }

    [Test]
    public void Solution2Test() {
        var instance = new AoC2024Day04();
        var result = instance.Solution2();
        Assert.That(result, Is.EqualTo(1854));
        Console.WriteLine($"Result: {result}");
    }
}