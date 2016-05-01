using System.Collections.Generic;
using GoPro.Hero.Commands;

namespace GoPro.Hero.Filtering
{
    public interface IFilter<TO> where TO : IFilterProvider<TO>
    {
        void Initialize(TO owner);

        IEnumerable<T> GetValidStates<T, TC>(string command) where TC : CommandMultiChoice<T, TO>;
        IEnumerable<bool> GetValidStates<TC>(string command) where TC : CommandBoolean<TO>;
    }
}