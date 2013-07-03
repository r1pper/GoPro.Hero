using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using GoPro.Hero.Api.Utilities;

namespace GoPro.Hero.Api
{
    public class CameraExtendedSettings:CameraSettings
    {
        public byte HlsSegmentSize{get;private set;}
        public BurstRate BurstRate{get;private set;}
        public ContinuousShot ContinuousShot{get;private set;}
        public WhiteBalance WhiteBalance {get;private set;}
        public byte BracketingMode {get;private set;}
        public byte PhotoInVideo {get;private set;}
        public byte LoopingVideoMode {get;private set;}
        public byte  SlideshowSettings {get;private set;}
        public byte BroadcastSettings {get;private set;}
        public byte TimeLapseStyle{get;private set;}
        public int VideoLoopCounter{get;private set;}
        public byte ExternalBattery {get;private set;}
        public bool IsBombieAttached {get;private set;}
        public bool IsLcdAttached {get;private set;}
        public bool IsBroadcasting {get;private set;}
        public bool IsUploading {get;private set;}
        public byte LcdVolume {get;private set;}
        public byte LcdBrightness{get;private set;}
        public byte LcdSleepTimer {get;private set;}
        public new VideoResolution VideoResolution {get;private set;}
        public FrameRate FrameRate{get;private set;}

        internal protected override void Update(Stream stream)
        {
            using (var binReader = new BinaryReader(stream))
            {
                binReader.ReadByte();

                base.FillSettings(binReader);

                this.HlsSegmentSize = binReader.ReadByte();
                this.BurstRate = binReader.ReadEnum<BurstRate>();
                this.ContinuousShot = binReader.ReadEnum<ContinuousShot>();
                this.WhiteBalance = binReader.ReadEnum<WhiteBalance>();
                this.BracketingMode = binReader.ReadByte();
                this.PhotoInVideo = binReader.ReadByte();
                this.LoopingVideoMode = binReader.ReadByte();
                this.SlideshowSettings = binReader.ReadByte();
                this.BroadcastSettings = binReader.ReadByte();
                this.TimeLapseStyle = binReader.ReadByte();
                this.VideoLoopCounter = binReader.ReadInt32();
                this.ExternalBattery = binReader.ReadByte();

                var field = binReader.ReadByte();
                this.IsBombieAttached = (byte)(field & 0x8)>0;
                this.IsLcdAttached = (byte)(field & 0x4)>0;
                this.IsBroadcasting = (byte)(field & 0x2)>0;
                this.IsUploading = (byte)(field & 0x1)>0;

                this.LcdVolume = binReader.ReadByte();
                this.LcdBrightness = binReader.ReadByte();
                this.LcdSleepTimer = binReader.ReadByte();
                this.VideoResolution = binReader.ReadEnum<VideoResolution>();
                this.FrameRate = binReader.ReadEnum<FrameRate>();
            }
        }
  
    }
}
