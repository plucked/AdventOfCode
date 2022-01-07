using System.Security.Cryptography;
using System.Text;
using BenchmarkDotNet.Attributes;

namespace AdventOfCode.Year2016;

public class AoC2016Day05 {
    private string input;

    public AoC2016Day05(string? input = null) {
        this.input = input ?? "reyedfim";
    }

    [Benchmark]
    public string Solution1() {
        string password = "";
        long prevNonce = -1;
        for (int i = 0; i < 8; i++) {
            var nonce = GetNonce(prevNonce + 1);
            prevNonce = nonce;
            var res = MD5.HashData(Encoding.UTF8.GetBytes(input + nonce));
            password += Convert.ToHexString(res)[5];
        }

        return password.ToLower();
    }

    [Benchmark]
    public string Solution2() {
        string password = "        ";
        long prevNonce = -1;
        while (password.Contains(' ')) {
            var nonce = GetNonce(prevNonce + 1);
            prevNonce = nonce;
            var res = MD5.HashData(Encoding.UTF8.GetBytes(input + nonce));
            var hex = Convert.ToHexString(res);
            if (hex[5] >= '0' && hex[5] <= '7') {
                var pos = hex[5] - '0';
                if (password[pos] != ' ') {
                    continue;
                }

                password = password.Substring(0, pos) + hex[6] + password.Substring(pos + 1);
            }
        }

        return password.ToLower();
    }

    private unsafe long GetNonce(long offset) {
        var buffer = stackalloc byte[64];
        var hashResultBuffer = stackalloc byte[16];
        var inputBytes = Encoding.UTF8.GetBytes(input);
        for (int i = 0; i < inputBytes.Length; i++) {
            buffer[i] = inputBytes[i];
        }

        long nonce = offset;
        while (true) {
            var t = Encoding.UTF8.GetBytes(nonce.ToString());
            for (int i = 0; i < t.Length; i++) {
                buffer[i + inputBytes.Length] = t[i];
            }

            var bufferSpan = new ReadOnlySpan<byte>(buffer, inputBytes.Length + t.Length);
            var hashResult = new Span<byte>(hashResultBuffer, 16);
            MD5.HashData(bufferSpan, hashResult);
            if (hashResult[0] == 0 && hashResult[1] == 0 && hashResult[2] < 16) {
                return nonce;
            }

            ++nonce;
        }
    }
}