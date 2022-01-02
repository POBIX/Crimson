/*
 * Based on https://github.com/NogginBops/ImGui.NET_OpenTK_Sample
 * (lightly modified to fit Crimson)
 */

using System.Runtime.CompilerServices;
using ImGuiNET;
using OpenGL;

using NVector2 = System.Numerics.Vector2;

namespace Crimson;

public class ImGuiController : IDisposable
{
    private bool frameBegun;

    private uint vertexArray;
    private uint vertexBuffer;
    private int vertexBufferSize;
    private uint indexBuffer;
    private int indexBufferSize;

    private Texture fontTexture;
    private Material shader;

    private NVector2 scaleFactor = NVector2.One;

    /// <summary>
    /// Constructs a new ImGuiController.
    /// </summary>
    public ImGuiController()
    {
        IntPtr context = ImGui.CreateContext();
        ImGui.SetCurrentContext(context);
        ImGuiIOPtr io = ImGui.GetIO();
        io.Fonts.AddFontDefault();

        io.BackendFlags |= ImGuiBackendFlags.RendererHasVtxOffset;

        CreateDeviceResources();
        SetKeyMappings();

        SetPerFrameImGuiData(Engine.PhysicsStep);

        ImGui.NewFrame();
        frameBegun = true;
    }

    public unsafe void CreateDeviceResources()
    {
        vertexArray = Gl.CreateVertexArray();

        vertexBufferSize = 10000;
        indexBufferSize = 2000;

        vertexBuffer = Gl.CreateBuffer();
        indexBuffer = Gl.CreateBuffer();
        Gl.NamedBufferData(vertexBuffer, (uint)vertexBufferSize, IntPtr.Zero, BufferUsage.DynamicDraw);
        Gl.NamedBufferData(indexBuffer, (uint)indexBufferSize, IntPtr.Zero, BufferUsage.DynamicDraw);

        RecreateFontDeviceTexture();

        shader = new();
        shader.AttachShaderText(ShaderType.Fragment, Resources.Read("shaders/text.frag"));
        shader.AttachShaderText(ShaderType.Vertex, Resources.Read("shaders/text.vert"));
        shader.Link();

        Gl.VertexArrayVertexBuffer(vertexArray, 0, vertexBuffer, IntPtr.Zero, Unsafe.SizeOf<ImDrawVert>());
        Gl.VertexArrayElementBuffer(vertexArray, indexBuffer);

        uint pos = shader.GetAttribLocation("VERTEX");
        Gl.EnableVertexArrayAttrib(vertexArray, pos);
        Gl.VertexArrayAttribBinding(vertexArray, pos, 0);

        uint uv = shader.GetAttribLocation("TEX_COORDS");
        Gl.EnableVertexArrayAttrib(vertexArray, uv);
        Gl.VertexArrayAttribBinding(vertexArray, uv, 0);
        Gl.VertexArrayAttribFormat(vertexArray, uv, 2, VertexAttribType.Float, false, (uint)sizeof(Vector2));

        uint col = shader.GetAttribLocation("TEXT_COLOR");
        Gl.EnableVertexArrayAttrib(vertexArray, col);
        Gl.VertexArrayAttribBinding(vertexArray, col, 0);
        Gl.VertexArrayAttribFormat(vertexArray, col, 4, VertexAttribType.UnsignedByte, true, 2 * (uint)sizeof(Vector2));
    }

    /// <summary>
    /// Recreates the device texture used to render text.
    /// </summary>
    public void RecreateFontDeviceTexture()
    {
        ImGuiIOPtr io = ImGui.GetIO();
        io.Fonts.GetTexDataAsRGBA32(out IntPtr pixels, out int width, out int height, out _);

        fontTexture = new(width, height, filter: true);
        fontTexture.SetData(pixels);

        io.Fonts.SetTexID((IntPtr)fontTexture.id);

        io.Fonts.ClearTexData();
    }

    /// <summary>
    /// Renders the ImGui draw list data.
    /// This method requires a <see cref="GraphicsDevice"/> because it may create new DeviceBuffers if the size of vertex
    /// or index data has increased beyond the capacity of the existing buffers.
    /// A <see cref="CommandList"/> is needed to submit drawing and resource update commands.
    /// </summary>
    public void Render()
    {
        if (!frameBegun) return;
        frameBegun = false;
        ImGui.Render();
        RenderImDrawData(ImGui.GetDrawData());
    }

