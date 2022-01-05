using System.Security.Cryptography;
using System.Text;
using BenchmarkDotNet.Attributes;

namespace AdventOfCode.Year2015;

/// <summary>
/// The problem is finding a md5 hash with leading zeroes. It is clear that the puzzle
/// is not about implementing a md5 hash algorithm, rather than detecting how to figure out
/// how the leading zeroes of a hex string can be read from a byte array.
///
/// A hex of 'FF' = 255
/// A hex of '0F' = 15
/// A hex of '00' = 0
///
/// By using this check, you can quickly iterate over the nonce until you find one where either the first bytes are
/// 0, 0, and less than 16 = 5 leading zeroes
/// 0, 0, 0 = 6 leading zeroes
/// </summary>
public class AoC2015Day04 {
    private byte[] input;

    public AoC2015Day04(string? customInput = null) {
        input = Encoding.UTF8.GetBytes(customInput ?? "iwrupvqb");
    }

    [Benchmark]
    public long Solution1() {
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