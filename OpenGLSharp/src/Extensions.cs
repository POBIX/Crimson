using System.Xml;

namespace OpenGLSharp;

internal static class Extensions
{
    public static string GetAttrib(this XmlNode node, string name) =>
        node.Attributes![name]!.Value;

    public static bool TryGetAttrib(this XmlNode node, string name, out string value)
    {
        value = node.Attributes?[name]?.Value ?? "";
        return value != "";
    }
}
