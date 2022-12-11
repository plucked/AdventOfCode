using System.Text.RegularExpressions;
using BenchmarkDotNet.Attributes;

namespace AdventOfCode.Year2022;

public class AoC2022Day11 {
    private string input;

    public AoC2022Day11(string input) {
        this.input = input;
    }

    [Benchmark]
    public long Solution1() {
        Dictionary<int, Queue<long>> monkeyItems = new Dictionary<int, Queue<long>>();
        Dictionary<int, Func<long, long>> monkeyOperations = new Dictionary<int, Func<long, long>>();
        Dictionary<int, int> positiveMonkeyTestTarget = new Dictionary<int, int>();
        Dictionary<int, int> negativeMonkeyTestTarget = new Dictionary<int, int>();
        Dictionary<int, Func<long, long>> monkeyTest = new Dictionary<int, Func<long, long>>();
        Dictionary<int, long> inspections = new Dictionary<int, long>();

        var lines = input.Split(Environment.NewLine);

        var monkeyIdx = 0;
        for (var lineIdx = 0; lineIdx < lines.Length; lineIdx++) {
            var line = lines[lineIdx];
            if (line.StartsWith("Monkey")) {
                monkeyIdx = int.Parse(line.Split(' ')[1].TrimEnd(':'));
                monkeyItems.Add(monkeyIdx, new Queue<long>());
            } else if (line.StartsWith("  Starting items:")) {
                var items = line.Split(':')[1].Split(',').Select(x => long.Parse(x.Trim()));
                foreach (var item in items) {
                    monkeyItems[monkeyIdx].Enqueue(item);
                }
            } else if (line.StartsWith("  Operation:")) {
                var operation = line.Split(':')[1].Trim();
                if (operation.StartsWith("new = old * old")) {
                    monkeyOperations.Add(monkeyIdx, x => x * x);
                } else if (operation.StartsWith("new = old * ")) {
                    var multiplier = int.Parse(operation.Split(' ')[4]);
                    monkeyOperations.Add(monkeyIdx, x => x * multiplier);
                } else if (operation.StartsWith("new = old + ")) {
                    var adder = int.Parse(operation.Split(' ')[4]);
                    monkeyOperations.Add(monkeyIdx, x => x + adder);
                }
            } else if (line.StartsWith("    If true: throw to monkey")) {
                var target = int.Parse(line.Trim().Split(' ')[5]);
                positiveMonkeyTestTarget.Add(monkeyIdx, target);
            } else if (line.StartsWith("    If false: throw to monkey")) {
                var target = int.Parse(line.Trim().Split(' ')[5]);
                negativeMonkeyTestTarget.Add(monkeyIdx, target);
            } else if (line.StartsWith("  Test: divisible by ")) {
                var remainder = line.Substring("  Test: divisible by ".Length);
                var divisor = int.Parse(remainder.Trim());
                monkeyTest.Add(monkeyIdx, x => x % divisor);
            }
        }

        for (var round = 0; round < 20; round++) {
            for (var monkey = 0; monkey < monkeyItems.Count; ++monkey) {
                while (monkeyItems[monkey].TryDequeue(out var item)) {
                    var worryLevel = monkeyOperations[monkey](item);
                    worryLevel /= 3;
                    var testResult = monkeyTest[monkey](worryLevel);
                    var target = testResult == 0 ? positiveMonkeyTestTarget[monkey] : negativeMonkeyTestTarget[monkey];
                    monkeyItems[target].Enqueue(worryLevel);

                    if (inspections.ContainsKey(monkey)) {
                        inspections[monkey]++;
                    } else {
                        inspections.Add(monkey, 1);
                    }
                }
            }
        }

        foreach (var inspection in inspections) {
            Console.WriteLine($"Monkey {inspection.Key} inspected {inspection.Value} items");
        }

        var sorted = inspections.OrderByDescending(x => x.Value).Take(2).Select(x => x.Value).ToArray();
        return sorted[0] * sorted[1];
    }

