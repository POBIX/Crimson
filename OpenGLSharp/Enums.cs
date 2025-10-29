// ReSharper disable InconsistentNaming

namespace OpenGL;

[Flags]
public enum AttribMask : uint
{
    CurrentBit = 0x00000001,
    PointBit = 0x00000002,
    LineBit = 0x00000004,
    PolygonBit = 0x00000008,
    PolygonStippleBit = 0x00000010,
    PixelModeBit = 0x00000020,
    LightingBit = 0x00000040,
    FogBit = 0x00000080,
    DepthBufferBit = 0x00000100,
    AccumBufferBit = 0x00000200,
    StencilBufferBit = 0x00000400,
    ViewportBit = 0x00000800,
    TransformBit = 0x00001000,
    EnableBit = 0x00002000,
    ColorBufferBit = 0x00004000,
    HintBit = 0x00008000,
    EvalBit = 0x00010000,
    ListBit = 0x00020000,
    TextureBit = 0x00040000,
    ScissorBit = 0x00080000,
    MultisampleBit = 0x20000000,
    MultisampleBitArb = 0x20000000,
    MultisampleBitExt = 0x20000000,
    MultisampleBit3dfx = 0x20000000,
    AllAttribBits = 0xFFFFFFFF,
}

public enum ClearBufferMask
{
    DepthBufferBit = 0x00000100,
    AccumBufferBit = 0x00000200,
    StencilBufferBit = 0x00000400,
    ColorBufferBit = 0x00004000,
    CoverageBufferBitNv = 0x00008000,
}

[Flags]
public enum BufferStorageMask
{
    DynamicStorageBit = 0x0100,
    DynamicStorageBitExt = 0x0100,
    ClientStorageBit = 0x0200,
    ClientStorageBitExt = 0x0200,
    SparseStorageBitArb = 0x0400,
    LgpuSeparateStorageBitNvx = 0x0800,
    PerGpuStorageBitNv = 0x0800,
    ExternalStorageBitNvx = 0x2000,
    MapReadBit = 0x0001,
    MapReadBitExt = 0x0001,
    MapWriteBit = 0x0002,
    MapWriteBitExt = 0x0002,
    MapPersistentBit = 0x0040,
    MapPersistentBitExt = 0x0040,
    MapCoherentBit = 0x0080,
    MapCoherentBitExt = 0x0080,
}

[Flags]
public enum ClientAttribMask : uint
{
    ClientPixelStoreBit = 0x00000001,
    ClientVertexArrayBit = 0x00000002,
    ClientAllAttribBits = 0xFFFFFFFF,
}

[Flags]
public enum ContextFlagMask
{
    ContextFlagForwardCompatibleBit = 0x00000001,
    ContextFlagDebugBit = 0x00000002,
    ContextFlagDebugBitKhr = 0x00000002,
    ContextFlagRobustAccessBit = 0x00000004,
    ContextFlagRobustAccessBitArb = 0x00000004,
    ContextFlagNoErrorBit = 0x00000008,
    ContextFlagNoErrorBitKhr = 0x00000008,
    ContextFlagProtectedContentBitExt = 0x00000010,
}

[Flags]
public enum ContextProfileMask
{
    ContextCoreProfileBit = 0x00000001,
    ContextCompatibilityProfileBit = 0x00000002,
}

[Flags]
public enum MapBufferAccessMask
{
    MapReadBit = 0x0001,
    MapReadBitExt = 0x0001,
    MapWriteBit = 0x0002,
    MapWriteBitExt = 0x0002,
    MapInvalidateRangeBit = 0x0004,
    MapInvalidateRangeBitExt = 0x0004,
    MapInvalidateBufferBit = 0x0008,
    MapInvalidateBufferBitExt = 0x0008,
    MapFlushExplicitBit = 0x0010,
    MapFlushExplicitBitExt = 0x0010,
    MapUnsynchronizedBit = 0x0020,
    MapUnsynchronizedBitExt = 0x0020,
    MapPersistentBit = 0x0040,
    MapPersistentBitExt = 0x0040,
    MapCoherentBit = 0x0080,
    MapCoherentBitExt = 0x0080,
}

[Flags]
public enum MemoryBarrierMask : uint
{
    VertexAttribArrayBarrierBit = 0x00000001,
    VertexAttribArrayBarrierBitExt = 0x00000001,
    ElementArrayBarrierBit = 0x00000002,
    ElementArrayBarrierBitExt = 0x00000002,
    UniformBarrierBit = 0x00000004,
    UniformBarrierBitExt = 0x00000004,
    TextureFetchBarrierBit = 0x00000008,
    TextureFetchBarrierBitExt = 0x00000008,
    ShaderGlobalAccessBarrierBitNv = 0x00000010,
    ShaderImageAccessBarrierBit = 0x00000020,
    ShaderImageAccessBarrierBitExt = 0x00000020,
    CommandBarrierBit = 0x00000040,
    CommandBarrierBitExt = 0x00000040,
    PixelBufferBarrierBit = 0x00000080,
    PixelBufferBarrierBitExt = 0x00000080,
    TextureUpdateBarrierBit = 0x00000100,
    TextureUpdateBarrierBitExt = 0x00000100,
    BufferUpdateBarrierBit = 0x00000200,
    BufferUpdateBarrierBitExt = 0x00000200,
    FramebufferBarrierBit = 0x00000400,
    FramebufferBarrierBitExt = 0x00000400,
    TransformFeedbackBarrierBit = 0x00000800,
    TransformFeedbackBarrierBitExt = 0x00000800,
    AtomicCounterBarrierBit = 0x00001000,
    AtomicCounterBarrierBitExt = 0x00001000,
    ShaderStorageBarrierBit = 0x00002000,
    ClientMappedBufferBarrierBit = 0x00004000,
    ClientMappedBufferBarrierBitExt = 0x00004000,
    QueryBufferBarrierBit = 0x00008000,
    AllBarrierBits = 0xFFFFFFFF,
    AllBarrierBitsExt = 0xFFFFFFFF,
}

[Flags]
public enum OcclusionQueryEventMaskAMD : uint
{
    QueryDepthPassEventBitAmd = 0x00000001,
    QueryDepthFailEventBitAmd = 0x00000002,
    QueryStencilFailEventBitAmd = 0x00000004,
    QueryDepthBoundsFailEventBitAmd = 0x00000008,
    QueryAllEventBitsAmd = 0xFFFFFFFF,
}

[Flags]
public enum SyncObjectMask
{
    SyncFlushCommandsBit = 0x00000001,
    SyncFlushCommandsBitApple = 0x00000001,
}

[Flags]
public enum UseProgramStageMask : uint
{
    VertexShaderBit = 0x00000001,
    VertexShaderBitExt = 0x00000001,
    FragmentShaderBit = 0x00000002,
    FragmentShaderBitExt = 0x00000002,
    GeometryShaderBit = 0x00000004,
    GeometryShaderBitExt = 0x00000004,
    GeometryShaderBitOes = 0x00000004,
    TessControlShaderBit = 0x00000008,
    TessControlShaderBitExt = 0x00000008,
    TessControlShaderBitOes = 0x00000008,
    TessEvaluationShaderBit = 0x00000010,
    TessEvaluationShaderBitExt = 0x00000010,
    TessEvaluationShaderBitOes = 0x00000010,
    ComputeShaderBit = 0x00000020,
    MeshShaderBitNv = 0x00000040,
    TaskShaderBitNv = 0x00000080,
    AllShaderBits = 0xFFFFFFFF,
    AllShaderBitsExt = 0xFFFFFFFF,
}

[Flags]
public enum SubgroupSupportedFeatures
{
    SubgroupFeatureBasicBitKhr = 0x00000001,
    SubgroupFeatureVoteBitKhr = 0x00000002,
    SubgroupFeatureArithmeticBitKhr = 0x00000004,
    SubgroupFeatureBallotBitKhr = 0x00000008,
    SubgroupFeatureShuffleBitKhr = 0x00000010,
    SubgroupFeatureShuffleRelativeBitKhr = 0x00000020,
    SubgroupFeatureClusteredBitKhr = 0x00000040,
    SubgroupFeatureQuadBitKhr = 0x00000080,
    SubgroupFeaturePartitionedBitNv = 0x00000100,
}

[Flags]
public enum TextureStorageMaskAMD
{
    TextureStorageSparseBitAmd = 0x00000001,
}

[Flags]
public enum FragmentShaderDestMaskATI
{
    RedBitAti = 0x00000001,
    GreenBitAti = 0x00000002,
    BlueBitAti = 0x00000004,
}

[Flags]
public enum FragmentShaderDestModMaskATI
{
    _2XBitAti = 0x00000001,
    _4XBitAti = 0x00000002,
    _8XBitAti = 0x00000004,
    HalfBitAti = 0x00000008,
    QuarterBitAti = 0x00000010,
    EighthBitAti = 0x00000020,
    SaturateBitAti = 0x00000040,
}

[Flags]
public enum FragmentShaderColorModMaskATI
{
    CompBitAti = 0x00000002,
    NegateBitAti = 0x00000004,
    BiasBitAti = 0x00000008,
}

[Flags]
public enum TraceMaskMESA
{
    TraceOperationsBitMesa = 0x0001,
    TracePrimitivesBitMesa = 0x0002,
    TraceArraysBitMesa = 0x0004,
    TraceTexturesBitMesa = 0x0008,
    TracePixelsBitMesa = 0x0010,
    TraceErrorsBitMesa = 0x0020,
    TraceAllBitsMesa = 0xFFFF,
}

[Flags]
public enum PathRenderingMaskNV
{
}

public enum PathFontStyle
{
    BoldBitNv = 0x01,
    ItalicBitNv = 0x02,
    None = 0,
}

public enum PathMetricMask
{
    GlyphWidthBitNv = 0x01,
    GlyphHeightBitNv = 0x02,
    GlyphHorizontalBearingXBitNv = 0x04,
    GlyphHorizontalBearingYBitNv = 0x08,
    GlyphHorizontalBearingAdvanceBitNv = 0x10,
    GlyphVerticalBearingXBitNv = 0x20,
    GlyphVerticalBearingYBitNv = 0x40,
    GlyphVerticalBearingAdvanceBitNv = 0x80,
    GlyphHasKerningBitNv = 0x100,
    FontXMinBoundsBitNv = 0x00010000,
    FontYMinBoundsBitNv = 0x00020000,
    FontXMaxBoundsBitNv = 0x00040000,
    FontYMaxBoundsBitNv = 0x00080000,
    FontUnitsPerEmBitNv = 0x00100000,
    FontAscenderBitNv = 0x00200000,
    FontDescenderBitNv = 0x00400000,
    FontHeightBitNv = 0x00800000,
    FontMaxAdvanceWidthBitNv = 0x01000000,
    FontMaxAdvanceHeightBitNv = 0x02000000,
    FontUnderlinePositionBitNv = 0x04000000,
    FontUnderlineThicknessBitNv = 0x08000000,
    FontHasKerningBitNv = 0x10000000,
    FontNumGlyphIndicesBitNv = 0x20000000,
}

[Flags]
public enum PerformanceQueryCapsMaskINTEL
{
    PerfquerySingleContextIntel = 0x00000000,
    PerfqueryGlobalContextIntel = 0x00000001,
}

[Flags]
public enum VertexHintsMaskPGI : uint
{
    Vertex23BitPgi = 0x00000004,
    Vertex4BitPgi = 0x00000008,
    Color3BitPgi = 0x00010000,
    Color4BitPgi = 0x00020000,
    EdgeflagBitPgi = 0x00040000,
    IndexBitPgi = 0x00080000,
    MatAmbientBitPgi = 0x00100000,
    MatAmbientAndDiffuseBitPgi = 0x00200000,
    MatDiffuseBitPgi = 0x00400000,
    MatEmissionBitPgi = 0x00800000,
    MatColorIndexesBitPgi = 0x01000000,
    MatShininessBitPgi = 0x02000000,
    MatSpecularBitPgi = 0x04000000,
    NormalBitPgi = 0x08000000,
    Texcoord1BitPgi = 0x10000000,
    Texcoord2BitPgi = 0x20000000,
    Texcoord3BitPgi = 0x40000000,
    Texcoord4BitPgi = 0x80000000,
}

[Flags]
public enum BufferBitQCOM : uint
{
    ColorBufferBit0Qcom = 0x00000001,
    ColorBufferBit1Qcom = 0x00000002,
    ColorBufferBit2Qcom = 0x00000004,
    ColorBufferBit3Qcom = 0x00000008,
    ColorBufferBit4Qcom = 0x00000010,
    ColorBufferBit5Qcom = 0x00000020,
    ColorBufferBit6Qcom = 0x00000040,
    ColorBufferBit7Qcom = 0x00000080,
    DepthBufferBit0Qcom = 0x00000100,
    DepthBufferBit1Qcom = 0x00000200,
    DepthBufferBit2Qcom = 0x00000400,
    DepthBufferBit3Qcom = 0x00000800,
    DepthBufferBit4Qcom = 0x00001000,
    DepthBufferBit5Qcom = 0x00002000,
    DepthBufferBit6Qcom = 0x00004000,
    DepthBufferBit7Qcom = 0x00008000,
    StencilBufferBit0Qcom = 0x00010000,
    StencilBufferBit1Qcom = 0x00020000,
    StencilBufferBit2Qcom = 0x00040000,
    StencilBufferBit3Qcom = 0x00080000,
    StencilBufferBit4Qcom = 0x00100000,
    StencilBufferBit5Qcom = 0x00200000,
    StencilBufferBit6Qcom = 0x00400000,
    StencilBufferBit7Qcom = 0x00800000,
    MultisampleBufferBit0Qcom = 0x01000000,
    MultisampleBufferBit1Qcom = 0x02000000,
    MultisampleBufferBit2Qcom = 0x04000000,
    MultisampleBufferBit3Qcom = 0x08000000,
    MultisampleBufferBit4Qcom = 0x10000000,
    MultisampleBufferBit5Qcom = 0x20000000,
    MultisampleBufferBit6Qcom = 0x40000000,
    MultisampleBufferBit7Qcom = 0x80000000,
}

[Flags]
public enum FoveationConfigBitQCOM
{
    FoveationEnableBitQcom = 0x00000001,
    FoveationScaledBinMethodBitQcom = 0x00000002,
    FoveationSubsampledLayoutMethodBitQcom = 0x00000004,
}

[Flags]
public enum FfdMaskSGIX
{
    TextureDeformationBitSgix = 0x00000001,
    GeometryDeformationBitSgix = 0x00000002,
}

public enum CommandOpcodesNV
{
    TerminateSequenceCommandNv = 0x0000,
    NopCommandNv = 0x0001,
    DrawElementsCommandNv = 0x0002,
    DrawArraysCommandNv = 0x0003,
    DrawElementsStripCommandNv = 0x0004,
    DrawArraysStripCommandNv = 0x0005,
    DrawElementsInstancedCommandNv = 0x0006,
    DrawArraysInstancedCommandNv = 0x0007,
    ElementAddressCommandNv = 0x0008,
    AttributeAddressCommandNv = 0x0009,
    UniformAddressCommandNv = 0x000A,
    BlendColorCommandNv = 0x000B,
    StencilRefCommandNv = 0x000C,
    LineWidthCommandNv = 0x000D,
    PolygonOffsetCommandNv = 0x000E,
    AlphaRefCommandNv = 0x000F,
    ViewportCommandNv = 0x0010,
    ScissorCommandNv = 0x0011,
    FrontFaceCommandNv = 0x0012,
}

public enum MapTextureFormatINTEL
{
    LayoutDefaultIntel = 0,
    LayoutLinearIntel = 1,
    LayoutLinearCpuCachedIntel = 2,
}

public enum PathRenderingTokenNV
{
}

public enum PathCoordType
{
    ClosePathNv = 0x00,
    MoveToNv = 0x02,
    RelativeMoveToNv = 0x03,
    LineToNv = 0x04,
    RelativeLineToNv = 0x05,
    HorizontalLineToNv = 0x06,
    RelativeHorizontalLineToNv = 0x07,
    VerticalLineToNv = 0x08,
    RelativeVerticalLineToNv = 0x09,
    QuadraticCurveToNv = 0x0A,
    RelativeQuadraticCurveToNv = 0x0B,
    CubicCurveToNv = 0x0C,
    RelativeCubicCurveToNv = 0x0D,
    SmoothQuadraticCurveToNv = 0x0E,
    RelativeSmoothQuadraticCurveToNv = 0x0F,
    SmoothCubicCurveToNv = 0x10,
    RelativeSmoothCubicCurveToNv = 0x11,
    SmallCcwArcToNv = 0x12,
    RelativeSmallCcwArcToNv = 0x13,
    SmallCwArcToNv = 0x14,
    RelativeSmallCwArcToNv = 0x15,
    LargeCcwArcToNv = 0x16,
    RelativeLargeCcwArcToNv = 0x17,
    LargeCwArcToNv = 0x18,
    RelativeLargeCwArcToNv = 0x19,
    ConicCurveToNv = 0x1A,
    RelativeConicCurveToNv = 0x1B,
    RoundedRectNv = 0xE8,
    RelativeRoundedRectNv = 0xE9,
    RoundedRect2Nv = 0xEA,
    RelativeRoundedRect2Nv = 0xEB,
    RoundedRect4Nv = 0xEC,
    RelativeRoundedRect4Nv = 0xED,
    RoundedRect8Nv = 0xEE,
    RelativeRoundedRect8Nv = 0xEF,
    RestartPathNv = 0xF0,
    DupFirstCubicCurveToNv = 0xF2,
    DupLastCubicCurveToNv = 0xF4,
    RectNv = 0xF6,
    RelativeRectNv = 0xF7,
    CircularCcwArcToNv = 0xF8,
    CircularCwArcToNv = 0xFA,
    CircularTangentArcToNv = 0xFC,
    ArcToNv = 0xFE,
    RelativeArcToNv = 0xFF,
}

public enum TransformFeedbackTokenNV
{
    NextBufferNv = -2,
    SkipComponents4Nv = -3,
    SkipComponents3Nv = -4,
    SkipComponents2Nv = -5,
    SkipComponents1Nv = -6,
}

public enum TriangleListSUN
{
    RestartSun = 0x0001,
    ReplaceMiddleSun = 0x0002,
    ReplaceOldestSun = 0x0003,
}

public enum Boolean
{
    False = 0,
    True = 1,
}

public enum VertexShaderWriteMaskEXT
{
    False = 0,
    True = 1,
}

public enum ClampColorModeARB
{
    False = 0,
    True = 1,
    FixedOnly = 0x891D,
    FixedOnlyArb = 0x891D,
}

public enum GraphicsResetStatus
{
    NoError = 0,
    GuiltyContextReset = 0x8253,
    InnocentContextReset = 0x8254,
    UnknownContextReset = 0x8255,
}

public enum ErrorCode
{
    NoError = 0,
    InvalidEnum = 0x0500,
    InvalidValue = 0x0501,
    InvalidOperation = 0x0502,
    StackOverflow = 0x0503,
    StackUnderflow = 0x0504,
    OutOfMemory = 0x0505,
    InvalidFramebufferOperation = 0x0506,
    InvalidFramebufferOperationExt = 0x0506,
    InvalidFramebufferOperationOes = 0x0506,
    TableTooLargeExt = 0x8031,
    TableTooLarge = 0x8031,
    TextureTooLargeExt = 0x8065,
}

public enum TextureSwizzle
{
    Zero = 0,
    One = 1,
    Red = 0x1903,
    Green = 0x1904,
    Blue = 0x1905,
    Alpha = 0x1906,
}

public enum StencilOp
{
    Zero = 0,
    Invert = 0x150A,
    Keep = 0x1E00,
    Replace = 0x1E01,
    Incr = 0x1E02,
    Decr = 0x1E03,
    IncrWrap = 0x8507,
    DecrWrap = 0x8508,
}

public enum BlendingFactor
{
    Zero = 0,
    One = 1,
    SrcColor = 0x0300,
    OneMinusSrcColor = 0x0301,
    SrcAlpha = 0x0302,
    OneMinusSrcAlpha = 0x0303,
    DstAlpha = 0x0304,
    OneMinusDstAlpha = 0x0305,
    DstColor = 0x0306,
    OneMinusDstColor = 0x0307,
    SrcAlphaSaturate = 0x0308,
    ConstantColor = 0x8001,
    OneMinusConstantColor = 0x8002,
    ConstantAlpha = 0x8003,
    OneMinusConstantAlpha = 0x8004,
    Src1Alpha = 0x8589,
    Src1Color = 0x88F9,
    OneMinusSrc1Color = 0x88FA,
    OneMinusSrc1Alpha = 0x88FB,
}

public enum SyncBehaviorFlags
{
    None = 0,
}

public enum TextureCompareMode
{
    None = 0,
    CompareRToTexture = 0x884E,
    CompareRefToTexture = 0x884E,
}

public enum PathColorFormat
{
    None = 0,
    Alpha = 0x1906,
    Rgb = 0x1907,
    Rgba = 0x1908,
    Luminance = 0x1909,
    LuminanceAlpha = 0x190A,
    Intensity = 0x8049,
}

public enum CombinerBiasNV
{
    None = 0,
    BiasByNegativeOneHalfNv = 0x8541,
}

public enum CombinerScaleNV
{
    None = 0,
    ScaleByTwoNv = 0x853E,
    ScaleByFourNv = 0x853F,
    ScaleByOneHalfNv = 0x8540,
}

public enum DrawBufferMode
{
    None = 0,
    NoneOes = 0,
    FrontLeft = 0x0400,
    FrontRight = 0x0401,
    BackLeft = 0x0402,
    BackRight = 0x0403,
    Front = 0x0404,
    Back = 0x0405,
    Left = 0x0406,
    Right = 0x0407,
    FrontAndBack = 0x0408,
    Aux0 = 0x0409,
    Aux1 = 0x040A,
    Aux2 = 0x040B,
    Aux3 = 0x040C,
    ColorAttachment0 = 0x8CE0,
    ColorAttachment1 = 0x8CE1,
    ColorAttachment2 = 0x8CE2,
    ColorAttachment3 = 0x8CE3,
    ColorAttachment4 = 0x8CE4,
    ColorAttachment5 = 0x8CE5,
    ColorAttachment6 = 0x8CE6,
    ColorAttachment7 = 0x8CE7,
    ColorAttachment8 = 0x8CE8,
    ColorAttachment9 = 0x8CE9,
    ColorAttachment10 = 0x8CEA,
    ColorAttachment11 = 0x8CEB,
    ColorAttachment12 = 0x8CEC,
    ColorAttachment13 = 0x8CED,
    ColorAttachment14 = 0x8CEE,
    ColorAttachment15 = 0x8CEF,
    ColorAttachment16 = 0x8CF0,
    ColorAttachment17 = 0x8CF1,
    ColorAttachment18 = 0x8CF2,
    ColorAttachment19 = 0x8CF3,
    ColorAttachment20 = 0x8CF4,
    ColorAttachment21 = 0x8CF5,
    ColorAttachment22 = 0x8CF6,
    ColorAttachment23 = 0x8CF7,
    ColorAttachment24 = 0x8CF8,
    ColorAttachment25 = 0x8CF9,
    ColorAttachment26 = 0x8CFA,
    ColorAttachment27 = 0x8CFB,
    ColorAttachment28 = 0x8CFC,
    ColorAttachment29 = 0x8CFD,
    ColorAttachment30 = 0x8CFE,
    ColorAttachment31 = 0x8CFF,
}

public enum PixelTexGenMode
{
    None = 0,
    Rgb = 0x1907,
    Rgba = 0x1908,
    Luminance = 0x1909,
    LuminanceAlpha = 0x190A,
    PixelTexGenAlphaReplaceSgix = 0x8187,
    PixelTexGenAlphaNoReplaceSgix = 0x8188,
    PixelTexGenAlphaLsSgix = 0x8189,
    PixelTexGenAlphaMsSgix = 0x818A,
}

public enum ReadBufferMode
{
    None = 0,
    NoneOes = 0,
    FrontLeft = 0x0400,
    FrontRight = 0x0401,
    BackLeft = 0x0402,
    BackRight = 0x0403,
    Front = 0x0404,
    Back = 0x0405,
    Left = 0x0406,
    Right = 0x0407,
    Aux0 = 0x0409,
    Aux1 = 0x040A,
    Aux2 = 0x040B,
    Aux3 = 0x040C,
    ColorAttachment0 = 0x8CE0,
    ColorAttachment1 = 0x8CE1,
    ColorAttachment2 = 0x8CE2,
    ColorAttachment3 = 0x8CE3,
    ColorAttachment4 = 0x8CE4,
    ColorAttachment5 = 0x8CE5,
    ColorAttachment6 = 0x8CE6,
    ColorAttachment7 = 0x8CE7,
    ColorAttachment8 = 0x8CE8,
    ColorAttachment9 = 0x8CE9,
    ColorAttachment10 = 0x8CEA,
    ColorAttachment11 = 0x8CEB,
    ColorAttachment12 = 0x8CEC,
    ColorAttachment13 = 0x8CED,
    ColorAttachment14 = 0x8CEE,
    ColorAttachment15 = 0x8CEF,
}

public enum ColorBuffer
{
    None = 0,
    FrontLeft = 0x0400,
    FrontRight = 0x0401,
    BackLeft = 0x0402,
    BackRight = 0x0403,
    Front = 0x0404,
    Back = 0x0405,
    Left = 0x0406,
    Right = 0x0407,
    FrontAndBack = 0x0408,
    ColorAttachment0 = 0x8CE0,
    ColorAttachment1 = 0x8CE1,
    ColorAttachment2 = 0x8CE2,
    ColorAttachment3 = 0x8CE3,
    ColorAttachment4 = 0x8CE4,
    ColorAttachment5 = 0x8CE5,
    ColorAttachment6 = 0x8CE6,
    ColorAttachment7 = 0x8CE7,
    ColorAttachment8 = 0x8CE8,
    ColorAttachment9 = 0x8CE9,
    ColorAttachment10 = 0x8CEA,
    ColorAttachment11 = 0x8CEB,
    ColorAttachment12 = 0x8CEC,
    ColorAttachment13 = 0x8CED,
    ColorAttachment14 = 0x8CEE,
    ColorAttachment15 = 0x8CEF,
    ColorAttachment16 = 0x8CF0,
    ColorAttachment17 = 0x8CF1,
    ColorAttachment18 = 0x8CF2,
    ColorAttachment19 = 0x8CF3,
    ColorAttachment20 = 0x8CF4,
    ColorAttachment21 = 0x8CF5,
    ColorAttachment22 = 0x8CF6,
    ColorAttachment23 = 0x8CF7,
    ColorAttachment24 = 0x8CF8,
    ColorAttachment25 = 0x8CF9,
    ColorAttachment26 = 0x8CFA,
    ColorAttachment27 = 0x8CFB,
    ColorAttachment28 = 0x8CFC,
    ColorAttachment29 = 0x8CFD,
    ColorAttachment30 = 0x8CFE,
    ColorAttachment31 = 0x8CFF,
}

public enum PathGenMode
{
    None = 0,
    EyeLinear = 0x2400,
    ObjectLinear = 0x2401,
    Constant = 0x8576,
    PathObjectBoundingBoxNv = 0x908A,
}

public enum PathTransformType
{
    None = 0,
    TranslateXNv = 0x908E,
    TranslateYNv = 0x908F,
    Translate2dNv = 0x9090,
    Translate3dNv = 0x9091,
    Affine2dNv = 0x9092,
    Affine3dNv = 0x9094,
    TransposeAffine2dNv = 0x9096,
    TransposeAffine3dNv = 0x9098,
}

