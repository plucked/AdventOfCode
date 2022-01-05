using System.Reflection;

namespace AdventOfCode.Utilities;

public static class EmbeddedInput {
    private static readonly Dictionary<string, string> cache = new();

    public static string ReadAllText(string resourceName) {
        if (cache.TryGetValue(resourceName, out var c)) {
            return c;
        }

        using Stream? stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(GetAssemblyResourceName(resourceName));
        if (stream == null) {
            throw new Exception($"Unable to create stream from resource '{resourceName}'");
        }

        using var reader = new StreamReader(stream);
        var result = reader.ReadToEnd();
        cache[resourceName] = result;
        return result;
    }

    public static string[] ReadAllLines(string resourceName) {
        if (cache.TryGetValue(resourceName, out var c)) {
            return c.Split(Environment.NewLine);
        }

        using Stream? stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(GetAssemblyResourceName(resourceName));
        if (stream == null) {
            throw new Exception($"Unable to create stream from resource '{resourceName}'");
        }

        using var reader = new StreamReader(stream);
        var result = reader.ReadToEnd();
        cache[resourceName] = result;
        return result.Split(Environment.NewLine);
    }

    private static string GetAssemblyResourceName(string resourceName) {
        resourceName = resourceName.Replace("/", ".");
        string assemblyResourceName = Assembly.GetExecutingAssembly().GetManifestResourceNames().Single(str => str.EndsWith(resourceName));
        return assemblyResourceName;
    }
}