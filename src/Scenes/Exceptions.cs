using System;

namespace Crimson.Scenes
{
    public class UnexpectedTokenException : Exception
    {
        public UnexpectedTokenException(int line) : base($"Unexpected token on line {line}.") { }
    }

    public class IndentException : Exception
    {
        public IndentException(int line) : base($"Wrong indent on line {line}") { }
    }

    public class TypeNotFoundException : Exception
    {
        public TypeNotFoundException(int line) : base($"A non-existent type was attached to an entity on line {line}.") { }
    }

    public class NotComponentException : Exception
    {
        public NotComponentException(int line)
            : base($"A type that doesn't inherit from Crimson.Component was attached to an entity on line {line}. ") { }
    }

    public class TypeMismatchException : Exception
    {
        public TypeMismatchException(int line) : base($"Constant with wrong type was passed to variable on line {line}") { }
    }

    public class CustomParserException : Exception
    {
        public CustomParserException(int line) : base($"A custom parser threw an exception on line {line}") { }
    }

    public class NoSetHandlerException : Exception
    {
        public NoSetHandlerException(int line, Type type) : base(
            $"Line {line}: Type {type} does not have a set handler." +
            " Register one using Crimson.Scenes.SceneReader.RegisterSetHandler()."
        ) { }
    }
}