public enum PrimitiveType
{
    Points = 0x0000,
    Lines = 0x0001,
    LineLoop = 0x0002,
    LineStrip = 0x0003,
    Triangles = 0x0004,
    TriangleStrip = 0x0005,
    TriangleFan = 0x0006,
    Quads = 0x0007,
    QuadsExt = 0x0007,
    QuadStrip = 0x0008,
    Polygon = 0x0009,
    LinesAdjacency = 0x000A,
    LinesAdjacencyArb = 0x000A,
    LinesAdjacencyExt = 0x000A,
    LineStripAdjacency = 0x000B,
    LineStripAdjacencyArb = 0x000B,
    LineStripAdjacencyExt = 0x000B,
    TrianglesAdjacency = 0x000C,
    TrianglesAdjacencyArb = 0x000C,
    TrianglesAdjacencyExt = 0x000C,
    TriangleStripAdjacency = 0x000D,
    TriangleStripAdjacencyArb = 0x000D,
    TriangleStripAdjacencyExt = 0x000D,
    Patches = 0x000E,
    PatchesExt = 0x000E,
}

public enum AccumOp
{
    Accum = 0x0100,
    Load = 0x0101,
    Return = 0x0102,
    Mult = 0x0103,
    Add = 0x0104,
}

public enum TextureEnvMode
{
    Add = 0x0104,
    Blend = 0x0BE2,
    Modulate = 0x2100,
    Decal = 0x2101,
    ReplaceExt = 0x8062,
    TextureEnvBiasSgix = 0x80BE,
}

public enum LightEnvModeSGIX
{
    Add = 0x0104,
    Replace = 0x1E01,
    Modulate = 0x2100,
}

public enum StencilFunction
{
    Never = 0x0200,
    Less = 0x0201,
    Equal = 0x0202,
    Lequal = 0x0203,
    Greater = 0x0204,
    Notequal = 0x0205,
    Gequal = 0x0206,
    Always = 0x0207,
}

public enum IndexFunctionEXT
{
    Never = 0x0200,
    Less = 0x0201,
    Equal = 0x0202,
    Lequal = 0x0203,
    Greater = 0x0204,
    Notequal = 0x0205,
    Gequal = 0x0206,
    Always = 0x0207,
}

public enum AlphaFunction
{
    Never = 0x0200,
    Less = 0x0201,
    Equal = 0x0202,
    Lequal = 0x0203,
    Greater = 0x0204,
    Notequal = 0x0205,
    Gequal = 0x0206,
    Always = 0x0207,
}

public enum DepthFunction
{
    Never = 0x0200,
    Less = 0x0201,
    Equal = 0x0202,
    Lequal = 0x0203,
    Greater = 0x0204,
    Notequal = 0x0205,
    Gequal = 0x0206,
    Always = 0x0207,
}

public enum ColorMaterialFace
{
    Front = 0x0404,
    Back = 0x0405,
    FrontAndBack = 0x0408,
}

public enum CullFaceMode
{
    Front = 0x0404,
    Back = 0x0405,
    FrontAndBack = 0x0408,
}

public enum StencilFaceDirection
{
    Front = 0x0404,
    Back = 0x0405,
    FrontAndBack = 0x0408,
}

public enum MaterialFace
{
    Front = 0x0404,
    Back = 0x0405,
    FrontAndBack = 0x0408,
}

public enum FeedbackType
{
    _2D = 0x0600,
    _3D = 0x0601,
    _3DColor = 0x0602,
    _3DColorTexture = 0x0603,
    _4DColorTexture = 0x0604,
}

public enum FeedBackToken
{
    PassThroughToken = 0x0700,
    PointToken = 0x0701,
    LineToken = 0x0702,
    PolygonToken = 0x0703,
    BitmapToken = 0x0704,
    DrawPixelToken = 0x0705,
    CopyPixelToken = 0x0706,
    LineResetToken = 0x0707,
}

public enum FogMode
{
    Exp = 0x0800,
    Exp2 = 0x0801,
    Linear = 0x2601,
    FogFuncSgis = 0x812A,
}

public enum FrontFaceDirection
{
    Cw = 0x0900,
    Ccw = 0x0901,
}

public enum MapQuery
{
    Coeff = 0x0A00,
    Order = 0x0A01,
    Domain = 0x0A02,
}

public enum GetMapQuery
{
    Coeff = 0x0A00,
    Order = 0x0A01,
    Domain = 0x0A02,
}

public enum GetPName
{
    CurrentColor = 0x0B00,
    CurrentIndex = 0x0B01,
    CurrentNormal = 0x0B02,
    CurrentTextureCoords = 0x0B03,
    CurrentRasterColor = 0x0B04,
    CurrentRasterIndex = 0x0B05,
    CurrentRasterTextureCoords = 0x0B06,
    CurrentRasterPosition = 0x0B07,
    CurrentRasterPositionValid = 0x0B08,
    CurrentRasterDistance = 0x0B09,
    PointSmooth = 0x0B10,
    PointSize = 0x0B11,
    PointSizeRange = 0x0B12,
    SmoothPointSizeRange = 0x0B12,
    PointSizeGranularity = 0x0B13,
    SmoothPointSizeGranularity = 0x0B13,
    LineSmooth = 0x0B20,
    LineWidth = 0x0B21,
    LineWidthRange = 0x0B22,
    SmoothLineWidthRange = 0x0B22,
    LineWidthGranularity = 0x0B23,
    SmoothLineWidthGranularity = 0x0B23,
    LineStipple = 0x0B24,
    LineStipplePattern = 0x0B25,
    LineStippleRepeat = 0x0B26,
    ListMode = 0x0B30,
    MaxListNesting = 0x0B31,
    ListBase = 0x0B32,
    ListIndex = 0x0B33,
    PolygonMode = 0x0B40,
    PolygonSmooth = 0x0B41,
    PolygonStipple = 0x0B42,
    EdgeFlag = 0x0B43,
    CullFace = 0x0B44,
    CullFaceMode = 0x0B45,
    FrontFace = 0x0B46,
    Lighting = 0x0B50,
    LightModelLocalViewer = 0x0B51,
    LightModelTwoSide = 0x0B52,
    LightModelAmbient = 0x0B53,
    ShadeModel = 0x0B54,
    ColorMaterialFace = 0x0B55,
    ColorMaterialParameter = 0x0B56,
    ColorMaterial = 0x0B57,
    Fog = 0x0B60,
    FogIndex = 0x0B61,
    FogDensity = 0x0B62,
    FogStart = 0x0B63,
    FogEnd = 0x0B64,
    FogMode = 0x0B65,
    FogColor = 0x0B66,
    DepthRange = 0x0B70,
    DepthTest = 0x0B71,
    DepthWritemask = 0x0B72,
    DepthClearValue = 0x0B73,
    DepthFunc = 0x0B74,
    AccumClearValue = 0x0B80,
    StencilTest = 0x0B90,
    StencilClearValue = 0x0B91,
    StencilFunc = 0x0B92,
    StencilValueMask = 0x0B93,
    StencilFail = 0x0B94,
    StencilPassDepthFail = 0x0B95,
    StencilPassDepthPass = 0x0B96,
    StencilRef = 0x0B97,
    StencilWritemask = 0x0B98,
    MatrixMode = 0x0BA0,
    Normalize = 0x0BA1,
    Viewport = 0x0BA2,
    ModelviewStackDepth = 0x0BA3,
    Modelview0StackDepthExt = 0x0BA3,
    ProjectionStackDepth = 0x0BA4,
    TextureStackDepth = 0x0BA5,
    ModelviewMatrix = 0x0BA6,
    Modelview0MatrixExt = 0x0BA6,
    ProjectionMatrix = 0x0BA7,
    TextureMatrix = 0x0BA8,
    AttribStackDepth = 0x0BB0,
    ClientAttribStackDepth = 0x0BB1,
    AlphaTest = 0x0BC0,
    AlphaTestQcom = 0x0BC0,
    AlphaTestFunc = 0x0BC1,
    AlphaTestFuncQcom = 0x0BC1,
    AlphaTestRef = 0x0BC2,
    AlphaTestRefQcom = 0x0BC2,
    Dither = 0x0BD0,
    BlendDst = 0x0BE0,
    BlendSrc = 0x0BE1,
    Blend = 0x0BE2,
    LogicOpMode = 0x0BF0,
    IndexLogicOp = 0x0BF1,
    LogicOp = 0x0BF1,
    ColorLogicOp = 0x0BF2,
    AuxBuffers = 0x0C00,
    DrawBuffer = 0x0C01,
    DrawBufferExt = 0x0C01,
    ReadBuffer = 0x0C02,
    ReadBufferExt = 0x0C02,
    ReadBufferNv = 0x0C02,
    ScissorBox = 0x0C10,
    ScissorTest = 0x0C11,
    IndexClearValue = 0x0C20,
    IndexWritemask = 0x0C21,
    ColorClearValue = 0x0C22,
    ColorWritemask = 0x0C23,
    IndexMode = 0x0C30,
    RgbaMode = 0x0C31,
    Doublebuffer = 0x0C32,
    Stereo = 0x0C33,
    RenderMode = 0x0C40,
    PerspectiveCorrectionHint = 0x0C50,
    PointSmoothHint = 0x0C51,
    LineSmoothHint = 0x0C52,
    PolygonSmoothHint = 0x0C53,
    FogHint = 0x0C54,
    TextureGenS = 0x0C60,
    TextureGenT = 0x0C61,
    TextureGenR = 0x0C62,
    TextureGenQ = 0x0C63,
    PixelMapIToISize = 0x0CB0,
    PixelMapSToSSize = 0x0CB1,
    PixelMapIToRSize = 0x0CB2,
    PixelMapIToGSize = 0x0CB3,
    PixelMapIToBSize = 0x0CB4,
    PixelMapIToASize = 0x0CB5,
    PixelMapRToRSize = 0x0CB6,
    PixelMapGToGSize = 0x0CB7,
    PixelMapBToBSize = 0x0CB8,
    PixelMapAToASize = 0x0CB9,
    UnpackSwapBytes = 0x0CF0,
    UnpackLsbFirst = 0x0CF1,
    UnpackRowLength = 0x0CF2,
    UnpackSkipRows = 0x0CF3,
    UnpackSkipPixels = 0x0CF4,
    UnpackAlignment = 0x0CF5,
    PackSwapBytes = 0x0D00,
    PackLsbFirst = 0x0D01,
    PackRowLength = 0x0D02,
    PackSkipRows = 0x0D03,
    PackSkipPixels = 0x0D04,
    PackAlignment = 0x0D05,
    MapColor = 0x0D10,
    MapStencil = 0x0D11,
    IndexShift = 0x0D12,
    IndexOffset = 0x0D13,
    RedScale = 0x0D14,
    RedBias = 0x0D15,
    ZoomX = 0x0D16,
    ZoomY = 0x0D17,
    GreenScale = 0x0D18,
    GreenBias = 0x0D19,
    BlueScale = 0x0D1A,
    BlueBias = 0x0D1B,
    AlphaScale = 0x0D1C,
    AlphaBias = 0x0D1D,
    DepthScale = 0x0D1E,
    DepthBias = 0x0D1F,
    MaxEvalOrder = 0x0D30,
    MaxLights = 0x0D31,
    MaxClipPlanes = 0x0D32,
    MaxClipDistances = 0x0D32,
    MaxTextureSize = 0x0D33,
    MaxPixelMapTable = 0x0D34,
    MaxAttribStackDepth = 0x0D35,
    MaxModelviewStackDepth = 0x0D36,
    MaxNameStackDepth = 0x0D37,
    MaxProjectionStackDepth = 0x0D38,
    MaxTextureStackDepth = 0x0D39,
    MaxViewportDims = 0x0D3A,
    MaxClientAttribStackDepth = 0x0D3B,
    SubpixelBits = 0x0D50,
    IndexBits = 0x0D51,
    RedBits = 0x0D52,
    GreenBits = 0x0D53,
    BlueBits = 0x0D54,
    AlphaBits = 0x0D55,
    DepthBits = 0x0D56,
    StencilBits = 0x0D57,
    AccumRedBits = 0x0D58,
    AccumGreenBits = 0x0D59,
    AccumBlueBits = 0x0D5A,
    AccumAlphaBits = 0x0D5B,
    NameStackDepth = 0x0D70,
    AutoNormal = 0x0D80,
    Map1Color4 = 0x0D90,
    Map1Index = 0x0D91,
    Map1Normal = 0x0D92,
    Map1TextureCoord1 = 0x0D93,
    Map1TextureCoord2 = 0x0D94,
    Map1TextureCoord3 = 0x0D95,
    Map1TextureCoord4 = 0x0D96,
    Map1Vertex3 = 0x0D97,
    Map1Vertex4 = 0x0D98,
    Map2Color4 = 0x0DB0,
    Map2Index = 0x0DB1,
    Map2Normal = 0x0DB2,
    Map2TextureCoord1 = 0x0DB3,
    Map2TextureCoord2 = 0x0DB4,
    Map2TextureCoord3 = 0x0DB5,
    Map2TextureCoord4 = 0x0DB6,
    Map2Vertex3 = 0x0DB7,
    Map2Vertex4 = 0x0DB8,
    Map1GridDomain = 0x0DD0,
    Map1GridSegments = 0x0DD1,
    Map2GridDomain = 0x0DD2,
    Map2GridSegments = 0x0DD3,
    Texture1d = 0x0DE0,
    Texture2d = 0x0DE1,
    FeedbackBufferSize = 0x0DF1,
    FeedbackBufferType = 0x0DF2,
    SelectionBufferSize = 0x0DF4,
    PolygonOffsetUnits = 0x2A00,
    PolygonOffsetPoint = 0x2A01,
    PolygonOffsetLine = 0x2A02,
    ClipPlane0 = 0x3000,
    ClipPlane1 = 0x3001,
    ClipPlane2 = 0x3002,
    ClipPlane3 = 0x3003,
    ClipPlane4 = 0x3004,
    ClipPlane5 = 0x3005,
    Light0 = 0x4000,
    Light1 = 0x4001,
    Light2 = 0x4002,
    Light3 = 0x4003,
    Light4 = 0x4004,
    Light5 = 0x4005,
    Light6 = 0x4006,
    Light7 = 0x4007,
    BlendColor = 0x8005,
    BlendColorExt = 0x8005,
    BlendEquationExt = 0x8009,
    BlendEquationRgb = 0x8009,
    PackCmykHintExt = 0x800E,
    UnpackCmykHintExt = 0x800F,
    Convolution1dExt = 0x8010,
    Convolution2dExt = 0x8011,
    Separable2dExt = 0x8012,
    PostConvolutionRedScaleExt = 0x801C,
    PostConvolutionGreenScaleExt = 0x801D,
    PostConvolutionBlueScaleExt = 0x801E,
    PostConvolutionAlphaScaleExt = 0x801F,
    PostConvolutionRedBiasExt = 0x8020,
    PostConvolutionGreenBiasExt = 0x8021,
    PostConvolutionBlueBiasExt = 0x8022,
    PostConvolutionAlphaBiasExt = 0x8023,
    HistogramExt = 0x8024,
    MinmaxExt = 0x802E,
    PolygonOffsetFill = 0x8037,
    PolygonOffsetFactor = 0x8038,
    PolygonOffsetBiasExt = 0x8039,
    RescaleNormalExt = 0x803A,
    TextureBinding1d = 0x8068,
    TextureBinding2d = 0x8069,
    Texture3dBindingExt = 0x806A,
    TextureBinding3d = 0x806A,
    PackSkipImages = 0x806B,
    PackSkipImagesExt = 0x806B,
    PackImageHeight = 0x806C,
    PackImageHeightExt = 0x806C,
    UnpackSkipImages = 0x806D,
    UnpackSkipImagesExt = 0x806D,
    UnpackImageHeight = 0x806E,
    UnpackImageHeightExt = 0x806E,
    Texture3dExt = 0x806F,
    Max3dTextureSize = 0x8073,
    Max3dTextureSizeExt = 0x8073,
    VertexArray = 0x8074,
    NormalArray = 0x8075,
    ColorArray = 0x8076,
    IndexArray = 0x8077,
    TextureCoordArray = 0x8078,
    EdgeFlagArray = 0x8079,
    VertexArraySize = 0x807A,
    VertexArrayType = 0x807B,
    VertexArrayStride = 0x807C,
    VertexArrayCountExt = 0x807D,
    NormalArrayType = 0x807E,
    NormalArrayStride = 0x807F,
    NormalArrayCountExt = 0x8080,
    ColorArraySize = 0x8081,
    ColorArrayType = 0x8082,
    ColorArrayStride = 0x8083,
    ColorArrayCountExt = 0x8084,
    IndexArrayType = 0x8085,
    IndexArrayStride = 0x8086,
    IndexArrayCountExt = 0x8087,
    TextureCoordArraySize = 0x8088,
    TextureCoordArrayType = 0x8089,
    TextureCoordArrayStride = 0x808A,
    TextureCoordArrayCountExt = 0x808B,
    EdgeFlagArrayStride = 0x808C,
    EdgeFlagArrayCountExt = 0x808D,
    InterlaceSgix = 0x8094,
    DetailTexture2dBindingSgis = 0x8096,
    MultisampleSgis = 0x809D,
    SampleAlphaToMaskSgis = 0x809E,
    SampleAlphaToOneSgis = 0x809F,
    SampleMaskSgis = 0x80A0,
    SampleBuffers = 0x80A8,
    SampleBuffersSgis = 0x80A8,
    Samples = 0x80A9,
    SamplesSgis = 0x80A9,
    SampleCoverageValue = 0x80AA,
    SampleMaskValueSgis = 0x80AA,
    SampleCoverageInvert = 0x80AB,
    SampleMaskInvertSgis = 0x80AB,
    SamplePatternSgis = 0x80AC,
    ColorMatrixSgi = 0x80B1,
    ColorMatrixStackDepthSgi = 0x80B2,
    MaxColorMatrixStackDepthSgi = 0x80B3,
    PostColorMatrixRedScaleSgi = 0x80B4,
    PostColorMatrixGreenScaleSgi = 0x80B5,
    PostColorMatrixBlueScaleSgi = 0x80B6,
    PostColorMatrixAlphaScaleSgi = 0x80B7,
    PostColorMatrixRedBiasSgi = 0x80B8,
    PostColorMatrixGreenBiasSgi = 0x80B9,
    PostColorMatrixBlueBiasSgi = 0x80BA,
    PostColorMatrixAlphaBiasSgi = 0x80BB,
    TextureColorTableSgi = 0x80BC,
    BlendDstRgb = 0x80C8,
    BlendSrcRgb = 0x80C9,
    BlendDstAlpha = 0x80CA,
    BlendSrcAlpha = 0x80CB,
    ColorTableSgi = 0x80D0,
    PostConvolutionColorTableSgi = 0x80D1,
    PostColorMatrixColorTableSgi = 0x80D2,
    MaxElementsVertices = 0x80E8,
    MaxElementsIndices = 0x80E9,
    PointSizeMinSgis = 0x8126,
    PointSizeMaxSgis = 0x8127,
    PointFadeThresholdSize = 0x8128,
    PointFadeThresholdSizeSgis = 0x8128,
    DistanceAttenuationSgis = 0x8129,
    FogFuncPointsSgis = 0x812B,
    MaxFogFuncPointsSgis = 0x812C,
    PackSkipVolumesSgis = 0x8130,
    PackImageDepthSgis = 0x8131,
    UnpackSkipVolumesSgis = 0x8132,
    UnpackImageDepthSgis = 0x8133,
    Texture4dSgis = 0x8134,
    Max4dTextureSizeSgis = 0x8138,
    PixelTexGenSgix = 0x8139,
    PixelTileBestAlignmentSgix = 0x813E,
    PixelTileCacheIncrementSgix = 0x813F,
    PixelTileWidthSgix = 0x8140,
    PixelTileHeightSgix = 0x8141,
    PixelTileGridWidthSgix = 0x8142,
    PixelTileGridHeightSgix = 0x8143,
    PixelTileGridDepthSgix = 0x8144,
    PixelTileCacheSizeSgix = 0x8145,
    SpriteSgix = 0x8148,
    SpriteModeSgix = 0x8149,
    SpriteAxisSgix = 0x814A,
    SpriteTranslationSgix = 0x814B,
    Texture4dBindingSgis = 0x814F,
    MaxClipmapDepthSgix = 0x8177,
    MaxClipmapVirtualDepthSgix = 0x8178,
    PostTextureFilterBiasRangeSgix = 0x817B,
    PostTextureFilterScaleRangeSgix = 0x817C,
    ReferencePlaneSgix = 0x817D,
    ReferencePlaneEquationSgix = 0x817E,
    IrInstrument1Sgix = 0x817F,
    InstrumentMeasurementsSgix = 0x8181,
    CalligraphicFragmentSgix = 0x8183,
    FramezoomSgix = 0x818B,
    FramezoomFactorSgix = 0x818C,
    MaxFramezoomFactorSgix = 0x818D,
    GenerateMipmapHintSgis = 0x8192,
    DeformationsMaskSgix = 0x8196,
    FogOffsetSgix = 0x8198,
    FogOffsetValueSgix = 0x8199,
    LightModelColorControl = 0x81F8,
    SharedTexturePaletteExt = 0x81FB,
    MajorVersion = 0x821B,
    MinorVersion = 0x821C,
    NumExtensions = 0x821D,
    ContextFlags = 0x821E,
    ProgramPipelineBinding = 0x825A,
    MaxViewports = 0x825B,
    ViewportSubpixelBits = 0x825C,
    ViewportBoundsRange = 0x825D,
    LayerProvokingVertex = 0x825E,
    ViewportIndexProvokingVertex = 0x825F,
    MaxComputeUniformComponents = 0x8263,
    MaxComputeAtomicCounterBuffers = 0x8264,
    MaxComputeAtomicCounters = 0x8265,
    MaxCombinedComputeUniformComponents = 0x8266,
    MaxDebugGroupStackDepth = 0x826C,
    DebugGroupStackDepth = 0x826D,
    MaxUniformLocations = 0x826E,
    VertexBindingDivisor = 0x82D6,
    VertexBindingOffset = 0x82D7,
    VertexBindingStride = 0x82D8,
    MaxVertexAttribRelativeOffset = 0x82D9,
    MaxVertexAttribBindings = 0x82DA,
    MaxLabelLength = 0x82E8,
    ConvolutionHintSgix = 0x8316,
    AsyncMarkerSgix = 0x8329,
    PixelTexGenModeSgix = 0x832B,
    AsyncHistogramSgix = 0x832C,
    MaxAsyncHistogramSgix = 0x832D,
    PixelTextureSgis = 0x8353,
    AsyncTexImageSgix = 0x835C,
    AsyncDrawPixelsSgix = 0x835D,
    AsyncReadPixelsSgix = 0x835E,
    MaxAsyncTexImageSgix = 0x835F,
    MaxAsyncDrawPixelsSgix = 0x8360,
    MaxAsyncReadPixelsSgix = 0x8361,
    VertexPreclipSgix = 0x83EE,
    VertexPreclipHintSgix = 0x83EF,
    FragmentLightingSgix = 0x8400,
    FragmentColorMaterialSgix = 0x8401,
    FragmentColorMaterialFaceSgix = 0x8402,
    FragmentColorMaterialParameterSgix = 0x8403,
    MaxFragmentLightsSgix = 0x8404,
    MaxActiveLightsSgix = 0x8405,
    LightEnvModeSgix = 0x8407,
    FragmentLightModelLocalViewerSgix = 0x8408,
    FragmentLightModelTwoSideSgix = 0x8409,
    FragmentLightModelAmbientSgix = 0x840A,
    FragmentLightModelNormalInterpolationSgix = 0x840B,
    FragmentLight0Sgix = 0x840C,
    PackResampleSgix = 0x842E,
    UnpackResampleSgix = 0x842F,
    AliasedPointSizeRange = 0x846D,
    AliasedLineWidthRange = 0x846E,
    ActiveTexture = 0x84E0,
    MaxRenderbufferSize = 0x84E8,
    TextureCompressionHint = 0x84EF,
    TextureBindingRectangle = 0x84F6,
    TextureBindingRectangleArb = 0x84F6,
    TextureBindingRectangleNv = 0x84F6,
    MaxRectangleTextureSize = 0x84F8,
    MaxTextureLodBias = 0x84FD,
    TextureBindingCubeMap = 0x8514,
    TextureBindingCubeMapArb = 0x8514,
    TextureBindingCubeMapExt = 0x8514,
    TextureBindingCubeMapOes = 0x8514,
    MaxCubeMapTextureSize = 0x851C,
    PackSubsampleRateSgix = 0x85A0,
    UnpackSubsampleRateSgix = 0x85A1,
    VertexArrayBinding = 0x85B5,
    ProgramPointSize = 0x8642,
    NumCompressedTextureFormats = 0x86A2,
    CompressedTextureFormats = 0x86A3,
    NumProgramBinaryFormats = 0x87FE,
    ProgramBinaryFormats = 0x87FF,
    StencilBackFunc = 0x8800,
    StencilBackFail = 0x8801,
    StencilBackPassDepthFail = 0x8802,
    StencilBackPassDepthPass = 0x8803,
    MaxDrawBuffers = 0x8824,
    BlendEquationAlpha = 0x883D,
    MaxVertexAttribs = 0x8869,
    MaxTextureImageUnits = 0x8872,
    ArrayBufferBinding = 0x8894,
    ElementArrayBufferBinding = 0x8895,
    PixelPackBufferBinding = 0x88ED,
    PixelUnpackBufferBinding = 0x88EF,
    MaxDualSourceDrawBuffers = 0x88FC,
    MaxArrayTextureLayers = 0x88FF,
    MinProgramTexelOffset = 0x8904,
    MaxProgramTexelOffset = 0x8905,
    SamplerBinding = 0x8919,
    UniformBufferBinding = 0x8A28,
    UniformBufferStart = 0x8A29,
    UniformBufferSize = 0x8A2A,
    MaxVertexUniformBlocks = 0x8A2B,
    MaxGeometryUniformBlocks = 0x8A2C,
    MaxFragmentUniformBlocks = 0x8A2D,
    MaxCombinedUniformBlocks = 0x8A2E,
    MaxUniformBufferBindings = 0x8A2F,
    MaxUniformBlockSize = 0x8A30,
    MaxCombinedVertexUniformComponents = 0x8A31,
    MaxCombinedGeometryUniformComponents = 0x8A32,
    MaxCombinedFragmentUniformComponents = 0x8A33,
    UniformBufferOffsetAlignment = 0x8A34,
    MaxFragmentUniformComponents = 0x8B49,
    MaxVertexUniformComponents = 0x8B4A,
    MaxVaryingFloats = 0x8B4B,
    MaxVaryingComponents = 0x8B4B,
    MaxVertexTextureImageUnits = 0x8B4C,
    MaxCombinedTextureImageUnits = 0x8B4D,
    FragmentShaderDerivativeHint = 0x8B8B,
    CurrentProgram = 0x8B8D,
    ImplementationColorReadType = 0x8B9A,
    ImplementationColorReadFormat = 0x8B9B,
    TextureBinding1dArray = 0x8C1C,
    TextureBinding2dArray = 0x8C1D,
    MaxGeometryTextureImageUnits = 0x8C29,
    MaxTextureBufferSize = 0x8C2B,
    TextureBindingBuffer = 0x8C2C,
    TransformFeedbackBufferStart = 0x8C84,
    TransformFeedbackBufferSize = 0x8C85,
    TransformFeedbackBufferBinding = 0x8C8F,
    MotionEstimationSearchBlockXQcom = 0x8C90,
    MotionEstimationSearchBlockYQcom = 0x8C91,
    StencilBackRef = 0x8CA3,
    StencilBackValueMask = 0x8CA4,
    StencilBackWritemask = 0x8CA5,
    DrawFramebufferBinding = 0x8CA6,
    RenderbufferBinding = 0x8CA7,
    ReadFramebufferBinding = 0x8CAA,
    MaxColorAttachments = 0x8CDF,
    MaxColorAttachmentsExt = 0x8CDF,
    MaxColorAttachmentsNv = 0x8CDF,
    MaxElementIndex = 0x8D6B,
    MaxGeometryUniformComponents = 0x8DDF,
    ShaderBinaryFormats = 0x8DF8,
    NumShaderBinaryFormats = 0x8DF9,
    ShaderCompiler = 0x8DFA,
    MaxVertexUniformVectors = 0x8DFB,
    MaxVaryingVectors = 0x8DFC,
    MaxFragmentUniformVectors = 0x8DFD,
    Timestamp = 0x8E28,
    ProvokingVertex = 0x8E4F,
    MaxSampleMaskWords = 0x8E59,
    MaxTessControlUniformBlocks = 0x8E89,
    MaxTessEvaluationUniformBlocks = 0x8E8A,
    FetchPerSampleArm = 0x8F65,
    FragmentShaderFramebufferFetchMrtArm = 0x8F66,
    PrimitiveRestartIndex = 0x8F9E,
    MinMapBufferAlignment = 0x90BC,
    ShaderStorageBufferBinding = 0x90D3,
    ShaderStorageBufferStart = 0x90D4,
    ShaderStorageBufferSize = 0x90D5,
    MaxVertexShaderStorageBlocks = 0x90D6,
    MaxGeometryShaderStorageBlocks = 0x90D7,
    MaxTessControlShaderStorageBlocks = 0x90D8,
    MaxTessEvaluationShaderStorageBlocks = 0x90D9,
    MaxFragmentShaderStorageBlocks = 0x90DA,
    MaxComputeShaderStorageBlocks = 0x90DB,
    MaxCombinedShaderStorageBlocks = 0x90DC,
    MaxShaderStorageBufferBindings = 0x90DD,
    ShaderStorageBufferOffsetAlignment = 0x90DF,
    MaxComputeWorkGroupInvocations = 0x90EB,
    DispatchIndirectBufferBinding = 0x90EF,
    TextureBinding2dMultisample = 0x9104,
    TextureBinding2dMultisampleArray = 0x9105,
    MaxColorTextureSamples = 0x910E,
    MaxDepthTextureSamples = 0x910F,
    MaxIntegerSamples = 0x9110,
    MaxServerWaitTimeout = 0x9111,
    MaxVertexOutputComponents = 0x9122,
    MaxGeometryInputComponents = 0x9123,
    MaxGeometryOutputComponents = 0x9124,
    MaxFragmentInputComponents = 0x9125,
    ContextProfileMask = 0x9126,
    TextureBufferOffsetAlignment = 0x919F,
    MaxComputeUniformBlocks = 0x91BB,
    MaxComputeTextureImageUnits = 0x91BC,
    MaxComputeWorkGroupCount = 0x91BE,
    MaxComputeWorkGroupSize = 0x91BF,
    MaxVertexAtomicCounters = 0x92D2,
    MaxTessControlAtomicCounters = 0x92D3,
    MaxTessEvaluationAtomicCounters = 0x92D4,
    MaxGeometryAtomicCounters = 0x92D5,
    MaxFragmentAtomicCounters = 0x92D6,
    MaxCombinedAtomicCounters = 0x92D7,
    MaxFramebufferWidth = 0x9315,
    MaxFramebufferHeight = 0x9316,
    MaxFramebufferLayers = 0x9317,
    MaxFramebufferSamples = 0x9318,
    NumDeviceUuidsExt = 0x9596,
    DeviceUuidExt = 0x9597,
    DriverUuidExt = 0x9598,
    DeviceLuidExt = 0x9599,
    DeviceNodeMaskExt = 0x959A,
    ShadingRateImagePerPrimitiveNv = 0x95B1,
    ShadingRateImagePaletteCountNv = 0x95B2,
    MaxTimelineSemaphoreValueDifferenceNv = 0x95B6,
    ShadingRateQcom = 0x96A4,
}

