using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;

namespace GoPro.Hero.Api.Browser.Media
{
    interface IMedia
    {
        void Initialize(JToken token, IGeneralBrowser browser);
    }
}
