using System.Text;
using BenchmarkDotNet.Attributes;

namespace AdventOfCode.Year2022;

public class AoC2022Day13 {
    private class Node : IComparable<Node>, ICloneable {
        public class Value {
            public bool IsNumber {
                get { return Number.HasValue; }
            }
            public int? Number { get; set; }
            public Node Node { get; set; }

            public Value(int number) {
                Number = number;
            }

            public Value(Node node) {
                Node = node;
            }

            public void TurnNumberIntoNode(Node parent) {
                Node = new Node { Parent = parent };
                Node.Items.Add(new Value(Number.Value));
                Number = null;
            }

            public override string ToString() {
                return IsNumber ? Number.ToString() : Node.ToString();
            }
        }

        public List<Value> Items { get; } = new();
        public Node? Parent { get; set; }

        public static Node Parse(string input) {
            var current = new Node();
            var charArray = input.ToArray();
            for (int i = 1; i < charArray.Length - 1; i++) {
                switch (charArray[i]) {
                    case '[':
                        var newNode = new Node { Parent = current };
                        current.Items.Add(new Value(newNode));
                        current = newNode;
                        break;
                    case ']':
                        current = current.Parent ?? current;
                        break;
                    case ',':
                        continue;
                    default:
                        var numberString = new StringBuilder();
                        while (char.IsDigit(charArray[i])) {
                            numberString.Append(charArray[i]);
                            i++;
                        }

                        --i;
                        current.Items.Add(new Value(int.Parse(numberString.ToString())));
                        break;
                }
            }

            return current;
        }

        public int CompareTo(Node other) {
            var a = this.Clone() as Node;
            var b = other.Clone() as Node;

            for (int j = 0; j < a.Items.Count; j++) {
                var thisItem = a.Items[j];

                if (b.Items.Count <= j) {
                    return 1;
                }

                var otherItem = b.Items[j];

                if (thisItem.IsNumber && otherItem.IsNumber) {
                    if (thisItem.Number < otherItem.Number) {
                        return -1;
                    }

                    if (thisItem.Number > otherItem.Number) {
                        return 1;
                    }
                } else {
                    if (thisItem.IsNumber) {
                        thisItem.TurnNumberIntoNode(this);
                    }

                    if (otherItem.IsNumber) {
                        otherItem.TurnNumberIntoNode(other);
                    }

                    var result = thisItem.Node.CompareTo(otherItem.Node);
                    if (result != 0) {
                        return result;
                    }
                }
            }

            if (a.Items.Count < b.Items.Count) {
                return -1;
            }

            return 0;
        }

        public object Clone() {
            var clone = new Node();
            foreach (var item in Items) {
                clone.Items.Add(item.IsNumber ? new Value(item.Number.Value) : new Value((Node)item.Node.Clone()));
            }

            return clone;
        }
    }

    private List<Node> nodes;

    public AoC2022Day13(string input) {
        var lines = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
        nodes = lines.Select(Node.Parse).ToList();
    }

    [Benchmark]
    public long Solution1() {
        var pairIndex = 1;
        var rightOrder = 0;
        for (int i = 0; i < nodes.Count; i += 2) {
            var first = nodes[i];
            var second = nodes[i + 1];

            var result = first.CompareTo(second);
            if (result < 0) {
                rightOrder += pairIndex;
            }

            pairIndex++;
        }

        return rightOrder;
    }

    [Benchmark]
    public long Solution2() {
        var keyA = new Node();
        var keyASub = new Node() { Parent = keyA };
        keyASub.Items.Add(new Node.Value(2));
        keyA.Items.Add(new Node.Value(keyASub));
        nodes.Add(keyA);

        var keyB = keyA.Clone() as Node;
        keyB.Items[0].Node.Items[0].Number = 6;
        nodes.Add(keyB);

        nodes.Sort();

        return (nodes.IndexOf(keyA) + 1) * (nodes.IndexOf(keyB) + 1);
    }
}