    [Benchmark]
    public long Solution2() {
        var regEx = new Regex(
                @"Monkey (?<monkeyIdx>\d):
  Starting items: (?<items>[\d, ]*)
  Operation: new = old (?<operand>[\+\*]) ((?<operandVal>:\d*|\w*))
  Test: divisible by (?<divisibleBy>\w*)
    If true: throw to monkey (?<positiveMonkey>\w*)
    If false: throw to monkey (?<negativeMonkey>\w*)");

        var matches = regEx.Matches(input);

        var monkeyCount = 0;
        var allItems = new List<(ulong itemVal, int monkeyIdx)>();
        var allOperandsIsMultiplicationOtherwiseAddition = new Dictionary<int, bool>();
        var allOperandValues = new Dictionary<int, ulong>();
        var allDivisibleBy = new Dictionary<int, ulong>();
        var allPositiveMonkey = new Dictionary<int, int>();
        var allNegativeMonkey = new Dictionary<int, int>();

        foreach (Match match in matches) {
            monkeyCount++;
            var monkeyIdx = int.Parse(match.Groups["monkeyIdx"].Value);
            var items = match.Groups["items"].Value.Split(',').Select(x => ulong.Parse(x.Trim())).ToArray();
            var operand = match.Groups["operand"].Value[0] == '*';
            var operandValTemp = match.Groups["operandVal"].Value;
            ulong operandVal;
            if (operandValTemp == "old") {
                operandVal = 0;
            } else {
                operandVal = ulong.Parse(operandValTemp);
            }

            var divisibleBy = ulong.Parse(match.Groups["divisibleBy"].Value);
            var positiveMonkey = int.Parse(match.Groups["positiveMonkey"].Value);
            var negativeMonkey = int.Parse(match.Groups["negativeMonkey"].Value);

            allItems.AddRange(items.Select(x => (x, monkeyIdx)));
            allOperandsIsMultiplicationOtherwiseAddition.Add(monkeyIdx, operand);
            allOperandValues.Add(monkeyIdx, operandVal);
            allDivisibleBy.Add(monkeyIdx, divisibleBy);
            allPositiveMonkey.Add(monkeyIdx, positiveMonkey);
            allNegativeMonkey.Add(monkeyIdx, negativeMonkey);
        }

        ulong len = 1;
        foreach (var t in allDivisibleBy) {
            len *= t.Value;
        }

        var inspections = new Dictionary<int, long>();
        var rounds = 10000;

        for (var i = 0; i < allItems.Count; i++) {
            (var item, var startMonkeyIndex) = allItems[i];
            var currentItem = item;
            var monkeyHoldingTheItem = startMonkeyIndex;

            for (var round = 0; round < rounds; round++) {
                for (var monkeyIndex = 0; monkeyIndex < monkeyCount; monkeyIndex++) {
                    if (monkeyHoldingTheItem != monkeyIndex) {
                        continue;
                    }

                    if (inspections.TryGetValue(monkeyIndex, out var inspectionCount)) {
                        inspections[monkeyIndex] = inspectionCount + 1;
                    } else {
                        inspections.Add(monkeyIndex, 1);
                    }

                    var operandValue = allOperandValues[monkeyHoldingTheItem];
                    if (operandValue == 0) {
                        operandValue = currentItem;
                    }

                    currentItem = allOperandsIsMultiplicationOtherwiseAddition[monkeyHoldingTheItem] ? currentItem * operandValue : currentItem + operandValue;
                    currentItem %= len;
                    var testResult = currentItem % allDivisibleBy[monkeyHoldingTheItem];
                    monkeyHoldingTheItem = testResult == 0 ? allPositiveMonkey[monkeyHoldingTheItem] : allNegativeMonkey[monkeyHoldingTheItem];
                }
            }
        }

        var sorted = inspections.OrderByDescending(x => x.Value).Take(2).Select(x => x.Value).ToArray();
        return sorted[0] * sorted[1];
    }
}