    /// <summary>
    /// Updates ImGui input and IO configuration state.
    /// </summary>
    public void Update(float deltaSeconds)
    {
        if (frameBegun) ImGui.Render();

        SetPerFrameImGuiData(deltaSeconds);
        UpdateImGuiInput();

        frameBegun = true;
        ImGui.NewFrame();
    }

    /// <summary>
    /// Sets per-frame data based on the associated window.
    /// This is called by Update(float).
    /// </summary>
    private void SetPerFrameImGuiData(float delta)
    {
        ImGuiIOPtr io = ImGui.GetIO();
        io.DisplaySize = new(
            Engine.Width / scaleFactor.X,
            Engine.Height / scaleFactor.Y
        );
        io.DisplayFramebufferScale = scaleFactor;
        io.DeltaTime = delta;
    }

    private List<char> pressedChars = new();
    private Key[] possibleKeys = Enum.GetValues<Key>();

    private void UpdateImGuiInput()
    {
        ImGuiIOPtr io = ImGui.GetIO();

        io.MouseDown[0] = Mouse.IsDown(MouseButton.Left);
        io.MouseDown[1] = Mouse.IsDown(MouseButton.Right);
        io.MouseDown[2] = Mouse.IsDown(MouseButton.Middle);

        io.MousePos = new(Mouse.X, Mouse.Y);

        foreach (Key key in possibleKeys)
        {
            if (key == Key.Unknown) continue;
            io.KeysDown[(int)key] = Keyboard.IsDown(key);
        }

        foreach (char c in pressedChars)
            io.AddInputCharacter(c);

        pressedChars.Clear();

        io.KeyCtrl = Keyboard.IsDown(Key.Ctrl) || Keyboard.IsDown(Key.RCtrl);
        io.KeyAlt = Keyboard.IsDown(Key.Alt) || Keyboard.IsDown(Key.RAlt);
        io.KeyShift = Keyboard.IsDown(Key.Shift) || Keyboard.IsDown(Key.RShift);
    }

    internal void PressChar(char keyChar) => pressedChars.Add(keyChar);

    internal static void MouseScroll(Vector2 offset)
    {
        ImGuiIOPtr io = ImGui.GetIO();

        io.MouseWheel = offset.y;
        io.MouseWheelH = offset.x;
    }

    private static void SetKeyMappings()
    {
        ImGuiIOPtr io = ImGui.GetIO();
        io.KeyMap[(int)ImGuiKey.Tab] = (int)Key.Tab;
        io.KeyMap[(int)ImGuiKey.LeftArrow] = (int)Key.Left;
        io.KeyMap[(int)ImGuiKey.RightArrow] = (int)Key.Right;
        io.KeyMap[(int)ImGuiKey.UpArrow] = (int)Key.Up;
        io.KeyMap[(int)ImGuiKey.DownArrow] = (int)Key.Down;
        io.KeyMap[(int)ImGuiKey.PageUp] = (int)Key.PageUp;
        io.KeyMap[(int)ImGuiKey.PageDown] = (int)Key.PageDown;
        io.KeyMap[(int)ImGuiKey.Home] = (int)Key.Home;
        io.KeyMap[(int)ImGuiKey.End] = (int)Key.End;
        io.KeyMap[(int)ImGuiKey.Delete] = (int)Key.Delete;
        io.KeyMap[(int)ImGuiKey.Backspace] = (int)Key.Backspace;
        io.KeyMap[(int)ImGuiKey.Enter] = (int)Key.Enter;
        io.KeyMap[(int)ImGuiKey.Escape] = (int)Key.Escape;
        io.KeyMap[(int)ImGuiKey.A] = (int)Key.A;
        io.KeyMap[(int)ImGuiKey.C] = (int)Key.C;
        io.KeyMap[(int)ImGuiKey.V] = (int)Key.V;
        io.KeyMap[(int)ImGuiKey.X] = (int)Key.X;
        io.KeyMap[(int)ImGuiKey.Y] = (int)Key.Y;
        io.KeyMap[(int)ImGuiKey.Z] = (int)Key.Z;
    }

