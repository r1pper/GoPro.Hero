using System.Collections.Generic;
using GoPro.Hero.Api.Commands;

namespace GoPro.Hero.Api.Filtering
{
    public interface IFilter<TO> where TO : IFilterProvider
    {
        void Initialize(TO owner);

        IEnumerable<T> GetValidStates<T, TC>(string command) where TC : CommandMultiChoice<T, TO>;
        IEnumerable<bool> GetValidStates<TC>(string command) where TC : CommandBoolean<TO>;
    }
}