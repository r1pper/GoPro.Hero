using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GoPro.Hero.Api
{
    public enum Orientation { Up = 0, Down = 1 }
    public enum VideoStandard { Ntsc = 0, Pal = 1 }
    public enum WhiteBalance { Auto, Wb3000K, Wb5500K, Wb6500K, Raw }
    public enum Sound { S100Percent, S70Percent, Off }
    public enum Led { Four, Two, Zero }
    public enum Mode { Video, Photo, Burst, TimeLapse }
    public enum TimeLapse { Half, One, Two, Five, Ten, Thirty, Sixty }
    public enum BurstRate { Sec3Per1, Sec5Per1, Sec10Per1, Sec10Per2, Sec30Per1, Sec30Per2, Sec30Per3 }
    public enum ContiniusShot { Single, Sps3, Sps5, Sps10 }
    public enum PhotoResolution { Wide12Mp, Wide7Mp, Medium7Mp, Medium5Mp }
    public enum FieldOfView { Wide, Medium, Narrow }
    public enum VideoResolution { Vr4kCin, Vr4K, Vr2Point7KCin, Vr2Point7, Vr1440, Vr1080, Vr960, Vr720 }
    public enum FrameRate { Fps12, Fps24Fps25, Fps30, Fps48, Fps50, Fps60, Fps100, Fps120, Fps240 }

}
