using System.Security.Cryptography;
using System.Text;
using BenchmarkDotNet.Attributes;

namespace AdventOfCode.Year2015;

public class AoC2015Day04 {
    private byte[] input;
    
    [GlobalSetup(Targets = new[] { nameof(Solution1), nameof(Solution2) })]
    public void BenchmarkSetup() {
        Setup();
    }

    public void Setup(string? customInput = null) {
        input = Encoding.UTF8.GetBytes(customInput ?? "iwrupvqb");
    }

    [Benchmark]
    public unsafe long Solution1() {
        return GetNonce(true);
    }

    [Benchmark]
    public long Solution2() {
        return GetNonce(false);
    }
    
    private unsafe long GetNonce(bool fiveZeroes) {
        var buffer = stackalloc byte[64];
        var hashResultBuffer = stackalloc byte[16];
        for (int i = 0; i < input.Length; i++) {
            buffer[i] = input[i];
        }

        long nonce = 0;
        while (true) {
            var t = Encoding.UTF8.GetBytes(nonce.ToString());
            for (int i = 0; i < t.Length; i++) {
                buffer[i + input.Length] = t[i];
            }
            
            var bufferSpan = new ReadOnlySpan<byte>(buffer, input.Length + t.Length);
            var hashResult = new Span<byte>(hashResultBuffer, 16);
            MD5.HashData(bufferSpan, hashResult);
            if (hashResult[0] == 0 && hashResult[1] == 0 && (fiveZeroes ? hashResult[2] < 16 : hashResult[2] == 0)) {
                return nonce;
            }

            ++nonce;
        }
    } 
}