public enum VertexShaderTextureUnitParameter
{
    CurrentTextureCoords = 0x0B03,
    TextureMatrix = 0x0BA8,
}

public enum EnableCap
{
    PointSmooth = 0x0B10,
    LineSmooth = 0x0B20,
    LineStipple = 0x0B24,
    PolygonSmooth = 0x0B41,
    PolygonStipple = 0x0B42,
    CullFace = 0x0B44,
    Lighting = 0x0B50,
    ColorMaterial = 0x0B57,
    Fog = 0x0B60,
    DepthTest = 0x0B71,
    StencilTest = 0x0B90,
    Normalize = 0x0BA1,
    AlphaTest = 0x0BC0,
    Dither = 0x0BD0,
    Blend = 0x0BE2,
    IndexLogicOp = 0x0BF1,
    ColorLogicOp = 0x0BF2,
    ScissorTest = 0x0C11,
    TextureGenS = 0x0C60,
    TextureGenT = 0x0C61,
    TextureGenR = 0x0C62,
    TextureGenQ = 0x0C63,
    AutoNormal = 0x0D80,
    Map1Color4 = 0x0D90,
    Map1Index = 0x0D91,
    Map1Normal = 0x0D92,
    Map1TextureCoord1 = 0x0D93,
    Map1TextureCoord2 = 0x0D94,
    Map1TextureCoord3 = 0x0D95,
    Map1TextureCoord4 = 0x0D96,
    Map1Vertex3 = 0x0D97,
    Map1Vertex4 = 0x0D98,
    Map2Color4 = 0x0DB0,
    Map2Index = 0x0DB1,
    Map2Normal = 0x0DB2,
    Map2TextureCoord1 = 0x0DB3,
    Map2TextureCoord2 = 0x0DB4,
    Map2TextureCoord3 = 0x0DB5,
    Map2TextureCoord4 = 0x0DB6,
    Map2Vertex3 = 0x0DB7,
    Map2Vertex4 = 0x0DB8,
    Texture1d = 0x0DE0,
    Texture2d = 0x0DE1,
    PolygonOffsetPoint = 0x2A01,
    PolygonOffsetLine = 0x2A02,
    ClipPlane0 = 0x3000,
    ClipDistance0 = 0x3000,
    ClipPlane1 = 0x3001,
    ClipDistance1 = 0x3001,
    ClipPlane2 = 0x3002,
    ClipDistance2 = 0x3002,
    ClipPlane3 = 0x3003,
    ClipDistance3 = 0x3003,
    ClipPlane4 = 0x3004,
    ClipDistance4 = 0x3004,
    ClipPlane5 = 0x3005,
    ClipDistance5 = 0x3005,
    ClipDistance6 = 0x3006,
    ClipDistance7 = 0x3007,
    Light0 = 0x4000,
    Light1 = 0x4001,
    Light2 = 0x4002,
    Light3 = 0x4003,
    Light4 = 0x4004,
    Light5 = 0x4005,
    Light6 = 0x4006,
    Light7 = 0x4007,
    Convolution1dExt = 0x8010,
    Convolution2dExt = 0x8011,
    Separable2dExt = 0x8012,
    HistogramExt = 0x8024,
    MinmaxExt = 0x802E,
    PolygonOffsetFill = 0x8037,
    RescaleNormalExt = 0x803A,
    Texture3dExt = 0x806F,
    VertexArray = 0x8074,
    NormalArray = 0x8075,
    ColorArray = 0x8076,
    IndexArray = 0x8077,
    TextureCoordArray = 0x8078,
    EdgeFlagArray = 0x8079,
    InterlaceSgix = 0x8094,
    Multisample = 0x809D,
    MultisampleSgis = 0x809D,
    SampleAlphaToCoverage = 0x809E,
    SampleAlphaToMaskSgis = 0x809E,
    SampleAlphaToOne = 0x809F,
    SampleAlphaToOneSgis = 0x809F,
    SampleCoverage = 0x80A0,
    SampleMaskSgis = 0x80A0,
    TextureColorTableSgi = 0x80BC,
    ColorTable = 0x80D0,
    ColorTableSgi = 0x80D0,
    PostConvolutionColorTable = 0x80D1,
    PostConvolutionColorTableSgi = 0x80D1,
    PostColorMatrixColorTable = 0x80D2,
    PostColorMatrixColorTableSgi = 0x80D2,
    Texture4dSgis = 0x8134,
    PixelTexGenSgix = 0x8139,
    SpriteSgix = 0x8148,
    ReferencePlaneSgix = 0x817D,
    IrInstrument1Sgix = 0x817F,
    CalligraphicFragmentSgix = 0x8183,
    FramezoomSgix = 0x818B,
    FogOffsetSgix = 0x8198,
    SharedTexturePaletteExt = 0x81FB,
    DebugOutputSynchronous = 0x8242,
    AsyncHistogramSgix = 0x832C,
    PixelTextureSgis = 0x8353,
    AsyncTexImageSgix = 0x835C,
    AsyncDrawPixelsSgix = 0x835D,
    AsyncReadPixelsSgix = 0x835E,
    FragmentLightingSgix = 0x8400,
    FragmentColorMaterialSgix = 0x8401,
    FragmentLight0Sgix = 0x840C,
    FragmentLight1Sgix = 0x840D,
    FragmentLight2Sgix = 0x840E,
    FragmentLight3Sgix = 0x840F,
    FragmentLight4Sgix = 0x8410,
    FragmentLight5Sgix = 0x8411,
    FragmentLight6Sgix = 0x8412,
    FragmentLight7Sgix = 0x8413,
    TextureRectangle = 0x84F5,
    TextureRectangleArb = 0x84F5,
    TextureRectangleNv = 0x84F5,
    TextureCubeMap = 0x8513,
    TextureCubeMapArb = 0x8513,
    TextureCubeMapExt = 0x8513,
    TextureCubeMapOes = 0x8513,
    ProgramPointSize = 0x8642,
    DepthClamp = 0x864F,
    TextureCubeMapSeamless = 0x884F,
    SampleShading = 0x8C36,
    RasterizerDiscard = 0x8C89,
    PrimitiveRestartFixedIndex = 0x8D69,
    FramebufferSrgb = 0x8DB9,
    SampleMask = 0x8E51,
    FetchPerSampleArm = 0x8F65,
    PrimitiveRestart = 0x8F9D,
    DebugOutput = 0x92E0,
    ShadingRateImagePerPrimitiveNv = 0x95B1,
    ShadingRatePreserveAspectRatioQcom = 0x96A5,
}

public enum LightModelParameter
{
    LightModelLocalViewer = 0x0B51,
    LightModelTwoSide = 0x0B52,
    LightModelAmbient = 0x0B53,
    LightModelColorControl = 0x81F8,
    LightModelColorControlExt = 0x81F8,
}

public enum FogPName
{
    FogIndex = 0x0B61,
    FogDensity = 0x0B62,
    FogStart = 0x0B63,
    FogEnd = 0x0B64,
    FogMode = 0x0B65,
    FogCoordSrc = 0x8450,
}

public enum FogParameter
{
    FogIndex = 0x0B61,
    FogDensity = 0x0B62,
    FogStart = 0x0B63,
    FogEnd = 0x0B64,
    FogMode = 0x0B65,
    FogColor = 0x0B66,
    FogOffsetValueSgix = 0x8199,
}

public enum GetFramebufferParameter
{
    Doublebuffer = 0x0C32,
    Stereo = 0x0C33,
    SampleBuffers = 0x80A8,
    Samples = 0x80A9,
    ImplementationColorReadType = 0x8B9A,
    ImplementationColorReadFormat = 0x8B9B,
    FramebufferDefaultWidth = 0x9310,
    FramebufferDefaultHeight = 0x9311,
    FramebufferDefaultLayers = 0x9312,
    FramebufferDefaultSamples = 0x9313,
    FramebufferDefaultFixedSampleLocations = 0x9314,
}

public enum HintTarget
{
    PerspectiveCorrectionHint = 0x0C50,
    PointSmoothHint = 0x0C51,
    LineSmoothHint = 0x0C52,
    PolygonSmoothHint = 0x0C53,
    FogHint = 0x0C54,
    PackCmykHintExt = 0x800E,
    UnpackCmykHintExt = 0x800F,
    PhongHintWin = 0x80EB,
    ClipVolumeClippingHintExt = 0x80F0,
    TextureMultiBufferHintSgix = 0x812E,
    GenerateMipmapHint = 0x8192,
    GenerateMipmapHintSgis = 0x8192,
    ProgramBinaryRetrievableHint = 0x8257,
    ConvolutionHintSgix = 0x8316,
    ScalebiasHintSgix = 0x8322,
    LineQualityHintSgix = 0x835B,
    VertexPreclipSgix = 0x83EE,
    VertexPreclipHintSgix = 0x83EF,
    TextureCompressionHint = 0x84EF,
    TextureCompressionHintArb = 0x84EF,
    VertexArrayStorageHintApple = 0x851F,
    MultisampleFilterHintNv = 0x8534,
    TransformHintApple = 0x85B1,
    TextureStorageHintApple = 0x85BC,
    FragmentShaderDerivativeHint = 0x8B8B,
    FragmentShaderDerivativeHintArb = 0x8B8B,
    FragmentShaderDerivativeHintOes = 0x8B8B,
    BinningControlHintQcom = 0x8FB0,
    PreferDoublebufferHintPgi = 0x1A1F8,
    ConserveMemoryHintPgi = 0x1A1FD,
    ReclaimMemoryHintPgi = 0x1A1FE,
    NativeGraphicsBeginHintPgi = 0x1A203,
    NativeGraphicsEndHintPgi = 0x1A204,
    AlwaysFastHintPgi = 0x1A20C,
    AlwaysSoftHintPgi = 0x1A20D,
    AllowDrawObjHintPgi = 0x1A20E,
    AllowDrawWinHintPgi = 0x1A20F,
    AllowDrawFrgHintPgi = 0x1A210,
    AllowDrawMemHintPgi = 0x1A211,
    StrictDepthfuncHintPgi = 0x1A216,
    StrictLightingHintPgi = 0x1A217,
    StrictScissorHintPgi = 0x1A218,
    FullStippleHintPgi = 0x1A219,
    ClipNearHintPgi = 0x1A220,
    ClipFarHintPgi = 0x1A221,
    WideLineHintPgi = 0x1A222,
    BackNormalsHintPgi = 0x1A223,
    VertexDataHintPgi = 0x1A22A,
    VertexConsistentHintPgi = 0x1A22B,
    MaterialSideHintPgi = 0x1A22C,
    MaxVertexHintPgi = 0x1A22D,
}

public enum PixelMap
{
    PixelMapIToI = 0x0C70,
    PixelMapSToS = 0x0C71,
    PixelMapIToR = 0x0C72,
    PixelMapIToG = 0x0C73,
    PixelMapIToB = 0x0C74,
    PixelMapIToA = 0x0C75,
    PixelMapRToR = 0x0C76,
    PixelMapGToG = 0x0C77,
    PixelMapBToB = 0x0C78,
    PixelMapAToA = 0x0C79,
}

public enum GetPixelMap
{
    PixelMapIToI = 0x0C70,
    PixelMapSToS = 0x0C71,
    PixelMapIToR = 0x0C72,
    PixelMapIToG = 0x0C73,
    PixelMapIToB = 0x0C74,
    PixelMapIToA = 0x0C75,
    PixelMapRToR = 0x0C76,
    PixelMapGToG = 0x0C77,
    PixelMapBToB = 0x0C78,
    PixelMapAToA = 0x0C79,
}

public enum PixelStoreParameter
{
    UnpackSwapBytes = 0x0CF0,
    UnpackLsbFirst = 0x0CF1,
    UnpackRowLength = 0x0CF2,
    UnpackRowLengthExt = 0x0CF2,
    UnpackSkipRows = 0x0CF3,
    UnpackSkipRowsExt = 0x0CF3,
    UnpackSkipPixels = 0x0CF4,
    UnpackSkipPixelsExt = 0x0CF4,
    UnpackAlignment = 0x0CF5,
    PackSwapBytes = 0x0D00,
    PackLsbFirst = 0x0D01,
    PackRowLength = 0x0D02,
    PackSkipRows = 0x0D03,
    PackSkipPixels = 0x0D04,
    PackAlignment = 0x0D05,
    PackSkipImages = 0x806B,
    PackSkipImagesExt = 0x806B,
    PackImageHeight = 0x806C,
    PackImageHeightExt = 0x806C,
    UnpackSkipImages = 0x806D,
    UnpackSkipImagesExt = 0x806D,
    UnpackImageHeight = 0x806E,
    UnpackImageHeightExt = 0x806E,
    PackSkipVolumesSgis = 0x8130,
    PackImageDepthSgis = 0x8131,
    UnpackSkipVolumesSgis = 0x8132,
    UnpackImageDepthSgis = 0x8133,
    PixelTileWidthSgix = 0x8140,
    PixelTileHeightSgix = 0x8141,
    PixelTileGridWidthSgix = 0x8142,
    PixelTileGridHeightSgix = 0x8143,
    PixelTileGridDepthSgix = 0x8144,
    PixelTileCacheSizeSgix = 0x8145,
    PackResampleSgix = 0x842E,
    UnpackResampleSgix = 0x842F,
    PackSubsampleRateSgix = 0x85A0,
    UnpackSubsampleRateSgix = 0x85A1,
    PackResampleOml = 0x8984,
    UnpackResampleOml = 0x8985,
}

public enum PixelTransferParameter
{
    MapColor = 0x0D10,
    MapStencil = 0x0D11,
    IndexShift = 0x0D12,
    IndexOffset = 0x0D13,
    RedScale = 0x0D14,
    RedBias = 0x0D15,
    GreenScale = 0x0D18,
    GreenBias = 0x0D19,
    BlueScale = 0x0D1A,
    BlueBias = 0x0D1B,
    AlphaScale = 0x0D1C,
    AlphaBias = 0x0D1D,
    DepthScale = 0x0D1E,
    DepthBias = 0x0D1F,
    PostConvolutionRedScale = 0x801C,
    PostConvolutionRedScaleExt = 0x801C,
    PostConvolutionGreenScale = 0x801D,
    PostConvolutionGreenScaleExt = 0x801D,
    PostConvolutionBlueScale = 0x801E,
    PostConvolutionBlueScaleExt = 0x801E,
    PostConvolutionAlphaScale = 0x801F,
    PostConvolutionAlphaScaleExt = 0x801F,
    PostConvolutionRedBias = 0x8020,
    PostConvolutionRedBiasExt = 0x8020,
    PostConvolutionGreenBias = 0x8021,
    PostConvolutionGreenBiasExt = 0x8021,
    PostConvolutionBlueBias = 0x8022,
    PostConvolutionBlueBiasExt = 0x8022,
    PostConvolutionAlphaBias = 0x8023,
    PostConvolutionAlphaBiasExt = 0x8023,
    PostColorMatrixRedScale = 0x80B4,
    PostColorMatrixRedScaleSgi = 0x80B4,
    PostColorMatrixGreenScale = 0x80B5,
    PostColorMatrixGreenScaleSgi = 0x80B5,
    PostColorMatrixBlueScale = 0x80B6,
    PostColorMatrixBlueScaleSgi = 0x80B6,
    PostColorMatrixAlphaScale = 0x80B7,
    PostColorMatrixAlphaScaleSgi = 0x80B7,
    PostColorMatrixRedBias = 0x80B8,
    PostColorMatrixRedBiasSgi = 0x80B8,
    PostColorMatrixGreenBias = 0x80B9,
    PostColorMatrixGreenBiasSgi = 0x80B9,
    PostColorMatrixBlueBias = 0x80BA,
    PostColorMatrixBlueBiasSgi = 0x80BA,
    PostColorMatrixAlphaBias = 0x80BB,
    PostColorMatrixAlphaBiasSgi = 0x80BB,
}

public enum IndexMaterialParameterEXT
{
    IndexOffset = 0x0D13,
}

public enum MapTarget
{
    Map1Color4 = 0x0D90,
    Map1Index = 0x0D91,
    Map1Normal = 0x0D92,
    Map1TextureCoord1 = 0x0D93,
    Map1TextureCoord2 = 0x0D94,
    Map1TextureCoord3 = 0x0D95,
    Map1TextureCoord4 = 0x0D96,
    Map1Vertex3 = 0x0D97,
    Map1Vertex4 = 0x0D98,
    Map2Color4 = 0x0DB0,
    Map2Index = 0x0DB1,
    Map2Normal = 0x0DB2,
    Map2TextureCoord1 = 0x0DB3,
    Map2TextureCoord2 = 0x0DB4,
    Map2TextureCoord3 = 0x0DB5,
    Map2TextureCoord4 = 0x0DB6,
    Map2Vertex3 = 0x0DB7,
    Map2Vertex4 = 0x0DB8,
    GeometryDeformationSgix = 0x8194,
    TextureDeformationSgix = 0x8195,
}

public enum CopyImageSubDataTarget
{
    Texture1d = 0x0DE0,
    Texture2d = 0x0DE1,
    Texture3d = 0x806F,
    TextureRectangle = 0x84F5,
    TextureCubeMap = 0x8513,
    Texture1dArray = 0x8C18,
    Texture2dArray = 0x8C1A,
    Renderbuffer = 0x8D41,
    TextureCubeMapArray = 0x9009,
    Texture2dMultisample = 0x9100,
    Texture2dMultisampleArray = 0x9102,
}

public enum TextureTarget
{
    Texture1d = 0x0DE0,
    Texture2d = 0x0DE1,
    ProxyTexture1d = 0x8063,
    ProxyTexture1dExt = 0x8063,
    ProxyTexture2d = 0x8064,
    ProxyTexture2dExt = 0x8064,
    Texture3d = 0x806F,
    Texture3dExt = 0x806F,
    Texture3dOes = 0x806F,
    ProxyTexture3d = 0x8070,
    ProxyTexture3dExt = 0x8070,
    DetailTexture2dSgis = 0x8095,
    Texture4dSgis = 0x8134,
    ProxyTexture4dSgis = 0x8135,
    TextureRectangle = 0x84F5,
    TextureRectangleArb = 0x84F5,
    TextureRectangleNv = 0x84F5,
    ProxyTextureRectangle = 0x84F7,
    ProxyTextureRectangleArb = 0x84F7,
    ProxyTextureRectangleNv = 0x84F7,
    TextureCubeMap = 0x8513,
    TextureCubeMapArb = 0x8513,
    TextureCubeMapExt = 0x8513,
    TextureCubeMapOes = 0x8513,
    TextureCubeMapPositiveX = 0x8515,
    TextureCubeMapNegativeX = 0x8516,
    TextureCubeMapPositiveY = 0x8517,
    TextureCubeMapNegativeY = 0x8518,
    TextureCubeMapPositiveZ = 0x8519,
    TextureCubeMapNegativeZ = 0x851A,
    ProxyTextureCubeMap = 0x851B,
    ProxyTextureCubeMapArb = 0x851B,
    ProxyTextureCubeMapExt = 0x851B,
    Texture1dArray = 0x8C18,
    ProxyTexture1dArray = 0x8C19,
    ProxyTexture1dArrayExt = 0x8C19,
    Texture2dArray = 0x8C1A,
    ProxyTexture2dArray = 0x8C1B,
    ProxyTexture2dArrayExt = 0x8C1B,
    TextureBuffer = 0x8C2A,
    Renderbuffer = 0x8D41,
    TextureCubeMapArray = 0x9009,
    TextureCubeMapArrayArb = 0x9009,
    TextureCubeMapArrayExt = 0x9009,
    TextureCubeMapArrayOes = 0x9009,
    ProxyTextureCubeMapArray = 0x900B,
    ProxyTextureCubeMapArrayArb = 0x900B,
    Texture2dMultisample = 0x9100,
    ProxyTexture2dMultisample = 0x9101,
    Texture2dMultisampleArray = 0x9102,
    ProxyTexture2dMultisampleArray = 0x9103,
}

public enum GetPointervPName
{
    FeedbackBufferPointer = 0x0DF0,
    SelectionBufferPointer = 0x0DF3,
    VertexArrayPointer = 0x808E,
    VertexArrayPointerExt = 0x808E,
    NormalArrayPointer = 0x808F,
    NormalArrayPointerExt = 0x808F,
    ColorArrayPointer = 0x8090,
    ColorArrayPointerExt = 0x8090,
    IndexArrayPointer = 0x8091,
    IndexArrayPointerExt = 0x8091,
    TextureCoordArrayPointer = 0x8092,
    TextureCoordArrayPointerExt = 0x8092,
    EdgeFlagArrayPointer = 0x8093,
    EdgeFlagArrayPointerExt = 0x8093,
    InstrumentBufferPointerSgix = 0x8180,
    DebugCallbackFunction = 0x8244,
    DebugCallbackUserParam = 0x8245,
}

public enum TextureParameterName
{
    TextureWidth = 0x1000,
    TextureHeight = 0x1001,
    TextureInternalFormat = 0x1003,
    TextureComponents = 0x1003,
    TextureBorderColor = 0x1004,
    TextureBorderColorNv = 0x1004,
    TextureBorder = 0x1005,
    TextureMagFilter = 0x2800,
    TextureMinFilter = 0x2801,
    TextureWrapS = 0x2802,
    TextureWrapT = 0x2803,
    TextureRedSize = 0x805C,
    TextureGreenSize = 0x805D,
    TextureBlueSize = 0x805E,
    TextureAlphaSize = 0x805F,
    TextureLuminanceSize = 0x8060,
    TextureIntensitySize = 0x8061,
    TexturePriority = 0x8066,
    TexturePriorityExt = 0x8066,
    TextureResident = 0x8067,
    TextureDepthExt = 0x8071,
    TextureWrapR = 0x8072,
    TextureWrapRExt = 0x8072,
    TextureWrapROes = 0x8072,
    DetailTextureLevelSgis = 0x809A,
    DetailTextureModeSgis = 0x809B,
    DetailTextureFuncPointsSgis = 0x809C,
    SharpenTextureFuncPointsSgis = 0x80B0,
    ShadowAmbientSgix = 0x80BF,
    DualTextureSelectSgis = 0x8124,
    QuadTextureSelectSgis = 0x8125,
    Texture4dsizeSgis = 0x8136,
    TextureWrapQSgis = 0x8137,
    TextureMinLod = 0x813A,
    TextureMinLodSgis = 0x813A,
    TextureMaxLod = 0x813B,
    TextureMaxLodSgis = 0x813B,
    TextureBaseLevel = 0x813C,
    TextureBaseLevelSgis = 0x813C,
    TextureMaxLevel = 0x813D,
    TextureMaxLevelSgis = 0x813D,
    TextureFilter4SizeSgis = 0x8147,
    TextureClipmapCenterSgix = 0x8171,
    TextureClipmapFrameSgix = 0x8172,
    TextureClipmapOffsetSgix = 0x8173,
    TextureClipmapVirtualDepthSgix = 0x8174,
    TextureClipmapLodOffsetSgix = 0x8175,
    TextureClipmapDepthSgix = 0x8176,
    PostTextureFilterBiasSgix = 0x8179,
    PostTextureFilterScaleSgix = 0x817A,
    TextureLodBiasSSgix = 0x818E,
    TextureLodBiasTSgix = 0x818F,
    TextureLodBiasRSgix = 0x8190,
    GenerateMipmap = 0x8191,
    GenerateMipmapSgis = 0x8191,
    TextureCompareSgix = 0x819A,
    TextureCompareOperatorSgix = 0x819B,
    TextureLequalRSgix = 0x819C,
    TextureGequalRSgix = 0x819D,
    TextureMaxClampSSgix = 0x8369,
    TextureMaxClampTSgix = 0x836A,
    TextureMaxClampRSgix = 0x836B,
    TextureMaxAnisotropy = 0x84FE,
    TextureLodBias = 0x8501,
    TextureCompareMode = 0x884C,
    TextureCompareFunc = 0x884D,
    TextureSwizzleR = 0x8E42,
    TextureSwizzleG = 0x8E43,
    TextureSwizzleB = 0x8E44,
    TextureSwizzleA = 0x8E45,
    TextureSwizzleRgba = 0x8E46,
    TextureUnnormalizedCoordinatesArm = 0x8F6A,
    DepthStencilTextureMode = 0x90EA,
    TextureTilingExt = 0x9580,
    TextureFoveatedCutoffDensityQcom = 0x96A0,
}

