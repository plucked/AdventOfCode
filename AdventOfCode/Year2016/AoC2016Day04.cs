using System.Text.RegularExpressions;
using AdventOfCode.Utilities;
using BenchmarkDotNet.Attributes;

namespace AdventOfCode.Year2016;

public class AoC2016Day04 {
    private Room[] rooms;

    private struct Room {
        public string Name;
        public int SectorId;
        public string CheckSum;
    }

    public AoC2016Day04(string[]? customInput = null) {
        var regex = new Regex("(.*)-(\\d*)\\[(\\w*)\\]$", RegexOptions.Compiled);
        var lines = (customInput ?? EmbeddedInput.ReadAllLines("Year2016/2016_04_input.txt"));
        rooms = lines.Select(
                             l => {
                                 var match = regex.Match(l);
                                 return new Room() { Name = match.Groups[1].Value, SectorId = int.Parse(match.Groups[2].Value), CheckSum = match.Groups[3].Value };
                             })
                     .ToArray();
    }

    [Benchmark]
    public long Solution1() {
        long result = 0;
        var temp = new List<(char, int)>();

        foreach (var room in rooms) {
            var n = room.Name.Replace("-", "").ToCharArray();
            Array.Sort(n);
            var prev = n[0];
            var count = 0;
            temp.Clear();
            for (int i = 0; i < n.Length; i++) {
                if (n[i] != prev) {
                    temp.Add((prev, count));
                    count = 1;
                    prev = n[i];
                } else {
                    ++count;
                }
            }

            temp.Add((prev, count));
            temp.Sort(
                    (a, b) => {
                        var c = b.Item2.CompareTo(a.Item2);
                        if (c != 0) {
                            return c;
                        }

                        return a.Item1.CompareTo(b.Item1);
                    });

            bool valid = true;
            for (int i = 0; i < room.CheckSum.Length; i++) {
                if (temp[i].Item1 != room.CheckSum[i]) {
                    valid = false;
                    break;
                }
            }

            if (valid) {
                result += room.SectorId;
            }
        }

        return result;
    }

    [Benchmark]
    public long Solution2() {
        foreach (var room in rooms) {
            var c = room.Name.ToCharArray();
            for (int i = 0; i < c.Length; i++) {
                if (c[i] == '-') {
                    continue;
                }

                var n = c[i] - 'a';
                n += room.SectorId;
                n = n % 26;
                c[i] = (char)('a' + n);
            }

            var decrypted = new string(c);
            if (decrypted == "northpole-object-storage") {
                return room.SectorId;
            }
        }

        return 0;
    }
}