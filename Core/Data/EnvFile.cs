using System;
using System.Collections.Generic;
using System.IO;

namespace ReportForge.Data
{
    /// <summary>
    /// Reads mssql.env from the solution root. Temporary — replace with App.config + encryption later.
    /// </summary>
    internal static class EnvFile
    {
        private static readonly Dictionary<string, string> _values = Load();

        public static string Get(string key)
        {
            if (!_values.TryGetValue(key, out var v))
                throw new InvalidOperationException($"Missing key '{key}' in mssql.env");
            return v;
        }

        private static Dictionary<string, string> Load()
        {
            var path = FindFile("mssql.env")
                ?? throw new FileNotFoundException("mssql.env not found walking up from " + AppDomain.CurrentDomain.BaseDirectory);

            var dict = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            foreach (var raw in File.ReadAllLines(path))
            {
                var line = raw.Trim();
                if (line.Length == 0 || line.StartsWith("#")) continue;
                var eq = line.IndexOf('=');
                if (eq <= 0) continue;
                dict[line.Substring(0, eq).Trim()] = line.Substring(eq + 1).Trim();
            }
            return dict;
        }

        private static string FindFile(string name)
        {
            var dir = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);
            while (dir != null)
            {
                var candidate = Path.Combine(dir.FullName, name);
                if (File.Exists(candidate)) return candidate;
                dir = dir.Parent;
            }
            return null;
        }
    }
}
