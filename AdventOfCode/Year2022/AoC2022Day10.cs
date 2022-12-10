using System.Text;
using BenchmarkDotNet.Attributes;

namespace AdventOfCode.Year2022;

public class AoC2022Day10 {
    private struct Instruction {
        public enum Operation {
            AddX = 1,
            Noop = 2,
        }

        public Operation Op { get; set; }
        public int? Argument { get; set; }

        public int Cycles =>
                Op switch {
                        Operation.AddX => 2,
                        Operation.Noop => 1,
                        _ => throw new ArgumentOutOfRangeException()
                };
    }

    private List<Instruction> instructions;

    public AoC2022Day10(string input) {
        var lines = input.Split(Environment.NewLine);
        instructions = new List<Instruction>();
        foreach (var line in lines) {
            var parts = line.Split(' ');
            var op = parts[0] switch {
                    "addx" => Instruction.Operation.AddX,
                    "noop" => Instruction.Operation.Noop,
                    _ => throw new ArgumentOutOfRangeException()
            };
            var arg = parts.Length > 1 ? int.Parse(parts[1]) : (int?)null;
            instructions.Add(new Instruction { Op = op, Argument = arg });
        }
    }

    [Benchmark]
    public long Solution1() {
        var currentCycle = 0;
        var register = 1;
        var signalStrength = 0;
        foreach (var instruction in instructions) {
            for (var instructionCycle = 0; instructionCycle < instruction.Cycles; instructionCycle++) {
                currentCycle++;

                if ((currentCycle + 20) % 40 == 0 && currentCycle <= 220) {
                    signalStrength += currentCycle * register;
                }
            }

            if (instruction.Op == Instruction.Operation.AddX) {
                register += instruction.Argument!.Value;
            }
        }

        return signalStrength;
    }

    [Benchmark]
    public string Solution2() {
        var currentCycle = 0;
        var register = 1;
        var screen = new StringBuilder();
        foreach (var instruction in instructions) {
            for (var instructionCycle = 0; instructionCycle < instruction.Cycles; instructionCycle++) {
                var cursor = currentCycle % 40;

                if (currentCycle > 0 && cursor == 0) {
                    screen.Append(Environment.NewLine);
                }

                var spriteCenter = register;
                var spriteStart = spriteCenter - 1;
                var spriteEnd = spriteCenter + 1;
                if (cursor >= spriteStart && cursor <= spriteEnd) {
                    screen.Append('#');
                } else {
                    screen.Append('.');
                }

                currentCycle++;
            }

            if (instruction.Op == Instruction.Operation.AddX) {
                register += instruction.Argument!.Value;
            }
        }

        Console.WriteLine(screen);
        return screen.ToString();
    }
}