using AdventOfCode.Year2022;
using NUnit.Framework;

namespace AdventOfCodeTests.Year2022;

[TestFixture]
public class AoC2022Day09Tests {
    [TestCase("R 4\nU 4\nL 3\nD 1\nR 4\nD 1\nL 5\nR 2", 13)]
    [TestCase(null, 6018)]
    public void Solution1(string input, long expect) {
        var instance = new AoC2022Day09(input);
        Assert.That(expect, Is.EqualTo(instance.Solution1()));
    }

    [TestCase("R 4\nU 4\nL 3\nD 1\nR 4\nD 1\nL 5\nR 2", 1)]
    [TestCase("R 5\nU 8\nL 8\nD 3\nR 17\nD 10\nL 25\nU 20", 36)]
    [TestCase(null, 2619)]
    public void Solution2(string input, long expect) {
        var instance = new AoC2022Day09(input);
        Assert.That(expect, Is.EqualTo(instance.Solution2()));
    }
}