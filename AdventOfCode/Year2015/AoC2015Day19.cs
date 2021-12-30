using BenchmarkDotNet.Attributes;

namespace AdventOfCode.Year2015;

public class AoC2015Day19 {
    private List<Replacement> replacements = new();
    private string molecule;

    private struct Replacement {
        public string From;
        public string To;

        public override string ToString() {
            return $"{From} => {To}";
        }
    }

    [GlobalSetup(Targets = new[] { nameof(Solution1), nameof(Solution2) })]
    public void BenchmarkSetup() {
        Setup();
    }

    public void Setup(string[]? customInput = null) {
        var lines = (customInput ?? File.ReadAllLines("Year2015/2015_19_input.txt"));

        for (int i = 0; i < lines.Length - 2; i++) {
            var line = lines[i];
            var split = line.Split(" => ");
            replacements.Add(new Replacement { From = split[0], To = split[1] });
        }

        molecule = lines[^1];
    }

    [Benchmark]
    public long Solution1() {
        var result = new HashSet<string>();

        foreach (var replacement in replacements) {
            var index = 0;
            while (true) {
                index = molecule.IndexOf(replacement.From, index, StringComparison.Ordinal);
                if (index == -1) {
                    break;
                }

                var newMolecule = molecule.Substring(0, index) + replacement.To + molecule.Substring(index + replacement.From.Length);
                result.Add(newMolecule);
                index += replacement.From.Length;
            }
        }

        return result.Count;
    }

    /// <summary>
    /// The trick here is to deconstruct the molecule from back to start
    /// for example:
    /// e => H
    /// e => O
    /// H => HO
    /// H => OH
    /// O => HH
    ///
    /// HOHOHO
    ///
    /// We find the first replacement we match from the end so we look at the first sub string of HOHOH[O] = [O]
    /// That one has a match 'e', but we have a condition that 'e' can only be the last replacement
    /// We look at the next sub string HOHO[HO] = HO
    /// Now we match 'H' and create a new molecule out of it HOHOH
    /// We repeat now from the end again and try to find the first replacement. The replacement doesn't have to be at the end
    /// of the sub string, can also be in the middle or beginning. 
    /// </summary>
    /// <returns></returns>
    [Benchmark]
    public long Solution2() {
        var steps = 0;
        while (molecule != "e") {
            for (int startIndex = molecule.Length - 1; startIndex >= 0; startIndex--) {
                foreach (var replacement in replacements) {
                    if (replacement.From == "e" && molecule != replacement.To) {
                        continue;
                    }
                    
                    var findIndex = molecule.IndexOf(replacement.To, startIndex);
                    if (findIndex == -1) {
                        continue;
                    }

                    molecule = molecule.Substring(0, findIndex) + replacement.From + molecule.Substring(findIndex + replacement.To.Length);
                    ++steps;
                    break;
                }
            }
        }
        
        return steps;
    }
}