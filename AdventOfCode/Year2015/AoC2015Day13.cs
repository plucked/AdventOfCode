using BenchmarkDotNet.Attributes;

namespace AdventOfCode.Year2015;

/// <summary>
/// This puzzle is basically the same as Day 9, just with a different input
/// We will just transform the input of this puzzle and run it with the day 9 solution
/// </summary>
public class AoC2015Day13 {
    public string[] input;

    [GlobalSetup(Targets = new[] { nameof(Solution1), nameof(Solution2) })]
    public void BenchmarkSetup() {
        Setup();
    }

    public void Setup(string[]? lines = null) {
        input = lines ?? File.ReadAllLines("Year2015/2015_13_input.txt");
    }

    [Benchmark]
    public long Solution1() {
        return Run(false);
    }

    [Benchmark]
    public long Solution2() {
        return Run(true);
    }

    private long Run(bool addSanta) {
        var relations = new Dictionary<string, Dictionary<string, int>>();
        var names = new HashSet<string>();

        foreach (var line in input) {
            var split1 = line.Split(" would ");
            var name1 = split1[0];
            var gain = split1[1].StartsWith("gain");
            var split2 = split1[1].Split(" happiness units by sitting next to ");
            var happiness = int.Parse(split2[0].Substring("gain ".Length));
            happiness = gain ? happiness : -happiness;
            var name2 = split2[1].Substring(0, split2[1].Length - 1);

            if (relations.TryGetValue(name1, out var relation) == false) {
                relation = new Dictionary<string, int>();
                relations[name1] = relation;
            }

            relation[name2] = happiness;
            names.Add(name1);
        }

        if (addSanta) {
            relations["Santa"] = new Dictionary<string, int>();
            foreach (var name in names) {
                relations["Santa"][name] = 0;
                relations[name]["Santa"] = 0;
            }

            names.Add("Santa");
        }

        var day9Input = new List<string>();
        var namesSorted = names.OrderBy(n => n).ToArray();
        for (int i = 0; i < namesSorted.Length - 1; i++) {
            for (int j = i + 1; j < namesSorted.Length; j++) {
                var happiness = relations[namesSorted[i]][namesSorted[j]] + relations[namesSorted[j]][namesSorted[i]];
                day9Input.Add($"{namesSorted[i]} to {namesSorted[j]} = {happiness}");
            }
        }

        var day9 = new AoC2015Day09();
        day9.Setup(day9Input.ToArray());
        return day9.Run(false, true);
    }
}