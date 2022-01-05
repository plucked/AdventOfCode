using AdventOfCode.Utilities;
using BenchmarkDotNet.Attributes;

namespace AdventOfCode.Year2015;

public class AoC2015Day12 {
    private char[] input;

    public AoC2015Day12(string? line = null) {
        input = (line ?? EmbeddedInput.ReadAllText("Year2015/2015_12_input.txt")).ToCharArray();
    }

    [Benchmark]
    public long Solution1() {
        var result = 0L;
        bool readingNumber = false;
        long number = 0;
        bool minusSign = false;
        for (int i = 0; i < input.Length; i++) {
            var c = input[i];
            if (c >= '0' && c <= '9') {
                if (readingNumber == false) {
                    readingNumber = true;
                    minusSign = input[i - 1] == '-';
                    number = c - '0';
                } else {
                    number *= 10L;
                    number += c - '0';
                }
            } else if (readingNumber) {
                readingNumber = false;
                if (minusSign) {
                    number = -number;
                }

                result += number;
                number = 0;
            }
        }

        return result;
    }

    [Benchmark]
    public long Solution2() {
        // we store the results per { } object so we can remove values when a "red" is detected
        var objectResults = new List<long>();
        objectResults.Add(0);

        // remember which type the current object is
        var objectStack = new Stack<char>();

        bool readingNumber = false;
        long number = 0;
        bool minusSign = false;
        int ignoreResultsIndex = -1;
        for (int i = 0; i < input.Length; i++) {
            var a = input[i];

            // reading a number, early exit
            if (a >= '0' && a <= '9') {
                if (readingNumber == false) {
                    readingNumber = true;
                    minusSign = input[i - 1] == '-';
                    number = a - '0';
                } else {
                    number *= 10L;
                    number += a - '0';
                }

                continue;
            }

            // finish reading a number, add the result to the current object
            if (readingNumber) {
                readingNumber = false;
                if (minusSign) {
                    number = -number;
                }

                objectResults[^1] += number;
                number = 0;
            }

            // check if there is a :"red" property coming
            char? b = i + 1 < input.Length ? input[i + 1] : null;
            char? c = i + 2 < input.Length ? input[i + 2] : null;
            char? d = i + 3 < input.Length ? input[i + 3] : null;
            char? e = i + 4 < input.Length ? input[i + 4] : null;
            char? f = i + 5 < input.Length ? input[i + 5] : null;

            if (ignoreResultsIndex == -1 && a == ':' && b == '"' && c == 'r' && d == 'e' && e == 'd' && f == '"' && objectStack.Peek() == '{') {
                // remember the FIRST object depth saw this
                ignoreResultsIndex = objectResults.Count - 1;
            } else if (a == '[') {
                // just to remember that we are in an array
                objectStack.Push('[');
            } else if (a == ']') {
                objectStack.Pop();
            } else if (a == '{') {
                objectResults.Add(0);
                objectStack.Push('{');
            } else if (a == '}') {
                objectStack.Pop();
                // we don't add the result of an object to the parent object when it was marked as 
                // ignored because we saw an "red" property value
                if (ignoreResultsIndex != -1 && objectResults.Count - 1 > ignoreResultsIndex) {
                } else if (ignoreResultsIndex != -1 && objectResults.Count - 1 == ignoreResultsIndex) {
                    ignoreResultsIndex = -1;
                } else if (ignoreResultsIndex == -1) {
                    // add the value to the parent object results
                    objectResults[^2] += objectResults[^1];
                }

                objectResults.RemoveAt(objectResults.Count - 1);
            }
        }

        // the "root" object results are what we want to read 
        return objectResults[0];
    }
}