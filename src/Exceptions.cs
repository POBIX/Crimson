using System.Runtime.Serialization;

namespace Crimson;

public class RendererException : Exception
{
    public RendererException() { }
    public RendererException(string message) : base(message) { }
    public RendererException(string message, Exception inner) : base(message, inner) { }
    protected RendererException(SerializationInfo info, StreamingContext context) : base(info, context) { }
}