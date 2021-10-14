using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Crimson.Scenes
{
    internal delegate bool SetHandlerDelegateI(string source, out object result);

    public delegate bool SetHandlerDelegate<T>(string source, out T result);

    internal static class Parser
    {
        public enum Indent
        {
            None,
            One,
            Two,
        }

        public enum Context
        {
            None,
            Entity,
            Component,
            Lighting, // the container for the lights
            Light, // the lights themselves
        }

        private static Lex[] lex;
        private static int i;

        private static Lex curr;

        private static List<string> namespaces = new() {"Crimson"};

        private static Func<Token?>[] table = new Func<Token?>[Enum.GetNames<Token>().Length];

        private static List<ISceneObject> output = new();

        private static Indent indent = Indent.None;
        private static Context context = Context.None;

        private static Entity entity = null;
        private static Component component = null;

        private static Dictionary<Type, SetHandlerDelegateI> setHandlers = new();
        private static Dictionary<string, string> vars = new();

        private static void SetFunc(Token t, Func<Token?> a) =>
            table[(int)t] = a;

        private static Lex GetNext(Token? expect)
        {
            curr = lex[i++];
            if (expect == null || curr.token == expect) return curr;
            throw new UnexpectedTokenException(curr.line);
        }

        static Parser()
        {
            SetFunc(Token.None, ParseNone);
            SetFunc(Token.Using, ParseUsing);
            SetFunc(Token.Indent, ParseIndent);
            SetFunc(Token.Unindent, ParseUnindent);
            SetFunc(Token.Newline, () => null);
            SetFunc(Token.Lighting, ParseLighting);

            SetFunc(Token.EOF, ParseEOF);
        }

        private static Token? ParseLighting()
        {
            context = Context.Lighting;
            return null;
        }

        private static Token? ParseEOF()
        {
            if (component != null)
            {
                entity.AddComponent(component);
                output.Add(entity);
            }
            else if (entity != null) output.Add(entity);

            return Token.EOF;
        }

        private static Token? ParseNone()
        {
            // this function should get called when you want to parse an entity, a component, or set a variable.

            switch (indent)
            {
                case Indent.None:
                    if (lex[i].token == Token.Equals)
                    {
                        // a variable declaration.
                        string key = curr.source;
                        GetNext(Token.Equals);
                        GetNext(null); // null and not None because it can also be a an Int, a String, etc.
                        vars.Add(key, curr.source);
                        GetNext(Token.Newline);
                        return null;
                    }
                    return ParseEntity();
                case Indent.One:
                    if (entity == null) throw new IndentException(curr.line);
                    return ParseComponent();
                case Indent.Two:
                    if (component == null) throw new IndentException(curr.line);
                    return ParseVariable();
                default: throw new IndentException(curr.line);
            }
        }

        private static Token? ParseUnindent()
        {
            switch (indent)
            {
                case Indent.None: throw new IndentException(curr.line);
                case Indent.One:
                    indent = Indent.None;
                    if (entity != null)
                    {
                        output.Add(entity);
                        entity = null;
                    }
                    break;
                case Indent.Two:
                    indent = Indent.One;
                    if (component != null)
                    {
                        entity.AddComponent(component);
                        component = null;
                    }
                    break;
                default: throw new ArgumentOutOfRangeException(nameof(indent));
            }
            return null;
        }

        private static Token? ParseIndent()
        {
            indent = indent switch
            {
                Indent.None => Indent.One,
                Indent.One => Indent.Two,
                Indent.Two => throw new IndentException(curr.line),
                _ => throw new ArgumentOutOfRangeException(nameof(indent))
            };
            return null;
        }

        private static Token? ParseUsing()
        {
            namespaces.Add(GetNext(Token.None).source);
            return null;
        }

        private static Token? ParseEntity()
        {
            entity = new Entity {Name = curr.source};
            GetNext(Token.Colon);
            switch (GetNext(null).token)
            {
                case Token.Vector:
                    entity.Position = Vector2.Parse(curr.source);
                    return Token.Newline;
                case Token.Newline: break;
                default: throw new UnexpectedTokenException(curr.line);
            }
            return Token.Indent;
        }

        private static Token? ParseComponent()
        {
            Type type = Type.GetType(curr.source, false);
            for (int j = 0; j < namespaces.Count && type == null; j++)
                type = Type.GetType($"{namespaces[j]}.{curr.source}", false);

            if (type == null) throw new TypeNotFoundException(curr.line);
            component = Activator.CreateInstance(type) as Component;
            if (component == null) throw new NotComponentException(curr.line);

            switch (GetNext(null).token)
            {
                case Token.Colon:
                    GetNext(Token.Newline);
                    return Token.Indent;
                case Token.Newline:
                    entity.AddComponent(component);
                    component = null;
                    return null;
                default: throw new UnexpectedTokenException(curr.line);
            }
        }

        private static Token? ParseVariable()
        {
            string name = curr.source;
            GetNext(Token.Equals);

            MemberInfo v = component.GetType().GetVariable(
                name,
                BindingFlags.Default | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic
            );

            GetNext(null); // the value. should not be None since it can be None, Int, Vector, etc.
            string val = vars.ContainsKey(curr.source) ? vars[curr.source] : curr.source;

            if (v == null)
            {
                if (component is ICustomParser p)
                {
                    if (!p.Parse(name, val))
                        throw new CustomParserException(curr.line);
                }
                else throw new MissingMemberException($"Line {curr.line} - no member named {name} in component.");
            }
            else
            {
                Type type = v.GetVariableType();
                if (type.IsEnum)
                {
                    if (Enum.TryParse(type, val, out object res))
                    {
                        v.SetValue(component, res);
                        goto end; // yes, i just used goto. this is by far the cleanest solution for this situation.
                    }
                }
                if (!setHandlers.ContainsKey(type))
                    throw new NoSetHandlerException(curr.line, type);
                if (setHandlers[type](val, out object result))
                    v.SetValue(component, result);
                else throw new TypeMismatchException(curr.line);
            }
            end:
            GetNext(Token.Newline);
            return null;
        }

        public static void RegisterSetHandler<T>(SetHandlerDelegate<T> func)
        {
            setHandlers.Add(typeof(T), (string source, out object result) =>
            {
                if (func(source, out T res))
                {
                    result = res;
                    return true;
                }
                result = null;
                return false;
            });
        }

        public static List<ISceneObject> Parse(IEnumerable<Lex> lexed)
        {
            lex = lexed.ToArray();

            Token? t = null;
            while (t != Token.EOF)
            {
                Func<Token?> a = table[(int)GetNext(t).token];
                if (a == null) throw new UnexpectedTokenException(curr.line);
                t = a();
            }

            return output;
        }
    }
}
