﻿using BenchmarkDotNet.Running;

public static class Program {
    static void Main(string[] args) => BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args);    
}
