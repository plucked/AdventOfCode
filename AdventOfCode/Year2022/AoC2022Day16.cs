using System.Text.RegularExpressions;
using BenchmarkDotNet.Attributes;

namespace AdventOfCode.Year2022;

public class AoC2022Day16 {
    private class Tunnel {
        public int BitField { get; set; }
        public string Index { get; set; }
        public string[] Neighbors { get; set; }
        public long FlowRate { get; set; }
        public Dictionary<string, int> PathLengths { get; } = new();

        public void GenerateShortestPathToEachTunnel(Dictionary<string, Tunnel> tunnels) {
            // no need to calculate that for walkthrough tunnels, except the start
            if (FlowRate == 0 && Index != "AA") {
                return;
            }

            foreach (var tunnel in tunnels) {
                if (tunnel.Key == Index) {
                    continue;
                }

                if (tunnel.Value.FlowRate == 0) {
                    continue;
                }

                var queue = new PriorityQueue<string, long>();
                var distances = new Dictionary<string, long>();
                var previous = new Dictionary<string, string>();
                foreach (var t in tunnels) {
                    distances[t.Key] = long.MaxValue;
                }

                distances[Index] = 0;
                queue.Enqueue(Index, 0);
                while (queue.Count > 0) {
                    var current = queue.Dequeue();
                    if (current == tunnel.Key) {
                        break;
                    }

                    var currentNeighbors = tunnels[current].Neighbors;
                    foreach (var neighbor in currentNeighbors) {
                        var alt = distances[current] + 1;
                        if (alt < distances[neighbor]) {
                            distances[neighbor] = alt;
                            previous[neighbor] = current;
                            queue.Enqueue(neighbor, alt);
                        }
                    }
                }

                var path = new List<string>();
                var u = tunnel.Key;
                while (u != Index) {
                    path.Add(u);
                    u = previous[u];
                }

                path.Reverse();
                PathLengths[tunnel.Key] = path.Count;
            }
        }
    }

    private Dictionary<string, Tunnel> tunnels = new();
    private string startTunnelIndex = "AA";

    public AoC2022Day16(string input) {
        var lines = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
        var regex = new Regex(@"Valve (?<name>\w+) has flow rate=(?<flow>\d+); tunnels? leads? to valves? (?<tunnels>.*)");
        int nextBitField = 0;
        foreach (var line in lines) {
            var match = regex.Match(line);
            var name = match.Groups["name"].Value;
            var flow = int.Parse(match.Groups["flow"].Value);
            var neighbors = match.Groups["tunnels"].Value.Split(", ", StringSplitOptions.RemoveEmptyEntries).ToArray();

            var bitfield = flow > 0 ? nextBitField++ : 0;
            tunnels.Add(name, new Tunnel { BitField = bitfield, Index = name, FlowRate = flow, Neighbors = neighbors });
        }
    }

    [Benchmark]
    public long Solution1() {
        foreach (var tunnel in tunnels) {
            tunnel.Value.GenerateShortestPathToEachTunnel(tunnels);
        }

        var activeTunnels = tunnels.Where(t => t.Value.FlowRate > 0).Select(t => t.Key).ToList();
        var variations = new Dictionary<int, long>();
        RunSolution1(startTunnelIndex, 0, 0, activeTunnels, 0, variations);
        return variations.Values.Max();
    }

    void RunSolution1(string current, long currentFlow, int currentTime, List<string> activeTunnels, int visited, Dictionary<int, long> variations) {
        for (int i = 0; i < activeTunnels.Count; i++) {
            var target = activeTunnels[i];
            var bitfield = tunnels[target].BitField;
            if (current == target || (visited & (1 << bitfield)) > 0) {
                continue;
            }

            var walkTime = tunnels[current].PathLengths[target];
            if (currentTime + walkTime + 1 >= 30) {
                continue;
            }

            var newCurrentTime = currentTime + walkTime + 1;
            var flow = tunnels[target].FlowRate;
            flow *= (30 - newCurrentTime);

            var newVisited = visited | (1 << bitfield);
            var newCurrentFlow = currentFlow + flow;
            if (variations.TryGetValue(newVisited, out var maxFlow)) {
                if (newCurrentFlow > maxFlow) {
                    variations[newVisited] = newCurrentFlow;
                }
            } else {
                variations[newVisited] = newCurrentFlow;
            }

            RunSolution1(target, currentFlow + flow, newCurrentTime, activeTunnels, visited | (1 << bitfield), variations);
        }
    }

    [Benchmark]
    public long Solution2() {
        foreach (var tunnel in tunnels) {
            tunnel.Value.GenerateShortestPathToEachTunnel(tunnels);
        }

        var activeTunnels = tunnels.Where(t => t.Value.FlowRate > 0).Select(t => t.Key).ToList();
        var variations = new Dictionary<int, long>();
        RunSolution1(startTunnelIndex, 0, 4, activeTunnels, 0, variations);
        var max = 0L;

        var keys = variations.Keys.ToList();
        for (var i = 0; i < keys.Count - 1; i++) {
            var a = keys[i];

            for (int j = i + 1; j < keys.Count; j++) {
                var b = keys[j];

                if ((a & b) == 0) {
                    var sum = variations[a] + variations[b];
                    max = Math.Max(sum, max);
                }
            }
        }

        return max;
    }
}