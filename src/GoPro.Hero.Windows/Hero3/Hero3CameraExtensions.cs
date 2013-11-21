using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GoPro.Hero.Browser.Media;

namespace GoPro.Hero.Hero3
{
    public static class Hero3CameraExtensions
    {
        public static MediaBrowser Contents(this Hero3Camera camera)
        {
            return camera.Browse<GoProMediaBrowser>();
        }
    }
}
