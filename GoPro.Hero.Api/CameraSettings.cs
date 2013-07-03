using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using GoPro.Hero.Api.Utilities;

namespace GoPro.Hero.Api
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
        public VideoStandard Ntsc { get; protected set; }
        public bool PreviewActive { get; protected set; }

        public byte Battery { get; protected set; }
        public byte UsbMode { get; protected set; }
        public ushort PhotosAvailableSpace { get; protected set; }
        public ushort PhotosCount { get; protected set; }
        public ushort VideosAvailableSpace { get; protected set; }
        public ushort VideosCount { get; protected set; }
        public bool Shutter { get; protected set; }

        public bool Busy { get; protected set; }
        public bool ProTune { get; protected set; }
        public bool PreviewAvailable { get; protected set; }

        internal protected virtual void Update(Stream stream)
        {
            using (var binReader = new BinaryReader(stream))
            {
                binReader.ReadByte();

                FillSettings(binReader);
            }
        }

        protected void FillSettings(BinaryReader binReader)
        {
            this.Mode = binReader.ReadEnum<Mode>();
            this.MicrophoneMode = binReader.ReadByte();
            this.OnDefault = binReader.ReadEnum<Mode>();
            this.SpotMeter = binReader.ReadByte()>0;
            this.TimeLapse = binReader.ReadEnum<TimeLapse>();
            this.AutoPowerOff = binReader.ReadByte()>0;
            this.FieldOfView = binReader.ReadEnum<FieldOfView>();
            this.PhotoResolution = binReader.ReadEnum<PhotoResolution>();
            this.VideoResolution = binReader.ReadEnum<VideoResolution>();
            this.AudioInput = binReader.ReadByte();
            this.PlayMode = binReader.ReadByte();
            this.PlaybackPosition = binReader.ReadUInt32();
            this.BeepSound = binReader.ReadEnum<BeepSound>();
            this.LedBlink = binReader.ReadEnum<LedBlink>();

            var field = binReader.ReadByte();
            this.PreviewActive = (byte)(field & 0x1)>0;
            this.LiveFeed = (byte)(field & 0x2)>0;
            this.Orientation = (Orientation)(field & 0x4);
            this.OneButton = (byte)(field & 0x8)>0;
            this.OnScreen = (byte)(field & 0x10)>0;
            this.Ntsc = (VideoStandard)(field & 0x20);
            this.LocateCamera = (byte)(field & 0x40)>0;

            this.Battery = binReader.ReadByte();
            this.UsbMode = binReader.ReadByte();
            this.PhotosAvailableSpace = binReader.ReadUInt16BigEndian();
            this.PhotosCount = binReader.ReadUInt16BigEndian();
            this.VideosAvailableSpace = binReader.ReadUInt16BigEndian();
            this.VideosCount = binReader.ReadUInt16BigEndian();
            this.Shutter = binReader.ReadByte()>0;

            field = binReader.ReadByte();
            this.Busy = (byte)(field & 0x1)>0;
            this.ProTune = (byte)(field & 0x2)>0;
            this.PreviewAvailable = (byte)(field & 0x4)>0;
        }
    }
}
