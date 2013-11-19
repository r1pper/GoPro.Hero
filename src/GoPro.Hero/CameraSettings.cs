using System;
using System.IO;
using GoPro.Hero.Utilities;

namespace GoPro.Hero
{
    public class CameraSettings
    {
        public Mode Mode { get; protected set; }
        public byte MicrophoneMode { get; protected set; }
        public Mode OnDefault { get; protected set; }
        public bool SpotMeter { get; protected set; }
        public TimeLapse TimeLapse { get; protected set; }
        public bool AutoPowerOff { get; protected set; }
        public FieldOfView FieldOfView { get; protected set; }
        public PhotoResolution PhotoResolution { get; protected set; }

        [Obsolete("NOTE: does not work for Hero3 Black Edition use ExtendedSettings.")]
        public VideoResolution VideoResolution { get; protected set; }

        public byte AudioInput { get; protected set; }
        public byte PlayMode { get; protected set; }
        public uint PlaybackPosition { get; protected set; }
        public BeepSound BeepSound { get; protected set; }
        public LedBlink LedBlink { get; protected set; }

        public bool OnScreen { get; protected set; }
        public bool OneButton { get; protected set; }
        public Orientation Orientation { get; protected set; }
        public bool LiveFeed { get; protected set; }
        public bool LocateCamera { get; protected set; }
        public VideoStandard VideoStandard { get; protected set; }
        public bool PreviewActive { get; protected set; }

        public byte Battery { get; protected set; }
        public byte UsbMode { get; protected set; }
        public ushort PhotosAvailableSpace { get; protected set; }
        public ushort PhotosCount { get; protected set; }
        public ushort VideosAvailableSpace { get; protected set; }
        public ushort VideosCount { get; protected set; }
        public bool Shutter { get; protected set; }

        public bool Busy { get; protected set; }
        public bool Protune { get; protected set; }
        public bool PreviewAvailable { get; protected set; }

        protected internal virtual void Update(Stream stream)
        {
            using (var binReader = new BinaryReader(stream))
            {
                binReader.ReadByte();

                FillSettings(binReader);
            }
        }

        protected void FillSettings(BinaryReader binReader)
        {
            Mode = binReader.ReadEnum<Mode>();
            MicrophoneMode = binReader.ReadByte();
            OnDefault = binReader.ReadEnum<Mode>();
            SpotMeter = binReader.ReadByte() > 0;
            TimeLapse = binReader.ReadEnum<TimeLapse>();
            AutoPowerOff = binReader.ReadByte() > 0;
            FieldOfView = binReader.ReadEnum<FieldOfView>();
            PhotoResolution = binReader.ReadEnum<PhotoResolution>();
            VideoResolution = binReader.ReadEnum<VideoResolution>();
            AudioInput = binReader.ReadByte();
            PlayMode = binReader.ReadByte();
            PlaybackPosition = binReader.ReadUInt32();
            BeepSound = binReader.ReadEnum<BeepSound>();
            LedBlink = binReader.ReadEnum<LedBlink>();

            var field = binReader.ReadByte();
            PreviewActive = (byte) (field & 0x1) > 0;
            LiveFeed = (byte) (field & 0x2) > 0;
            Orientation = (Orientation) (field & 0x4);
            OneButton = (byte) (field & 0x8) > 0;
            OnScreen = (byte) (field & 0x10) > 0;
            VideoStandard = (VideoStandard) (field & 0x20);
            LocateCamera = (byte) (field & 0x40) > 0;

            Battery = binReader.ReadByte();
            UsbMode = binReader.ReadByte();
            PhotosAvailableSpace = binReader.ReadUInt16BigEndian();
            PhotosCount = binReader.ReadUInt16BigEndian();
            VideosAvailableSpace = binReader.ReadUInt16BigEndian();
            VideosCount = binReader.ReadUInt16BigEndian();
            Shutter = binReader.ReadByte() > 0;

            field = binReader.ReadByte();
            Busy = (byte) (field & 0x1) > 0;
            Protune = (byte) (field & 0x2) > 0;
            PreviewAvailable = (byte) (field & 0x4) > 0;
        }
    }
}