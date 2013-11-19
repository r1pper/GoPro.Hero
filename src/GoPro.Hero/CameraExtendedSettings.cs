using System.IO;
using GoPro.Hero.Utilities;

namespace GoPro.Hero
{
    public sealed class CameraExtendedSettings : CameraSettings
    {
        internal CameraExtendedSettings()
        {
        }

        public byte HlsSegmentSize { get; private set; }
        public BurstRate BurstRate { get; private set; }
        public ContinuousShot ContinuousShot { get; private set; }
        public WhiteBalance WhiteBalance { get; private set; }
        public byte BracketingMode { get; private set; }
        public byte PhotoInVideo { get; private set; }
        public LoopingVideo LoopingVideoMode { get; private set; }
        public byte SlideshowSettings { get; private set; }
        public byte BroadcastSettings { get; private set; }
        public byte TimeLapseStyle { get; private set; }
        public int VideoLoopCounter { get; private set; }
        public byte ExternalBattery { get; private set; }
        public bool IsBombieAttached { get; private set; }
        public bool IsLcdAttached { get; private set; }
        public bool IsBroadcasting { get; private set; }
        public bool IsUploading { get; private set; }
        public byte LcdVolume { get; private set; }
        public byte LcdBrightness { get; private set; }
        public byte LcdSleepTimer { get; private set; }
        public new VideoResolution VideoResolution { get; private set; }
        public FrameRate FrameRate { get; private set; }

        protected internal override void Update(Stream stream)
        {
            using (var binReader = new BinaryReader(stream))
            {
                binReader.ReadByte();

                base.FillSettings(binReader);

                HlsSegmentSize = binReader.ReadByte();
                BurstRate = binReader.ReadEnum<BurstRate>();
                ContinuousShot = binReader.ReadEnum<ContinuousShot>();
                WhiteBalance = binReader.ReadEnum<WhiteBalance>();
                BracketingMode = binReader.ReadByte();
                PhotoInVideo = binReader.ReadByte();
                LoopingVideoMode = binReader.ReadEnum<LoopingVideo>();
                SlideshowSettings = binReader.ReadByte();
                BroadcastSettings = binReader.ReadByte();
                TimeLapseStyle = binReader.ReadByte();
                VideoLoopCounter = binReader.ReadInt32();
                ExternalBattery = binReader.ReadByte();

                var field = binReader.ReadByte();
                IsBombieAttached = (byte) (field & 0x8) > 0;
                IsLcdAttached = (byte) (field & 0x4) > 0;
                IsBroadcasting = (byte) (field & 0x2) > 0;
                IsUploading = (byte) (field & 0x1) > 0;

                LcdVolume = binReader.ReadByte();
                LcdBrightness = binReader.ReadByte();
                LcdSleepTimer = binReader.ReadByte();
                VideoResolution = binReader.ReadEnum<VideoResolution>();
                FrameRate = binReader.ReadEnum<FrameRate>();
            }
        }
    }
}