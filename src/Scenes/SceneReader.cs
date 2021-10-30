using System;
using System.Collections.Generic;
using System.IO;

namespace Crimson.Scenes
{
    public static class SceneReader
    {
        static SceneReader()
        {
            RegisterSetHandler((string source, out int result) => int.TryParse(source, out result));
            RegisterSetHandler((string source, out long result) => long.TryParse(source, out result));
            RegisterSetHandler((string source, out short result) => short.TryParse(source, out result));
            RegisterSetHandler((string source, out uint result) => uint.TryParse(source, out result));
            RegisterSetHandler((string source, out ulong result) => ulong.TryParse(source, out result));
            RegisterSetHandler((string source, out ushort result) => ushort.TryParse(source, out result));
            RegisterSetHandler((string source, out nint result) => nint.TryParse(source, out result));
            RegisterSetHandler((string source, out nuint result) => nuint.TryParse(source, out result));
            RegisterSetHandler((string source, out float result) => float.TryParse(source, out result));
            RegisterSetHandler((string source, out double result) => double.TryParse(source, out result));
            RegisterSetHandler((string source, out decimal result) => decimal.TryParse(source, out result));
            RegisterSetHandler((string source, out bool result) => bool.TryParse(source, out result));
            RegisterSetHandler((string source, out byte result) => byte.TryParse(source, out result));
            RegisterSetHandler((string source, out sbyte result) => sbyte.TryParse(source, out result));
            RegisterSetHandler((string source, out Vector2 result) => Vector2.TryParse(source, out result));
            RegisterSetHandler((string source, out Vector3 result) => Vector3.TryParse(source, out result));
            RegisterSetHandler((string source, out Vector4 result) => Vector4.TryParse(source, out result));
            RegisterSetHandler((string source, out Rect result) => Rect.TryParse(source, out result));
            RegisterSetHandler((string source, out Color result) => Color.TryParse(source, out result));

            RegisterSetHandler((string source, out string result) => Helper.TryParseString(source, out result));
            RegisterSetHandler((string source, out char result) =>
            {
                if (source[0] != '\'' || source[^1] != '\'' || source.Length != 3)
                {
                    result = '\0';
                    return false;
                }
                result = source[1];
                return true;
            });

            RegisterSetHandler((string source, out Texture result) =>
            {
                if (Helper.TryParseString(source, out string str))
                {
                    result = new Texture(str);
                    return true;
                }
                result = null;
                return false;
            });

            // RegisterSetHandler((string source, out CollisionLayer result) =>
            // {
            //     if (ulong.TryParse(source, out ulong l))
            //     {
            //         result = new(l);
            //         return true;
            //     }
            //     result = new(0);
            //     return false;
            // });
        }

        public static IEnumerable<ISceneObject> LoadScene(string text)
        {
            foreach (ISceneObject o in Parser.Parse(Lexer.Lex(text)))
                yield return o;
        }

        public static void RegisterSetHandler<T>(SetHandlerDelegate<T> func) =>
            Parser.RegisterSetHandler(func);
    }
}
