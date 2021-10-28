using System;
using System.Collections.Generic;
using OpenGL;

namespace Crimson
{
    public static class Graphics
    {
        private static Material material;

        internal struct DrawInfo
        {
            public Color col;
            public Vector2 pos;
            public float rot;
            public Vector2 size;
            public PrimitiveType mode;
            public int first;
            public int count;

            public DrawInfo(Color col, Vector2 pos, float rot, Vector2 size, PrimitiveType mode, int first, int count)
            {
                this.col = col;
                this.pos = pos;
                this.rot = rot;
                this.size = size;
                this.mode = mode;
                this.first = first;
                this.count = count;
            }
        }

        internal static Queue<DrawInfo> fQueue = new();
        internal static Queue<DrawInfo> uQueue = new();
        internal static Queue<DrawInfo> cuQueue = new();
        internal static Queue<DrawInfo> Queue { get; set; } = fQueue;

        internal static Framebuffer fbo;
        private static Texture screenTexture;
        private static Material screenMat;

        private static readonly float[] ScreenVertices =
        {
            -1, 1, 0, 1,
            -1, -1, 0, 0,
            1, -1, 1, 0,
            1, -1, 1, 0,
            1, 1, 1, 1,
            -1, 1, 0, 1,
        };

        private static readonly float[] Vertices =
        {
            // point
            0, 0,

            // line
            -0.5f, -0.5f,
            0.5f, 0.5f,

            // rect (fill)
            -0.5f, 0.5f,
            -0.5f, -0.5f,
            0.5f, -0.5f,
            0.5f, -0.5f,
            0.5f, 0.5f,
            -0.5f, 0.5f,

            // rect (outline)
            -0.5f, 0.5f,
            0.5f, 0.5f,
            0.5f, -0.5f,
            -0.5f, -0.5f,

            // circle (generated with scripts/circle.py)
            #region circle
            1.0f, 0.0f,
            0.9975640502598242f, 0.0697564737441253f,
            0.9902680687415704f, 0.13917310096006544f,
            0.9781476007338057f, 0.20791169081775934f,
            0.9612616959383189f, 0.27563735581699916f,
            0.9396926207859084f, 0.3420201433256687f,
            0.9135454576426009f, 0.40673664307580015f,
            0.882947592858927f, 0.46947156278589075f,
            0.848048096156426f, 0.5299192642332049f,
            0.8090169943749475f, 0.5877852522924731f,
            0.766044443118978f, 0.6427876096865393f,
            0.7193398003386512f, 0.6946583704589973f,
            0.6691306063588582f, 0.7431448254773941f,
            0.6156614753256583f, 0.7880107536067219f,
            0.5591929034707469f, 0.8290375725550416f,
            0.5000000000000001f, 0.8660254037844386f,
            0.43837114678907746f, 0.898794046299167f,
            0.37460659341591196f, 0.9271838545667874f,
            0.3090169943749473f, 0.9510565162951536f,
            0.24192189559966745f, 0.9702957262759966f,
            0.17364817766693f, 0.9848077530122081f,
            0.10452846326765301f, 0.9945218953682734f,
            0.034899496702500414f, 0.9993908270190958f,
            -0.03489949670250162f, 0.9993908270190958f,
            -0.10452846326765422f, 0.9945218953682733f,
            -0.1736481776669312f, 0.9848077530122079f,
            -0.24192189559966865f, 0.9702957262759963f,
            -0.3090169943749484f, 0.9510565162951532f,
            -0.37460659341591307f, 0.927183854566787f,
            -0.43837114678907835f, 0.8987940462991666f,
            -0.500000000000001f, 0.866025403784438f,
            -0.5591929034707478f, 0.8290375725550411f,
            -0.6156614753256593f, 0.7880107536067211f,
            -0.6691306063588592f, 0.7431448254773934f,
            -0.7193398003386522f, 0.6946583704589961f,
            -0.766044443118979f, 0.6427876096865381f,
            -0.8090169943749485f, 0.5877852522924718f,
            -0.848048096156427f, 0.5299192642332035f,
            -0.8829475928589278f, 0.46947156278588914f,
            -0.9135454576426016f, 0.40673664307579843f,
            -0.9396926207859091f, 0.34202014332566677f,
            -0.9612616959383194f, 0.2756373558169971f,
            -0.9781476007338061f, 0.20791169081775712f,
            -0.9902680687415707f, 0.1391731009600631f,
            -0.9975640502598244f, 0.06975647374412286f,
            -1.0f, -2.5420705791856405e-15f,
            -0.9975640502598241f, -0.06975647374412794f,
            -0.9902680687415699f, -0.13917310096006816f,
            -0.978147600733805f, -0.20791169081776212f,
            -0.961261695938318f, -0.275637355817002f,
            -0.9396926207859073f, -0.3420201433256716f,
            -0.9135454576425996f, -0.40673664307580304f,
            -0.8829475928589254f, -0.46947156278589364f,
            -0.8480480961564242f, -0.5299192642332078f,
            -0.8090169943749455f, -0.5877852522924759f,
            -0.7660444431189758f, -0.642787609686542f,
            -0.7193398003386486f, -0.6946583704589999f,
            -0.6691306063588555f, -0.7431448254773967f,
            -0.6156614753256553f, -0.7880107536067243f,
            -0.5591929034707436f, -0.8290375725550438f,
            -0.49999999999999656f, -0.8660254037844406f,
            -0.43837114678907374f, -0.8987940462991688f,
            -0.3746065934159082f, -0.927183854566789f,
            -0.30901699437494334f, -0.9510565162951549f,
            -0.24192189559966348f, -0.9702957262759976f,
            -0.17364817766692595f, -0.9848077530122088f,
            -0.10452846326764893f, -0.9945218953682738f,
            -0.03489949670249632f, -0.9993908270190959f,
            0.034899496702505715f, -0.9993908270190955f,
            0.10452846326765829f, -0.9945218953682728f,
            0.17364817766693522f, -0.9848077530122072f,
            0.24192189559967262f, -0.9702957262759953f,
            0.3090169943749523f, -0.951056516295152f,
            0.3746065934159169f, -0.9271838545667854f,
            0.4383711467890822f, -0.8987940462991647f,
            0.5000000000000047f, -0.8660254037844359f,
            0.5591929034707515f, -0.8290375725550386f,
            0.6156614753256627f, -0.7880107536067185f,
            0.6691306063588625f, -0.7431448254773905f,
            0.7193398003386552f, -0.6946583704589931f,
            0.7660444431189818f, -0.6427876096865348f,
            0.8090169943749509f, -0.5877852522924683f,
            0.8480480961564292f, -0.5299192642331998f,
            0.8829475928589299f, -0.4694715627858853f,
            0.9135454576426034f, -0.4067366430757945f,
            0.9396926207859105f, -0.3420201433256627f,
            0.9612616959383207f, -0.27563735581699295f,
            0.978147600733807f, -0.2079116908177529f,
            0.9902680687415713f, -0.13917310096005883f,
            0.9975640502598248f, -0.06975647374411856f,
            #endregion
        };

