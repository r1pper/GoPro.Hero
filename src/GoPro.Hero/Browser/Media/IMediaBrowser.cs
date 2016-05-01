using GoPro.Hero.Filtering;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GoPro.Hero.Browser.Media
{
    public interface IMediaBrowser<T>:IGeneralBrowser<T> where T :ICamera<T>,IFilterProvider<T>
    {
        Task<IMedia<T>> ContentAsync(string name);
        IMedia<T> Content(string name);

        Task<IEnumerable<IMedia<T>>> ContentsAsync();
        IEnumerable<IMedia<T>> Contents();
    }
}
