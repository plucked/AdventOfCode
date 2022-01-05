using AdventOfCode.Utilities;
using BenchmarkDotNet.Attributes;

namespace AdventOfCode.Year2015;

public class AoC2015Day05 {
    private string[] input;

    public AoC2015Day05(string[]? customInput = null) {
        input = customInput ?? EmbeddedInput.ReadAllLines("Year2015/2015_05_input.txt");
    }

    [Benchmark]
    public long Solution1() {
        var niceWords = 0L;
        foreach (var line in input) {
            var vowels = 0;
            var letterInRow = false;
            var lastLetter = '!';
            foreach (var c in line) {
                if (lastLetter == 'a' && c == 'b' || lastLetter == 'c' && c == 'd' || lastLetter == 'p' && c == 'q' || lastLetter == 'x' && c == 'y') {
                    goto notnice;
                }

                if (lastLetter == c) {
                    letterInRow = true;
                }

                if (c == 'a' || c == 'e' || c == 'i' || c == 'o' || c == 'u') {
                    ++vowels;
                }

                lastLetter = c;
            }

            if (vowels > 2 && letterInRow) {
                ++niceWords;
            }

            notnice: ;
        }

        return niceWords;
    }

    [Benchmark]
    public long Solution2() {
        var niceWords = 0L;
        foreach (var line in input) {
            var letterInRow = false;
            var lastLetter1 = '!';
            var lastLetter2 = '!';
            var foundPair = false;

            for (int i = 0; i < line.Length - 1; i++) {
                var a1 = line[i];
                var a2 = line[i + 1];

                for (int j = i + 2; j < line.Length - 1; j++) {
                    var b1 = line[j];
                    var b2 = line[j + 1];

                    if (a1 == b1 && a2 == b2) {
                        foundPair = true;
                        goto stopLoop;
                    }
                }

                continue;
                stopLoop:
                break;
            }

            foreach (var c in line) {
                if (lastLetter2 == c) {
                    letterInRow = true;
                }

                lastLetter2 = lastLetter1;
                lastLetter1 = c;
            }

            if (foundPair && letterInRow) {
                ++niceWords;
            }
        }

        return niceWords;
    }
}