public enum GetTextureParameter
{
    TextureWidth = 0x1000,
    TextureHeight = 0x1001,
    TextureInternalFormat = 0x1003,
    TextureComponents = 0x1003,
    TextureBorderColor = 0x1004,
    TextureBorderColorNv = 0x1004,
    TextureBorder = 0x1005,
    TextureMagFilter = 0x2800,
    TextureMinFilter = 0x2801,
    TextureWrapS = 0x2802,
    TextureWrapT = 0x2803,
    TextureRedSize = 0x805C,
    TextureGreenSize = 0x805D,
    TextureBlueSize = 0x805E,
    TextureAlphaSize = 0x805F,
    TextureLuminanceSize = 0x8060,
    TextureIntensitySize = 0x8061,
    TexturePriority = 0x8066,
    TextureResident = 0x8067,
    TextureDepthExt = 0x8071,
    TextureWrapRExt = 0x8072,
    DetailTextureLevelSgis = 0x809A,
    DetailTextureModeSgis = 0x809B,
    DetailTextureFuncPointsSgis = 0x809C,
    SharpenTextureFuncPointsSgis = 0x80B0,
    ShadowAmbientSgix = 0x80BF,
    DualTextureSelectSgis = 0x8124,
    QuadTextureSelectSgis = 0x8125,
    Texture4dsizeSgis = 0x8136,
    TextureWrapQSgis = 0x8137,
    TextureMinLodSgis = 0x813A,
    TextureMaxLodSgis = 0x813B,
    TextureBaseLevelSgis = 0x813C,
    TextureMaxLevelSgis = 0x813D,
    TextureFilter4SizeSgis = 0x8147,
    TextureClipmapCenterSgix = 0x8171,
    TextureClipmapFrameSgix = 0x8172,
    TextureClipmapOffsetSgix = 0x8173,
    TextureClipmapVirtualDepthSgix = 0x8174,
    TextureClipmapLodOffsetSgix = 0x8175,
    TextureClipmapDepthSgix = 0x8176,
    PostTextureFilterBiasSgix = 0x8179,
    PostTextureFilterScaleSgix = 0x817A,
    TextureLodBiasSSgix = 0x818E,
    TextureLodBiasTSgix = 0x818F,
    TextureLodBiasRSgix = 0x8190,
    GenerateMipmapSgis = 0x8191,
    TextureCompareSgix = 0x819A,
    TextureCompareOperatorSgix = 0x819B,
    TextureLequalRSgix = 0x819C,
    TextureGequalRSgix = 0x819D,
    TextureMaxClampSSgix = 0x8369,
    TextureMaxClampTSgix = 0x836A,
    TextureMaxClampRSgix = 0x836B,
    TextureUnnormalizedCoordinatesArm = 0x8F6A,
    SurfaceCompressionExt = 0x96C0,
}

public enum SamplerParameterF
{
    TextureBorderColor = 0x1004,
    TextureMinLod = 0x813A,
    TextureMaxLod = 0x813B,
    TextureMaxAnisotropy = 0x84FE,
    TextureLodBias = 0x8501,
    TextureUnnormalizedCoordinatesArm = 0x8F6A,
}

public enum DebugSeverity
{
    DontCare = 0x1100,
    DebugSeverityNotification = 0x826B,
    DebugSeverityHigh = 0x9146,
    DebugSeverityMedium = 0x9147,
    DebugSeverityLow = 0x9148,
}

public enum HintMode
{
    DontCare = 0x1100,
    Fastest = 0x1101,
    Nicest = 0x1102,
}

public enum DebugSource
{
    DontCare = 0x1100,
    DebugSourceApi = 0x8246,
    DebugSourceWindowSystem = 0x8247,
    DebugSourceShaderCompiler = 0x8248,
    DebugSourceThirdParty = 0x8249,
    DebugSourceApplication = 0x824A,
    DebugSourceOther = 0x824B,
}

public enum DebugType
{
    DontCare = 0x1100,
    DebugTypeError = 0x824C,
    DebugTypeDeprecatedBehavior = 0x824D,
    DebugTypeUndefinedBehavior = 0x824E,
    DebugTypePortability = 0x824F,
    DebugTypePerformance = 0x8250,
    DebugTypeOther = 0x8251,
    DebugTypeMarker = 0x8268,
    DebugTypePushGroup = 0x8269,
    DebugTypePopGroup = 0x826A,
}

public enum MaterialParameter
{
    Ambient = 0x1200,
    Diffuse = 0x1201,
    Specular = 0x1202,
    Emission = 0x1600,
    Shininess = 0x1601,
    AmbientAndDiffuse = 0x1602,
    ColorIndexes = 0x1603,
}

public enum FragmentLightParameterSGIX
{
    Ambient = 0x1200,
    Diffuse = 0x1201,
    Specular = 0x1202,
    Position = 0x1203,
    SpotDirection = 0x1204,
    SpotExponent = 0x1205,
    SpotCutoff = 0x1206,
    ConstantAttenuation = 0x1207,
    LinearAttenuation = 0x1208,
    QuadraticAttenuation = 0x1209,
}

public enum ColorMaterialParameter
{
    Ambient = 0x1200,
    Diffuse = 0x1201,
    Specular = 0x1202,
    Emission = 0x1600,
    AmbientAndDiffuse = 0x1602,
}

public enum LightParameter
{
    Position = 0x1203,
    SpotDirection = 0x1204,
    SpotExponent = 0x1205,
    SpotCutoff = 0x1206,
    ConstantAttenuation = 0x1207,
    LinearAttenuation = 0x1208,
    QuadraticAttenuation = 0x1209,
}

public enum ListMode
{
    Compile = 0x1300,
    CompileAndExecute = 0x1301,
}

public enum VertexAttribIType
{
    Byte = 0x1400,
    UnsignedByte = 0x1401,
    Short = 0x1402,
    UnsignedShort = 0x1403,
    Int = 0x1404,
    UnsignedInt = 0x1405,
}

public enum WeightPointerTypeARB
{
    Byte = 0x1400,
    UnsignedByte = 0x1401,
    Short = 0x1402,
    UnsignedShort = 0x1403,
    Int = 0x1404,
    UnsignedInt = 0x1405,
    Float = 0x1406,
    Double = 0x140A,
}

public enum TangentPointerTypeEXT
{
    Byte = 0x1400,
    Short = 0x1402,
    Int = 0x1404,
    Float = 0x1406,
    Double = 0x140A,
    DoubleExt = 0x140A,
}

public enum BinormalPointerTypeEXT
{
    Byte = 0x1400,
    Short = 0x1402,
    Int = 0x1404,
    Float = 0x1406,
    Double = 0x140A,
    DoubleExt = 0x140A,
}

public enum ColorPointerType
{
    Byte = 0x1400,
    UnsignedByte = 0x1401,
    UnsignedShort = 0x1403,
    UnsignedInt = 0x1405,
}

public enum ListNameType
{
    Byte = 0x1400,
    UnsignedByte = 0x1401,
    Short = 0x1402,
    UnsignedShort = 0x1403,
    Int = 0x1404,
    UnsignedInt = 0x1405,
    Float = 0x1406,
    _2Bytes = 0x1407,
    _3Bytes = 0x1408,
    _4Bytes = 0x1409,
}

public enum NormalPointerType
{
    Byte = 0x1400,
    Short = 0x1402,
    Int = 0x1404,
    Float = 0x1406,
    Double = 0x140A,
}

public enum PixelType
{
    Byte = 0x1400,
    UnsignedByte = 0x1401,
    Short = 0x1402,
    UnsignedShort = 0x1403,
    Int = 0x1404,
    UnsignedInt = 0x1405,
    Float = 0x1406,
    Bitmap = 0x1A00,
    UnsignedByte332 = 0x8032,
    UnsignedByte332Ext = 0x8032,
    UnsignedShort4444 = 0x8033,
    UnsignedShort4444Ext = 0x8033,
    UnsignedShort5551 = 0x8034,
    UnsignedShort5551Ext = 0x8034,
    UnsignedInt8888 = 0x8035,
    UnsignedInt8888Ext = 0x8035,
    UnsignedInt1010102 = 0x8036,
    UnsignedInt1010102Ext = 0x8036,
}

public enum VertexAttribType
{
    Byte = 0x1400,
    UnsignedByte = 0x1401,
    Short = 0x1402,
    UnsignedShort = 0x1403,
    Int = 0x1404,
    UnsignedInt = 0x1405,
    Float = 0x1406,
    Double = 0x140A,
    HalfFloat = 0x140B,
    Fixed = 0x140C,
    UnsignedInt2101010Rev = 0x8368,
    UnsignedInt10f11f11fRev = 0x8C3B,
    Int2101010Rev = 0x8D9F,
}

public enum VertexAttribPointerType
{
    Byte = 0x1400,
    UnsignedByte = 0x1401,
    Short = 0x1402,
    UnsignedShort = 0x1403,
    Int = 0x1404,
    UnsignedInt = 0x1405,
    Float = 0x1406,
    Double = 0x140A,
    HalfFloat = 0x140B,
    Fixed = 0x140C,
    Int64Arb = 0x140E,
    Int64Nv = 0x140E,
    UnsignedInt64Arb = 0x140F,
    UnsignedInt64Nv = 0x140F,
    UnsignedInt2101010Rev = 0x8368,
    UnsignedInt10f11f11fRev = 0x8C3B,
    Int2101010Rev = 0x8D9F,
}

public enum ScalarType
{
    UnsignedByte = 0x1401,
    UnsignedShort = 0x1403,
    UnsignedInt = 0x1405,
}

public enum ReplacementCodeTypeSUN
{
    UnsignedByte = 0x1401,
    UnsignedShort = 0x1403,
    UnsignedInt = 0x1405,
}

public enum ElementPointerTypeATI
{
    UnsignedByte = 0x1401,
    UnsignedShort = 0x1403,
    UnsignedInt = 0x1405,
}

public enum MatrixIndexPointerTypeARB
{
    UnsignedByte = 0x1401,
    UnsignedShort = 0x1403,
    UnsignedInt = 0x1405,
}

public enum DrawElementsType
{
    UnsignedByte = 0x1401,
    UnsignedShort = 0x1403,
    UnsignedInt = 0x1405,
}

public enum SecondaryColorPointerTypeIBM
{
    Short = 0x1402,
    Int = 0x1404,
    Float = 0x1406,
    Double = 0x140A,
}

public enum IndexPointerType
{
    Short = 0x1402,
    Int = 0x1404,
    Float = 0x1406,
    Double = 0x140A,
}

public enum TexCoordPointerType
{
    Short = 0x1402,
    Int = 0x1404,
    Float = 0x1406,
    Double = 0x140A,
}

public enum VertexPointerType
{
    Short = 0x1402,
    Int = 0x1404,
    Float = 0x1406,
    Double = 0x140A,
}

public enum PixelFormat
{
    UnsignedShort = 0x1403,
    UnsignedInt = 0x1405,
    ColorIndex = 0x1900,
    StencilIndex = 0x1901,
    DepthComponent = 0x1902,
    Red = 0x1903,
    RedExt = 0x1903,
    Green = 0x1904,
    Blue = 0x1905,
    Alpha = 0x1906,
    Rgb = 0x1907,
    Rgba = 0x1908,
    Luminance = 0x1909,
    LuminanceAlpha = 0x190A,
    AbgrExt = 0x8000,
    CmykExt = 0x800C,
    CmykaExt = 0x800D,
    Bgr = 0x80E0,
    Bgra = 0x80E1,
    Ycrcb422Sgix = 0x81BB,
    Ycrcb444Sgix = 0x81BC,
    Rg = 0x8227,
    RgInteger = 0x8228,
    DepthStencil = 0x84F9,
    RedInteger = 0x8D94,
    GreenInteger = 0x8D95,
    BlueInteger = 0x8D96,
    RgbInteger = 0x8D98,
    RgbaInteger = 0x8D99,
    BgrInteger = 0x8D9A,
    BgraInteger = 0x8D9B,
}

public enum AttributeType
{
    Int = 0x1404,
    UnsignedInt = 0x1405,
    Float = 0x1406,
    Double = 0x140A,
    Int64Arb = 0x140E,
    Int64Nv = 0x140E,
    UnsignedInt64Arb = 0x140F,
    UnsignedInt64Nv = 0x140F,
    FloatVec2 = 0x8B50,
    FloatVec2Arb = 0x8B50,
    FloatVec3 = 0x8B51,
    FloatVec3Arb = 0x8B51,
    FloatVec4 = 0x8B52,
    FloatVec4Arb = 0x8B52,
    IntVec2 = 0x8B53,
    IntVec2Arb = 0x8B53,
    IntVec3 = 0x8B54,
    IntVec3Arb = 0x8B54,
    IntVec4 = 0x8B55,
    IntVec4Arb = 0x8B55,
    Bool = 0x8B56,
    BoolArb = 0x8B56,
    BoolVec2 = 0x8B57,
    BoolVec2Arb = 0x8B57,
    BoolVec3 = 0x8B58,
    BoolVec3Arb = 0x8B58,
    BoolVec4 = 0x8B59,
    BoolVec4Arb = 0x8B59,
    FloatMat2 = 0x8B5A,
    FloatMat2Arb = 0x8B5A,
    FloatMat3 = 0x8B5B,
    FloatMat3Arb = 0x8B5B,
    FloatMat4 = 0x8B5C,
    FloatMat4Arb = 0x8B5C,
    Sampler1d = 0x8B5D,
    Sampler1dArb = 0x8B5D,
    Sampler2d = 0x8B5E,
    Sampler2dArb = 0x8B5E,
    Sampler3d = 0x8B5F,
    Sampler3dArb = 0x8B5F,
    Sampler3dOes = 0x8B5F,
    SamplerCube = 0x8B60,
    SamplerCubeArb = 0x8B60,
    Sampler1dShadow = 0x8B61,
    Sampler1dShadowArb = 0x8B61,
    Sampler2dShadow = 0x8B62,
    Sampler2dShadowArb = 0x8B62,
    Sampler2dShadowExt = 0x8B62,
    Sampler2dRect = 0x8B63,
    Sampler2dRectArb = 0x8B63,
    Sampler2dRectShadow = 0x8B64,
    Sampler2dRectShadowArb = 0x8B64,
    FloatMat2x3 = 0x8B65,
    FloatMat2x3Nv = 0x8B65,
    FloatMat2x4 = 0x8B66,
    FloatMat2x4Nv = 0x8B66,
    FloatMat3x2 = 0x8B67,
    FloatMat3x2Nv = 0x8B67,
    FloatMat3x4 = 0x8B68,
    FloatMat3x4Nv = 0x8B68,
    FloatMat4x2 = 0x8B69,
    FloatMat4x2Nv = 0x8B69,
    FloatMat4x3 = 0x8B6A,
    FloatMat4x3Nv = 0x8B6A,
    SamplerBuffer = 0x8DC2,
    Sampler1dArrayShadow = 0x8DC3,
    Sampler2dArrayShadow = 0x8DC4,
    SamplerCubeShadow = 0x8DC5,
    UnsignedIntVec2 = 0x8DC6,
    UnsignedIntVec3 = 0x8DC7,
    UnsignedIntVec4 = 0x8DC8,
    IntSampler1d = 0x8DC9,
    IntSampler2d = 0x8DCA,
    IntSampler3d = 0x8DCB,
    IntSamplerCube = 0x8DCC,
    IntSampler2dRect = 0x8DCD,
    IntSampler1dArray = 0x8DCE,
    IntSampler2dArray = 0x8DCF,
    IntSamplerBuffer = 0x8DD0,
    UnsignedIntSampler1d = 0x8DD1,
    UnsignedIntSampler2d = 0x8DD2,
    UnsignedIntSampler3d = 0x8DD3,
    UnsignedIntSamplerCube = 0x8DD4,
    UnsignedIntSampler2dRect = 0x8DD5,
    UnsignedIntSampler1dArray = 0x8DD6,
    UnsignedIntSampler2dArray = 0x8DD7,
    UnsignedIntSamplerBuffer = 0x8DD8,
    DoubleMat2 = 0x8F46,
    DoubleMat3 = 0x8F47,
    DoubleMat4 = 0x8F48,
    DoubleMat2x3 = 0x8F49,
    DoubleMat2x4 = 0x8F4A,
    DoubleMat3x2 = 0x8F4B,
    DoubleMat3x4 = 0x8F4C,
    DoubleMat4x2 = 0x8F4D,
    DoubleMat4x3 = 0x8F4E,
    Int64Vec2Arb = 0x8FE9,
    Int64Vec3Arb = 0x8FEA,
    Int64Vec4Arb = 0x8FEB,
    UnsignedInt64Vec2Arb = 0x8FF5,
    UnsignedInt64Vec3Arb = 0x8FF6,
    UnsignedInt64Vec4Arb = 0x8FF7,
    DoubleVec2 = 0x8FFC,
    DoubleVec3 = 0x8FFD,
    DoubleVec4 = 0x8FFE,
    SamplerCubeMapArray = 0x900C,
    SamplerCubeMapArrayShadow = 0x900D,
    IntSamplerCubeMapArray = 0x900E,
    UnsignedIntSamplerCubeMapArray = 0x900F,
    Image1d = 0x904C,
    Image2d = 0x904D,
    Image3d = 0x904E,
    Image2dRect = 0x904F,
    ImageCube = 0x9050,
    ImageBuffer = 0x9051,
    Image1dArray = 0x9052,
    Image2dArray = 0x9053,
    ImageCubeMapArray = 0x9054,
    Image2dMultisample = 0x9055,
    Image2dMultisampleArray = 0x9056,
    IntImage1d = 0x9057,
    IntImage2d = 0x9058,
    IntImage3d = 0x9059,
    IntImage2dRect = 0x905A,
    IntImageCube = 0x905B,
    IntImageBuffer = 0x905C,
    IntImage1dArray = 0x905D,
    IntImage2dArray = 0x905E,
    IntImageCubeMapArray = 0x905F,
    IntImage2dMultisample = 0x9060,
    IntImage2dMultisampleArray = 0x9061,
    UnsignedIntImage1d = 0x9062,
    UnsignedIntImage2d = 0x9063,
    UnsignedIntImage3d = 0x9064,
    UnsignedIntImage2dRect = 0x9065,
    UnsignedIntImageCube = 0x9066,
    UnsignedIntImageBuffer = 0x9067,
    UnsignedIntImage1dArray = 0x9068,
    UnsignedIntImage2dArray = 0x9069,
    UnsignedIntImageCubeMapArray = 0x906A,
    UnsignedIntImage2dMultisample = 0x906B,
    UnsignedIntImage2dMultisampleArray = 0x906C,
    Sampler2dMultisample = 0x9108,
    IntSampler2dMultisample = 0x9109,
    UnsignedIntSampler2dMultisample = 0x910A,
    Sampler2dMultisampleArray = 0x910B,
    IntSampler2dMultisampleArray = 0x910C,
    UnsignedIntSampler2dMultisampleArray = 0x910D,
}

public enum UniformType
{
    Int = 0x1404,
    UnsignedInt = 0x1405,
    Float = 0x1406,
    Double = 0x140A,
    FloatVec2 = 0x8B50,
    FloatVec3 = 0x8B51,
    FloatVec4 = 0x8B52,
    IntVec2 = 0x8B53,
    IntVec3 = 0x8B54,
    IntVec4 = 0x8B55,
    Bool = 0x8B56,
    BoolVec2 = 0x8B57,
    BoolVec3 = 0x8B58,
    BoolVec4 = 0x8B59,
    FloatMat2 = 0x8B5A,
    FloatMat3 = 0x8B5B,
    FloatMat4 = 0x8B5C,
    Sampler1d = 0x8B5D,
    Sampler2d = 0x8B5E,
    Sampler3d = 0x8B5F,
    SamplerCube = 0x8B60,
    Sampler1dShadow = 0x8B61,
    Sampler2dShadow = 0x8B62,
    Sampler2dRect = 0x8B63,
    Sampler2dRectShadow = 0x8B64,
    FloatMat2x3 = 0x8B65,
    FloatMat2x4 = 0x8B66,
    FloatMat3x2 = 0x8B67,
    FloatMat3x4 = 0x8B68,
    FloatMat4x2 = 0x8B69,
    FloatMat4x3 = 0x8B6A,
    Sampler1dArray = 0x8DC0,
    Sampler2dArray = 0x8DC1,
    SamplerBuffer = 0x8DC2,
    Sampler1dArrayShadow = 0x8DC3,
    Sampler2dArrayShadow = 0x8DC4,
    SamplerCubeShadow = 0x8DC5,
    UnsignedIntVec2 = 0x8DC6,
    UnsignedIntVec3 = 0x8DC7,
    UnsignedIntVec4 = 0x8DC8,
    IntSampler1d = 0x8DC9,
    IntSampler2d = 0x8DCA,
    IntSampler3d = 0x8DCB,
    IntSamplerCube = 0x8DCC,
    IntSampler2dRect = 0x8DCD,
    IntSampler1dArray = 0x8DCE,
    IntSampler2dArray = 0x8DCF,
    IntSamplerBuffer = 0x8DD0,
    UnsignedIntSampler1d = 0x8DD1,
    UnsignedIntSampler2d = 0x8DD2,
    UnsignedIntSampler3d = 0x8DD3,
    UnsignedIntSamplerCube = 0x8DD4,
    UnsignedIntSampler2dRect = 0x8DD5,
    UnsignedIntSampler1dArray = 0x8DD6,
    UnsignedIntSampler2dArray = 0x8DD7,
    UnsignedIntSamplerBuffer = 0x8DD8,
    DoubleMat2 = 0x8F46,
    DoubleMat3 = 0x8F47,
    DoubleMat4 = 0x8F48,
    DoubleMat2x3 = 0x8F49,
    DoubleMat2x4 = 0x8F4A,
    DoubleMat3x2 = 0x8F4B,
    DoubleMat3x4 = 0x8F4C,
    DoubleMat4x2 = 0x8F4D,
    DoubleMat4x3 = 0x8F4E,
    DoubleVec2 = 0x8FFC,
    DoubleVec3 = 0x8FFD,
    DoubleVec4 = 0x8FFE,
    SamplerCubeMapArray = 0x900C,
    SamplerCubeMapArrayShadow = 0x900D,
    IntSamplerCubeMapArray = 0x900E,
    UnsignedIntSamplerCubeMapArray = 0x900F,
    Sampler2dMultisample = 0x9108,
    IntSampler2dMultisample = 0x9109,
    UnsignedIntSampler2dMultisample = 0x910A,
    Sampler2dMultisampleArray = 0x910B,
    IntSampler2dMultisampleArray = 0x910C,
    UnsignedIntSampler2dMultisampleArray = 0x910D,
}

public enum GlslTypeToken
{
    Int = 0x1404,
    UnsignedInt = 0x1405,
    Float = 0x1406,
    Double = 0x140A,
    FloatVec2 = 0x8B50,
    FloatVec3 = 0x8B51,
    FloatVec4 = 0x8B52,
    IntVec2 = 0x8B53,
    IntVec3 = 0x8B54,
    IntVec4 = 0x8B55,
    Bool = 0x8B56,
    BoolVec2 = 0x8B57,
    BoolVec3 = 0x8B58,
    BoolVec4 = 0x8B59,
    FloatMat2 = 0x8B5A,
    FloatMat3 = 0x8B5B,
    FloatMat4 = 0x8B5C,
    Sampler1d = 0x8B5D,
    Sampler2d = 0x8B5E,
    Sampler3d = 0x8B5F,
    SamplerCube = 0x8B60,
    Sampler1dShadow = 0x8B61,
    Sampler2dShadow = 0x8B62,
    Sampler2dRect = 0x8B63,
    Sampler2dRectShadow = 0x8B64,
    FloatMat2x3 = 0x8B65,
    FloatMat2x4 = 0x8B66,
    FloatMat3x2 = 0x8B67,
    FloatMat3x4 = 0x8B68,
    FloatMat4x2 = 0x8B69,
    FloatMat4x3 = 0x8B6A,
    Sampler1dArray = 0x8DC0,
    Sampler2dArray = 0x8DC1,
    SamplerBuffer = 0x8DC2,
    Sampler1dArrayShadow = 0x8DC3,
    Sampler2dArrayShadow = 0x8DC4,
    SamplerCubeShadow = 0x8DC5,
    UnsignedIntVec2 = 0x8DC6,
    UnsignedIntVec3 = 0x8DC7,
    UnsignedIntVec4 = 0x8DC8,
    IntSampler1d = 0x8DC9,
    IntSampler2d = 0x8DCA,
    IntSampler3d = 0x8DCB,
    IntSamplerCube = 0x8DCC,
    IntSampler2dRect = 0x8DCD,
    IntSampler1dArray = 0x8DCE,
    IntSampler2dArray = 0x8DCF,
    IntSamplerBuffer = 0x8DD0,
    UnsignedIntSampler1d = 0x8DD1,
    UnsignedIntSampler2d = 0x8DD2,
    UnsignedIntSampler3d = 0x8DD3,
    UnsignedIntSamplerCube = 0x8DD4,
    UnsignedIntSampler2dRect = 0x8DD5,
    UnsignedIntSampler1dArray = 0x8DD6,
    UnsignedIntSampler2dArray = 0x8DD7,
    UnsignedIntSamplerBuffer = 0x8DD8,
    DoubleMat2 = 0x8F46,
    DoubleMat3 = 0x8F47,
    DoubleMat4 = 0x8F48,
    DoubleVec2 = 0x8FFC,
    DoubleVec3 = 0x8FFD,
    DoubleVec4 = 0x8FFE,
    SamplerCubeMapArray = 0x900C,
    SamplerCubeMapArrayShadow = 0x900D,
    IntSamplerCubeMapArray = 0x900E,
    UnsignedIntSamplerCubeMapArray = 0x900F,
    Image1d = 0x904C,
    Image2d = 0x904D,
    Image3d = 0x904E,
    Image2dRect = 0x904F,
    ImageCube = 0x9050,
    ImageBuffer = 0x9051,
    Image1dArray = 0x9052,
    Image2dArray = 0x9053,
    ImageCubeMapArray = 0x9054,
    Image2dMultisample = 0x9055,
    Image2dMultisampleArray = 0x9056,
    IntImage1d = 0x9057,
    IntImage2d = 0x9058,
    IntImage3d = 0x9059,
    IntImage2dRect = 0x905A,
    IntImageCube = 0x905B,
    IntImageBuffer = 0x905C,
    IntImage1dArray = 0x905D,
    IntImage2dArray = 0x905E,
    IntImageCubeMapArray = 0x905F,
    IntImage2dMultisample = 0x9060,
    IntImage2dMultisampleArray = 0x9061,
    UnsignedIntImage1d = 0x9062,
    UnsignedIntImage2d = 0x9063,
    UnsignedIntImage3d = 0x9064,
    UnsignedIntImage2dRect = 0x9065,
    UnsignedIntImageCube = 0x9066,
    UnsignedIntImageBuffer = 0x9067,
    UnsignedIntImage1dArray = 0x9068,
    UnsignedIntImage2dArray = 0x9069,
    UnsignedIntImageCubeMapArray = 0x906A,
    UnsignedIntImage2dMultisample = 0x906B,
    UnsignedIntImage2dMultisampleArray = 0x906C,
    Sampler2dMultisample = 0x9108,
    IntSampler2dMultisample = 0x9109,
    UnsignedIntSampler2dMultisample = 0x910A,
    Sampler2dMultisampleArray = 0x910B,
    IntSampler2dMultisampleArray = 0x910C,
    UnsignedIntSampler2dMultisampleArray = 0x910D,
    UnsignedIntAtomicCounter = 0x92DB,
}

public enum MapTypeNV
{
    Float = 0x1406,
    Double = 0x140A,
}

public enum VertexWeightPointerTypeEXT
{
    Float = 0x1406,
}

public enum FogCoordinatePointerType
{
    Float = 0x1406,
    Double = 0x140A,
}

public enum FogPointerTypeEXT
{
    Float = 0x1406,
    Double = 0x140A,
}

public enum FogPointerTypeIBM
{
    Float = 0x1406,
    Double = 0x140A,
}

public enum VertexAttribLType
{
    Double = 0x140A,
}

public enum LogicOp
{
    Clear = 0x1500,
    And = 0x1501,
    AndReverse = 0x1502,
    Copy = 0x1503,
    AndInverted = 0x1504,
    Noop = 0x1505,
    Xor = 0x1506,
    Or = 0x1507,
    Nor = 0x1508,
    Equiv = 0x1509,
    Invert = 0x150A,
    OrReverse = 0x150B,
    CopyInverted = 0x150C,
    OrInverted = 0x150D,
    Nand = 0x150E,
    Set = 0x150F,
}

