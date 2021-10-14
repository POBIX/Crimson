using System.Collections.Generic;
using System.Linq;

namespace Crimson.Scenes
{
    internal enum Token
    {
        Ignore = -1,
        None,
        Using,
        Whitespace,
        Tab,
        Indent,
        Unindent,
        Colon,
        Equals,
        Newline,
        String,
        Vector,
        Number,
        Bool,
        Comma,
        Comment,
        Lighting,
        EOF
    }

    internal struct Lex
    {
        public string source;
        public Token token;
        public int line;
    }

    internal static class Lexer
    {
        private static IEnumerable<string> Split(string s, string seps)
        {
            int start = 0, index;

            while ((index = s.IndexOfAny(seps.ToCharArray(), start)) != -1)
            {
                if (index - start > 0)
                    yield return s.Substring(start, index - start);

                yield return s.Substring(index, 1);
                start = index + 1;
            }

            if (start < s.Length) yield return s[start..];
        }

        private static IEnumerable<string> Merge(string[] s, char open, char close)
        {
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i][0] == open)
                {
                    string o = s[i++];
                    while (i < s.Length - 1 && s[i][0] != close)
                        o += s[i++];
                    o += s[i]; // add the terminator
                    yield return o;
                }
                else yield return s[i];
            }

        }

        /// <summary> Merges splits in between <paramref name="open"/> and <paramref name="close"/>. </summary>
        private static IEnumerable<string> Merge(string[] s, string open, string close)
        {
            string[] o = s;
            for (int i = 0; i < open.Length; i++)
                o = Merge(o, open[i], close[i]).ToArray();
            return o;
        }

        /// <summary>
        /// Combines continuous whitespace into group of two.
        /// </summary>
        private static IEnumerable<string> Combine(string[] s, char c)
        {
            for (int i = 0; i < s.Length; i++)
            {
                if (i < s.Length - 1 && s[i][0] == c && s[++i][0] == c)
                    yield return "  ";
                else yield return s[i];
            }
        }

        private static Token Tokenize(string s)
        {
            return s switch
            {
                "using" => Token.Using,
                " " => Token.Whitespace,
                "\t" => Token.Tab,
                "  " => Token.Tab,
                "\r" => Token.Ignore,
                ":" => Token.Colon,
                "=" => Token.Equals,
                "\n" => Token.Newline,
                "," => Token.Comma,
                "#" => Token.Comment,
                "Lighting" => Token.Lighting,
                _ => TokenizeType(s)
            };
        }

        private static Token TokenizeType(string s)
        {
            if (s[0] == '(' && s[^1] == ')') return Token.Vector;
            if (s[0] == '"' && s[^1] == '"') return Token.String;
            if (float.TryParse(s, out _)) return Token.Number;
            if (bool.TryParse(s, out _)) return Token.Bool;
            return Token.None;
        }

        public static IEnumerable<Lex> Lex(string source)
        {
            string[] originalSplit = Split(source, "#\r\n\t ()[]{},\":").ToArray();
            // merge anything inside of brackets and quotes and combine whitespace.
            string[] split = Combine(Merge(originalSplit, "([{\"", ")]}\"").ToArray(), ' ').ToArray();

            Token[] tokens = new Token[split.Length];
            for (int i = 0; i < split.Length; i++)
                tokens[i] = Tokenize(split[i]);

            int prevTab = 0;
            int currTab = 0;
            int line = 1;
            bool first = true;
            for (int i = 0; i < tokens.Length; i++)
            {
                var l = new Lex {line = line, source = split[i]};
                switch (tokens[i])
                {
                    case Token.Tab:
                        currTab++;
                        continue;

                    case Token.Newline:
                        // we only want to set the previous tab counter if there was any text at all on this line.
                        // this is done so empty lines don't output unindents.
                        if (!first) prevTab = currTab;
                        currTab = 0;
                        first = true;

                        l.token = tokens[i];

                        line++;
                        break;

                    case Token.Whitespace:
                    case Token.Ignore:
                        continue;

                    case Token.Comment:
                        while (i < tokens.Length && tokens[i] != Token.Newline) i++;
                        if (!first) i--;
                        continue;

                    default:
                        if (first)
                        {
                            first = false;
                            Lex t = new Lex {line = line, source = ""};
                            if (currTab > prevTab)
                            {
                                t.token = Token.Indent;
                                for (int j = 0; j < currTab - prevTab; j++)
                                    yield return t;
                            }
                            else if (currTab < prevTab)
                            {
                                t.token = Token.Unindent;
                                for (int j = 0; j < prevTab - currTab; j++)
                                    yield return t;
                            }
                        }

                        l.token = tokens[i];
                        break;
                }

                yield return l;
            }

            yield return new Lex {line = line, source = "", token = Token.EOF};
        }
    }
}
