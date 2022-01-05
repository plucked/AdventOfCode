using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Diagnosers;
using BenchmarkDotNet.Exporters;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;

public static class Program {
    private static void Main(string[] args) {
        var config = ManualConfig.CreateMinimumViable().AddJob(Job.Default).AddExporter(MarkdownExporter.Default).AddDiagnoser(MemoryDiagnoser.Default);

        BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args, config);
    }
}