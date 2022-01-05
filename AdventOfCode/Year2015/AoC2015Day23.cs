using AdventOfCode.Utilities;
using BenchmarkDotNet.Attributes;

namespace AdventOfCode.Year2015;

public class AoC2015Day23 {
    private Instruction[] instructions;
    private int resultOfRegister = 1;

    private struct Instruction {
        public InstructionType InstType;
        public int Register;
        public int Value;

        public enum InstructionType {
            Half,
            Triple,
            Increment,
            Jump,
            JumpIfEven,
            JumpIfOne
        }

        public override string ToString() {
            return $"{InstType} {Register}, {Value}";
        }
    }

    public AoC2015Day23(string[]? customInput = null, int resultOfRegister = 1) {
        var lines = customInput ?? EmbeddedInput.ReadAllLines("Year2015/2015_23_input.txt");
        instructions = lines.Select(
                                    line => {
                                        var code = line.Substring(0, 3);
                                        switch (code) {
                                            case "hlf":
                                                return new Instruction { InstType = Instruction.InstructionType.Half, Register = line.Substring(4, 1) == "a" ? 0 : 1 };
                                            case "tpl":
                                                return new Instruction { InstType = Instruction.InstructionType.Triple, Register = line.Substring(4, 1) == "a" ? 0 : 1 };
                                            case "inc":
                                                return new Instruction { InstType = Instruction.InstructionType.Increment, Register = line.Substring(4, 1) == "a" ? 0 : 1 };
                                            case "jmp":
                                                return new Instruction { InstType = Instruction.InstructionType.Jump, Value = int.Parse(line.Substring(4)) };
                                            case "jie": {
                                                var r = line.Substring(4, 1) == "a" ? 0 : 1;
                                                var v = int.Parse(line.Substring(6).Trim());
                                                return new Instruction { InstType = Instruction.InstructionType.JumpIfEven, Register = r, Value = v };
                                            }
                                            case "jio": {
                                                var r = line.Substring(4, 1) == "a" ? 0 : 1;
                                                var v = int.Parse(line.Substring(6).Trim());
                                                return new Instruction { InstType = Instruction.InstructionType.JumpIfOne, Register = r, Value = v };
                                            }
                                            default:
                                                throw new Exception();
                                        }
                                    })
                            .ToArray();
        this.resultOfRegister = resultOfRegister;
    }

    [Benchmark]
    public long Solution1() {
        return Run(new int[2], resultOfRegister);
    }

    [Benchmark]
    public long Solution2() {
        return Run(new[] { 1, 0 }, 1);
    }

    private long Run(int[] initialRegister, int resultOfRegister) {
        int[] register = initialRegister;

        var nextInstruction = 0;
        while (nextInstruction >= 0 && nextInstruction < instructions.Length) {
            var current = instructions[nextInstruction];
            switch (current.InstType) {
                case Instruction.InstructionType.Half:
                    register[current.Register] /= 2;
                    nextInstruction++;
                    break;
                case Instruction.InstructionType.Triple:
                    register[current.Register] *= 3;
                    nextInstruction++;
                    break;
                case Instruction.InstructionType.Increment:
                    register[current.Register]++;
                    nextInstruction++;
                    break;
                case Instruction.InstructionType.Jump:
                    if (nextInstruction + current.Value == nextInstruction) {
                        nextInstruction = -1;
                    }

                    nextInstruction += current.Value;
                    break;
                case Instruction.InstructionType.JumpIfEven:
                    if (register[current.Register] % 2 == 0) {
                        if (nextInstruction + current.Value == nextInstruction) {
                            nextInstruction = -1;
                        }

                        nextInstruction += current.Value;
                    } else {
                        nextInstruction++;
                    }

                    break;
                case Instruction.InstructionType.JumpIfOne:
                    if (register[current.Register] == 1) {
                        if (nextInstruction + current.Value == nextInstruction) {
                            nextInstruction = -1;
                        }

                        nextInstruction += current.Value;
                    } else {
                        nextInstruction++;
                    }

                    break;
            }
        }

        return register[resultOfRegister];
    }
}