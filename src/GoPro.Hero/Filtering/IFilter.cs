using System.Collections.Generic;
using GoPro.Hero.Commands;

namespace GoPro.Hero.Filtering
{
    public interface IFilter<TO> where TO : IFilterProvider<TO>
    {
        void Initialize(TO owner);

        IEnumerable<T> GetValidStates<T, TC>(string command) where TC : ICommandMultiChoice<T, TO,TC>;
        IEnumerable<bool> GetValidStates<TC>(string command) where TC : ICommandBoolean<TO,TC>;
    }
}