        private const int PointIndex = 0;
        private const int PointLength = 1;

        private const int LineIndex = PointIndex + PointLength;
        private const int LineLength = 2;

        private const int RectFillIndex = LineIndex + LineLength;
        private const int RectFillLength = 6;

        private const int RectOutlineIndex = RectFillIndex + RectFillLength;
        private const int RectOutlineLength = 4;

        private const int CircleIndex = RectOutlineIndex + RectOutlineLength;
        private const int CircleLength = 90;

        internal static void Init()
        {
            material = new();
            material.AttachShaderText(ShaderType.Fragment, Resources.Read("shaders/basic.frag"));
            material.AttachShaderText(ShaderType.Vertex, Resources.Read("shaders/basic.vert"));
            material.Link();

            material.FeedBuffer(Vertices);
            material.BindVAO();
            uint loc = material.GetAttribLocation("VERTEX");
            Gl.EnableVertexAttribArray(loc);
            material.VertexAttribPointer("VERTEX", IntPtr.Zero, 2);

            screenMat = new();
            screenMat.AttachShaderText(ShaderType.Fragment, Resources.Read("shaders/screen.frag"));
            screenMat.AttachShaderText(ShaderType.Vertex, Resources.Read("shaders/screen.vert"));
            screenMat.Link();

            screenMat.FeedBuffer(ScreenVertices);
            screenMat.BindVAO();

            screenMat.VertexAttribPointer("VERTEX", IntPtr.Zero, 4, out uint vertexLoc);
            Material.EnableVertexAttribArray(vertexLoc);

            screenMat.EnableVertexAttribArray("TEX_COORDS", out uint uvLoc);
            Material.VertexAttribPointer(uvLoc, new IntPtr(2 * sizeof(float)), 4);

            InitFBO(out fbo, out screenTexture);
        }

