using BenchmarkDotNet.Attributes;

namespace AdventOfCode.Year2022;

public class AoC2022Day14 {
    private (int x, int y) dropStart;
    private char[,] map;

    public AoC2022Day14(string input) {
        var lines = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
        var rocksLine = new List<(int x1, int y1, int x2, int y2)>();
        foreach (var line in lines) {
            var parts = line.Split(" -> ", StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < parts.Length - 1; i++) {
                var a = parts[i];
                var b = parts[i + 1];
                var aSplit = a.Split(',');
                var bSplit = b.Split(',');
                // parse 
                var x1 = int.Parse(aSplit[0]);
                var y1 = int.Parse(aSplit[1]);

                var x2 = int.Parse(bSplit[0]);
                var y2 = int.Parse(bSplit[1]);

                // swap if x1 > x2
                // also swap y1 and y2
                if (x1 > x2) {
                    var temp = x1;
                    x1 = x2;
                    x2 = temp;

                    temp = y1;
                    y1 = y2;
                    y2 = temp;
                }

                // swap if y1 > y2
                // also swap x1 and x2
                if (y1 > y2) {
                    var temp = y1;
                    y1 = y2;
                    y2 = temp;

                    temp = x1;
                    x1 = x2;
                    x2 = temp;
                }

                rocksLine.Add((x1, y1, x2, y2));
            }
        }

        var xMin = rocksLine.Min(r => r.x1);
        xMin = Math.Min(xMin, rocksLine.Min(r => r.x2));

        var xMax = rocksLine.Max(r => r.x1);
        xMax = Math.Max(xMax, rocksLine.Max(r => r.x2));

        var yMax = rocksLine.Max(r => r.y1);
        yMax = Math.Max(yMax, rocksLine.Max(r => r.y2));

        // remap x
        var xRange = xMax - xMin;
        // remap the drop start
        dropStart = (500 - xMin, 0);

        map = new char[xRange + 1, yMax + 1];

        // initialize the map with '.'
        for (int x = 0; x < map.GetLength(0); x++) {
            for (int y = 0; y < map.GetLength(1); y++) {
                map[x, y] = '.';
            }
        }

        foreach (var rock in rocksLine) {
            // draw each rock line with a #
            for (int x = rock.x1; x <= rock.x2; x++) {
                for (int y = rock.y1; y <= rock.y2; y++) {
                    map[x - xMin, y] = '#';
                }
            }
        }
    }

    [Benchmark]
    public long Solution1() {
        var sandRestCount = 0;
        while (true) {
            var dropOfTheMap = false;
            var sand = dropStart;
            while (true) {
                if (map[sand.x, sand.y] != '.') {
                    dropOfTheMap = true;
                    break;
                }

                if (sand.y + 1 >= map.GetLength(1)) {
                    dropOfTheMap = true;
                    break;
                }

                var belowOnMap = map[sand.x, sand.y + 1];
                if (belowOnMap == '.') {
                    sand.y++;
                } else {
                    var diagonals = new[] { (sand.x - 1, sand.y + 1), (sand.x + 1, sand.y + 1) };

                    // check if diagonal[0] is on map
                    if (diagonals[0].Item1 < 0) {
                        dropOfTheMap = true;
                        break;
                    }

                    var bottomLeftOnMap = map[diagonals[0].Item1, diagonals[0].Item2];
                    if (bottomLeftOnMap == '.') {
                        sand = diagonals[0];
                        continue;
                    }

                    // check if diagonal[1] is on map
                    if (diagonals[1].Item1 >= map.GetLength(0)) {
                        dropOfTheMap = true;
                        break;
                    }

                    var bottomRightOnMap = map[diagonals[1].Item1, diagonals[1].Item2];
                    if (bottomRightOnMap == '.') {
                        sand = diagonals[1];
                        continue;
                    }

                    map[sand.x, sand.y] = 'o';
                    ++sandRestCount;
                    break;
                }
            }

            if (dropOfTheMap) {
                break;
            }
        }

        return sandRestCount;
    }

    [Benchmark]
    public long Solution2() {
        // okay this is a lazy solution I know
        // TODO: refactor this

        var xExtension = (map.GetLength(1) + 2) * 2;

        var newMap = new char[map.GetLength(0) + xExtension, map.GetLength(1)];
        for (int x = 0; x < newMap.GetLength(0); x++) {
            for (int y = 0; y < newMap.GetLength(1); y++) {
                newMap[x, y] = '.';
            }
        }

        for (int x = 0; x < map.GetLength(0); x++) {
            for (int y = 0; y < map.GetLength(1); y++) {
                newMap[x + xExtension / 2, y] = map[x, y];
            }
        }

        map = newMap;

        var newMap2 = new char[map.GetLength(0), map.GetLength(1) + 2];
        for (int x = 0; x < newMap2.GetLength(0); x++) {
            for (int y = 0; y < newMap2.GetLength(1); y++) {
                if (y == newMap2.GetLength(1) - 2) {
                    newMap2[x, y] = '.';
                } else if (y == newMap2.GetLength(1) - 1) {
                    newMap2[x, y] = '#';
                } else {
                    newMap2[x, y] = map[x, y];
                }
            }
        }

        map = newMap2;

        dropStart = (dropStart.x + xExtension / 2, dropStart.y);

        return Solution1();
    }
}