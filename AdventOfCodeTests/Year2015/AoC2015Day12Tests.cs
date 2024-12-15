using System;
using AdventOfCode.Year2015;
using NUnit.Framework;

namespace AdventOfCodeTests.Year2015;

[TestFixture]
public class AoC2015Day12Tests {
    [TestCase("[1,2,3]", 6)]
    [TestCase("{\"a\":2,\"b\":4}", 6)]
    [TestCase("[[[3]]]", 3)]
    [TestCase("{\"a\":{\"b\":4},\"c\":-1}", 3)]
    [TestCase("{\"a\":[-1,1]}", 0)]
    [TestCase("[-1,{\"a\":1}]", 0)]
    [TestCase("[]", 0)]
    [TestCase("{}", 0)]
    public void Solution1Example(string input, long expect) {
        var instance = new AoC2015Day12(input);
        Assert.That(expect, Is.EqualTo(instance.Solution1()));
    }

    [Test]
    public void Solution1Test() {
        var instance = new AoC2015Day12();
        var result = instance.Solution1();
        Assert.That(191164, Is.EqualTo(result));
        Console.WriteLine($"Result: {result}");
    }

    [TestCase("[1,2,3]", 6)]
    [TestCase("[1,{\"c\":\"red\",\"b\":2},3]", 4)]
    [TestCase("{\"d\":\"red\",\"e\":[1,2,3,4],\"f\":5}", 0)]
    [TestCase("[1,\"red\",5]", 6)]
    [TestCase("{\"a\":1,b:[{\"c\":3,\"e\":\"red\"}]}", 1)]
    [TestCase("[{\"a\":\"red\",\"b\":7},{c:8}]", 8)]
    [TestCase("{\"a\":{\"b\":{\"c\":\"red\",\"d\":1},\"e\":2},\"f\":3}", 5)]
    [TestCase("{\"a\":{\"b\":{\"d\":1},\"c\":\"red\",\"e\":2},\"f\":3}", 3)]
    [TestCase("{\"a\":{\"b\":\"red\",\"c\":3},\"d\":\"red\",\"e\":3}", 0)]
    public void Solution2Example(string input, long expect) {
        var instance = new AoC2015Day12(input);
        Assert.That(expect, Is.EqualTo(instance.Solution2()));
    }

    [Test]
    public void Solution2Test() {
        var instance = new AoC2015Day12();
        var result = instance.Solution2();
        Assert.That(87842, Is.EqualTo(result));
        Console.WriteLine($"Result: {result}");
    }
}