public enum PathFillMode
{
    Invert = 0x150A,
    PathFillModeNv = 0x9080,
    CountUpNv = 0x9088,
    CountDownNv = 0x9089,
}

public enum MatrixMode
{
    Modelview = 0x1700,
    Modelview0Ext = 0x1700,
    Projection = 0x1701,
    Texture = 0x1702,
}

public enum ObjectIdentifier
{
    Texture = 0x1702,
    VertexArray = 0x8074,
    Buffer = 0x82E0,
    Shader = 0x82E1,
    Program = 0x82E2,
    Query = 0x82E3,
    ProgramPipeline = 0x82E4,
    Sampler = 0x82E6,
    Framebuffer = 0x8D40,
    Renderbuffer = 0x8D41,
    TransformFeedback = 0x8E22,
}

public enum Buffer
{
    Color = 0x1800,
    Depth = 0x1801,
    Stencil = 0x1802,
}

public enum PixelCopyType
{
    Color = 0x1800,
    ColorExt = 0x1800,
    Depth = 0x1801,
    DepthExt = 0x1801,
    Stencil = 0x1802,
    StencilExt = 0x1802,
}

public enum InvalidateFramebufferAttachment
{
    Color = 0x1800,
    Depth = 0x1801,
    Stencil = 0x1802,
    DepthStencilAttachment = 0x821A,
    ColorAttachment0 = 0x8CE0,
    ColorAttachment0Ext = 0x8CE0,
    ColorAttachment0Nv = 0x8CE0,
    ColorAttachment0Oes = 0x8CE0,
    ColorAttachment1 = 0x8CE1,
    ColorAttachment1Ext = 0x8CE1,
    ColorAttachment1Nv = 0x8CE1,
    ColorAttachment2 = 0x8CE2,
    ColorAttachment2Ext = 0x8CE2,
    ColorAttachment2Nv = 0x8CE2,
    ColorAttachment3 = 0x8CE3,
    ColorAttachment3Ext = 0x8CE3,
    ColorAttachment3Nv = 0x8CE3,
    ColorAttachment4 = 0x8CE4,
    ColorAttachment4Ext = 0x8CE4,
    ColorAttachment4Nv = 0x8CE4,
    ColorAttachment5 = 0x8CE5,
    ColorAttachment5Ext = 0x8CE5,
    ColorAttachment5Nv = 0x8CE5,
    ColorAttachment6 = 0x8CE6,
    ColorAttachment6Ext = 0x8CE6,
    ColorAttachment6Nv = 0x8CE6,
    ColorAttachment7 = 0x8CE7,
    ColorAttachment7Ext = 0x8CE7,
    ColorAttachment7Nv = 0x8CE7,
    ColorAttachment8 = 0x8CE8,
    ColorAttachment8Ext = 0x8CE8,
    ColorAttachment8Nv = 0x8CE8,
    ColorAttachment9 = 0x8CE9,
    ColorAttachment9Ext = 0x8CE9,
    ColorAttachment9Nv = 0x8CE9,
    ColorAttachment10 = 0x8CEA,
    ColorAttachment10Ext = 0x8CEA,
    ColorAttachment10Nv = 0x8CEA,
    ColorAttachment11 = 0x8CEB,
    ColorAttachment11Ext = 0x8CEB,
    ColorAttachment11Nv = 0x8CEB,
    ColorAttachment12 = 0x8CEC,
    ColorAttachment12Ext = 0x8CEC,
    ColorAttachment12Nv = 0x8CEC,
    ColorAttachment13 = 0x8CED,
    ColorAttachment13Ext = 0x8CED,
    ColorAttachment13Nv = 0x8CED,
    ColorAttachment14 = 0x8CEE,
    ColorAttachment14Ext = 0x8CEE,
    ColorAttachment14Nv = 0x8CEE,
    ColorAttachment15 = 0x8CEF,
    ColorAttachment15Ext = 0x8CEF,
    ColorAttachment15Nv = 0x8CEF,
    ColorAttachment16 = 0x8CF0,
    ColorAttachment17 = 0x8CF1,
    ColorAttachment18 = 0x8CF2,
    ColorAttachment19 = 0x8CF3,
    ColorAttachment20 = 0x8CF4,
    ColorAttachment21 = 0x8CF5,
    ColorAttachment22 = 0x8CF6,
    ColorAttachment23 = 0x8CF7,
    ColorAttachment24 = 0x8CF8,
    ColorAttachment25 = 0x8CF9,
    ColorAttachment26 = 0x8CFA,
    ColorAttachment27 = 0x8CFB,
    ColorAttachment28 = 0x8CFC,
    ColorAttachment29 = 0x8CFD,
    ColorAttachment30 = 0x8CFE,
    ColorAttachment31 = 0x8CFF,
    DepthAttachment = 0x8D00,
    DepthAttachmentExt = 0x8D00,
    DepthAttachmentOes = 0x8D00,
    StencilAttachmentExt = 0x8D20,
    StencilAttachmentOes = 0x8D20,
}

public enum InternalFormat
{
    StencilIndex = 0x1901,
    StencilIndexOes = 0x1901,
    DepthComponent = 0x1902,
    Red = 0x1903,
    RedExt = 0x1903,
    Rgb = 0x1907,
    Rgba = 0x1908,
    R3G3B2 = 0x2A10,
    Alpha4 = 0x803B,
    Alpha4Ext = 0x803B,
    Alpha8 = 0x803C,
    Alpha8Ext = 0x803C,
    Alpha8Oes = 0x803C,
    Alpha12 = 0x803D,
    Alpha12Ext = 0x803D,
    Alpha16 = 0x803E,
    Alpha16Ext = 0x803E,
    Luminance4 = 0x803F,
    Luminance4Ext = 0x803F,
    Luminance8 = 0x8040,
    Luminance8Ext = 0x8040,
    Luminance8Oes = 0x8040,
    Luminance12 = 0x8041,
    Luminance12Ext = 0x8041,
    Luminance16 = 0x8042,
    Luminance16Ext = 0x8042,
    Luminance4Alpha4 = 0x8043,
    Luminance4Alpha4Ext = 0x8043,
    Luminance4Alpha4Oes = 0x8043,
    Luminance6Alpha2 = 0x8044,
    Luminance6Alpha2Ext = 0x8044,
    Luminance8Alpha8 = 0x8045,
    Luminance8Alpha8Ext = 0x8045,
    Luminance8Alpha8Oes = 0x8045,
    Luminance12Alpha4 = 0x8046,
    Luminance12Alpha4Ext = 0x8046,
    Luminance12Alpha12 = 0x8047,
    Luminance12Alpha12Ext = 0x8047,
    Luminance16Alpha16 = 0x8048,
    Luminance16Alpha16Ext = 0x8048,
    Intensity = 0x8049,
    Intensity4 = 0x804A,
    Intensity4Ext = 0x804A,
    Intensity8 = 0x804B,
    Intensity8Ext = 0x804B,
    Intensity12 = 0x804C,
    Intensity12Ext = 0x804C,
    Intensity16 = 0x804D,
    Intensity16Ext = 0x804D,
    Rgb2Ext = 0x804E,
    Rgb4 = 0x804F,
    Rgb4Ext = 0x804F,
    Rgb5 = 0x8050,
    Rgb5Ext = 0x8050,
    Rgb8 = 0x8051,
    Rgb8Ext = 0x8051,
    Rgb8Oes = 0x8051,
    Rgb10 = 0x8052,
    Rgb10Ext = 0x8052,
    Rgb12 = 0x8053,
    Rgb12Ext = 0x8053,
    Rgb16 = 0x8054,
    Rgb16Ext = 0x8054,
    Rgba2 = 0x8055,
    Rgba2Ext = 0x8055,
    Rgba4 = 0x8056,
    Rgba4Ext = 0x8056,
    Rgba4Oes = 0x8056,
    Rgb5A1 = 0x8057,
    Rgb5A1Ext = 0x8057,
    Rgb5A1Oes = 0x8057,
    Rgba8 = 0x8058,
    Rgba8Ext = 0x8058,
    Rgba8Oes = 0x8058,
    Rgb10A2 = 0x8059,
    Rgb10A2Ext = 0x8059,
    Rgba12 = 0x805A,
    Rgba12Ext = 0x805A,
    Rgba16 = 0x805B,
    Rgba16Ext = 0x805B,
    DualAlpha4Sgis = 0x8110,
    DualAlpha8Sgis = 0x8111,
    DualAlpha12Sgis = 0x8112,
    DualAlpha16Sgis = 0x8113,
    DualLuminance4Sgis = 0x8114,
    DualLuminance8Sgis = 0x8115,
    DualLuminance12Sgis = 0x8116,
    DualLuminance16Sgis = 0x8117,
    DualIntensity4Sgis = 0x8118,
    DualIntensity8Sgis = 0x8119,
    DualIntensity12Sgis = 0x811A,
    DualIntensity16Sgis = 0x811B,
    DualLuminanceAlpha4Sgis = 0x811C,
    DualLuminanceAlpha8Sgis = 0x811D,
    QuadAlpha4Sgis = 0x811E,
    QuadAlpha8Sgis = 0x811F,
    QuadLuminance4Sgis = 0x8120,
    QuadLuminance8Sgis = 0x8121,
    QuadIntensity4Sgis = 0x8122,
    QuadIntensity8Sgis = 0x8123,
    DepthComponent16 = 0x81A5,
    DepthComponent16Arb = 0x81A5,
    DepthComponent16Oes = 0x81A5,
    DepthComponent16Sgix = 0x81A5,
    DepthComponent24 = 0x81A6,
    DepthComponent24Arb = 0x81A6,
    DepthComponent24Oes = 0x81A6,
    DepthComponent24Sgix = 0x81A6,
    DepthComponent32 = 0x81A7,
    DepthComponent32Arb = 0x81A7,
    DepthComponent32Oes = 0x81A7,
    DepthComponent32Sgix = 0x81A7,
    CompressedRed = 0x8225,
    CompressedRg = 0x8226,
    Rg = 0x8227,
    R8 = 0x8229,
    R8Ext = 0x8229,
    R16 = 0x822A,
    R16Ext = 0x822A,
    Rg8 = 0x822B,
    Rg8Ext = 0x822B,
    Rg16 = 0x822C,
    Rg16Ext = 0x822C,
    R16f = 0x822D,
    R16fExt = 0x822D,
    R32f = 0x822E,
    R32fExt = 0x822E,
    Rg16f = 0x822F,
    Rg16fExt = 0x822F,
    Rg32f = 0x8230,
    Rg32fExt = 0x8230,
    R8i = 0x8231,
    R8ui = 0x8232,
    R16i = 0x8233,
    R16ui = 0x8234,
    R32i = 0x8235,
    R32ui = 0x8236,
    Rg8i = 0x8237,
    Rg8ui = 0x8238,
    Rg16i = 0x8239,
    Rg16ui = 0x823A,
    Rg32i = 0x823B,
    Rg32ui = 0x823C,
    CompressedRgbS3tcDxt1Ext = 0x83F0,
    CompressedRgbaS3tcDxt1Ext = 0x83F1,
    CompressedRgbaS3tcDxt3Angle = 0x83F2,
    CompressedRgbaS3tcDxt3Ext = 0x83F2,
    CompressedRgbaS3tcDxt5Angle = 0x83F3,
    CompressedRgbaS3tcDxt5Ext = 0x83F3,
    CompressedRgb = 0x84ED,
    CompressedRgba = 0x84EE,
    DepthStencil = 0x84F9,
    DepthStencilExt = 0x84F9,
    DepthStencilNv = 0x84F9,
    DepthStencilOes = 0x84F9,
    DepthStencilMesa = 0x8750,
    Rgba32f = 0x8814,
    Rgba32fArb = 0x8814,
    Rgba32fExt = 0x8814,
    Rgb32f = 0x8815,
    Rgb32fArb = 0x8815,
    Rgb32fExt = 0x8815,
    Rgba16f = 0x881A,
    Rgba16fArb = 0x881A,
    Rgba16fExt = 0x881A,
    Rgb16f = 0x881B,
    Rgb16fArb = 0x881B,
    Rgb16fExt = 0x881B,
    Depth24Stencil8 = 0x88F0,
    Depth24Stencil8Ext = 0x88F0,
    Depth24Stencil8Oes = 0x88F0,
    R11fG11fB10f = 0x8C3A,
    R11fG11fB10fApple = 0x8C3A,
    R11fG11fB10fExt = 0x8C3A,
    Rgb9E5 = 0x8C3D,
    Rgb9E5Apple = 0x8C3D,
    Rgb9E5Ext = 0x8C3D,
    Srgb = 0x8C40,
    SrgbExt = 0x8C40,
    Srgb8 = 0x8C41,
    Srgb8Ext = 0x8C41,
    Srgb8Nv = 0x8C41,
    SrgbAlpha = 0x8C42,
    SrgbAlphaExt = 0x8C42,
    Srgb8Alpha8 = 0x8C43,
    Srgb8Alpha8Ext = 0x8C43,
    CompressedSrgb = 0x8C48,
    CompressedSrgbAlpha = 0x8C49,
    CompressedSrgbS3tcDxt1Ext = 0x8C4C,
    CompressedSrgbS3tcDxt1Nv = 0x8C4C,
    CompressedSrgbAlphaS3tcDxt1Ext = 0x8C4D,
    CompressedSrgbAlphaS3tcDxt1Nv = 0x8C4D,
    CompressedSrgbAlphaS3tcDxt3Ext = 0x8C4E,
    CompressedSrgbAlphaS3tcDxt3Nv = 0x8C4E,
    CompressedSrgbAlphaS3tcDxt5Ext = 0x8C4F,
    CompressedSrgbAlphaS3tcDxt5Nv = 0x8C4F,
    DepthComponent32f = 0x8CAC,
    Depth32fStencil8 = 0x8CAD,
    StencilIndex1 = 0x8D46,
    StencilIndex1Ext = 0x8D46,
    StencilIndex1Oes = 0x8D46,
    StencilIndex4 = 0x8D47,
    StencilIndex4Ext = 0x8D47,
    StencilIndex4Oes = 0x8D47,
    StencilIndex8 = 0x8D48,
    StencilIndex8Ext = 0x8D48,
    StencilIndex8Oes = 0x8D48,
    StencilIndex16 = 0x8D49,
    StencilIndex16Ext = 0x8D49,
    Rgb565Oes = 0x8D62,
    Rgb565 = 0x8D62,
    Etc1Rgb8Oes = 0x8D64,
    Rgba32ui = 0x8D70,
    Rgba32uiExt = 0x8D70,
    Rgb32ui = 0x8D71,
    Rgb32uiExt = 0x8D71,
    Alpha32uiExt = 0x8D72,
    Intensity32uiExt = 0x8D73,
    Luminance32uiExt = 0x8D74,
    LuminanceAlpha32uiExt = 0x8D75,
    Rgba16ui = 0x8D76,
    Rgba16uiExt = 0x8D76,
    Rgb16ui = 0x8D77,
    Rgb16uiExt = 0x8D77,
    Alpha16uiExt = 0x8D78,
    Intensity16uiExt = 0x8D79,
    Luminance16uiExt = 0x8D7A,
    LuminanceAlpha16uiExt = 0x8D7B,
    Rgba8ui = 0x8D7C,
    Rgba8uiExt = 0x8D7C,
    Rgb8ui = 0x8D7D,
    Rgb8uiExt = 0x8D7D,
    Alpha8uiExt = 0x8D7E,
    Intensity8uiExt = 0x8D7F,
    Luminance8uiExt = 0x8D80,
    LuminanceAlpha8uiExt = 0x8D81,
    Rgba32i = 0x8D82,
    Rgba32iExt = 0x8D82,
    Rgb32i = 0x8D83,
    Rgb32iExt = 0x8D83,
    Alpha32iExt = 0x8D84,
    Intensity32iExt = 0x8D85,
    Luminance32iExt = 0x8D86,
    LuminanceAlpha32iExt = 0x8D87,
    Rgba16i = 0x8D88,
    Rgba16iExt = 0x8D88,
    Rgb16i = 0x8D89,
    Rgb16iExt = 0x8D89,
    Alpha16iExt = 0x8D8A,
    Intensity16iExt = 0x8D8B,
    Luminance16iExt = 0x8D8C,
    LuminanceAlpha16iExt = 0x8D8D,
    Rgba8i = 0x8D8E,
    Rgba8iExt = 0x8D8E,
    Rgb8i = 0x8D8F,
    Rgb8iExt = 0x8D8F,
    Alpha8iExt = 0x8D90,
    Intensity8iExt = 0x8D91,
    Luminance8iExt = 0x8D92,
    LuminanceAlpha8iExt = 0x8D93,
    DepthComponent32fNv = 0x8DAB,
    Depth32fStencil8Nv = 0x8DAC,
    CompressedRedRgtc1 = 0x8DBB,
    CompressedRedRgtc1Ext = 0x8DBB,
    CompressedSignedRedRgtc1 = 0x8DBC,
    CompressedSignedRedRgtc1Ext = 0x8DBC,
    CompressedRedGreenRgtc2Ext = 0x8DBD,
    CompressedRgRgtc2 = 0x8DBD,
    CompressedSignedRedGreenRgtc2Ext = 0x8DBE,
    CompressedSignedRgRgtc2 = 0x8DBE,
    CompressedRgbaBptcUnorm = 0x8E8C,
    CompressedRgbaBptcUnormArb = 0x8E8C,
    CompressedRgbaBptcUnormExt = 0x8E8C,
    CompressedSrgbAlphaBptcUnorm = 0x8E8D,
    CompressedSrgbAlphaBptcUnormArb = 0x8E8D,
    CompressedSrgbAlphaBptcUnormExt = 0x8E8D,
    CompressedRgbBptcSignedFloat = 0x8E8E,
    CompressedRgbBptcSignedFloatArb = 0x8E8E,
    CompressedRgbBptcSignedFloatExt = 0x8E8E,
    CompressedRgbBptcUnsignedFloat = 0x8E8F,
    CompressedRgbBptcUnsignedFloatArb = 0x8E8F,
    CompressedRgbBptcUnsignedFloatExt = 0x8E8F,
    R8Snorm = 0x8F94,
    Rg8Snorm = 0x8F95,
    Rgb8Snorm = 0x8F96,
    Rgba8Snorm = 0x8F97,
    R16Snorm = 0x8F98,
    R16SnormExt = 0x8F98,
    Rg16Snorm = 0x8F99,
    Rg16SnormExt = 0x8F99,
    Rgb16Snorm = 0x8F9A,
    Rgb16SnormExt = 0x8F9A,
    Rgba16Snorm = 0x8F9B,
    Rgba16SnormExt = 0x8F9B,
    Sr8Ext = 0x8FBD,
    Srg8Ext = 0x8FBE,
    Rgb10A2ui = 0x906F,
    CompressedR11Eac = 0x9270,
    CompressedR11EacOes = 0x9270,
    CompressedSignedR11Eac = 0x9271,
    CompressedSignedR11EacOes = 0x9271,
    CompressedRg11Eac = 0x9272,
    CompressedRg11EacOes = 0x9272,
    CompressedSignedRg11Eac = 0x9273,
    CompressedSignedRg11EacOes = 0x9273,
    CompressedRgb8Etc2 = 0x9274,
    CompressedRgb8Etc2Oes = 0x9274,
    CompressedSrgb8Etc2 = 0x9275,
    CompressedSrgb8Etc2Oes = 0x9275,
    CompressedRgb8PunchthroughAlpha1Etc2 = 0x9276,
    CompressedRgb8PunchthroughAlpha1Etc2Oes = 0x9276,
    CompressedSrgb8PunchthroughAlpha1Etc2 = 0x9277,
    CompressedSrgb8PunchthroughAlpha1Etc2Oes = 0x9277,
    CompressedRgba8Etc2Eac = 0x9278,
    CompressedRgba8Etc2EacOes = 0x9278,
    CompressedSrgb8Alpha8Etc2Eac = 0x9279,
    CompressedSrgb8Alpha8Etc2EacOes = 0x9279,
    CompressedRgbaAstc4x4 = 0x93B0,
    CompressedRgbaAstc4x4Khr = 0x93B0,
    CompressedRgbaAstc5x4 = 0x93B1,
    CompressedRgbaAstc5x4Khr = 0x93B1,
    CompressedRgbaAstc5x5 = 0x93B2,
    CompressedRgbaAstc5x5Khr = 0x93B2,
    CompressedRgbaAstc6x5 = 0x93B3,
    CompressedRgbaAstc6x5Khr = 0x93B3,
    CompressedRgbaAstc6x6 = 0x93B4,
    CompressedRgbaAstc6x6Khr = 0x93B4,
    CompressedRgbaAstc8x5 = 0x93B5,
    CompressedRgbaAstc8x5Khr = 0x93B5,
    CompressedRgbaAstc8x6 = 0x93B6,
    CompressedRgbaAstc8x6Khr = 0x93B6,
    CompressedRgbaAstc8x8 = 0x93B7,
    CompressedRgbaAstc8x8Khr = 0x93B7,
    CompressedRgbaAstc10x5 = 0x93B8,
    CompressedRgbaAstc10x5Khr = 0x93B8,
    CompressedRgbaAstc10x6 = 0x93B9,
    CompressedRgbaAstc10x6Khr = 0x93B9,
    CompressedRgbaAstc10x8 = 0x93BA,
    CompressedRgbaAstc10x8Khr = 0x93BA,
    CompressedRgbaAstc10x10 = 0x93BB,
    CompressedRgbaAstc10x10Khr = 0x93BB,
    CompressedRgbaAstc12x10 = 0x93BC,
    CompressedRgbaAstc12x10Khr = 0x93BC,
    CompressedRgbaAstc12x12 = 0x93BD,
    CompressedRgbaAstc12x12Khr = 0x93BD,
    CompressedRgbaAstc3x3x3Oes = 0x93C0,
    CompressedRgbaAstc4x3x3Oes = 0x93C1,
    CompressedRgbaAstc4x4x3Oes = 0x93C2,
    CompressedRgbaAstc4x4x4Oes = 0x93C3,
    CompressedRgbaAstc5x4x4Oes = 0x93C4,
    CompressedRgbaAstc5x5x4Oes = 0x93C5,
    CompressedRgbaAstc5x5x5Oes = 0x93C6,
    CompressedRgbaAstc6x5x5Oes = 0x93C7,
    CompressedRgbaAstc6x6x5Oes = 0x93C8,
    CompressedRgbaAstc6x6x6Oes = 0x93C9,
    CompressedSrgb8Alpha8Astc4x4 = 0x93D0,
    CompressedSrgb8Alpha8Astc4x4Khr = 0x93D0,
    CompressedSrgb8Alpha8Astc5x4 = 0x93D1,
    CompressedSrgb8Alpha8Astc5x4Khr = 0x93D1,
    CompressedSrgb8Alpha8Astc5x5 = 0x93D2,
    CompressedSrgb8Alpha8Astc5x5Khr = 0x93D2,
    CompressedSrgb8Alpha8Astc6x5 = 0x93D3,
    CompressedSrgb8Alpha8Astc6x5Khr = 0x93D3,
    CompressedSrgb8Alpha8Astc6x6 = 0x93D4,
    CompressedSrgb8Alpha8Astc6x6Khr = 0x93D4,
    CompressedSrgb8Alpha8Astc8x5 = 0x93D5,
    CompressedSrgb8Alpha8Astc8x5Khr = 0x93D5,
    CompressedSrgb8Alpha8Astc8x6 = 0x93D6,
    CompressedSrgb8Alpha8Astc8x6Khr = 0x93D6,
    CompressedSrgb8Alpha8Astc8x8 = 0x93D7,
    CompressedSrgb8Alpha8Astc8x8Khr = 0x93D7,
    CompressedSrgb8Alpha8Astc10x5 = 0x93D8,
    CompressedSrgb8Alpha8Astc10x5Khr = 0x93D8,
    CompressedSrgb8Alpha8Astc10x6 = 0x93D9,
    CompressedSrgb8Alpha8Astc10x6Khr = 0x93D9,
    CompressedSrgb8Alpha8Astc10x8 = 0x93DA,
    CompressedSrgb8Alpha8Astc10x8Khr = 0x93DA,
    CompressedSrgb8Alpha8Astc10x10 = 0x93DB,
    CompressedSrgb8Alpha8Astc10x10Khr = 0x93DB,
    CompressedSrgb8Alpha8Astc12x10 = 0x93DC,
    CompressedSrgb8Alpha8Astc12x10Khr = 0x93DC,
    CompressedSrgb8Alpha8Astc12x12 = 0x93DD,
    CompressedSrgb8Alpha8Astc12x12Khr = 0x93DD,
    CompressedSrgb8Alpha8Astc3x3x3Oes = 0x93E0,
    CompressedSrgb8Alpha8Astc4x3x3Oes = 0x93E1,
    CompressedSrgb8Alpha8Astc4x4x3Oes = 0x93E2,
    CompressedSrgb8Alpha8Astc4x4x4Oes = 0x93E3,
    CompressedSrgb8Alpha8Astc5x4x4Oes = 0x93E4,
    CompressedSrgb8Alpha8Astc5x5x4Oes = 0x93E5,
    CompressedSrgb8Alpha8Astc5x5x5Oes = 0x93E6,
    CompressedSrgb8Alpha8Astc6x5x5Oes = 0x93E7,
    CompressedSrgb8Alpha8Astc6x6x5Oes = 0x93E8,
    CompressedSrgb8Alpha8Astc6x6x6Oes = 0x93E9,
}

public enum DepthStencilTextureMode
{
    StencilIndex = 0x1901,
    DepthComponent = 0x1902,
}

public enum CombinerComponentUsageNV
{
    Blue = 0x1905,
    Alpha = 0x1906,
    Rgb = 0x1907,
}

public enum CombinerPortionNV
{
    Alpha = 0x1906,
    Rgb = 0x1907,
}

public enum PolygonMode
{
    Point = 0x1B00,
    Line = 0x1B01,
    Fill = 0x1B02,
}

public enum MeshMode1
{
    Point = 0x1B00,
    Line = 0x1B01,
}

public enum MeshMode2
{
    Point = 0x1B00,
    Line = 0x1B01,
    Fill = 0x1B02,
}

public enum EvalMapsModeNV
{
    FillNv = 0x1B02,
}

public enum RenderingMode
{
    Render = 0x1C00,
    Feedback = 0x1C01,
    Select = 0x1C02,
}

public enum ShadingModel
{
    Flat = 0x1D00,
    Smooth = 0x1D01,
}

public enum StringName
{
    Vendor = 0x1F00,
    Renderer = 0x1F01,
    Version = 0x1F02,
    Extensions = 0x1F03,
    ShadingLanguageVersion = 0x8B8C,
}

public enum TextureCoordName
{
    S = 0x2000,
    T = 0x2001,
    R = 0x2002,
    Q = 0x2003,
}

public enum TextureEnvParameter
{
    TextureEnvMode = 0x2200,
    TextureEnvColor = 0x2201,
}

public enum TextureEnvTarget
{
    TextureEnv = 0x2300,
}

public enum TextureGenMode
{
    EyeLinear = 0x2400,
    ObjectLinear = 0x2401,
    SphereMap = 0x2402,
    EyeDistanceToPointSgis = 0x81F0,
    ObjectDistanceToPointSgis = 0x81F1,
    EyeDistanceToLineSgis = 0x81F2,
    ObjectDistanceToLineSgis = 0x81F3,
}

public enum TextureGenParameter
{
    TextureGenMode = 0x2500,
    ObjectPlane = 0x2501,
    EyePlane = 0x2502,
    EyePointSgis = 0x81F4,
    ObjectPointSgis = 0x81F5,
    EyeLineSgis = 0x81F6,
    ObjectLineSgis = 0x81F7,
}

public enum BlitFramebufferFilter
{
    Nearest = 0x2600,
    Linear = 0x2601,
}

