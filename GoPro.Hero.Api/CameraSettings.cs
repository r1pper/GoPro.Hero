using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GoPro.Hero.Api
{
    public class CameraSettings
    {
        public byte Mode { get; private set; }
        public byte MicrophoneMode { get; private set; }
        public byte OnDefault { get; private set; }
        public byte Exposure { get; private set; }
        public byte TimeLapse { get; private set; }
        public byte AutoPowerOff { get; private set; }
        public byte FieldOfView { get; private set; }
        public byte PhotoResolution { get; private set; }
        public byte VideoResolution { get; private set; }
        public byte AudioInput { get; private set; }
        public byte PlayMode { get; private set; }
        public byte PlaybackPosition { get; private set; }
        public byte BeepSound { get; private set; }
        public byte LedBlink { get; private set; }

        public bool OnScreen { get; private set; }
        public bool OneButton { get; private set; }
        public bool Orientation { get; private set; }
        public bool LiveFeed { get; private set; }
        public bool LocateCamera { get; private set; }
        public bool Ntsc { get; private set; }
        public bool PreviewAvailable { get; private set; }

        public byte Battery { get; private set; }
        public byte UsbMode { get; private set; }
        public byte PhotosAvailableSpace { get; private set; }
        public byte PhotosCount { get; private set; }
        public byte VideosAvailableSpace { get; private set; }
        public byte VideosCount { get; private set; }
        public byte Shutter { get; private set; }

        public bool Busy { get; private set; }
        public bool ProTune { get; private set; }

    }
}
