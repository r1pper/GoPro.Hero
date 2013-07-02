using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace GoPro.Hero.Api
{
    public class CameraExtendedSettings:CameraSettings
    {
        public byte HlsSegmentSize{get;private set;}
        public byte BurstMode{get;private set;}
        public byte ContiniousShot{get;private set;}
        public byte WhiteBalance {get;private set;}
        public byte BracketingMode {get;private set;}
        public byte PhotoInVideo {get;private set;}
        public byte LoopingVideoMode {get;private set;}
        public byte  SlideshowSettings {get;private set;}
        public byte BroadcastSettings {get;private set;}
        public byte TimeLapseStyle{get;private set;}
        public int VideoLoopCounter{get;private set;}
        public byte ExternalBattery {get;private set;}
        public byte BombieAttached {get;private set;}
        public byte LcdAttached {get;private set;}
        public byte IsBroadcasting {get;private set;}
        public byte IsUploading {get;private set;}
        public byte LcdVolume {get;private set;}
        public byte LcdBrightness{get;private set;}
        public byte LcdSleepTimer {get;private set;}
        public byte VideoResolution {get;private set;}
        public byte FrameRate{get;private set;}

        internal void Update(Stream stream)
        {
            using (var binReader = new BinaryReader(stream))
            {
                binReader.ReadByte();

                base.FillSettings(binReader);

                this.HlsSegmentSize = binReader.ReadByte();
                this.BurstMode = binReader.ReadByte();
                this.ContiniousShot = binReader.ReadByte();
                this.WhiteBalance = binReader.ReadByte();
                this.BracketingMode = binReader.ReadByte();
                this.PhotoInVideo = binReader.ReadByte();
                this.LoopingVideoMode = binReader.ReadByte();
                this.SlideshowSettings = binReader.ReadByte();
                this.BroadcastSettings = binReader.ReadByte();
                this.TimeLapseStyle = binReader.ReadByte();
                this.VideoLoopCounter = binReader.ReadInt32();
                this.ExternalBattery = binReader.ReadByte();

                var field = binReader.ReadByte();
                this.BombieAttached = (byte)(field & 0x8);
                this.LcdAttached = (byte)(field & 0x4);
                this.IsBroadcasting = (byte)(field & 0x2);
                this.IsUploading = (byte)(field & 0x1);

                this.LcdVolume = binReader.ReadByte();
                this.LcdBrightness = binReader.ReadByte();
                this.LcdSleepTimer = binReader.ReadByte();
                this.VideoResolution = binReader.ReadByte();
                this.FrameRate = binReader.ReadByte();
            }
        }
  
    }
}
