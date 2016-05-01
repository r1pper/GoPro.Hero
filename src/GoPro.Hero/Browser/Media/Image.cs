using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GoPro.Hero.Commands;
using Newtonsoft.Json.Linq;
using GoPro.Hero.Filtering;

namespace GoPro.Hero.Browser.Media
{
    public class Image<T> : Media<T,ImageParameters> where T :ICamera<T>,IFilterProvider<T>
    {
        public override string ToString()
        {
            return string.Format("image:{0}", base.Name);
        }
    }
}
