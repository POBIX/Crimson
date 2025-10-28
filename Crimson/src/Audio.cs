using System.Reflection;
using System.Runtime.InteropServices;
using libsndfileSharp;
using PortAudioSharp;
using static PortAudioSharp.PortAudio;

namespace Crimson;

/// <summary>
/// Plays audio files.
/// </summary>
public unsafe class AudioPlayer : IDisposable
{
    private IntPtr stream;
    /// <summary>Is the player currently paused?</summary>
    public bool IsPaused { get; private set; }
    /// <summary>Is the player currently playing audio?</summary>
    public bool IsPlaying { get; private set; }
    /// <summary>Will get invoked when the audio ends, and Loop is set to false.</summary>
    public event Action PlaybackFinished;

    /// <summary>The volume multiplier. Audio clipping can occur with high values.</summary>
    public float Volume { get => dataPtr->volume; set => dataPtr->volume = value; }
    /// <summary>Should the audio loop indefinitely?</summary>
    public bool Loop
    {
        get => dataPtr->flags.HasFlag(Flags.Loop);
        set
        {
            if (value) dataPtr->flags |= Flags.Loop;
            else dataPtr->flags &= ~Flags.Loop;
        }
    }

    // these fields exist because of a "callback was made on a garbage collected delegate" exception
    private static readonly PaStreamCallbackDelegate Callback = StreamCallback;
    // ReSharper disable once PrivateFieldCanBeConvertedToLocalVariable
    private readonly PaStreamFinishedCallbackDelegate streamFinishedCallback;

    private Data* dataPtr;
    private GCHandle dataGC;
    private IntPtr allocArr;

    [StructLayout(LayoutKind.Sequential)]
    private struct Data
    {
        public float* start;
        public float* curr;
        public int length;
        public int channels;
        public Flags flags;
        public float volume;
    }

    [Flags]
    private enum Flags
    {
        Loop = 0x1,
        Pause = 0x2,
        Setup = 0x4,
    }

    // the binding's version takes a ref IntPtr instead of an IntPtr and then it doesn't work.
    [DllImport("PortAudio.dll")]
    private static extern PaError Pa_SetStreamFinishedCallback(
        IntPtr stream,
        [MarshalAs(UnmanagedType.FunctionPtr)] PaStreamFinishedCallbackDelegate streamFinishedCallback
    );

    public AudioPlayer(string path, float volume = 1, bool loop = false)
    {
        using SndFile file = new(path);

        streamFinishedCallback = OnStreamFinished;

        const int readSize = 512;
        int size = readSize * 16;
        allocArr = Marshal.AllocHGlobal(size * sizeof(float));
        float* buffer = (float*)allocArr.ToPointer();

        int i = 0;
        long read;
        while ((read = file.readFloat(allocArr + i * sizeof(float), readSize)) != 0)
        {
            i += (int)read;
            if (i >= size)
            {
                size *= 2;
                allocArr = Marshal.ReAllocHGlobal(allocArr, new(size * sizeof(float)));
                buffer = (float*)allocArr.ToPointer();
            }
        }
        file.Close();

        PaStreamParameters ps = new PaStreamParameters
        {
            device = Pa_GetDefaultOutputDevice(),
            channelCount = file.Info.channels,
            sampleFormat = PaSampleFormat.paFloat32,
            hostApiSpecificStreamInfo = IntPtr.Zero,
        };
        ps.suggestedLatency = Pa_GetDeviceInfo(ps.device).defaultLowOutputLatency;

        PaStreamParameters? @null = null; // can't pass null to ref parameter.
        PaStreamParameters? psn = ps; // can't cast things with ref parameter.

        Data userData = new()
        {
            start = buffer,
            length = i,
            channels = ps.channelCount,
            volume = volume
        };
        if (loop) userData.flags |= Flags.Loop;
        else userData.flags &= ~Flags.Loop;
        userData.curr = userData.start;

        dataGC = GCHandle.Alloc(userData, GCHandleType.Pinned);
        IntPtr ptr = dataGC.AddrOfPinnedObject();
        dataPtr = (Data*)ptr.ToPointer();
        Pa_OpenStream(
            out stream, ref @null, ref psn,
            file.Info.samplerate, paFramesPerBufferUnspecified, PaStreamFlags.paClipOff,
            Callback, ptr
        );

        Pa_SetStreamFinishedCallback(stream, streamFinishedCallback);
    }

