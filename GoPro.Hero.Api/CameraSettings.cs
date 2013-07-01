using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace GoPro.Hero.Api
{
    public class CameraSettings
    {
        public byte Mode { get; protected set; }
        public byte MicrophoneMode { get; protected set; }
        public byte OnDefault { get; protected set; }
        public byte Exposure { get; protected set; }
        public byte TimeLapse { get; protected set; }
        public byte AutoPowerOff { get; protected set; }
        public byte FieldOfView { get; protected set; }
        public byte PhotoResolution { get; protected set; }
        public byte VideoResolution { get; protected set; }
        public byte AudioInput { get; protected set; }
        public byte PlayMode { get; protected set; }
        public byte PlaybackPosition { get; protected set; }
        public byte BeepSound { get; protected set; }
        public byte LedBlink { get; protected set; }

        public byte OnScreen { get; protected set; }
        public byte OneButton { get; protected set; }
        public byte Orientation { get; protected set; }
        public byte LiveFeed { get; protected set; }
        public byte LocateCamera { get; protected set; }
        public byte Ntsc { get; protected set; }
        public byte PreviewActive { get; protected set; }

        public byte Battery { get; protected set; }
        public byte UsbMode { get; protected set; }
        public ushort PhotosAvailableSpace { get; protected set; }
        public ushort PhotosCount { get; protected set; }
        public ushort VideosAvailableSpace { get; protected set; }
        public ushort VideosCount { get; protected set; }
        public byte Shutter { get; protected set; }

        public byte Busy { get; protected set; }
        public byte ProTune { get; protected set; }
        public byte PreviewAvailable { get; protected set; }

        internal protected virtual void Update(Stream stream)
        {
            using (var binReader = new BinaryReader(stream))
            {
                FillSettings(binReader);
            }
        }

        protected void FillSettings(BinaryReader binReader)
        {
            this.Mode = binReader.ReadByte();
            this.MicrophoneMode = binReader.ReadByte();
            this.OnDefault = binReader.ReadByte();
            this.Exposure = binReader.ReadByte();
            this.TimeLapse = binReader.ReadByte();
            this.AutoPowerOff = binReader.ReadByte();
            this.FieldOfView = binReader.ReadByte();
            this.PhotoResolution = binReader.ReadByte();
            this.VideoResolution = binReader.ReadByte();
            this.AudioInput = binReader.ReadByte();
            this.PlayMode = binReader.ReadByte();
            this.PlaybackPosition = binReader.ReadByte();
            this.BeepSound = binReader.ReadByte();
            this.LedBlink = binReader.ReadByte();

            var field = binReader.ReadByte();
            this.PreviewActive = (byte)(field & 0x1);
            this.LiveFeed = (byte)(field & 0x2);
            this.Orientation = (byte)(field & 0x4);
            this.OneButton = (byte)(field & 0x8);
            this.OnScreen = (byte)(field & 0x10);
            this.Ntsc = (byte)(field & 0x20);
            this.LocateCamera = (byte)(field & 0x40);

            this.Battery = binReader.ReadByte();
            this.UsbMode = binReader.ReadByte();
            this.PhotosAvailableSpace = binReader.ReadUInt16();
            this.PhotosCount = binReader.ReadUInt16();
            this.VideosAvailableSpace = binReader.ReadUInt16();
            this.VideosCount = binReader.ReadUInt16();
            this.Shutter = binReader.ReadByte();

            field = binReader.ReadByte();
            this.Busy = (byte)(field & 0x1);
            this.ProTune = (byte)(field & 0x2);
            this.PreviewAvailable = (byte)(field & 0x4);
        }
    }
}
