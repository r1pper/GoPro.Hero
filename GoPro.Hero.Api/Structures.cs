using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GoPro.Hero.Api
{
    public enum Orientation:byte { Up = 0, Down = 1 }
    public enum VideoStandard:byte { Ntsc = 0, Pal = 1 }
    public enum WhiteBalance:byte { Auto=0, Wb3000K=1, Wb5500K=2, Wb6500K=3, Raw=4 }
    public enum BeepSound:byte { S100Percent=2, S70Percent=1, Off=0 }
    public enum LedBlink { Four=2, Two=1, Zero=0 }
    public enum Mode:byte { Video=0, Photo=1, Burst=2, TimeLapse=3 }
    public enum TimeLapse { Half=0x0, One=0x1, Two=0x2, Five=0x5, Ten=0xa, Thirty=0x1e, Sixty=0x3c }
    public enum BurstRate { Sec3Per1=0, Sec5Per1=1, Sec10Per1=2, Sec10Per2=3, Sec30Per1=4, Sec30Per2=5, Sec30Per3=6 }
    public enum ContinuousShot { Single=0x0, Sps3=0x3, Sps5=0x5, Sps10=0xa }
    public enum PhotoResolution { Wide12Mp=5, Wide7Mp=4, Medium7Mp=6, Medium5Mp=3 }
    public enum FieldOfView { Wide=0, Medium=1, Narrow=2 }
    public enum VideoResolution:byte { Vr4kCin=8, Vr4K=6, Vr2Point7KCin=7, Vr2Point7=5, Vr1440=4, Vr1080=3, Vr960=2, Vr720=1, Wvga=0 }
    public enum FrameRate { Fps12=0x0,Fps15=0x1, Fps12p5=0xb, Fps24=0x2,Fps25=0x3, Fps30=0x4, Fps48=0x5, Fps50=0x6, Fps60=0x7, Fps100=0x8, Fps120=0x9, Fps240=0xa }
    public enum Model : byte { Hero2 = 1, Hero3White = 2, Hero3Silver = 3, Hero3Black = 4 }
    public enum LoopingVideo : byte { Off = 0x0, Max = 5, Min5 = 1, Min20 = 2, Min60 = 3, Min120 = 4 }

}
