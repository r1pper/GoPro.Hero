using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GoPro.Hero.Api.Commands;
using GoPro.Hero.Api.Commands.CameraCommands;
using GoPro.Hero.Api.Hero3;

namespace GoPro.Hero.Api.Filtering
{
    class FilterGeneric:IFilter<ICamera>
    {
        private ICamera _owner;

        public void Initialize(ICamera owner)
        {
            _owner = owner;
        }

        public IEnumerable<T> GetValidStates<T, C>() where C : CommandMultiChoice<T, ICamera>
        {
            throw new NotImplementedException();
        }

        public IEnumerable<bool> GetValidStates<C>() where C : CommandBoolean<ICamera>
        {
            return new[] { true, false };
        }
    }
}
