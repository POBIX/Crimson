using System.Text;
using System.Xml;

namespace OpenGLSharp;

internal static class CommandGenerator
{
    private static string ToCSharp(string type, string info, XmlAttributeCollection? attributes, bool param)
    {
        string actual = TypeGenerator.typedefs[type];

        string[] split = info.Split(' ');

        bool isPtr = split.Any(s => s.Contains('*'));
        bool isVoidPtr = false;
        if (isPtr) isVoidPtr = type == "void";

        bool isArr = attributes?["len"] != null && attributes["len"]!.Value != "1" && !isVoidPtr &&
                     (type != "string" && type != "StringBuilder" || isPtr);

        bool isOut = false;
        if (param && isPtr && !isArr && !split.Contains("const") && !isVoidPtr)
        {
            actual = actual.Insert(0, "out ");
            isOut = true;
        }

        if (attributes != null)
        {
            // if the type is an enum or bitfield, and it has a group, then its group is the enum's actual name.
            // unless it's SampleMaskNV, which is taken as a parameter but not defined anywhere within the specs.
            if (attributes["group"] != null && type is "GLenum" or "GLbitfield" or "GLint" &&
                TypeGenerator.enums.ContainsKey(attributes["group"]!.InnerText))
            {
                actual = attributes["group"]!.InnerText;
                if (actual == "Boolean") actual = "bool";
            }
            if (isArr) actual += "[]";
        }

        if (isPtr)
        {
            if (isOut) actual = actual.Replace(type, "IntPtr");
            else if (!isArr) actual += '*';
        }

        return actual;
    }

    private static void GetCSharpSignature(XmlNode parent, bool param, out string name, out string type)
    {
        // i have no idea why, but the spec has information like whether a parameter is const or pointer without a tag.
        // even though the actual type itself does have a tag (unless it's void!).
        // this makes it quite annoying to parse, we have to get the name and type and then delete them to find the info.

        const string stringText = "<ptype>GLchar</ptype> *";
        const string ubyteText = "<ptype>GLubyte</ptype> *";

        // if there's a const char*, it's a string.
        // if there's a char*, it's a StringBuilder.
        // also, some strings are written as ubytes in the specs with a group="String".
        if (parent.InnerXml.Contains("const " + stringText))
            parent.InnerXml = parent.InnerXml.Replace(stringText, "<ptype>string</ptype>");
        else if (parent.InnerXml.Contains(stringText))
            parent.InnerXml = parent.InnerXml.Replace(stringText, "<ptype>StringBuilder</ptype>");
        else if (parent.TryGetAttrib("group", out string g) && g == "String")
        {
            if (parent.InnerXml.Contains("const " + ubyteText))
                parent.InnerXml = parent.InnerXml.Replace(ubyteText, "<ptype>string</ptype>");
            else if (parent.InnerXml.Contains(ubyteText))
                parent.InnerXml = parent.InnerXml.Replace(stringText, "<ptype>StringBuilder</ptype>");
        }

        XmlNode? typeNode = parent["ptype"];
        type = typeNode?.InnerText ?? "void";
        XmlNode nameNode = parent["name"]!;
        name = nameNode.InnerText;

        if (typeNode != null)
            parent.RemoveChild(typeNode);
        parent.RemoveChild(nameNode);

        string info = parent.InnerText.Replace("void", "").Trim();
        while (info.Contains("  "))
            info = info.Replace("  ", " ");
        type = ToCSharp(type, info, parent.Attributes, param);
    }

    public static string Generate(XmlDocument specs)
    {
        StringBuilder sb = new();
        sb.AppendLine("// ReSharper disable InconsistentNaming\n");
        sb.AppendLine("#nullable disable");
        sb.AppendLine("using System.Runtime.InteropServices;");
        sb.AppendLine("using System.Text;");
        sb.AppendLine("namespace OpenGL;\n");
        sb.AppendLine("public static unsafe partial class Gl");
        sb.AppendLine("{");

        sb.AppendLine(
            "    public delegate void DebugProcDelegate(DebugSource source, DebugType type, uint id, " +
            "DebugSeverity severity, int length, IntPtr message, IntPtr userParam);\n"
        );
        sb.AppendLine(
            "    public delegate void DebugProcAMDDelegate(uint id, DebugType category, DebugSeverity severity, " +
            "int length, IntPtr message, IntPtr userParam);\n"
        );

        sb.AppendLine("    public delegate IntPtr GLVulkanProcNVDelegate();");

        List<string> names = new();

        foreach (XmlNode command in specs.GetElementsByTagName("commands")[0]!.ChildNodes)
        {
            if (!command.HasChildNodes) continue;
            GetCSharpSignature(command["proto"]!, false, out string name, out string type);

            Dictionary<string, string> ps = new();
            foreach (XmlNode param in command.SelectNodes("param")!)
            {
                GetCSharpSignature(param, true, out string n, out string t);
                if (n is "params" or "event" or "ref" or "string" or "object" or "base" or "in") n = $"@{n}";
                ps.Add(n, t);
            }

            sb.Append($"    internal delegate {type} {name}Delegate(");

            string paramList = "";
            string paramListIntPtr = "";
            foreach (var (n, t) in ps)
            {
                paramList += $"{t} {n}, ";
                paramListIntPtr += $"{(t.Contains('*') ? "IntPtr" : t)} {n}, ";
            }
            if (ps.Count != 0)
            {
                paramList = paramList.Remove(paramList.Length - 2, 2);
                paramListIntPtr = paramListIntPtr.Remove(paramListIntPtr.Length - 2, 2);
            }
            sb.AppendLine($"{paramList});\n");
            sb.AppendLine($"    internal static {name}Delegate {name};\n");
            sb.Append($"    public static {type} {name[2..]}({paramList}) => {name}(");
            foreach (var (n, t) in ps)
                sb.Append($"{(t.Contains("out ") ? "out " : "")}{n}, ");
            if (ps.Count != 0) sb.Remove(sb.Length - 2, 2);
            sb.AppendLine(");\n");

            if (paramList.Contains('*'))
            {
                sb.Append($"    public static {type} {name[2..]}({paramListIntPtr}) => {name}(");
                foreach (var (n, t) in ps)
                    sb.Append(t.Contains('*') ?
                        $"{(t.Contains("out ") ? "out " : "")}{(t != "void*" ? $"({t})" : "")}{n}.ToPointer(), " :
                        $"{(t.Contains("out ") ? "out " : "")}{n}, ");
                if (ps.Count != 0) sb.Remove(sb.Length - 2, 2);
                sb.AppendLine(");\n");
            }

            names.Add(name);
        }

        sb.AppendLine("    private static T LoadFunction<T>(IntPtr addr, out T del) =>");
        sb.AppendLine("        del = addr == IntPtr.Zero ? default : Marshal.GetDelegateForFunctionPointer<T>(addr);\n");

        sb.AppendLine("    public static void LoadFunctions(Func<string, IntPtr> getProcAddress)");
        sb.AppendLine("    {");
        foreach (string name in names)
            sb.AppendLine(
                $"        LoadFunction(getProcAddress(\"{name}\"), out {name});");
        sb.AppendLine("    }");

        sb.AppendLine("}");

        return sb.ToString();
    }
}
