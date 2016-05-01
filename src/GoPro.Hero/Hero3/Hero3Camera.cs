using System;
using System.Collections.Generic;
using GoPro.Hero.Browser;
using GoPro.Hero.Commands;
using GoPro.Hero.Filtering;
using System.Linq;
using GoPro.Hero.Browser.FileSystem;
using GoPro.Hero.Browser.Media;
using System.Threading.Tasks;
using GoPro.Hero.Utilities;

namespace GoPro.Hero.Hero3
{
    public class Hero3Camera : Camera
    {
        public Hero3Camera(Bacpac bacpac) : base(bacpac)
        {
            var filter = FilterGeneric.Create("GoPro.Hero.Hero3.Hero3FilterScheme.xml");
            SetFilter(filter);
        }

        public Hero3Camera(string address) : base(Bacpac.Create(address))
        {
        }

    }
}