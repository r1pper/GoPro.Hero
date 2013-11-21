using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoPro.Hero.Browser.Media
{
    internal sealed class MediaBuilder<TM>:Media<TM>where TM:MediaParameters
    {
        public new static T Create<T>(TM token, IGeneralBrowser browser) where T : Media<TM>, IMediaInitializer<TM>
        {
            return Media<TM>.Create<T>(token, browser);
        }
    }
}
