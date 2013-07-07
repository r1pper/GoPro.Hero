using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GoPro.Hero.Api.Commands;

namespace GoPro.Hero.Api.Filtering
{
    public interface IFilter<O>where O:IFilterProvider
    {
        void Initialize(O owner);

        IEnumerable<T> GetValidStates<T, C>() where C : CommandMultiChoice<T, O>;
        IEnumerable<bool> GetValidStates<C>() where C : CommandBoolean<O>;
    }
}
