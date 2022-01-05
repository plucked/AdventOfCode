using AdventOfCode.Utilities;
using BenchmarkDotNet.Attributes;

namespace AdventOfCode.Year2015;

public class AoC2015Day07 {
    private Instruction[] instructions;

    private struct Instruction {
        public Op Operation;
        public string? VariableA;
        public uint? ValueA;
        public string? VariableB;
        public uint? ValueB;
        public string Output;

        public enum Op {
            ASSIGN,
            AND,
            OR,
            NOT,
            LSHIFT,
            RSHIFT
        }
    }

    public AoC2015Day07(string[]? customInput = null) {
        var lines = customInput ?? EmbeddedInput.ReadAllLines("Year2015/2015_07_input.txt");
        instructions = lines.Select(
                                    line => {
                                        var split1 = line.Split(" -> ");
                                        var instruction = new Instruction { Output = split1[1] };

                                        if (Parse2Inputs(ref instruction, split1[0], "AND") == false &&
                                            Parse2Inputs(ref instruction, split1[0], "OR") == false &&
                                            Parse2Inputs(ref instruction, split1[0], "RSHIFT") == false &&
                                            Parse2Inputs(ref instruction, split1[0], "LSHIFT") == false) {
                                            if (line.Contains("NOT")) {
                                                instruction.Operation = Instruction.Op.NOT;
                                                var sub = split1[0].Substring("NOT ".Length);
                                                if (uint.TryParse(sub, out var number)) {
                                                    instruction.ValueA = number;
                                                } else {
                                                    instruction.VariableA = sub;
                                                }
                                            } else {
                                                instruction.Operation = Instruction.Op.ASSIGN;
                                                if (uint.TryParse(split1[0], out var number)) {
                                                    instruction.ValueA = number;
                                                } else {
                                                    instruction.VariableA = split1[0];
                                                }
                                            }
                                        }

                                        return instruction;
                                    })
                            .ToArray();

        bool Parse2Inputs(ref Instruction instruction, string line, string keyword) {
            if (line.Contains(keyword)) {
                instruction.Operation = Enum.Parse<Instruction.Op>(keyword);
                var split2 = line.Split($" {keyword} ");
                if (uint.TryParse(split2[0], out var number)) {
                    instruction.ValueA = number;
                } else {
                    instruction.VariableA = split2[0];
                }

                if (uint.TryParse(split2[1], out number)) {
                    instruction.ValueB = number;
                } else {
                    instruction.VariableB = split2[1];
                }
            } else {
                return false;
            }

            return true;
        }
    }

    [Benchmark]
    public Dictionary<string, uint> Solution1() {
        var variables = new Dictionary<string, uint>();
        var q = new Queue<Instruction>(instructions);

        while (q.TryDequeue(out var instruction)) {
            // don't run an instruction when the inputs are not calculated
            if (instruction.VariableA != null && variables.ContainsKey(instruction.VariableA) == false ||
                instruction.VariableB != null && variables.ContainsKey(instruction.VariableB) == false) {
                q.Enqueue(instruction);
                continue;
            }

            switch (instruction.Operation) {
                case Instruction.Op.ASSIGN:
                    variables[instruction.Output] = instruction.ValueA ?? variables[instruction.VariableA!];
                    break;
                case Instruction.Op.AND:
                    variables[instruction.Output] = (instruction.ValueA ?? variables[instruction.VariableA!]) & (instruction.ValueB ?? variables[instruction.VariableB!]);
                    break;
                case Instruction.Op.OR:
                    variables[instruction.Output] = (instruction.ValueA ?? variables[instruction.VariableA!]) | (instruction.ValueB ?? variables[instruction.VariableB!]);
                    break;
                case Instruction.Op.NOT:
                    variables[instruction.Output] = ~(instruction.ValueA ?? variables[instruction.VariableA!]);
                    variables[instruction.Output] &= 0b1111111111111111; // make sure we cap at 16bit
                    break;
                case Instruction.Op.LSHIFT:
                    variables[instruction.Output] = (instruction.ValueA ?? variables[instruction.VariableA!]) << (int)(instruction.ValueB ?? variables[instruction.VariableB!]);
                    variables[instruction.Output] &= 0b1111111111111111; // make sure we cap at 16bit
                    break;
                case Instruction.Op.RSHIFT:
                    variables[instruction.Output] = (instruction.ValueA ?? variables[instruction.VariableA!]) >> (int)(instruction.ValueB ?? variables[instruction.VariableB!]);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        return variables;
    }

    [Benchmark]
    public Dictionary<string, uint> Solution2() {
        // run solution 1 to get a
        var v = Solution1();
        // replace the instruction which assigns "b" with the value from a
        for (var i = 0; i < instructions.Length; i++) {
            var instruction = instructions[i];
            if (instruction.Operation != Instruction.Op.ASSIGN || instruction.Output != "b") {
                continue;
            }

            instruction.ValueA = v["a"];
            instructions[i] = instruction;
            break;
        }

        // rerun it
        return Solution1();
    }
}