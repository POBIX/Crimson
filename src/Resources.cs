using System.IO;
using System.Reflection;

namespace Crimson
{
    internal static class Resources
    {
        private static Assembly assembly = Assembly.GetExecutingAssembly();

        public static Stream Load(string name) =>
            assembly.GetManifestResourceStream($"Crimson.{name.Replace('/', '.')}");

        public static string Read(string name)
        {
            using Stream s = Load(name);
            using var reader = new StreamReader(s);
            return reader.ReadToEnd();
        }
    }
}
