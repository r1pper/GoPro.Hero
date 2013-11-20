using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoPro.Hero.Browser.Media
{
    public interface IMediaBrowser:IGeneralBrowser
    {
        Task<Media> ContentAsync(string name);
        Media Content(string name);

        Task<IEnumerable<Media>> ContentsAsync();
        IEnumerable<Media> Contents();
    }
}
