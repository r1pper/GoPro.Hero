using System.Collections.Generic;
using GoPro.Hero.Filtering;

namespace GoPro.Hero.Commands
{
    public interface ICommandMultiChoice<T, TO,TS> where TO : IFilterProvider<TO> where TS :ICommandMultiChoice<T,TO,TS>
    {
        T Selection { get; set; }

        TS Select(T mode);
        IEnumerable<T> ValidStates();
    }
}