public enum TextureMagFilter
{
    Nearest = 0x2600,
    Linear = 0x2601,
    LinearDetailSgis = 0x8097,
    LinearDetailAlphaSgis = 0x8098,
    LinearDetailColorSgis = 0x8099,
    LinearSharpenSgis = 0x80AD,
    LinearSharpenAlphaSgis = 0x80AE,
    LinearSharpenColorSgis = 0x80AF,
    Filter4Sgis = 0x8146,
    PixelTexGenQCeilingSgix = 0x8184,
    PixelTexGenQRoundSgix = 0x8185,
    PixelTexGenQFloorSgix = 0x8186,
}

public enum TextureMinFilter
{
    Nearest = 0x2600,
    Linear = 0x2601,
    NearestMipmapNearest = 0x2700,
    LinearMipmapNearest = 0x2701,
    NearestMipmapLinear = 0x2702,
    LinearMipmapLinear = 0x2703,
    Filter4Sgis = 0x8146,
    LinearClipmapLinearSgix = 0x8170,
    PixelTexGenQCeilingSgix = 0x8184,
    PixelTexGenQRoundSgix = 0x8185,
    PixelTexGenQFloorSgix = 0x8186,
    NearestClipmapNearestSgix = 0x844D,
    NearestClipmapLinearSgix = 0x844E,
    LinearClipmapNearestSgix = 0x844F,
}

public enum TextureWrapMode
{
    LinearMipmapLinear = 0x2703,
    Clamp = 0x2900,
    Repeat = 0x2901,
    ClampToBorder = 0x812D,
    ClampToBorderArb = 0x812D,
    ClampToBorderNv = 0x812D,
    ClampToBorderSgis = 0x812D,
    ClampToEdge = 0x812F,
    ClampToEdgeSgis = 0x812F,
    MirroredRepeat = 0x8370,
}

public enum SamplerParameterI
{
    TextureMagFilter = 0x2800,
    TextureMinFilter = 0x2801,
    TextureWrapS = 0x2802,
    TextureWrapT = 0x2803,
    TextureWrapR = 0x8072,
    TextureCompareMode = 0x884C,
    TextureCompareFunc = 0x884D,
    TextureUnnormalizedCoordinatesArm = 0x8F6A,
}

public enum SizedInternalFormat
{
    R3G3B2 = 0x2A10,
    Alpha4 = 0x803B,
    Alpha4Ext = 0x803B,
    Alpha8 = 0x803C,
    Alpha8Ext = 0x803C,
    Alpha8Oes = 0x803C,
    Alpha12 = 0x803D,
    Alpha12Ext = 0x803D,
    Alpha16 = 0x803E,
    Alpha16Ext = 0x803E,
    Luminance4 = 0x803F,
    Luminance4Ext = 0x803F,
    Luminance8 = 0x8040,
    Luminance8Ext = 0x8040,
    Luminance8Oes = 0x8040,
    Luminance12 = 0x8041,
    Luminance12Ext = 0x8041,
    Luminance16 = 0x8042,
    Luminance16Ext = 0x8042,
    Luminance4Alpha4 = 0x8043,
    Luminance4Alpha4Ext = 0x8043,
    Luminance4Alpha4Oes = 0x8043,
    Luminance6Alpha2 = 0x8044,
    Luminance6Alpha2Ext = 0x8044,
    Luminance8Alpha8 = 0x8045,
    Luminance8Alpha8Ext = 0x8045,
    Luminance8Alpha8Oes = 0x8045,
    Luminance12Alpha4 = 0x8046,
    Luminance12Alpha4Ext = 0x8046,
    Luminance12Alpha12 = 0x8047,
    Luminance12Alpha12Ext = 0x8047,
    Luminance16Alpha16 = 0x8048,
    Luminance16Alpha16Ext = 0x8048,
    Intensity4 = 0x804A,
    Intensity4Ext = 0x804A,
    Intensity8 = 0x804B,
    Intensity8Ext = 0x804B,
    Intensity12 = 0x804C,
    Intensity12Ext = 0x804C,
    Intensity16 = 0x804D,
    Intensity16Ext = 0x804D,
    Rgb2Ext = 0x804E,
    Rgb4 = 0x804F,
    Rgb4Ext = 0x804F,
    Rgb5 = 0x8050,
    Rgb5Ext = 0x8050,
    Rgb8 = 0x8051,
    Rgb8Ext = 0x8051,
    Rgb8Oes = 0x8051,
    Rgb10 = 0x8052,
    Rgb10Ext = 0x8052,
    Rgb12 = 0x8053,
    Rgb12Ext = 0x8053,
    Rgb16 = 0x8054,
    Rgb16Ext = 0x8054,
    Rgba2 = 0x8055,
    Rgba2Ext = 0x8055,
    Rgba4 = 0x8056,
    Rgba4Ext = 0x8056,
    Rgba4Oes = 0x8056,
    Rgb5A1 = 0x8057,
    Rgb5A1Ext = 0x8057,
    Rgb5A1Oes = 0x8057,
    Rgba8 = 0x8058,
    Rgba8Ext = 0x8058,
    Rgba8Oes = 0x8058,
    Rgb10A2 = 0x8059,
    Rgb10A2Ext = 0x8059,
    Rgba12 = 0x805A,
    Rgba12Ext = 0x805A,
    Rgba16 = 0x805B,
    Rgba16Ext = 0x805B,
    DepthComponent16 = 0x81A5,
    DepthComponent16Arb = 0x81A5,
    DepthComponent16Oes = 0x81A5,
    DepthComponent16Sgix = 0x81A5,
    DepthComponent24 = 0x81A6,
    DepthComponent24Arb = 0x81A6,
    DepthComponent24Oes = 0x81A6,
    DepthComponent24Sgix = 0x81A6,
    DepthComponent32 = 0x81A7,
    DepthComponent32Arb = 0x81A7,
    DepthComponent32Oes = 0x81A7,
    DepthComponent32Sgix = 0x81A7,
    R8 = 0x8229,
    R8Ext = 0x8229,
    R16 = 0x822A,
    R16Ext = 0x822A,
    Rg8 = 0x822B,
    Rg8Ext = 0x822B,
    Rg16 = 0x822C,
    Rg16Ext = 0x822C,
    R16f = 0x822D,
    R16fExt = 0x822D,
    R32f = 0x822E,
    R32fExt = 0x822E,
    Rg16f = 0x822F,
    Rg16fExt = 0x822F,
    Rg32f = 0x8230,
    Rg32fExt = 0x8230,
    R8i = 0x8231,
    R8ui = 0x8232,
    R16i = 0x8233,
    R16ui = 0x8234,
    R32i = 0x8235,
    R32ui = 0x8236,
    Rg8i = 0x8237,
    Rg8ui = 0x8238,
    Rg16i = 0x8239,
    Rg16ui = 0x823A,
    Rg32i = 0x823B,
    Rg32ui = 0x823C,
    CompressedRgbS3tcDxt1Ext = 0x83F0,
    CompressedRgbaS3tcDxt1Ext = 0x83F1,
    CompressedRgbaS3tcDxt3Angle = 0x83F2,
    CompressedRgbaS3tcDxt3Ext = 0x83F2,
    CompressedRgbaS3tcDxt5Angle = 0x83F3,
    CompressedRgbaS3tcDxt5Ext = 0x83F3,
    Rgba32f = 0x8814,
    Rgba32fArb = 0x8814,
    Rgba32fExt = 0x8814,
    Rgb32f = 0x8815,
    Rgb32fArb = 0x8815,
    Rgb32fExt = 0x8815,
    Rgba16f = 0x881A,
    Rgba16fArb = 0x881A,
    Rgba16fExt = 0x881A,
    Rgb16f = 0x881B,
    Rgb16fArb = 0x881B,
    Rgb16fExt = 0x881B,
    Depth24Stencil8 = 0x88F0,
    Depth24Stencil8Ext = 0x88F0,
    Depth24Stencil8Oes = 0x88F0,
    R11fG11fB10f = 0x8C3A,
    R11fG11fB10fApple = 0x8C3A,
    R11fG11fB10fExt = 0x8C3A,
    Rgb9E5 = 0x8C3D,
    Rgb9E5Apple = 0x8C3D,
    Rgb9E5Ext = 0x8C3D,
    Srgb8 = 0x8C41,
    Srgb8Ext = 0x8C41,
    Srgb8Nv = 0x8C41,
    Srgb8Alpha8 = 0x8C43,
    Srgb8Alpha8Ext = 0x8C43,
    CompressedSrgbS3tcDxt1Ext = 0x8C4C,
    CompressedSrgbS3tcDxt1Nv = 0x8C4C,
    CompressedSrgbAlphaS3tcDxt1Ext = 0x8C4D,
    CompressedSrgbAlphaS3tcDxt1Nv = 0x8C4D,
    CompressedSrgbAlphaS3tcDxt3Ext = 0x8C4E,
    CompressedSrgbAlphaS3tcDxt3Nv = 0x8C4E,
    CompressedSrgbAlphaS3tcDxt5Ext = 0x8C4F,
    CompressedSrgbAlphaS3tcDxt5Nv = 0x8C4F,
    DepthComponent32f = 0x8CAC,
    Depth32fStencil8 = 0x8CAD,
    StencilIndex1 = 0x8D46,
    StencilIndex1Ext = 0x8D46,
    StencilIndex1Oes = 0x8D46,
    StencilIndex4 = 0x8D47,
    StencilIndex4Ext = 0x8D47,
    StencilIndex4Oes = 0x8D47,
    StencilIndex8 = 0x8D48,
    StencilIndex8Ext = 0x8D48,
    StencilIndex8Oes = 0x8D48,
    StencilIndex16 = 0x8D49,
    StencilIndex16Ext = 0x8D49,
    Rgb565Oes = 0x8D62,
    Rgb565 = 0x8D62,
    Etc1Rgb8Oes = 0x8D64,
    Rgba32ui = 0x8D70,
    Rgba32uiExt = 0x8D70,
    Rgb32ui = 0x8D71,
    Rgb32uiExt = 0x8D71,
    Alpha32uiExt = 0x8D72,
    Intensity32uiExt = 0x8D73,
    Luminance32uiExt = 0x8D74,
    LuminanceAlpha32uiExt = 0x8D75,
    Rgba16ui = 0x8D76,
    Rgba16uiExt = 0x8D76,
    Rgb16ui = 0x8D77,
    Rgb16uiExt = 0x8D77,
    Alpha16uiExt = 0x8D78,
    Intensity16uiExt = 0x8D79,
    Luminance16uiExt = 0x8D7A,
    LuminanceAlpha16uiExt = 0x8D7B,
    Rgba8ui = 0x8D7C,
    Rgba8uiExt = 0x8D7C,
    Rgb8ui = 0x8D7D,
    Rgb8uiExt = 0x8D7D,
    Alpha8uiExt = 0x8D7E,
    Intensity8uiExt = 0x8D7F,
    Luminance8uiExt = 0x8D80,
    LuminanceAlpha8uiExt = 0x8D81,
    Rgba32i = 0x8D82,
    Rgba32iExt = 0x8D82,
    Rgb32i = 0x8D83,
    Rgb32iExt = 0x8D83,
    Alpha32iExt = 0x8D84,
    Intensity32iExt = 0x8D85,
    Luminance32iExt = 0x8D86,
    LuminanceAlpha32iExt = 0x8D87,
    Rgba16i = 0x8D88,
    Rgba16iExt = 0x8D88,
    Rgb16i = 0x8D89,
    Rgb16iExt = 0x8D89,
    Alpha16iExt = 0x8D8A,
    Intensity16iExt = 0x8D8B,
    Luminance16iExt = 0x8D8C,
    LuminanceAlpha16iExt = 0x8D8D,
    Rgba8i = 0x8D8E,
    Rgba8iExt = 0x8D8E,
    Rgb8i = 0x8D8F,
    Rgb8iExt = 0x8D8F,
    Alpha8iExt = 0x8D90,
    Intensity8iExt = 0x8D91,
    Luminance8iExt = 0x8D92,
    LuminanceAlpha8iExt = 0x8D93,
    DepthComponent32fNv = 0x8DAB,
    Depth32fStencil8Nv = 0x8DAC,
    CompressedRedRgtc1 = 0x8DBB,
    CompressedRedRgtc1Ext = 0x8DBB,
    CompressedSignedRedRgtc1 = 0x8DBC,
    CompressedSignedRedRgtc1Ext = 0x8DBC,
    CompressedRedGreenRgtc2Ext = 0x8DBD,
    CompressedRgRgtc2 = 0x8DBD,
    CompressedSignedRedGreenRgtc2Ext = 0x8DBE,
    CompressedSignedRgRgtc2 = 0x8DBE,
    CompressedRgbaBptcUnorm = 0x8E8C,
    CompressedRgbaBptcUnormArb = 0x8E8C,
    CompressedRgbaBptcUnormExt = 0x8E8C,
    CompressedSrgbAlphaBptcUnorm = 0x8E8D,
    CompressedSrgbAlphaBptcUnormArb = 0x8E8D,
    CompressedSrgbAlphaBptcUnormExt = 0x8E8D,
    CompressedRgbBptcSignedFloat = 0x8E8E,
    CompressedRgbBptcSignedFloatArb = 0x8E8E,
    CompressedRgbBptcSignedFloatExt = 0x8E8E,
    CompressedRgbBptcUnsignedFloat = 0x8E8F,
    CompressedRgbBptcUnsignedFloatArb = 0x8E8F,
    CompressedRgbBptcUnsignedFloatExt = 0x8E8F,
    R8Snorm = 0x8F94,
    Rg8Snorm = 0x8F95,
    Rgb8Snorm = 0x8F96,
    Rgba8Snorm = 0x8F97,
    R16Snorm = 0x8F98,
    R16SnormExt = 0x8F98,
    Rg16Snorm = 0x8F99,
    Rg16SnormExt = 0x8F99,
    Rgb16Snorm = 0x8F9A,
    Rgb16SnormExt = 0x8F9A,
    Rgba16Snorm = 0x8F9B,
    Rgba16SnormExt = 0x8F9B,
    Rgb10A2ui = 0x906F,
    CompressedR11Eac = 0x9270,
    CompressedR11EacOes = 0x9270,
    CompressedSignedR11Eac = 0x9271,
    CompressedSignedR11EacOes = 0x9271,
    CompressedRg11Eac = 0x9272,
    CompressedRg11EacOes = 0x9272,
    CompressedSignedRg11Eac = 0x9273,
    CompressedSignedRg11EacOes = 0x9273,
    CompressedRgb8Etc2 = 0x9274,
    CompressedRgb8Etc2Oes = 0x9274,
    CompressedSrgb8Etc2 = 0x9275,
    CompressedSrgb8Etc2Oes = 0x9275,
    CompressedRgb8PunchthroughAlpha1Etc2 = 0x9276,
    CompressedRgb8PunchthroughAlpha1Etc2Oes = 0x9276,
    CompressedSrgb8PunchthroughAlpha1Etc2 = 0x9277,
    CompressedSrgb8PunchthroughAlpha1Etc2Oes = 0x9277,
    CompressedRgba8Etc2Eac = 0x9278,
    CompressedRgba8Etc2EacOes = 0x9278,
    CompressedSrgb8Alpha8Etc2Eac = 0x9279,
    CompressedSrgb8Alpha8Etc2EacOes = 0x9279,
    CompressedRgbaAstc4x4 = 0x93B0,
    CompressedRgbaAstc4x4Khr = 0x93B0,
    CompressedRgbaAstc5x4 = 0x93B1,
    CompressedRgbaAstc5x4Khr = 0x93B1,
    CompressedRgbaAstc5x5 = 0x93B2,
    CompressedRgbaAstc5x5Khr = 0x93B2,
    CompressedRgbaAstc6x5 = 0x93B3,
    CompressedRgbaAstc6x5Khr = 0x93B3,
    CompressedRgbaAstc6x6 = 0x93B4,
    CompressedRgbaAstc6x6Khr = 0x93B4,
    CompressedRgbaAstc8x5 = 0x93B5,
    CompressedRgbaAstc8x5Khr = 0x93B5,
    CompressedRgbaAstc8x6 = 0x93B6,
    CompressedRgbaAstc8x6Khr = 0x93B6,
    CompressedRgbaAstc8x8 = 0x93B7,
    CompressedRgbaAstc8x8Khr = 0x93B7,
    CompressedRgbaAstc10x5 = 0x93B8,
    CompressedRgbaAstc10x5Khr = 0x93B8,
    CompressedRgbaAstc10x6 = 0x93B9,
    CompressedRgbaAstc10x6Khr = 0x93B9,
    CompressedRgbaAstc10x8 = 0x93BA,
    CompressedRgbaAstc10x8Khr = 0x93BA,
    CompressedRgbaAstc10x10 = 0x93BB,
    CompressedRgbaAstc10x10Khr = 0x93BB,
    CompressedRgbaAstc12x10 = 0x93BC,
    CompressedRgbaAstc12x10Khr = 0x93BC,
    CompressedRgbaAstc12x12 = 0x93BD,
    CompressedRgbaAstc12x12Khr = 0x93BD,
    CompressedRgbaAstc3x3x3Oes = 0x93C0,
    CompressedRgbaAstc4x3x3Oes = 0x93C1,
    CompressedRgbaAstc4x4x3Oes = 0x93C2,
    CompressedRgbaAstc4x4x4Oes = 0x93C3,
    CompressedRgbaAstc5x4x4Oes = 0x93C4,
    CompressedRgbaAstc5x5x4Oes = 0x93C5,
    CompressedRgbaAstc5x5x5Oes = 0x93C6,
    CompressedRgbaAstc6x5x5Oes = 0x93C7,
    CompressedRgbaAstc6x6x5Oes = 0x93C8,
    CompressedRgbaAstc6x6x6Oes = 0x93C9,
    CompressedSrgb8Alpha8Astc4x4 = 0x93D0,
    CompressedSrgb8Alpha8Astc4x4Khr = 0x93D0,
    CompressedSrgb8Alpha8Astc5x4 = 0x93D1,
    CompressedSrgb8Alpha8Astc5x4Khr = 0x93D1,
    CompressedSrgb8Alpha8Astc5x5 = 0x93D2,
    CompressedSrgb8Alpha8Astc5x5Khr = 0x93D2,
    CompressedSrgb8Alpha8Astc6x5 = 0x93D3,
    CompressedSrgb8Alpha8Astc6x5Khr = 0x93D3,
    CompressedSrgb8Alpha8Astc6x6 = 0x93D4,
    CompressedSrgb8Alpha8Astc6x6Khr = 0x93D4,
    CompressedSrgb8Alpha8Astc8x5 = 0x93D5,
    CompressedSrgb8Alpha8Astc8x5Khr = 0x93D5,
    CompressedSrgb8Alpha8Astc8x6 = 0x93D6,
    CompressedSrgb8Alpha8Astc8x6Khr = 0x93D6,
    CompressedSrgb8Alpha8Astc8x8 = 0x93D7,
    CompressedSrgb8Alpha8Astc8x8Khr = 0x93D7,
    CompressedSrgb8Alpha8Astc10x5 = 0x93D8,
    CompressedSrgb8Alpha8Astc10x5Khr = 0x93D8,
    CompressedSrgb8Alpha8Astc10x6 = 0x93D9,
    CompressedSrgb8Alpha8Astc10x6Khr = 0x93D9,
    CompressedSrgb8Alpha8Astc10x8 = 0x93DA,
    CompressedSrgb8Alpha8Astc10x8Khr = 0x93DA,
    CompressedSrgb8Alpha8Astc10x10 = 0x93DB,
    CompressedSrgb8Alpha8Astc10x10Khr = 0x93DB,
    CompressedSrgb8Alpha8Astc12x10 = 0x93DC,
    CompressedSrgb8Alpha8Astc12x10Khr = 0x93DC,
    CompressedSrgb8Alpha8Astc12x12 = 0x93DD,
    CompressedSrgb8Alpha8Astc12x12Khr = 0x93DD,
    CompressedSrgb8Alpha8Astc3x3x3Oes = 0x93E0,
    CompressedSrgb8Alpha8Astc4x3x3Oes = 0x93E1,
    CompressedSrgb8Alpha8Astc4x4x3Oes = 0x93E2,
    CompressedSrgb8Alpha8Astc4x4x4Oes = 0x93E3,
    CompressedSrgb8Alpha8Astc5x4x4Oes = 0x93E4,
    CompressedSrgb8Alpha8Astc5x5x4Oes = 0x93E5,
    CompressedSrgb8Alpha8Astc5x5x5Oes = 0x93E6,
    CompressedSrgb8Alpha8Astc6x5x5Oes = 0x93E7,
    CompressedSrgb8Alpha8Astc6x6x5Oes = 0x93E8,
    CompressedSrgb8Alpha8Astc6x6x6Oes = 0x93E9,
}

public enum InterleavedArrayFormat
{
    V2f = 0x2A20,
    V3f = 0x2A21,
    C4ubV2f = 0x2A22,
    C4ubV3f = 0x2A23,
    C3fV3f = 0x2A24,
    N3fV3f = 0x2A25,
    C4fN3fV3f = 0x2A26,
    T2fV3f = 0x2A27,
    T4fV4f = 0x2A28,
    T2fC4ubV3f = 0x2A29,
    T2fC3fV3f = 0x2A2A,
    T2fN3fV3f = 0x2A2B,
    T2fC4fN3fV3f = 0x2A2C,
    T4fC4fN3fV4f = 0x2A2D,
}

public enum ClipPlaneName
{
    ClipPlane0 = 0x3000,
    ClipDistance0 = 0x3000,
    ClipPlane1 = 0x3001,
    ClipDistance1 = 0x3001,
    ClipPlane2 = 0x3002,
    ClipDistance2 = 0x3002,
    ClipPlane3 = 0x3003,
    ClipDistance3 = 0x3003,
    ClipPlane4 = 0x3004,
    ClipDistance4 = 0x3004,
    ClipPlane5 = 0x3005,
    ClipDistance5 = 0x3005,
    ClipDistance6 = 0x3006,
    ClipDistance7 = 0x3007,
}

public enum LightName
{
    Light0 = 0x4000,
    Light1 = 0x4001,
    Light2 = 0x4002,
    Light3 = 0x4003,
    Light4 = 0x4004,
    Light5 = 0x4005,
    Light6 = 0x4006,
    Light7 = 0x4007,
    FragmentLight0Sgix = 0x840C,
    FragmentLight1Sgix = 0x840D,
    FragmentLight2Sgix = 0x840E,
    FragmentLight3Sgix = 0x840F,
    FragmentLight4Sgix = 0x8410,
    FragmentLight5Sgix = 0x8411,
    FragmentLight6Sgix = 0x8412,
    FragmentLight7Sgix = 0x8413,
}

public enum BlendEquationModeEXT
{
    FuncAdd = 0x8006,
    FuncAddExt = 0x8006,
    Min = 0x8007,
    MinExt = 0x8007,
    Max = 0x8008,
    MaxExt = 0x8008,
    FuncSubtract = 0x800A,
    FuncSubtractExt = 0x800A,
    FuncReverseSubtract = 0x800B,
    FuncReverseSubtractExt = 0x800B,
    AlphaMinSgix = 0x8320,
    AlphaMaxSgix = 0x8321,
}

public enum ConvolutionTarget
{
    Convolution1d = 0x8010,
    Convolution2d = 0x8011,
}

public enum ConvolutionTargetEXT
{
    Convolution1d = 0x8010,
    Convolution1dExt = 0x8010,
    Convolution2d = 0x8011,
    Convolution2dExt = 0x8011,
}

public enum SeparableTarget
{
    Separable2d = 0x8012,
}

public enum SeparableTargetEXT
{
    Separable2d = 0x8012,
    Separable2dExt = 0x8012,
}

public enum GetConvolutionParameter
{
    ConvolutionBorderMode = 0x8013,
    ConvolutionBorderModeExt = 0x8013,
    ConvolutionFilterScale = 0x8014,
    ConvolutionFilterScaleExt = 0x8014,
    ConvolutionFilterBias = 0x8015,
    ConvolutionFilterBiasExt = 0x8015,
    ConvolutionFormat = 0x8017,
    ConvolutionFormatExt = 0x8017,
    ConvolutionWidth = 0x8018,
    ConvolutionWidthExt = 0x8018,
    ConvolutionHeight = 0x8019,
    ConvolutionHeightExt = 0x8019,
    MaxConvolutionWidth = 0x801A,
    MaxConvolutionWidthExt = 0x801A,
    MaxConvolutionHeight = 0x801B,
    MaxConvolutionHeightExt = 0x801B,
    ConvolutionBorderColor = 0x8154,
}

public enum ConvolutionParameterEXT
{
    ConvolutionBorderMode = 0x8013,
    ConvolutionBorderModeExt = 0x8013,
    ConvolutionFilterScale = 0x8014,
    ConvolutionFilterScaleExt = 0x8014,
    ConvolutionFilterBias = 0x8015,
    ConvolutionFilterBiasExt = 0x8015,
}

public enum ConvolutionBorderModeEXT
{
    Reduce = 0x8016,
    ReduceExt = 0x8016,
}

public enum HistogramTarget
{
    Histogram = 0x8024,
    ProxyHistogram = 0x8025,
}

public enum HistogramTargetEXT
{
    Histogram = 0x8024,
    HistogramExt = 0x8024,
    ProxyHistogram = 0x8025,
    ProxyHistogramExt = 0x8025,
}

public enum GetHistogramParameterPNameEXT
{
    HistogramWidth = 0x8026,
    HistogramWidthExt = 0x8026,
    HistogramFormat = 0x8027,
    HistogramFormatExt = 0x8027,
    HistogramRedSize = 0x8028,
    HistogramRedSizeExt = 0x8028,
    HistogramGreenSize = 0x8029,
    HistogramGreenSizeExt = 0x8029,
    HistogramBlueSize = 0x802A,
    HistogramBlueSizeExt = 0x802A,
    HistogramAlphaSize = 0x802B,
    HistogramAlphaSizeExt = 0x802B,
    HistogramLuminanceSize = 0x802C,
    HistogramLuminanceSizeExt = 0x802C,
    HistogramSink = 0x802D,
    HistogramSinkExt = 0x802D,
}

public enum MinmaxTarget
{
    Minmax = 0x802E,
}

public enum MinmaxTargetEXT
{
    Minmax = 0x802E,
    MinmaxExt = 0x802E,
}

public enum GetMinmaxParameterPNameEXT
{
    MinmaxFormat = 0x802F,
    MinmaxFormatExt = 0x802F,
    MinmaxSink = 0x8030,
    MinmaxSinkExt = 0x8030,
}

public enum SamplePatternSGIS
{
    _1PassExt = 0x80A1,
    _1PassSgis = 0x80A1,
    _2Pass0Ext = 0x80A2,
    _2Pass0Sgis = 0x80A2,
    _2Pass1Ext = 0x80A3,
    _2Pass1Sgis = 0x80A3,
    _4Pass0Ext = 0x80A4,
    _4Pass0Sgis = 0x80A4,
    _4Pass1Ext = 0x80A5,
    _4Pass1Sgis = 0x80A5,
    _4Pass2Ext = 0x80A6,
    _4Pass2Sgis = 0x80A6,
    _4Pass3Ext = 0x80A7,
    _4Pass3Sgis = 0x80A7,
}

public enum SamplePatternEXT
{
    _1PassExt = 0x80A1,
    _2Pass0Ext = 0x80A2,
    _2Pass1Ext = 0x80A3,
    _4Pass0Ext = 0x80A4,
    _4Pass1Ext = 0x80A5,
    _4Pass2Ext = 0x80A6,
    _4Pass3Ext = 0x80A7,
}