    private void OnStreamFinished(IntPtr stream)
    {
        Data* data = (Data*)stream.ToPointer();
        // if this was called because of a setup call rather than the stream stopping for real, ignore it.
        if (data->flags.HasFlag(Flags.Setup)) return;
        IsPlaying = false;
        PlaybackFinished?.Invoke();
    }

    // this method is static because C requires function pointers to be,
    // and making it non static probably has a performance overhead, (i don't really know though?)
    // which we wouldn't be able to afford according to portaudio docs.
    // fields are passed through userdata instead.
    private static PaStreamCallbackResult StreamCallback(IntPtr input, IntPtr output, uint frameCount,
                                                         ref PaStreamCallbackTimeInfo timeInfo,
                                                         PaStreamCallbackFlags statusFlags, IntPtr userdata)
    {
        float* buffer = (float*)output.ToPointer();
        Data* data = (Data*)userdata.ToPointer();

        bool paused = data->flags.HasFlag(Flags.Pause);

        for (int i = 0; i < frameCount * data->channels; i++)
        {
            if (!paused)
            {
                *buffer++ = *data->curr++ * data->volume;

                // if we reached the file's end
                if (data->curr >= data->start + data->length)
                {
                    if (data->flags.HasFlag(Flags.Loop))
                    {
                        data->curr = data->start;
                        return PaStreamCallbackResult.paContinue;
                    }
                    return PaStreamCallbackResult.paComplete;
                }
            }
            else *buffer++ = 0;
        }

        return PaStreamCallbackResult.paContinue;
    }

    /// <summary> Start playing the audio file. See <seealso cref="Resume"/> for resuming it after a pause.</summary>
    public void Play()
    {
        dataPtr->curr = dataPtr->start;

        dataPtr->flags |= Flags.Setup;
        Pa_AbortStream(stream);
        dataPtr->flags &= ~Flags.Setup;

        Pa_StartStream(stream);

        IsPlaying = true;
    }

    /// <summary> Pauses the audio playback. Can be resumed with <seealso cref="Resume"/> </summary>
    public void Pause()
    {
        dataPtr->flags |= Flags.Pause;
        IsPaused = true;
    }

    /// <summary> Resumes audio that was previously paused with <seealso cref="Pause"/> </summary>
    public void Resume()
    {
        dataPtr->flags &= ~Flags.Pause;
        IsPaused = false;
    }

    /// <summary> Completely stops the audio. See <seealso cref="Pause"/> for pausing it. </summary>
    public void Stop()
    {
        Pa_StopStream(stream);
        IsPlaying = false;
    }

    private void ReleaseUnmanagedResources()
    {
        Pa_CloseStream(stream);
        dataGC.Free();
        Marshal.FreeHGlobal(allocArr);
    }

    public void Dispose()
    {
        ReleaseUnmanagedResources();
        GC.SuppressFinalize(this);
    }

    ~AudioPlayer() => ReleaseUnmanagedResources();

    private static IntPtr DllImportResolver(string libraryName, Assembly assembly, DllImportSearchPath? searchPath)
    {
        if (libraryName == "PortAudio.dll")
        {
            string platformLibrary;
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows)) platformLibrary = "PortAudio.dll";
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux)) platformLibrary = "portaudio";
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX)) platformLibrary = "libportaudio.dylib";
            else throw new PlatformNotSupportedException();

            if (NativeLibrary.TryLoad(platformLibrary, assembly, searchPath, out IntPtr handle))
                return handle;
        }
        return IntPtr.Zero;
    }

    internal static void Init()
    {
        NativeLibrary.SetDllImportResolver(typeof(PortAudio).Assembly, DllImportResolver);
        Pa_Initialize();
    }
    internal static void Terminate() => Pa_Terminate();
}

internal static class PortAudioLoader
{
}
