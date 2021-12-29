using System.Text;
using System.Text.RegularExpressions;
using BenchmarkDotNet.Attributes;

namespace AdventOfCode.Year2015;

public class AoC2015Day16 {
    // to map the compound name to an index
    private static List<string> lookup = new() {
            "children",
            "cats",
            "samoyeds",
            "pomeranians",
            "akitas",
            "vizslas",
            "goldfish",
            "trees",
            "cars",
            "perfumes"
    };

    private const int COMPOUND_CATS = 1;
    private const int COMPOUND_POMERANIANS = 3;
    private const int COMPOUND_GOLDFISH = 6;
    private const int COMPOUND_TREES = 7;

    private AuntSue masterAunt;
    private AuntSue[] aunts = new AuntSue[500];

    private unsafe struct AuntSue {
        public fixed int Compounds[10];

        public AuntSue(int compIdxA, int compAVal, int compIdxB, int compBVal, int compIdxC, int compCVal) {
            for (int i = 0; i < 10; i++) {
                Compounds[i] = -1;
            }

            Compounds[compIdxA] = compAVal;
            Compounds[compIdxB] = compBVal;
            Compounds[compIdxC] = compCVal;
        }

        public AuntSue(int[] compounds) {
            for (int i = 0; i < 10; i++) {
                Compounds[i] = compounds[i];
            }
        }

        public bool MatchSolution1(AuntSue masterAunt) {
            int matches = 0;
            for (int i = 0; i < 10; i++) {
                if (Compounds[i] == masterAunt.Compounds[i]) {
                    matches++;
                }
            }

            return matches == 3;
        }

        public bool MatchSolution2(AuntSue masterAunt) {
            int matches = 0;
            for (int i = 0; i < 10; i++) {
                if (i is COMPOUND_CATS or COMPOUND_TREES) {
                    if (Compounds[i] > masterAunt.Compounds[i]) {
                        matches++;
                    }
                } else if (i is COMPOUND_POMERANIANS or COMPOUND_GOLDFISH) {
                    if (Compounds[i] >= 0 && Compounds[i] < masterAunt.Compounds[i]) {
                        matches++;
                    }
                } else {
                    if (Compounds[i] == masterAunt.Compounds[i]) {
                        matches++;
                    }
                }
            }

            return matches == 3;
        }

        public override string ToString() {
            var sb = new StringBuilder();
            for (int i = 0; i < 10; i++) {
                sb.Append($"{lookup[i]}={Compounds[i]} ");
            }

            return sb.ToString();
        }
    }

    [GlobalSetup(Targets = new[] { nameof(Solution1), nameof(Solution2) })]
    public void BenchmarkSetup() {
        Setup();
    }

    public unsafe void Setup() {
        var lines = File.ReadAllLines("Year2015/2015_16_input.txt");
        var regex = new Regex("Sue (\\d+)\\: (\\w+)\\: (\\d+)\\, (\\w+)\\: (\\d+)\\, (\\w+)\\: (\\d+)", RegexOptions.Compiled);
        foreach (var line in lines) {
            var match = regex.Match(line);
            var index = int.Parse(match.Groups[1].Value);
            var compAIndex = lookup.IndexOf(match.Groups[2].Value);
            var compAVal = int.Parse(match.Groups[3].Value);
            var compBIndex = lookup.IndexOf(match.Groups[4].Value);
            var compBVal = int.Parse(match.Groups[5].Value);
            var compCIndex = lookup.IndexOf(match.Groups[6].Value);
            var compCVal = int.Parse(match.Groups[7].Value);

            aunts[index - 1] = new AuntSue(compAIndex, compAVal, compBIndex, compBVal, compCIndex, compCVal);
        }

        masterAunt = new AuntSue(new[] { 3, 7, 2, 3, 0, 0, 5, 3, 2, 1 });
    }

    [Benchmark]
    public long Solution1() {
        for (int i = 0; i < aunts.Length; i++) {
            if (aunts[i].MatchSolution1(masterAunt)) {
                return i + 1;
            }
        }

        return 0;
    }

    [Benchmark]
    public long Solution2() {
        for (int i = 0; i < aunts.Length; i++) {
            if (aunts[i].MatchSolution2(masterAunt)) {
                Console.WriteLine(masterAunt);
                Console.WriteLine(aunts[i]);
                return i + 1;
            }
        }

        return 0;
    }
}