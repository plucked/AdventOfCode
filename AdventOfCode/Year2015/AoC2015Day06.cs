using System.Text.RegularExpressions;
using AdventOfCode.Utilities;
using BenchmarkDotNet.Attributes;

namespace AdventOfCode.Year2015;

public class AoC2015Day06 {
    private const int WIDTH = 1000;
    private const int HEIGHT = 1000;
    private Instruction[] instructions;

    private struct Instruction {
        public Mode SelectedMode;
        public int X1;
        public int X2;
        public int Y1;
        public int Y2;

        public Instruction(Mode selectedMode, int x1, int y1, int x2, int y2) {
            SelectedMode = selectedMode;
            // make sure that x1 and y1 are the lowest, so we can iterate forward without checking
            X1 = x1 <= x2 ? x1 : x2;
            X2 = x1 <= x2 ? x2 : x1;
            Y1 = y1 <= y2 ? y1 : y2;
            Y2 = y1 <= y2 ? y2 : y1;
        }

        public enum Mode {
            TurnOn,
            TurnOff,
            Toggle
        }
    }

    public AoC2015Day06(string[]? customInput = null) {
        var lines = customInput ?? EmbeddedInput.ReadAllLines("Year2015/2015_06_input.txt");
        var regex = new Regex("(on|off|toggle) (\\d*),(\\d*) through (\\d*),(\\d*)");
        instructions = lines.Select(
                                    line => {
                                        var match = regex.Match(line);
                                        return new Instruction(
                                                match.Groups[1].Value == "on" ? Instruction.Mode.TurnOn :
                                                match.Groups[1].Value == "off" ? Instruction.Mode.TurnOff : Instruction.Mode.Toggle,
                                                int.Parse(match.Groups[2].Value),
                                                int.Parse(match.Groups[3].Value),
                                                int.Parse(match.Groups[4].Value),
                                                int.Parse(match.Groups[5].Value));
                                    })
                            .ToArray();
    }

    [Benchmark]
    public unsafe long Solution1() {
        var on = 0;
        // need to create a thread, because the default max stack size of the main thread is 1MB
        var thread = new Thread(
                () => {
                    var field = stackalloc byte[WIDTH * HEIGHT];
                    foreach (var instruction in instructions) {
                        switch (instruction.SelectedMode) {
                            case Instruction.Mode.TurnOn: {
                                for (int y = instruction.Y1; y <= instruction.Y2; y++) {
                                    for (int x = instruction.X1; x <= instruction.X2; x++) {
                                        var index = y * WIDTH + x;
                                        field[index] = 1;
                                    }
                                }

                                break;
                            }
                            case Instruction.Mode.TurnOff: {
                                for (int y = instruction.Y1; y <= instruction.Y2; y++) {
                                    for (int x = instruction.X1; x <= instruction.X2; x++) {
                                        var index = y * WIDTH + x;
                                        field[index] = 0;
                                    }
                                }

                                break;
                            }
                            case Instruction.Mode.Toggle: {
                                for (int y = instruction.Y1; y <= instruction.Y2; y++) {
                                    for (int x = instruction.X1; x <= instruction.X2; x++) {
                                        var index = y * WIDTH + x;
                                        field[index] = field[index] == 0 ? (byte)1 : (byte)0;
                                    }
                                }

                                break;
                            }
                            default:
                                throw new ArgumentOutOfRangeException();
                        }
                    }

                    for (int i = 0; i < HEIGHT * WIDTH; i++) {
                        if (field[i] == 1) {
                            ++on;
                        }
                    }
                },
                maxStackSize:WIDTH * HEIGHT * 2);

        thread.Start();
        thread.Join(); // wait for the thread to finish
        return on;
    }

    [Benchmark]
    public unsafe long Solution2() {
        var brightness = 0L;
        // need to create a thread, because the default max stack size of the main thread is 1MB
        var thread = new Thread(
                () => {
                    var field = stackalloc byte[WIDTH * HEIGHT];
                    foreach (var instruction in instructions) {
                        switch (instruction.SelectedMode) {
                            case Instruction.Mode.TurnOn: {
                                for (int y = instruction.Y1; y <= instruction.Y2; y++) {
                                    for (int x = instruction.X1; x <= instruction.X2; x++) {
                                        var index = y * WIDTH + x;
                                        field[index]++;
                                    }
                                }

                                break;
                            }
                            case Instruction.Mode.TurnOff: {
                                for (int y = instruction.Y1; y <= instruction.Y2; y++) {
                                    for (int x = instruction.X1; x <= instruction.X2; x++) {
                                        var index = y * WIDTH + x;
                                        var v = field[index];
                                        field[index] = (byte)(v - 1 >= 0 ? v - 1 : 0);
                                    }
                                }

                                break;
                            }
                            case Instruction.Mode.Toggle: {
                                for (int y = instruction.Y1; y <= instruction.Y2; y++) {
                                    for (int x = instruction.X1; x <= instruction.X2; x++) {
                                        var index = y * WIDTH + x;
                                        field[index] = (byte)(field[index] + 2);
                                    }
                                }

                                break;
                            }
                            default:
                                throw new ArgumentOutOfRangeException();
                        }
                    }

                    for (int i = 0; i < HEIGHT * WIDTH; i++) {
                        brightness += field[i];
                    }
                },
                maxStackSize:WIDTH * HEIGHT * 2);

        thread.Start();
        thread.Join(); // wait for the thread to finish
        return brightness;
    }
}