public enum InternalFormatPName
{
    Samples = 0x80A9,
    GenerateMipmap = 0x8191,
    InternalformatSupported = 0x826F,
    InternalformatPreferred = 0x8270,
    InternalformatRedSize = 0x8271,
    InternalformatGreenSize = 0x8272,
    InternalformatBlueSize = 0x8273,
    InternalformatAlphaSize = 0x8274,
    InternalformatDepthSize = 0x8275,
    InternalformatStencilSize = 0x8276,
    InternalformatSharedSize = 0x8277,
    InternalformatRedType = 0x8278,
    InternalformatGreenType = 0x8279,
    InternalformatBlueType = 0x827A,
    InternalformatAlphaType = 0x827B,
    InternalformatDepthType = 0x827C,
    InternalformatStencilType = 0x827D,
    MaxWidth = 0x827E,
    MaxHeight = 0x827F,
    MaxDepth = 0x8280,
    MaxLayers = 0x8281,
    ColorComponents = 0x8283,
    ColorRenderable = 0x8286,
    DepthRenderable = 0x8287,
    StencilRenderable = 0x8288,
    FramebufferRenderable = 0x8289,
    FramebufferRenderableLayered = 0x828A,
    FramebufferBlend = 0x828B,
    ReadPixels = 0x828C,
    ReadPixelsFormat = 0x828D,
    ReadPixelsType = 0x828E,
    TextureImageFormat = 0x828F,
    TextureImageType = 0x8290,
    GetTextureImageFormat = 0x8291,
    GetTextureImageType = 0x8292,
    Mipmap = 0x8293,
    AutoGenerateMipmap = 0x8295,
    ColorEncoding = 0x8296,
    SrgbRead = 0x8297,
    SrgbWrite = 0x8298,
    Filter = 0x829A,
    VertexTexture = 0x829B,
    TessControlTexture = 0x829C,
    TessEvaluationTexture = 0x829D,
    GeometryTexture = 0x829E,
    FragmentTexture = 0x829F,
    ComputeTexture = 0x82A0,
    TextureShadow = 0x82A1,
    TextureGather = 0x82A2,
    TextureGatherShadow = 0x82A3,
    ShaderImageLoad = 0x82A4,
    ShaderImageStore = 0x82A5,
    ShaderImageAtomic = 0x82A6,
    ImageTexelSize = 0x82A7,
    ImageCompatibilityClass = 0x82A8,
    ImagePixelFormat = 0x82A9,
    ImagePixelType = 0x82AA,
    SimultaneousTextureAndDepthTest = 0x82AC,
    SimultaneousTextureAndStencilTest = 0x82AD,
    SimultaneousTextureAndDepthWrite = 0x82AE,
    SimultaneousTextureAndStencilWrite = 0x82AF,
    TextureCompressedBlockWidth = 0x82B1,
    TextureCompressedBlockHeight = 0x82B2,
    TextureCompressedBlockSize = 0x82B3,
    ClearBuffer = 0x82B4,
    TextureView = 0x82B5,
    ViewCompatibilityClass = 0x82B6,
    TextureCompressed = 0x86A1,
    NumSurfaceCompressionFixedRatesExt = 0x8F6E,
    ImageFormatCompatibilityType = 0x90C7,
    ClearTexture = 0x9365,
    NumSampleCounts = 0x9380,
}

public enum ColorTableTargetSGI
{
    TextureColorTableSgi = 0x80BC,
    ProxyTextureColorTableSgi = 0x80BD,
    ColorTable = 0x80D0,
    ColorTableSgi = 0x80D0,
    PostConvolutionColorTable = 0x80D1,
    PostConvolutionColorTableSgi = 0x80D1,
    PostColorMatrixColorTable = 0x80D2,
    PostColorMatrixColorTableSgi = 0x80D2,
    ProxyColorTable = 0x80D3,
    ProxyColorTableSgi = 0x80D3,
    ProxyPostConvolutionColorTable = 0x80D4,
    ProxyPostConvolutionColorTableSgi = 0x80D4,
    ProxyPostColorMatrixColorTable = 0x80D5,
    ProxyPostColorMatrixColorTableSgi = 0x80D5,
}

public enum ColorTableTarget
{
    ColorTable = 0x80D0,
    PostConvolutionColorTable = 0x80D1,
    PostColorMatrixColorTable = 0x80D2,
    ProxyColorTable = 0x80D3,
    ProxyPostConvolutionColorTable = 0x80D4,
    ProxyPostColorMatrixColorTable = 0x80D5,
}

public enum GetColorTableParameterPNameSGI
{
    ColorTableScale = 0x80D6,
    ColorTableScaleSgi = 0x80D6,
    ColorTableBias = 0x80D7,
    ColorTableBiasSgi = 0x80D7,
    ColorTableFormat = 0x80D8,
    ColorTableFormatSgi = 0x80D8,
    ColorTableWidth = 0x80D9,
    ColorTableWidthSgi = 0x80D9,
    ColorTableRedSize = 0x80DA,
    ColorTableRedSizeSgi = 0x80DA,
    ColorTableGreenSize = 0x80DB,
    ColorTableGreenSizeSgi = 0x80DB,
    ColorTableBlueSize = 0x80DC,
    ColorTableBlueSizeSgi = 0x80DC,
    ColorTableAlphaSize = 0x80DD,
    ColorTableAlphaSizeSgi = 0x80DD,
    ColorTableLuminanceSize = 0x80DE,
    ColorTableLuminanceSizeSgi = 0x80DE,
    ColorTableIntensitySize = 0x80DF,
    ColorTableIntensitySizeSgi = 0x80DF,
}

public enum ColorTableParameterPNameSGI
{
    ColorTableScale = 0x80D6,
    ColorTableScaleSgi = 0x80D6,
    ColorTableBias = 0x80D7,
    ColorTableBiasSgi = 0x80D7,
    ColorTableFormat = 0x80D8,
    ColorTableFormatSgi = 0x80D8,
    ColorTableWidth = 0x80D9,
    ColorTableWidthSgi = 0x80D9,
    ColorTableRedSize = 0x80DA,
    ColorTableRedSizeSgi = 0x80DA,
    ColorTableGreenSize = 0x80DB,
    ColorTableGreenSizeSgi = 0x80DB,
    ColorTableBlueSize = 0x80DC,
    ColorTableBlueSizeSgi = 0x80DC,
    ColorTableAlphaSize = 0x80DD,
    ColorTableAlphaSizeSgi = 0x80DD,
    ColorTableLuminanceSize = 0x80DE,
    ColorTableLuminanceSizeSgi = 0x80DE,
    ColorTableIntensitySize = 0x80DF,
    ColorTableIntensitySizeSgi = 0x80DF,
}

public enum GetColorTableParameterPName
{
    ColorTableScale = 0x80D6,
    ColorTableBias = 0x80D7,
    ColorTableFormat = 0x80D8,
    ColorTableWidth = 0x80D9,
    ColorTableRedSize = 0x80DA,
    ColorTableGreenSize = 0x80DB,
    ColorTableBlueSize = 0x80DC,
    ColorTableAlphaSize = 0x80DD,
    ColorTableLuminanceSize = 0x80DE,
    ColorTableIntensitySize = 0x80DF,
}

public enum ColorTableParameterPName
{
    ColorTableScale = 0x80D6,
    ColorTableBias = 0x80D7,
    ColorTableFormat = 0x80D8,
    ColorTableWidth = 0x80D9,
    ColorTableRedSize = 0x80DA,
    ColorTableGreenSize = 0x80DB,
    ColorTableBlueSize = 0x80DC,
    ColorTableAlphaSize = 0x80DD,
    ColorTableLuminanceSize = 0x80DE,
    ColorTableIntensitySize = 0x80DF,
}

public enum BufferTargetARB
{
    ParameterBuffer = 0x80EE,
    ArrayBuffer = 0x8892,
    ElementArrayBuffer = 0x8893,
    PixelPackBuffer = 0x88EB,
    PixelUnpackBuffer = 0x88EC,
    UniformBuffer = 0x8A11,
    TextureBuffer = 0x8C2A,
    TransformFeedbackBuffer = 0x8C8E,
    CopyReadBuffer = 0x8F36,
    CopyWriteBuffer = 0x8F37,
    DrawIndirectBuffer = 0x8F3F,
    ShaderStorageBuffer = 0x90D2,
    DispatchIndirectBuffer = 0x90EE,
    QueryBuffer = 0x9192,
    AtomicCounterBuffer = 0x92C0,
}

public enum PointParameterNameSGIS
{
    PointSizeMin = 0x8126,
    PointSizeMinArb = 0x8126,
    PointSizeMinExt = 0x8126,
    PointSizeMinSgis = 0x8126,
    PointSizeMax = 0x8127,
    PointSizeMaxArb = 0x8127,
    PointSizeMaxExt = 0x8127,
    PointSizeMaxSgis = 0x8127,
    PointFadeThresholdSize = 0x8128,
    PointFadeThresholdSizeArb = 0x8128,
    PointFadeThresholdSizeExt = 0x8128,
    PointFadeThresholdSizeSgis = 0x8128,
    DistanceAttenuationExt = 0x8129,
    DistanceAttenuationSgis = 0x8129,
    PointDistanceAttenuation = 0x8129,
    PointDistanceAttenuationArb = 0x8129,
}

public enum PointParameterNameARB
{
    PointSizeMinExt = 0x8126,
    PointSizeMaxExt = 0x8127,
    PointFadeThresholdSize = 0x8128,
    PointFadeThresholdSizeExt = 0x8128,
}

public enum TextureFilterSGIS
{
    Filter4Sgis = 0x8146,
}

public enum TextureFilterFuncSGIS
{
    Filter4Sgis = 0x8146,
}

public enum SpriteParameterNameSGIX
{
    SpriteModeSgix = 0x8149,
}

public enum ImageTransformPNameHP
{
    ImageScaleXHp = 0x8155,
    ImageScaleYHp = 0x8156,
    ImageTranslateXHp = 0x8157,
    ImageTranslateYHp = 0x8158,
    ImageRotateAngleHp = 0x8159,
    ImageRotateOriginXHp = 0x815A,
    ImageRotateOriginYHp = 0x815B,
    ImageMagFilterHp = 0x815C,
    ImageMinFilterHp = 0x815D,
    ImageCubicWeightHp = 0x815E,
}

public enum ImageTransformTargetHP
{
    ImageTransform2dHp = 0x8161,
}

public enum ListParameterName
{
    ListPrioritySgix = 0x8182,
}

public enum PixelTexGenModeSGIX
{
    PixelTexGenQCeilingSgix = 0x8184,
    PixelTexGenQRoundSgix = 0x8185,
    PixelTexGenQFloorSgix = 0x8186,
    PixelTexGenAlphaLsSgix = 0x8189,
    PixelTexGenAlphaMsSgix = 0x818A,
}

public enum FfdTargetSGIX
{
    GeometryDeformationSgix = 0x8194,
    TextureDeformationSgix = 0x8195,
}

public enum CullParameterEXT
{
    CullVertexEyePositionExt = 0x81AB,
    CullVertexObjectPositionExt = 0x81AC,
}

public enum LightModelColorControl
{
    SingleColor = 0x81F9,
    SingleColorExt = 0x81F9,
    SeparateSpecularColor = 0x81FA,
    SeparateSpecularColorExt = 0x81FA,
}

public enum ProgramTarget
{
    TextFragmentShaderAti = 0x8200,
    VertexProgramArb = 0x8620,
    FragmentProgramArb = 0x8804,
    TessControlProgramNv = 0x891E,
    TessEvaluationProgramNv = 0x891F,
    GeometryProgramNv = 0x8C26,
    ComputeProgramNv = 0x90FB,
}

public enum FramebufferAttachmentParameterName
{
    FramebufferAttachmentColorEncoding = 0x8210,
    FramebufferAttachmentColorEncodingExt = 0x8210,
    FramebufferAttachmentComponentType = 0x8211,
    FramebufferAttachmentComponentTypeExt = 0x8211,
    FramebufferAttachmentRedSize = 0x8212,
    FramebufferAttachmentGreenSize = 0x8213,
    FramebufferAttachmentBlueSize = 0x8214,
    FramebufferAttachmentAlphaSize = 0x8215,
    FramebufferAttachmentDepthSize = 0x8216,
    FramebufferAttachmentStencilSize = 0x8217,
    FramebufferAttachmentObjectType = 0x8CD0,
    FramebufferAttachmentObjectTypeExt = 0x8CD0,
    FramebufferAttachmentObjectTypeOes = 0x8CD0,
    FramebufferAttachmentObjectName = 0x8CD1,
    FramebufferAttachmentObjectNameExt = 0x8CD1,
    FramebufferAttachmentObjectNameOes = 0x8CD1,
    FramebufferAttachmentTextureLevel = 0x8CD2,
    FramebufferAttachmentTextureLevelExt = 0x8CD2,
    FramebufferAttachmentTextureLevelOes = 0x8CD2,
    FramebufferAttachmentTextureCubeMapFace = 0x8CD3,
    FramebufferAttachmentTextureCubeMapFaceExt = 0x8CD3,
    FramebufferAttachmentTextureCubeMapFaceOes = 0x8CD3,
    FramebufferAttachmentTexture3dZoffsetExt = 0x8CD4,
    FramebufferAttachmentTexture3dZoffsetOes = 0x8CD4,
    FramebufferAttachmentTextureLayer = 0x8CD4,
    FramebufferAttachmentTextureLayerExt = 0x8CD4,
    FramebufferAttachmentTextureSamplesExt = 0x8D6C,
    FramebufferAttachmentLayered = 0x8DA7,
    FramebufferAttachmentLayeredArb = 0x8DA7,
    FramebufferAttachmentLayeredExt = 0x8DA7,
    FramebufferAttachmentLayeredOes = 0x8DA7,
    FramebufferAttachmentTextureScaleImg = 0x913F,
    FramebufferAttachmentTextureNumViewsOvr = 0x9630,
    FramebufferAttachmentTextureBaseViewIndexOvr = 0x9632,
}

public enum FramebufferStatus
{
    FramebufferUndefined = 0x8219,
    FramebufferComplete = 0x8CD5,
    FramebufferIncompleteAttachment = 0x8CD6,
    FramebufferIncompleteMissingAttachment = 0x8CD7,
    FramebufferIncompleteDrawBuffer = 0x8CDB,
    FramebufferIncompleteReadBuffer = 0x8CDC,
    FramebufferUnsupported = 0x8CDD,
    FramebufferIncompleteMultisample = 0x8D56,
    FramebufferIncompleteLayerTargets = 0x8DA8,
}

public enum FramebufferAttachment
{
    DepthStencilAttachment = 0x821A,
    ColorAttachment0 = 0x8CE0,
    ColorAttachment1 = 0x8CE1,
    ColorAttachment2 = 0x8CE2,
    ColorAttachment3 = 0x8CE3,
    ColorAttachment4 = 0x8CE4,
    ColorAttachment5 = 0x8CE5,
    ColorAttachment6 = 0x8CE6,
    ColorAttachment7 = 0x8CE7,
    ColorAttachment8 = 0x8CE8,
    ColorAttachment9 = 0x8CE9,
    ColorAttachment10 = 0x8CEA,
    ColorAttachment11 = 0x8CEB,
    ColorAttachment12 = 0x8CEC,
    ColorAttachment13 = 0x8CED,
    ColorAttachment14 = 0x8CEE,
    ColorAttachment15 = 0x8CEF,
    ColorAttachment16 = 0x8CF0,
    ColorAttachment17 = 0x8CF1,
    ColorAttachment18 = 0x8CF2,
    ColorAttachment19 = 0x8CF3,
    ColorAttachment20 = 0x8CF4,
    ColorAttachment21 = 0x8CF5,
    ColorAttachment22 = 0x8CF6,
    ColorAttachment23 = 0x8CF7,
    ColorAttachment24 = 0x8CF8,
    ColorAttachment25 = 0x8CF9,
    ColorAttachment26 = 0x8CFA,
    ColorAttachment27 = 0x8CFB,
    ColorAttachment28 = 0x8CFC,
    ColorAttachment29 = 0x8CFD,
    ColorAttachment30 = 0x8CFE,
    ColorAttachment31 = 0x8CFF,
    DepthAttachment = 0x8D00,
    StencilAttachment = 0x8D20,
}

public enum VertexBufferObjectParameter
{
    BufferImmutableStorage = 0x821F,
    BufferStorageFlags = 0x8220,
    BufferSize = 0x8764,
    BufferUsage = 0x8765,
    BufferAccess = 0x88BB,
    BufferMapped = 0x88BC,
    BufferAccessFlags = 0x911F,
    BufferMapLength = 0x9120,
    BufferMapOffset = 0x9121,
}

public enum BufferPNameARB
{
    BufferImmutableStorage = 0x821F,
    BufferStorageFlags = 0x8220,
    BufferSize = 0x8764,
    BufferSizeArb = 0x8764,
    BufferUsage = 0x8765,
    BufferUsageArb = 0x8765,
    BufferAccess = 0x88BB,
    BufferAccessArb = 0x88BB,
    BufferMapped = 0x88BC,
    BufferMappedArb = 0x88BC,
    BufferAccessFlags = 0x911F,
    BufferMapLength = 0x9120,
    BufferMapOffset = 0x9121,
}

public enum ProgramParameterPName
{
    ProgramBinaryRetrievableHint = 0x8257,
    ProgramSeparable = 0x8258,
}

public enum PipelineParameterName
{
    ActiveProgram = 0x8259,
    FragmentShader = 0x8B30,
    VertexShader = 0x8B31,
    InfoLogLength = 0x8B84,
    GeometryShader = 0x8DD9,
    TessEvaluationShader = 0x8E87,
    TessControlShader = 0x8E88,
}

public enum ProgramPropertyARB
{
    ComputeWorkGroupSize = 0x8267,
    ProgramBinaryLength = 0x8741,
    GeometryVerticesOut = 0x8916,
    GeometryInputType = 0x8917,
    GeometryOutputType = 0x8918,
    ActiveUniformBlockMaxNameLength = 0x8A35,
    ActiveUniformBlocks = 0x8A36,
    DeleteStatus = 0x8B80,
    LinkStatus = 0x8B82,
    ValidateStatus = 0x8B83,
    InfoLogLength = 0x8B84,
    AttachedShaders = 0x8B85,
    ActiveUniforms = 0x8B86,
    ActiveUniformMaxLength = 0x8B87,
    ActiveAttributes = 0x8B89,
    ActiveAttributeMaxLength = 0x8B8A,
    TransformFeedbackVaryingMaxLength = 0x8C76,
    TransformFeedbackBufferMode = 0x8C7F,
    TransformFeedbackVaryings = 0x8C83,
    ActiveAtomicCounterBuffers = 0x92D9,
}

public enum VertexAttribPropertyARB
{
    VertexAttribBinding = 0x82D4,
    VertexAttribRelativeOffset = 0x82D5,
    VertexAttribArrayEnabled = 0x8622,
    VertexAttribArraySize = 0x8623,
    VertexAttribArrayStride = 0x8624,
    VertexAttribArrayType = 0x8625,
    CurrentVertexAttrib = 0x8626,
    VertexAttribArrayLong = 0x874E,
    VertexAttribArrayNormalized = 0x886A,
    VertexAttribArrayBufferBinding = 0x889F,
    VertexAttribArrayInteger = 0x88FD,
    VertexAttribArrayIntegerExt = 0x88FD,
    VertexAttribArrayDivisor = 0x88FE,
}

public enum VertexArrayPName
{
    VertexAttribRelativeOffset = 0x82D5,
    VertexAttribArrayEnabled = 0x8622,
    VertexAttribArraySize = 0x8623,
    VertexAttribArrayStride = 0x8624,
    VertexAttribArrayType = 0x8625,
    VertexAttribArrayLong = 0x874E,
    VertexAttribArrayNormalized = 0x886A,
    VertexAttribArrayInteger = 0x88FD,
    VertexAttribArrayDivisor = 0x88FE,
}

public enum QueryObjectParameterName
{
    QueryTarget = 0x82EA,
    QueryResult = 0x8866,
    QueryResultAvailable = 0x8867,
    QueryResultNoWait = 0x9194,
}

public enum QueryTarget
{
    TransformFeedbackOverflow = 0x82EC,
    VerticesSubmitted = 0x82EE,
    PrimitivesSubmitted = 0x82EF,
    VertexShaderInvocations = 0x82F0,
    TimeElapsed = 0x88BF,
    SamplesPassed = 0x8914,
    AnySamplesPassed = 0x8C2F,
    PrimitivesGenerated = 0x8C87,
    TransformFeedbackPrimitivesWritten = 0x8C88,
    AnySamplesPassedConservative = 0x8D6A,
}

public enum PixelTransformTargetEXT
{
    PixelTransform2dExt = 0x8330,
}

public enum PixelTransformPNameEXT
{
    PixelMagFilterExt = 0x8331,
    PixelMinFilterExt = 0x8332,
    PixelCubicWeightExt = 0x8333,
}

public enum LightTextureModeEXT
{
    FragmentMaterialExt = 0x8349,
    FragmentNormalExt = 0x834A,
    FragmentColorExt = 0x834C,
    FragmentDepthExt = 0x8452,
}

public enum LightTexturePNameEXT
{
    AttenuationExt = 0x834D,
    ShadowAttenuationExt = 0x834E,
}

public enum PixelTexGenParameterNameSGIS
{
    PixelFragmentRgbSourceSgis = 0x8354,
    PixelFragmentAlphaSourceSgis = 0x8355,
}

public enum LightEnvParameterSGIX
{
    LightEnvModeSgix = 0x8407,
}

public enum FragmentLightModelParameterSGIX
{
    FragmentLightModelLocalViewerSgix = 0x8408,
    FragmentLightModelTwoSideSgix = 0x8409,
    FragmentLightModelAmbientSgix = 0x840A,
    FragmentLightModelNormalInterpolationSgix = 0x840B,
}

public enum FragmentLightNameSGIX
{
    FragmentLight0Sgix = 0x840C,
    FragmentLight1Sgix = 0x840D,
    FragmentLight2Sgix = 0x840E,
    FragmentLight3Sgix = 0x840F,
    FragmentLight4Sgix = 0x8410,
    FragmentLight5Sgix = 0x8411,
    FragmentLight6Sgix = 0x8412,
    FragmentLight7Sgix = 0x8413,
}

public enum PixelStoreResampleMode
{
    ResampleDecimateSgix = 0x8430,
    ResampleReplicateSgix = 0x8433,
    ResampleZeroFillSgix = 0x8434,
}

public enum TextureUnit
{
    Texture0 = 0x84C0,
    Texture1 = 0x84C1,
    Texture2 = 0x84C2,
    Texture3 = 0x84C3,
    Texture4 = 0x84C4,
    Texture5 = 0x84C5,
    Texture6 = 0x84C6,
    Texture7 = 0x84C7,
    Texture8 = 0x84C8,
    Texture9 = 0x84C9,
    Texture10 = 0x84CA,
    Texture11 = 0x84CB,
    Texture12 = 0x84CC,
    Texture13 = 0x84CD,
    Texture14 = 0x84CE,
    Texture15 = 0x84CF,
    Texture16 = 0x84D0,
    Texture17 = 0x84D1,
    Texture18 = 0x84D2,
    Texture19 = 0x84D3,
    Texture20 = 0x84D4,
    Texture21 = 0x84D5,
    Texture22 = 0x84D6,
    Texture23 = 0x84D7,
    Texture24 = 0x84D8,
    Texture25 = 0x84D9,
    Texture26 = 0x84DA,
    Texture27 = 0x84DB,
    Texture28 = 0x84DC,
    Texture29 = 0x84DD,
    Texture30 = 0x84DE,
    Texture31 = 0x84DF,
}

public enum CombinerRegisterNV
{
    Texture0Arb = 0x84C0,
    Texture1Arb = 0x84C1,
    PrimaryColorNv = 0x852C,
    SecondaryColorNv = 0x852D,
    Spare0Nv = 0x852E,
    Spare1Nv = 0x852F,
    DiscardNv = 0x8530,
}

public enum UniformBlockPName
{
    UniformBlockReferencedByTessControlShader = 0x84F0,
    UniformBlockReferencedByTessEvaluationShader = 0x84F1,
    UniformBlockBinding = 0x8A3F,
    UniformBlockDataSize = 0x8A40,
    UniformBlockNameLength = 0x8A41,
    UniformBlockActiveUniforms = 0x8A42,
    UniformBlockActiveUniformIndices = 0x8A43,
    UniformBlockReferencedByVertexShader = 0x8A44,
    UniformBlockReferencedByGeometryShader = 0x8A45,
    UniformBlockReferencedByFragmentShader = 0x8A46,
    UniformBlockReferencedByComputeShader = 0x90EC,
}

public enum FenceConditionNV
{
    AllCompletedNv = 0x84F2,
}

public enum FenceParameterNameNV
{
    FenceStatusNv = 0x84F3,
    FenceConditionNv = 0x84F4,
}

public enum CombinerVariableNV
{
    VariableANv = 0x8523,
    VariableBNv = 0x8524,
    VariableCNv = 0x8525,
    VariableDNv = 0x8526,
    VariableENv = 0x8527,
    VariableFNv = 0x8528,
    VariableGNv = 0x8529,
}

public enum PathColor
{
    PrimaryColorNv = 0x852C,
    SecondaryColorNv = 0x852D,
    PrimaryColor = 0x8577,
}

public enum CombinerMappingNV
{
    UnsignedIdentityNv = 0x8536,
    UnsignedInvertNv = 0x8537,
    ExpandNormalNv = 0x8538,
    ExpandNegateNv = 0x8539,
    HalfBiasNormalNv = 0x853A,
    HalfBiasNegateNv = 0x853B,
    SignedIdentityNv = 0x853C,
    SignedNegateNv = 0x853D,
}

public enum CombinerParameterNV
{
    CombinerInputNv = 0x8542,
    CombinerMappingNv = 0x8543,
    CombinerComponentUsageNv = 0x8544,
}

public enum CombinerStageNV
{
    Combiner0Nv = 0x8550,
    Combiner1Nv = 0x8551,
    Combiner2Nv = 0x8552,
    Combiner3Nv = 0x8553,
    Combiner4Nv = 0x8554,
    Combiner5Nv = 0x8555,
    Combiner6Nv = 0x8556,
    Combiner7Nv = 0x8557,
}

public enum RegisterCombinerPname
{
    Combine = 0x8570,
    CombineArb = 0x8570,
    CombineExt = 0x8570,
    CombineRgb = 0x8571,
    CombineRgbArb = 0x8571,
    CombineRgbExt = 0x8571,
    CombineAlpha = 0x8572,
    CombineAlphaArb = 0x8572,
    CombineAlphaExt = 0x8572,
    RgbScale = 0x8573,
    RgbScaleArb = 0x8573,
    RgbScaleExt = 0x8573,
    AddSigned = 0x8574,
    AddSignedArb = 0x8574,
    AddSignedExt = 0x8574,
    Interpolate = 0x8575,
    InterpolateArb = 0x8575,
    InterpolateExt = 0x8575,
    Constant = 0x8576,
    ConstantArb = 0x8576,
    ConstantExt = 0x8576,
    ConstantNv = 0x8576,
    PrimaryColor = 0x8577,
    PrimaryColorArb = 0x8577,
    PrimaryColorExt = 0x8577,
    Previous = 0x8578,
    PreviousArb = 0x8578,
    PreviousExt = 0x8578,
    Source0Rgb = 0x8580,
    Source0RgbArb = 0x8580,
    Source0RgbExt = 0x8580,
    Src0Rgb = 0x8580,
    Source1Rgb = 0x8581,
    Source1RgbArb = 0x8581,
    Source1RgbExt = 0x8581,
    Src1Rgb = 0x8581,
    Source2Rgb = 0x8582,
    Source2RgbArb = 0x8582,
    Source2RgbExt = 0x8582,
    Src2Rgb = 0x8582,
    Source3RgbNv = 0x8583,
    Source0Alpha = 0x8588,
    Source0AlphaArb = 0x8588,
    Source0AlphaExt = 0x8588,
    Src0Alpha = 0x8588,
    Source1Alpha = 0x8589,
    Source1AlphaArb = 0x8589,
    Source1AlphaExt = 0x8589,
    Src1Alpha = 0x8589,
    Src1AlphaExt = 0x8589,
    Source2Alpha = 0x858A,
    Source2AlphaArb = 0x858A,
    Source2AlphaExt = 0x858A,
    Src2Alpha = 0x858A,
    Source3AlphaNv = 0x858B,
    Operand0Rgb = 0x8590,
    Operand0RgbArb = 0x8590,
    Operand0RgbExt = 0x8590,
    Operand1Rgb = 0x8591,
    Operand1RgbArb = 0x8591,
    Operand1RgbExt = 0x8591,
    Operand2Rgb = 0x8592,
    Operand2RgbArb = 0x8592,
    Operand2RgbExt = 0x8592,
    Operand3RgbNv = 0x8593,
    Operand0Alpha = 0x8598,
    Operand0AlphaArb = 0x8598,
    Operand0AlphaExt = 0x8598,
    Operand1Alpha = 0x8599,
    Operand1AlphaArb = 0x8599,
    Operand1AlphaExt = 0x8599,
    Operand2Alpha = 0x859A,
    Operand2AlphaArb = 0x859A,
    Operand2AlphaExt = 0x859A,
    Operand3AlphaNv = 0x859B,
}

