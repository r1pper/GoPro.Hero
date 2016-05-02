using System.Collections.Generic;
using GoPro.Hero.Commands;
using GoPro.Hero.Utilities;

namespace GoPro.Hero.Filtering
{
    internal class NoFilter<TO> : IFilter<TO> where TO : IFilterProvider<TO>
    {
        public TO Owner { get; set; }

        public void Initialize(TO owner)
        {
            Owner = owner;
        }

        public IEnumerable<T> GetValidStates<T, TC>(string command) where TC : ICommandMultiChoice<T, TO,TC>
        {
            return Extensions.GetValues<T>();
        }

        public IEnumerable<bool> GetValidStates<TC>(string command) where TC : ICommandBoolean<TO,TC>
        {
            return new[] {true, false};
        }
    }
}