using GoPro.Hero.Filtering;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GoPro.Hero.Browser.Media
{
    public interface IMediaBrowser<T>:IMediaBrowser,IGeneralBrowser<T> where T :ICamera<T>,IFilterProvider<T>
    {
    }

    public interface IMediaBrowser
    {
        Uri Address { get; }
        string Destination { get; }

        Task<IMedia> ContentAsync(string name);
        IMedia Content(string name);

        Task<IEnumerable<IMedia>> ContentsAsync();
        IEnumerable<IMedia> Contents();
    }
}
