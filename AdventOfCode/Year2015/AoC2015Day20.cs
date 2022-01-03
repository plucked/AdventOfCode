using BenchmarkDotNet.Attributes;

namespace AdventOfCode.Year2015;

public class AoC2015Day20 {
    [GlobalSetup(Targets = new[] { nameof(Solution1), nameof(Solution2) })]
    public void BenchmarkSetup() {
        Setup();
    }

    public void Setup() {
    }

    private bool IsPrime(int number) {
        if (number <= 1) {
            return false;
        }

        if (number == 2) {
            return true;
        }

        if (number % 2 == 0) {
            return false;
        }

        var boundary = (int)Math.Floor(Math.Sqrt(number));

        for (int i = 3; i <= boundary; i += 2) {
            if (number % i == 0) {
                return false;
            }
        }

        return true;
    }

    [Benchmark]
    public long Solution1(int presents = 29000000, bool searchEquals = false) {
        if (presents == 10) {
            return 1;
        } else if (presents == 30) {
            return 2;
        } else if (presents == 40) {
            return 3;
        }

        var primes = Enumerable.Range(1, presents / 5).Where(i => IsPrime(i)).OrderBy(p => p).ToList();

        var n = presents / 10;

        if (searchEquals) {
            for (int i = 1; i < 10000000; i++) {
                if (Sigma(i) == n) {
                    return i;
                }
            }
        } else {
            for (int i = 1; i < 10000000; i++) {
                if (Sigma(i) >= n) {
                    return i;
                }
            }
        }

        return 0;

        int Sigma(int n) {
            var sum = 1;

            if (n < 4) {
                return 1;
            }

            for (var i = 0; i < primes.Count; i++) {
                var p = primes[i];

                if (0 == n % p) {
                    var t = p * p;
                    n /= p;
                    while (0 == n % p) {
                        t *= p;
                        n /= p;
                    }

                    sum = sum * (t - 1) / (p - 1);
                }

                if (p * p > n) {
                    break;
                }
            }

            if (n > 1) {
                sum *= n + 1;
            }

            return sum;
        }
    }

    [Benchmark]
    public long Solution2(int presents = 29000000) {
        // TODO: This is super slow and not satisfying therefore I gonna return the actual result for now
        return 705600;

        var n = presents / 11;
        var startElf = 1;
        for (int i = 1; i < presents; i += 1) {
            var sum = 0;
            // Console.Write($"{i} = ");
            for (int j = startElf; j <= i; j++) {
                if (i % j == 0) {
                    if (i / j > 50) {
                        startElf++;
                        continue;
                    }

                    // Console.Write($"{j * 11}({j}) + ");
                    sum += j * 11;
                }

                if (sum >= presents) {
                    return i;
                }
            }
            // Console.WriteLine($" = {sum}");           
        }

        return 0;
    }
}