using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GoPro.Hero.Browser.Media
{
    public abstract class MediaParameters
    {
        public string Name { get; set; }
        public long Size { get; set; }
    }

    public class VideoParameters : MediaParameters
    {
        public long LowResolutionSize { get; set; }

        public int? Duration { get; set; }
        public int? Profile { get; set; }
    }

    public class ImageParameters : MediaParameters { }

    public class TimeLapsedImageParameters : MediaParameters
    {
        public int Group { get; set; }
        public int Start { get; set; }
        public int End { get; set; }
    }
}
