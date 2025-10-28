using System.Collections;
using System.Reflection;
using Crimson;

namespace Crimson;

public static class INI
{
    private const StringSplitOptions SplitOptions =
        StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries;

    public struct Section : IEnumerable<KeyValuePair<string, object>>
    {
        public string Name { get; set; }
        public Dictionary<string, object> Fields { get; set; }

        public IEnumerator<KeyValuePair<string, object>> GetEnumerator() =>
            Fields?.GetEnumerator() ?? Enumerable.Empty<KeyValuePair<string, object>>().GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public Section(string name = "")
        {
            Name = name;
            Fields = new();
        }
    }

    private static object ParseString(string str)
    {
        if (int.TryParse(str, out var i))
            return i;
        if (float.TryParse(str, out var f))
            return f;
        if (bool.TryParse(str, out var b))
            return b;
        if (Vector2.TryParse(str, out var v2))
            return v2;
        if (Vector3.TryParse(str, out var v3))
            return v3;
        if (Vector4.TryParse(str, out var v4))
            return v4;
        if (Rect.TryParse(str, out var r))
            return r;
        if (Color.TryParse(str, out var c))
            return c;
        if (str!.StartsWith('"') && str.EndsWith('"'))
            return str[1..^1];
        if (str.StartsWith('[') && str.EndsWith(']'))
            return ParseArr(str).ToArray();

        return str;
    }

    private static IEnumerable<object> ParseArr(string str)
    {
        foreach (string el in str[1..^1].Split(',', SplitOptions))
            yield return ParseString(el);
    }

    public static IEnumerable<Section> Parse(string source)
    {
        Section current = new();
        foreach (string line in source.Split('\n', SplitOptions))
        {
            if (string.IsNullOrWhiteSpace(line) || line.StartsWith(';')) continue;

            if (line.StartsWith('[') && line.EndsWith(']'))
            {
                if (current.Name != "") // if this section isn't empty
                    yield return current;
                current = new Section(line[1..^1]);
                continue;
            }
            string[] split = line.Split('=');
            current.Fields.Add(split[0], ParseString(split[1]));
        }

        yield return current;
    }

    public static void ParseSettings(Section settings)
    {
        foreach (var (key, value) in settings)
        {
            PropertyInfo prop = typeof(Engine).GetProperty(key, BindingFlags.Static | BindingFlags.Public)!;
            prop.SetValue(
                null,
                value is IConvertible ?
                    Convert.ChangeType(value, prop.PropertyType) :
                    value
            );
        }
    }

    public static void ParseInput(Section input)
    {
        foreach (var (key, value) in input)
        {
            var arr = ((object[])value).Cast<string>().ToArray();
            Key[] keys = new Key[arr.Length];
            for (int i = 0; i < arr.Length; i++)
                keys[i] = Enum.Parse<Key>(arr[i]);
            Input.AddAction(key, new KeyboardAction(keys));
        }
    }

    public static void ParseAction(Section action)
    {
        foreach (InputBase i in Input.Handlers)
        {
            if (!action.Fields.TryGetValue(i.GetType().Name, out object val)) continue;

            string[] keys = ((object[])val).Cast<string>().ToArray();
            Type t = typeof(Action<>).MakeGenericType(i.Type);
            IInputAction a = (IInputAction)Activator.CreateInstance(t);
            foreach (string s in keys)
                a!.Add(Enum.Parse(i.Type, s));
        }
    }

    public static void ParseInput(IEnumerable<Section> subsections)
    {
        foreach (Section s in subsections)
            ParseAction(s);
    }
}

internal static class EnumerableSectionExtensions
{
    public static INI.Section? Find(this IEnumerable<INI.Section> s, string name) =>
        s.FirstOrDefault(i => i.Name == name);

    public static IEnumerable<INI.Section> FindSubs(this IEnumerable<INI.Section> s, string name) =>
        s.Where(i => i.Name.StartsWith($"{name}."));
}
