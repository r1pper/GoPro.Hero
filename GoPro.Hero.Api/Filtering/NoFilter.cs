using System.Collections.Generic;
using GoPro.Hero.Api.Commands;
using GoPro.Hero.Api.Utilities;

namespace GoPro.Hero.Api.Filtering
{
    internal class NoFilter<TO> : IFilter<TO> where TO : IFilterProvider
    {
        public TO Owner { get; set; }

        public void Initialize(TO owner)
        {
            Owner = owner;
        }

        public IEnumerable<T> GetValidStates<T, TC>() where TC : CommandMultiChoice<T, TO>
        {
            return Extensions.GetValues<T>();
        }

        public IEnumerable<bool> GetValidStates<TC>() where TC : CommandBoolean<TO>
        {
            return new[] {true, false};
        }
    }
}