    private void RenderImDrawData(ImDrawDataPtr drawData)
    {
        if (drawData.CmdListsCount == 0) return;

        for (int i = 0; i < drawData.CmdListsCount; i++)
        {
            ImDrawListPtr cmdList = drawData.CmdListsRange[i];

            int vertexSize = cmdList.VtxBuffer.Size * Unsafe.SizeOf<ImDrawVert>();
            if (vertexSize > vertexBufferSize)
            {
                int newSize = (int)Math.Max(vertexBufferSize * 1.5f, vertexSize);
                Gl.NamedBufferData(vertexBuffer, (uint)newSize, IntPtr.Zero, BufferUsage.DynamicDraw);
                vertexBufferSize = newSize;
            }

            int indexSize = cmdList.IdxBuffer.Size * sizeof(ushort);
            if (indexSize > indexBufferSize)
            {
                int newSize = (int)Math.Max(indexBufferSize * 1.5f, indexSize);
                Gl.NamedBufferData(indexBuffer, (uint)newSize, IntPtr.Zero, BufferUsage.DynamicDraw);
                indexBufferSize = newSize;
            }
        }

        // Setup orthographic projection matrix into our constant buffer
        ImGuiIOPtr io = ImGui.GetIO();
        Matrix mvp = Matrix.Ortho(0.0f, io.DisplaySize.X, io.DisplaySize.Y, 0.0f, -1.0f, 1.0f);

        shader.Use();
        shader.SetUniform("TRANSFORM", mvp, false);
        shader.SetUniform("TEXTURE", 0);

        Gl.BindVertexArray(vertexArray);

        drawData.ScaleClipRects(io.DisplayFramebufferScale);

        Gl.Enable(EnableCap.Blend);
        Gl.Enable(EnableCap.ScissorTest);
        Gl.BlendEquation(BlendEquationMode.FuncAdd);
        Gl.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
        Gl.Disable(EnableCap.CullFace);
        Gl.Disable(EnableCap.DepthTest);

        // Render command lists
        for (int n = 0; n < drawData.CmdListsCount; n++)
        {
            ImDrawListPtr cmdList = drawData.CmdListsRange[n];

            Gl.NamedBufferSubData(vertexBuffer, IntPtr.Zero,
                (uint)(cmdList.VtxBuffer.Size * Unsafe.SizeOf<ImDrawVert>()), cmdList.VtxBuffer.Data);

            Gl.NamedBufferSubData(indexBuffer, IntPtr.Zero, (uint)(cmdList.IdxBuffer.Size * sizeof(ushort)),
                cmdList.IdxBuffer.Data);

            int idxOffset = 0;

            for (int cmdI = 0; cmdI < cmdList.CmdBuffer.Size; cmdI++)
            {
                ImDrawCmdPtr pcmd = cmdList.CmdBuffer[cmdI];
                if (pcmd.UserCallback != IntPtr.Zero)
                    throw new NotImplementedException();

                Gl.ActiveTexture(TextureUnit.Texture0);
                Gl.BindTexture(TextureTarget.Texture2d, (uint)pcmd.TextureId);

                // We do _windowHeight - (int)clip.W instead of (int)clip.Y because gl has a flipped y axis.
                System.Numerics.Vector4 clip = pcmd.ClipRect;
                Gl.Scissor((int)clip.X, Engine.Height - (int)clip.W, (int)(clip.Z - clip.X), (int)(clip.W - clip.Y));

                if ((io.BackendFlags & ImGuiBackendFlags.RendererHasVtxOffset) != 0)
                    Gl.DrawElementsBaseVertex(
                        PrimitiveType.Triangles, (int)pcmd.ElemCount,
                        DrawElementsType.UnsignedShort, (IntPtr)(idxOffset * sizeof(ushort)), 0
                    );
                else
                    Gl.DrawElements(
                        PrimitiveType.Triangles, (int)pcmd.ElemCount, DrawElementsType.UnsignedShort,
                        (int)pcmd.IdxOffset * sizeof(ushort)
                    );

                idxOffset += (int)pcmd.ElemCount;
            }
        }

        Gl.Disable(EnableCap.Blend);
        Gl.Disable(EnableCap.ScissorTest);
    }

    private void ReleaseUnmanagedResources()
    {
        fontTexture.Dispose();
        shader.Dispose();
    }

    /// <summary>
    /// Frees all graphics resources used by the renderer.
    /// </summary>
    public void Dispose()
    {
        ReleaseUnmanagedResources();
        GC.SuppressFinalize(this);
    }

    ~ImGuiController() => ReleaseUnmanagedResources();
}