        private static void InitFBO(out Framebuffer fbo, out Texture tex)
        {
            fbo = new();
            fbo.Bind();

            tex = new(Engine.Width, Engine.Height);
            tex.Bind(0);

            fbo.AttachTexture(tex, 0);
            Texture resizeRef = tex; // can't use out parameter inside lambda expression
            Engine.Resize += (w, h) => resizeRef.Resize(w, h);
        }

        private static void SetVars(Color color, Vector2 pos, float rot, Vector2 size)
        {
            material.SetUniform("COLOR", color);
            material.SetUniform("TRANSFORM", Camera.GetTransform(pos, rot, size), false);
        }

        private static void DrawOrQueue(DrawInfo i)
        {
            /*if (Engine.Drawing)
            {
                if (Material.Current != material) material.Use();
                ExecuteDraw(i);
            }
            else*/ Queue.Enqueue(i);
        }

        public static void DrawPoint(Vector2 point, Color color) =>
            DrawOrQueue(new(color, point, 0, Vector2.One, PrimitiveType.Points, PointIndex, PointLength));

        public static void DrawPoint(float x, float y, Color color) =>
            DrawPoint(new(x, y), color);

        public static void FillRect(Rect rect, Color color) =>
            DrawOrQueue(new(color, rect.Position, 0, rect.Size, PrimitiveType.Triangles, RectFillIndex, RectFillLength));

        public static void FillRect(Vector2 pos, Vector2 size, Color color) =>
            FillRect(new(pos, size), color);

        public static void FillRect(float x, float y, float w, float h, Color color) =>
            FillRect(new(x, y, w, h), color);

        public static void DrawRect(Rect rect, Color color) =>
            DrawOrQueue(new(color, rect.Position, 0, rect.Size, PrimitiveType.LineLoop, RectOutlineIndex, RectOutlineLength));

        public static void DrawRect(Vector2 pos, Vector2 size, Color color) =>
            DrawRect(new(pos, size), color);

        public static void DrawRect(float x, float y, float w, float h, Color color) =>
            DrawRect(new(x, y, w, h), color);

        public static void DrawLine(Vector2 from, Vector2 to, Color color)
        {
            Vector2 size = to - from;
            DrawOrQueue(new(color, from + size / 2, 0, size, PrimitiveType.Lines, LineIndex, LineLength));
        }

        public static void DrawLine(Vector2 from, Vector2 dir, float length, Color color) =>
            DrawLine(from, from + dir * length, color);

        public static void DrawCircle(Vector2 pos, float radius, Color color) =>
            DrawOrQueue(new(color, pos, 0, new(radius, radius), PrimitiveType.LineLoop, CircleIndex, CircleLength));

        /// <summary>
        /// Clears the currently bound <seealso cref="Framebuffer"/>.
        /// </summary>
        public static void Clear() => Gl.Clear(ClearBufferMask.ColorBufferBit);

        private static void ExecuteDraw(DrawInfo i)
        {
            SetVars(i.col, i.pos, i.rot, i.size);
            DrawArrays(i.mode, i.first, i.count);
        }
        
        internal static void RenderToScreen()
        {
            // Draw the framebuffer onto the screen.
            Framebuffer.BindDefault();
            Clear();

            screenMat.Use();
            screenMat.BindVAO();
            screenMat.BindVBO();

            screenTexture.Bind(0);
            screenMat.SetUniform("TEXTURE", 0);

            screenMat.SetUniform("AMBIENT_LIGHT", Light.Ambience);
            screenMat.SetUniform("SCREEN_SIZE", Engine.Size);
            screenMat.SetUniform("CAM_SIZE", Camera.CurrentResolution);

            Engine.Scene.DrawLights(screenMat);

            DrawArrays(PrimitiveType.Triangles, 0, 6);
        }

        internal static void Draw()
        {
            // Draw anything which was drawn using this class.
            material.Use();
            material.BindVBO();
            material.BindVAO();
            while (fQueue.Count > 0)
                ExecuteDraw(fQueue.Dequeue());

            if (Engine.Updated)
            {
                cuQueue = new Queue<DrawInfo>(uQueue);
                uQueue.Clear();
            }

            foreach (DrawInfo i in cuQueue)
                ExecuteDraw(i);
        }

        public static void DrawArrays(PrimitiveType mode, int first, int count) =>
            Gl.DrawArrays(mode, first, count);
    }
}