public enum PixelStoreSubsampleRate
{
    PixelSubsample4444Sgix = 0x85A2,
    PixelSubsample2424Sgix = 0x85A3,
    PixelSubsample4242Sgix = 0x85A4,
}

public enum TextureNormalModeEXT
{
    PerturbExt = 0x85AE,
}

public enum VertexArrayPNameAPPLE
{
    StorageClientApple = 0x85B4,
    StorageCachedApple = 0x85BE,
    StorageSharedApple = 0x85BF,
}

public enum VertexAttribEnum
{
    VertexAttribArrayEnabled = 0x8622,
    VertexAttribArraySize = 0x8623,
    VertexAttribArrayStride = 0x8624,
    VertexAttribArrayType = 0x8625,
    CurrentVertexAttrib = 0x8626,
    VertexAttribArrayNormalized = 0x886A,
    VertexAttribArrayBufferBinding = 0x889F,
    VertexAttribArrayInteger = 0x88FD,
    VertexAttribArrayDivisor = 0x88FE,
}

public enum ProgramStringProperty
{
    ProgramStringArb = 0x8628,
}

public enum VertexAttribEnumNV
{
    ProgramParameterNv = 0x8644,
}

public enum VertexAttribPointerPropertyARB
{
    VertexAttribArrayPointer = 0x8645,
    VertexAttribArrayPointerArb = 0x8645,
}

public enum EvalTargetNV
{
    Eval2dNv = 0x86C0,
    EvalTriangular2dNv = 0x86C1,
}

public enum MapParameterNV
{
    MapTessellationNv = 0x86C2,
}

public enum MapAttribParameterNV
{
    MapAttribUOrderNv = 0x86C3,
    MapAttribVOrderNv = 0x86C4,
}

public enum ArrayObjectUsageATI
{
    StaticAti = 0x8760,
    DynamicAti = 0x8761,
}

public enum PreserveModeATI
{
    PreserveAti = 0x8762,
    DiscardAti = 0x8763,
}

public enum ArrayObjectPNameATI
{
    ObjectBufferSizeAti = 0x8764,
    ObjectBufferUsageAti = 0x8765,
}

public enum VertexStreamATI
{
    VertexStream0Ati = 0x876C,
    VertexStream1Ati = 0x876D,
    VertexStream2Ati = 0x876E,
    VertexStream3Ati = 0x876F,
    VertexStream4Ati = 0x8770,
    VertexStream5Ati = 0x8771,
    VertexStream6Ati = 0x8772,
    VertexStream7Ati = 0x8773,
}

public enum GetTexBumpParameterATI
{
    BumpRotMatrixAti = 0x8775,
    BumpRotMatrixSizeAti = 0x8776,
    BumpNumTexUnitsAti = 0x8777,
    BumpTexUnitsAti = 0x8778,
}

public enum TexBumpParameterATI
{
    BumpRotMatrixAti = 0x8775,
}

public enum VertexShaderOpEXT
{
    OpIndexExt = 0x8782,
    OpNegateExt = 0x8783,
    OpDot3Ext = 0x8784,
    OpDot4Ext = 0x8785,
    OpMulExt = 0x8786,
    OpAddExt = 0x8787,
    OpMaddExt = 0x8788,
    OpFracExt = 0x8789,
    OpMaxExt = 0x878A,
    OpMinExt = 0x878B,
    OpSetGeExt = 0x878C,
    OpSetLtExt = 0x878D,
    OpClampExt = 0x878E,
    OpFloorExt = 0x878F,
    OpRoundExt = 0x8790,
    OpExpBase2Ext = 0x8791,
    OpLogBase2Ext = 0x8792,
    OpPowerExt = 0x8793,
    OpRecipExt = 0x8794,
    OpRecipSqrtExt = 0x8795,
    OpSubExt = 0x8796,
    OpCrossProductExt = 0x8797,
    OpMultiplyMatrixExt = 0x8798,
    OpMovExt = 0x8799,
}

public enum DataTypeEXT
{
    ScalarExt = 0x87BE,
    VectorExt = 0x87BF,
    MatrixExt = 0x87C0,
}

public enum VertexShaderStorageTypeEXT
{
    VariantExt = 0x87C1,
    InvariantExt = 0x87C2,
    LocalConstantExt = 0x87C3,
    LocalExt = 0x87C4,
}

public enum VertexShaderCoordOutEXT
{
    XExt = 0x87D5,
    YExt = 0x87D6,
    ZExt = 0x87D7,
    WExt = 0x87D8,
    NegativeXExt = 0x87D9,
    NegativeYExt = 0x87DA,
    NegativeZExt = 0x87DB,
    NegativeWExt = 0x87DC,
    ZeroExt = 0x87DD,
    OneExt = 0x87DE,
    NegativeOneExt = 0x87DF,
}

public enum ParameterRangeEXT
{
    NormalizedRangeExt = 0x87E0,
    FullRangeExt = 0x87E1,
}

public enum VertexShaderParameterEXT
{
    CurrentVertexExt = 0x87E2,
    MvpMatrixExt = 0x87E3,
}

public enum GetVariantValueEXT
{
    VariantValueExt = 0x87E4,
    VariantDatatypeExt = 0x87E5,
    VariantArrayStrideExt = 0x87E6,
    VariantArrayTypeExt = 0x87E7,
}

public enum VariantCapEXT
{
    VariantArrayExt = 0x87E8,
}

public enum PNTrianglesPNameATI
{
    PnTrianglesPointModeAti = 0x87F2,
    PnTrianglesNormalModeAti = 0x87F3,
    PnTrianglesTesselationLevelAti = 0x87F4,
}

public enum QueryParameterName
{
    QueryCounterBits = 0x8864,
    CurrentQuery = 0x8865,
}

public enum OcclusionQueryParameterNameNV
{
    PixelCountNv = 0x8866,
    PixelCountAvailableNv = 0x8867,
}

public enum ProgramFormat
{
    ProgramFormatAsciiArb = 0x8875,
}

public enum PixelDataRangeTargetNV
{
    WritePixelDataRangeNv = 0x8878,
    ReadPixelDataRangeNv = 0x8879,
}

public enum CopyBufferSubDataTarget
{
    ArrayBuffer = 0x8892,
    ElementArrayBuffer = 0x8893,
    PixelPackBuffer = 0x88EB,
    PixelUnpackBuffer = 0x88EC,
    UniformBuffer = 0x8A11,
    TextureBuffer = 0x8C2A,
    TransformFeedbackBuffer = 0x8C8E,
    CopyReadBuffer = 0x8F36,
    CopyWriteBuffer = 0x8F37,
    DrawIndirectBuffer = 0x8F3F,
    ShaderStorageBuffer = 0x90D2,
    DispatchIndirectBuffer = 0x90EE,
    QueryBuffer = 0x9192,
    AtomicCounterBuffer = 0x92C0,
}

public enum BufferStorageTarget
{
    ArrayBuffer = 0x8892,
    ElementArrayBuffer = 0x8893,
    PixelPackBuffer = 0x88EB,
    PixelUnpackBuffer = 0x88EC,
    UniformBuffer = 0x8A11,
    TextureBuffer = 0x8C2A,
    TransformFeedbackBuffer = 0x8C8E,
    CopyReadBuffer = 0x8F36,
    CopyWriteBuffer = 0x8F37,
    DrawIndirectBuffer = 0x8F3F,
    ShaderStorageBuffer = 0x90D2,
    DispatchIndirectBuffer = 0x90EE,
    QueryBuffer = 0x9192,
    AtomicCounterBuffer = 0x92C0,
}

public enum BufferAccessARB
{
    ReadOnly = 0x88B8,
    WriteOnly = 0x88B9,
    ReadWrite = 0x88BA,
}

public enum BufferPointerNameARB
{
    BufferMapPointer = 0x88BD,
    BufferMapPointerArb = 0x88BD,
}

public enum VertexBufferObjectUsage
{
    StreamDraw = 0x88E0,
    StreamRead = 0x88E1,
    StreamCopy = 0x88E2,
    StaticDraw = 0x88E4,
    StaticRead = 0x88E5,
    StaticCopy = 0x88E6,
    DynamicDraw = 0x88E8,
    DynamicRead = 0x88E9,
    DynamicCopy = 0x88EA,
}

public enum BufferUsageARB
{
    StreamDraw = 0x88E0,
    StreamRead = 0x88E1,
    StreamCopy = 0x88E2,
    StaticDraw = 0x88E4,
    StaticRead = 0x88E5,
    StaticCopy = 0x88E6,
    DynamicDraw = 0x88E8,
    DynamicRead = 0x88E9,
    DynamicCopy = 0x88EA,
}

public enum ClampColorTargetARB
{
    ClampVertexColorArb = 0x891A,
    ClampFragmentColorArb = 0x891B,
    ClampReadColor = 0x891C,
    ClampReadColorArb = 0x891C,
}

public enum FragmentOpATI
{
    MovAti = 0x8961,
    AddAti = 0x8963,
    MulAti = 0x8964,
    SubAti = 0x8965,
    Dot3Ati = 0x8966,
    Dot4Ati = 0x8967,
    MadAti = 0x8968,
    LerpAti = 0x8969,
    CndAti = 0x896A,
    Cnd0Ati = 0x896B,
    Dot2AddAti = 0x896C,
}

public enum SwizzleOpATI
{
    SwizzleStrAti = 0x8976,
    SwizzleStqAti = 0x8977,
    SwizzleStrDrAti = 0x8978,
    SwizzleStqDqAti = 0x8979,
}

public enum ObjectTypeAPPLE
{
    DrawPixelsApple = 0x8A0A,
    FenceApple = 0x8A0B,
}

public enum UniformPName
{
    UniformType = 0x8A37,
    UniformSize = 0x8A38,
    UniformNameLength = 0x8A39,
    UniformBlockIndex = 0x8A3A,
    UniformOffset = 0x8A3B,
    UniformArrayStride = 0x8A3C,
    UniformMatrixStride = 0x8A3D,
    UniformIsRowMajor = 0x8A3E,
    UniformAtomicCounterBufferIndex = 0x92DA,
}

public enum SubroutineParameterName
{
    UniformSize = 0x8A38,
    UniformNameLength = 0x8A39,
    NumCompatibleSubroutines = 0x8E4A,
    CompatibleSubroutines = 0x8E4B,
}

public enum ShaderType
{
    FragmentShader = 0x8B30,
    FragmentShaderArb = 0x8B30,
    VertexShader = 0x8B31,
    VertexShaderArb = 0x8B31,
    GeometryShader = 0x8DD9,
    TessEvaluationShader = 0x8E87,
    TessControlShader = 0x8E88,
    ComputeShader = 0x91B9,
}

public enum ContainerType
{
    ProgramObjectArb = 0x8B40,
    ProgramObjectExt = 0x8B40,
}

public enum ShaderParameterName
{
    ShaderType = 0x8B4F,
    DeleteStatus = 0x8B80,
    CompileStatus = 0x8B81,
    InfoLogLength = 0x8B84,
    ShaderSourceLength = 0x8B88,
}

public enum ShaderBinaryFormat
{
    SgxBinaryImg = 0x8C0A,
    MaliShaderBinaryArm = 0x8F60,
    ShaderBinaryViv = 0x8FC4,
    ShaderBinaryDmp = 0x9250,
    GccsoShaderBinaryFj = 0x9260,
    ShaderBinaryFormatSpirV = 0x9551,
}

public enum TransformFeedbackPName
{
    TransformFeedbackBufferStart = 0x8C84,
    TransformFeedbackBufferSize = 0x8C85,
    TransformFeedbackBufferBinding = 0x8C8F,
    TransformFeedbackPaused = 0x8E23,
    TransformFeedbackActive = 0x8E24,
}

public enum TransformFeedbackBufferMode
{
    InterleavedAttribs = 0x8C8C,
    SeparateAttribs = 0x8C8D,
}

public enum ProgramInterface
{
    TransformFeedbackBuffer = 0x8C8E,
    Uniform = 0x92E1,
    UniformBlock = 0x92E2,
    ProgramInput = 0x92E3,
    ProgramOutput = 0x92E4,
    BufferVariable = 0x92E5,
    ShaderStorageBlock = 0x92E6,
    VertexSubroutine = 0x92E8,
    TessControlSubroutine = 0x92E9,
    TessEvaluationSubroutine = 0x92EA,
    GeometrySubroutine = 0x92EB,
    FragmentSubroutine = 0x92EC,
    ComputeSubroutine = 0x92ED,
    VertexSubroutineUniform = 0x92EE,
    TessControlSubroutineUniform = 0x92EF,
    TessEvaluationSubroutineUniform = 0x92F0,
    GeometrySubroutineUniform = 0x92F1,
    FragmentSubroutineUniform = 0x92F2,
    ComputeSubroutineUniform = 0x92F3,
    TransformFeedbackVarying = 0x92F4,
}

public enum ClipControlOrigin
{
    LowerLeft = 0x8CA1,
    UpperLeft = 0x8CA2,
}

public enum CheckFramebufferStatusTarget
{
    ReadFramebuffer = 0x8CA8,
    DrawFramebuffer = 0x8CA9,
    Framebuffer = 0x8D40,
}

public enum FramebufferTarget
{
    ReadFramebuffer = 0x8CA8,
    DrawFramebuffer = 0x8CA9,
    Framebuffer = 0x8D40,
    FramebufferOes = 0x8D40,
}

public enum RenderbufferParameterName
{
    RenderbufferCoverageSamplesNv = 0x8CAB,
    RenderbufferSamples = 0x8CAB,
    RenderbufferSamplesAngle = 0x8CAB,
    RenderbufferSamplesApple = 0x8CAB,
    RenderbufferSamplesExt = 0x8CAB,
    RenderbufferSamplesNv = 0x8CAB,
    RenderbufferWidth = 0x8D42,
    RenderbufferWidthExt = 0x8D42,
    RenderbufferWidthOes = 0x8D42,
    RenderbufferHeight = 0x8D43,
    RenderbufferHeightExt = 0x8D43,
    RenderbufferHeightOes = 0x8D43,
    RenderbufferInternalFormat = 0x8D44,
    RenderbufferInternalFormatExt = 0x8D44,
    RenderbufferInternalFormatOes = 0x8D44,
    RenderbufferRedSize = 0x8D50,
    RenderbufferRedSizeExt = 0x8D50,
    RenderbufferRedSizeOes = 0x8D50,
    RenderbufferGreenSize = 0x8D51,
    RenderbufferGreenSizeExt = 0x8D51,
    RenderbufferGreenSizeOes = 0x8D51,
    RenderbufferBlueSize = 0x8D52,
    RenderbufferBlueSizeExt = 0x8D52,
    RenderbufferBlueSizeOes = 0x8D52,
    RenderbufferAlphaSize = 0x8D53,
    RenderbufferAlphaSizeExt = 0x8D53,
    RenderbufferAlphaSizeOes = 0x8D53,
    RenderbufferDepthSize = 0x8D54,
    RenderbufferDepthSizeExt = 0x8D54,
    RenderbufferDepthSizeOes = 0x8D54,
    RenderbufferStencilSize = 0x8D55,
    RenderbufferStencilSizeExt = 0x8D55,
    RenderbufferStencilSizeOes = 0x8D55,
    RenderbufferColorSamplesNv = 0x8E10,
    RenderbufferSamplesImg = 0x9133,
    RenderbufferStorageSamplesAmd = 0x91B2,
}

public enum DrawBufferModeATI
{
    ColorAttachment0Nv = 0x8CE0,
    ColorAttachment1Nv = 0x8CE1,
    ColorAttachment2Nv = 0x8CE2,
    ColorAttachment3Nv = 0x8CE3,
    ColorAttachment4Nv = 0x8CE4,
    ColorAttachment5Nv = 0x8CE5,
    ColorAttachment6Nv = 0x8CE6,
    ColorAttachment7Nv = 0x8CE7,
    ColorAttachment8Nv = 0x8CE8,
    ColorAttachment9Nv = 0x8CE9,
    ColorAttachment10Nv = 0x8CEA,
    ColorAttachment11Nv = 0x8CEB,
    ColorAttachment12Nv = 0x8CEC,
    ColorAttachment13Nv = 0x8CED,
    ColorAttachment14Nv = 0x8CEE,
    ColorAttachment15Nv = 0x8CEF,
}

public enum RenderbufferTarget
{
    Renderbuffer = 0x8D41,
    RenderbufferOes = 0x8D41,
}

public enum ProgramStagePName
{
    ActiveSubroutines = 0x8DE5,
    ActiveSubroutineUniforms = 0x8DE6,
    ActiveSubroutineUniformLocations = 0x8E47,
    ActiveSubroutineMaxLength = 0x8E48,
    ActiveSubroutineUniformMaxLength = 0x8E49,
}

public enum PrecisionType
{
    LowFloat = 0x8DF0,
    MediumFloat = 0x8DF1,
    HighFloat = 0x8DF2,
    LowInt = 0x8DF3,
    MediumInt = 0x8DF4,
    HighInt = 0x8DF5,
}

public enum ConditionalRenderMode
{
    QueryWait = 0x8E13,
    QueryNoWait = 0x8E14,
    QueryByRegionWait = 0x8E15,
    QueryByRegionNoWait = 0x8E16,
    QueryWaitInverted = 0x8E17,
    QueryNoWaitInverted = 0x8E18,
    QueryByRegionWaitInverted = 0x8E19,
    QueryByRegionNoWaitInverted = 0x8E1A,
}

public enum BindTransformFeedbackTarget
{
    TransformFeedback = 0x8E22,
}

public enum QueryCounterTarget
{
    Timestamp = 0x8E28,
}

public enum ProgramResourceProperty
{
    NumCompatibleSubroutines = 0x8E4A,
    CompatibleSubroutines = 0x8E4B,
    Uniform = 0x92E1,
    IsPerPatch = 0x92E7,
    NameLength = 0x92F9,
    Type = 0x92FA,
    ArraySize = 0x92FB,
    Offset = 0x92FC,
    BlockIndex = 0x92FD,
    ArrayStride = 0x92FE,
    MatrixStride = 0x92FF,
    IsRowMajor = 0x9300,
    AtomicCounterBufferIndex = 0x9301,
    BufferBinding = 0x9302,
    BufferDataSize = 0x9303,
    NumActiveVariables = 0x9304,
    ActiveVariables = 0x9305,
    ReferencedByVertexShader = 0x9306,
    ReferencedByTessControlShader = 0x9307,
    ReferencedByTessEvaluationShader = 0x9308,
    ReferencedByGeometryShader = 0x9309,
    ReferencedByFragmentShader = 0x930A,
    ReferencedByComputeShader = 0x930B,
    TopLevelArraySize = 0x930C,
    TopLevelArrayStride = 0x930D,
    Location = 0x930E,
    LocationIndex = 0x930F,
    LocationComponent = 0x934A,
    TransformFeedbackBufferIndex = 0x934B,
    TransformFeedbackBufferStride = 0x934C,
}

public enum VertexProvokingMode
{
    FirstVertexConvention = 0x8E4D,
    LastVertexConvention = 0x8E4E,
}

public enum GetMultisamplePNameNV
{
    SamplePosition = 0x8E50,
    SampleLocationArb = 0x8E50,
    ProgrammableSampleLocationArb = 0x9341,
}

public enum PatchParameterName
{
    PatchVertices = 0x8E72,
    PatchDefaultInnerLevel = 0x8E73,
    PatchDefaultOuterLevel = 0x8E74,
}

public enum PathStringFormat
{
    PathFormatSvgNv = 0x9070,
    PathFormatPsNv = 0x9071,
}

public enum PathFontTarget
{
    StandardFontNameNv = 0x9072,
    SystemFontNameNv = 0x9073,
    FileNameNv = 0x9074,
}

public enum PathParameter
{
    PathStrokeWidthNv = 0x9075,
    PathEndCapsNv = 0x9076,
    PathInitialEndCapNv = 0x9077,
    PathTerminalEndCapNv = 0x9078,
    PathJoinStyleNv = 0x9079,
    PathMiterLimitNv = 0x907A,
    PathDashCapsNv = 0x907B,
    PathInitialDashCapNv = 0x907C,
    PathTerminalDashCapNv = 0x907D,
    PathDashOffsetNv = 0x907E,
    PathClientLengthNv = 0x907F,
    PathFillModeNv = 0x9080,
    PathFillMaskNv = 0x9081,
    PathFillCoverModeNv = 0x9082,
    PathStrokeCoverModeNv = 0x9083,
    PathStrokeMaskNv = 0x9084,
    PathObjectBoundingBoxNv = 0x908A,
    PathCommandCountNv = 0x909D,
    PathCoordCountNv = 0x909E,
    PathDashArrayCountNv = 0x909F,
    PathComputedLengthNv = 0x90A0,
    PathFillBoundingBoxNv = 0x90A1,
    PathStrokeBoundingBoxNv = 0x90A2,
    PathDashOffsetResetNv = 0x90B4,
}

public enum PathCoverMode
{
    PathFillCoverModeNv = 0x9082,
    ConvexHullNv = 0x908B,
    BoundingBoxNv = 0x908D,
    BoundingBoxOfBoundingBoxesNv = 0x909C,
}

public enum PathElementType
{
    Utf8Nv = 0x909A,
    Utf16Nv = 0x909B,
}

public enum PathHandleMissingGlyphs
{
    SkipMissingGlyphNv = 0x90A9,
    UseMissingGlyphNv = 0x90AA,
}

public enum PathListMode
{
    AccumAdjacentPairsNv = 0x90AD,
    AdjacentPairsNv = 0x90AE,
    FirstToRestNv = 0x90AF,
}

public enum AtomicCounterBufferPName
{
    AtomicCounterBufferReferencedByComputeShader = 0x90ED,
    AtomicCounterBufferBinding = 0x92C1,
    AtomicCounterBufferDataSize = 0x92C4,
    AtomicCounterBufferActiveAtomicCounters = 0x92C5,
    AtomicCounterBufferActiveAtomicCounterIndices = 0x92C6,
    AtomicCounterBufferReferencedByVertexShader = 0x92C7,
    AtomicCounterBufferReferencedByTessControlShader = 0x92C8,
    AtomicCounterBufferReferencedByTessEvaluationShader = 0x92C9,
    AtomicCounterBufferReferencedByGeometryShader = 0x92CA,
    AtomicCounterBufferReferencedByFragmentShader = 0x92CB,
}

public enum SyncParameterName
{
    ObjectType = 0x9112,
    SyncCondition = 0x9113,
    SyncStatus = 0x9114,
    SyncFlags = 0x9115,
}

public enum SyncCondition
{
    SyncGpuCommandsComplete = 0x9117,
}

public enum SyncStatus
{
    AlreadySignaled = 0x911A,
    TimeoutExpired = 0x911B,
    ConditionSatisfied = 0x911C,
    WaitFailed = 0x911D,
}

public enum ProgramInterfacePName
{
    ActiveResources = 0x92F5,
    MaxNameLength = 0x92F6,
    MaxNumActiveVariables = 0x92F7,
    MaxNumCompatibleSubroutines = 0x92F8,
}

public enum FramebufferParameterName
{
    FramebufferDefaultWidth = 0x9310,
    FramebufferDefaultHeight = 0x9311,
    FramebufferDefaultLayers = 0x9312,
    FramebufferDefaultSamples = 0x9313,
    FramebufferDefaultFixedSampleLocations = 0x9314,
}

public enum ClipControlDepth
{
    NegativeOneToOne = 0x935E,
    ZeroToOne = 0x935F,
}

public enum TextureLayout
{
    LayoutDepthReadOnlyStencilAttachmentExt = 0x9530,
    LayoutDepthAttachmentStencilReadOnlyExt = 0x9531,
    LayoutGeneralExt = 0x958D,
    LayoutColorAttachmentExt = 0x958E,
    LayoutDepthStencilAttachmentExt = 0x958F,
    LayoutDepthStencilReadOnlyExt = 0x9590,
    LayoutShaderReadOnlyExt = 0x9591,
    LayoutTransferSrcExt = 0x9592,
    LayoutTransferDstExt = 0x9593,
}

public enum MemoryObjectParameterName
{
    DedicatedMemoryObjectExt = 0x9581,
    ProtectedMemoryObjectExt = 0x959B,
}

public enum ExternalHandleType
{
    HandleTypeOpaqueFdExt = 0x9586,
    HandleTypeOpaqueWin32Ext = 0x9587,
    HandleTypeOpaqueWin32KmtExt = 0x9588,
    HandleTypeD3d12TilepoolExt = 0x9589,
    HandleTypeD3d12ResourceExt = 0x958A,
    HandleTypeD3d11ImageExt = 0x958B,
    HandleTypeD3d11ImageKmtExt = 0x958C,
    HandleTypeD3d12FenceExt = 0x9594,
}

public enum SemaphoreParameterName
{
    D3d12FenceValueExt = 0x9595,
    TimelineSemaphoreValueNv = 0x9595,
    SemaphoreTypeNv = 0x95B3,
    SemaphoreTypeBinaryNv = 0x95B4,
    SemaphoreTypeTimelineNv = 0x95B5,
}

public enum FramebufferFetchNoncoherent
{
    FramebufferFetchNoncoherentQcom = 0x96A2,
}

public enum ShadingRateQCOM
{
    ShadingRate1x1PixelsQcom = 0x96A6,
    ShadingRate1x2PixelsQcom = 0x96A7,
    ShadingRate2x1PixelsQcom = 0x96A8,
    ShadingRate2x2PixelsQcom = 0x96A9,
    ShadingRate1x4PixelsQcom = 0x96AA,
    ShadingRate4x1PixelsQcom = 0x96AB,
    ShadingRate4x2PixelsQcom = 0x96AC,
    ShadingRate2x4PixelsQcom = 0x96AD,
    ShadingRate4x4PixelsQcom = 0x96AE,
}

public enum TexStorageAttribs
{
    SurfaceCompressionExt = 0x96C0,
    SurfaceCompressionFixedRateDefaultExt = 0x96C2,
    SurfaceCompressionFixedRate1bpcExt = 0x96C4,
    SurfaceCompressionFixedRate2bpcExt = 0x96C5,
    SurfaceCompressionFixedRate3bpcExt = 0x96C6,
    SurfaceCompressionFixedRate4bpcExt = 0x96C7,
    SurfaceCompressionFixedRate5bpcExt = 0x96C8,
    SurfaceCompressionFixedRate6bpcExt = 0x96C9,
    SurfaceCompressionFixedRate7bpcExt = 0x96CA,
    SurfaceCompressionFixedRate8bpcExt = 0x96CB,
    SurfaceCompressionFixedRate9bpcExt = 0x96CC,
    SurfaceCompressionFixedRate10bpcExt = 0x96CD,
    SurfaceCompressionFixedRate11bpcExt = 0x96CE,
    SurfaceCompressionFixedRate12bpcExt = 0x96CF,
}

public enum TexStorageAttribss
{
    SurfaceCompressionFixedRateNoneExt = 0x96C1,
}

public enum HintTargetPGI
{
    VertexDataHintPgi = 0x1A22A,
    VertexConsistentHintPgi = 0x1A22B,
    MaterialSideHintPgi = 0x1A22C,
    MaxVertexHintPgi = 0x1A22D,
}

