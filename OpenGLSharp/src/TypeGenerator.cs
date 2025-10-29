using System.Globalization;
using System.Text;
using System.Xml;

namespace OpenGLSharp;

internal record EnumInfo(Dictionary<string, string> Values, bool Bitmask)
{
    public EnumInfo(bool bitmask) : this(new(), bitmask) { }
}

internal static class TypeGenerator
{
    // very c-centric hard-to-parse section, which is also relatively short, so doing this manually is faster
    public static Dictionary<string, string> typedefs = new()
    {
        ["GLenum"] = "uint",
        ["GLboolean"] = "bool",
        ["GLbitfield"] = "uint",
        ["GLvoid"] = "void",
        ["GLbyte"] = "sbyte",
        ["GLubyte"] = "byte",
        ["GLshort"] = "short",
        ["GLushort"] = "ushort",
        ["GLint"] = "int",
        ["GLuint"] = "uint",
        ["GLclampx"] = "int",
        ["GLsizei"] = "int",
        ["GLfloat"] = "float",
        ["GLclampf"] = "float",
        ["GLdouble"] = "double",
        ["GLclampd"] = "double",
        ["GLeglClientBufferEXT"] = "IntPtr",
        ["GLeglImageOES"] = "IntPtr",
        ["GLchar"] = "char",
        ["GLcharARB"] = "char",
        ["GLhandleARB"] = "uint",
        ["GLhandleARB_APPLE"] = "IntPtr",
        ["GLhalf"] = "ushort",
        ["GLhalfARB"] = "ushort",
        ["GLfixed"] = "int",
        ["GLintptr"] = "IntPtr",
        ["GLintptrARB"] = "IntPtr",
        ["GLsizeiptr"] = "nint", 
        ["GLsizeiptrARB"] = "nint",
        ["GLint64"] = "long",
        ["GLint64EXT"] = "long",
        ["GLuint64"] = "ulong",
        ["GLuint64EXT"] = "ulong",
        ["GLsync"] = "IntPtr",
        ["struct _cl_context"] = "IntPtr",
        ["struct _cl_event"] = "IntPtr",
        ["GLDEBUGPROC"] = "DebugProcDelegate",
        ["GLDEBUGPROCARB"] = "DebugProcDelegate",
        ["GLDEBUGPROCKHR"] = "DebugProcDelegate",
        ["GLDEBUGPROCAMD"] = "DebugProcAMDDelegate",
        ["GLhalfNV"] = "ushort",
        ["GLvdpauSurfaceNV"] = "IntPtr",
        ["GLVULKANPROCNV"] = "GLVulkanProcNVDelegate",

        // not in the xml, but it makes my life easier
        ["void"] = "void",
        ["string"] = "string",
        ["StringBuilder"] = "StringBuilder",
    };

    public static Dictionary<string, EnumInfo> enums = new();

    private static void CreateEnum(string name, bool bitmask) => enums.Add(name, new(bitmask));

    /// <summary>
    /// Converts a name of the form 'GL_DYNAMIC_STORAGE_BIT' to a name of the form 'DynamicStorageBit'
    /// </summary>
    private static string GetEnumName(string glName)
    {
        StringBuilder sb = new();
        for (int i = 2; i < glName.Length; i++)
            sb.Append(glName[i] == '_' ? glName[++i] : char.ToLower(glName[i]));
        if (char.IsDigit(sb[0]))
        {
            if (char.IsDigit(sb[1]))
                Console.WriteLine("fuck alert");
            sb[1] = char.ToUpper(sb[1]);
            sb.Insert(0, '_');
        }
        return sb.ToString();
    }

    public static string Generate(XmlDocument specs)
    {
        foreach (XmlNode node in specs.GetElementsByTagName("enums"))
        {
            if (node.TryGetAttrib("group", out string rootGroup))
            {
                // SpecialNumbers hate is because it's just a random collection of numbers, not actually an enum
                if (!enums.ContainsKey(rootGroup) && rootGroup != "SpecialNumbers")
                    CreateEnum(rootGroup, node.TryGetAttrib("type", out _));
            }

            foreach (XmlNode value in node.ChildNodes)
            {
                if (value.Name != "enum" || !value.TryGetAttrib("group", out string group)) continue;

                string[] names = group.Split(',');
                foreach (string name in names)
                {
                    if (name == "SpecialNumbers") continue;
                    if (!enums.ContainsKey(name)) CreateEnum(name, false);
                    enums[name].Values.Add(GetEnumName(value.GetAttrib("name")), value.GetAttrib("value"));
                }
            }
        }

        StringBuilder sb = new();
        sb.AppendLine("// ReSharper disable InconsistentNaming\n");
        sb.AppendLine("namespace OpenGL;\n");
        foreach (var (name, (values, bitmask)) in enums)
        {
            if (bitmask) sb.AppendLine("[Flags]");
            sb.Append($"public enum {name}");
            if (values.Any(v =>
                {
                    if (v.Value.StartsWith("0x"))
                        return uint.Parse(v.Value[2..], NumberStyles.HexNumber) > int.MaxValue;
                    if (v.Value.StartsWith('-')) return false;
                    return uint.Parse(v.Value) > int.MaxValue;
                }))
            {
                sb.Append(" : uint");
            }
            sb.AppendLine();

            sb.AppendLine("{");
            foreach (var (key, value) in values)
                sb.AppendLine($"    {key} = {value},");
            sb.AppendLine("}");
            sb.AppendLine();
        }

        return sb.ToString();
    }
}
