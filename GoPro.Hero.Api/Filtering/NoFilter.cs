using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GoPro.Hero.Api.Utilities;

namespace GoPro.Hero.Api.Filtering
{
    class NoFilter<O>:IFilter<O>where O:IFilterProvider
    {
        private O _owner;

        public void Initialize(O owner)
        {
            _owner = owner;
        }

        public bool IsAvailable(Commands.CommandRequest<O> command)
        {
            return true;
        }

        public bool IsAvailable<C>() where C : Commands.CommandRequest<O>
        {
            return true;
        }

        public O Available<C>(out bool state) where C : Commands.CommandRequest<O>
        {
            state = true;
            return _owner;
        }

        public IEnumerable<T> GetValidStates<T>(Commands.CommandMultiChoice<T, O> command)
        {
            return Extensions.GetValues<T>();
        }

        public IEnumerable<T> GetValidStates<T, C>() where C : Commands.CommandMultiChoice<T, O>
        {
            return Extensions.GetValues<T>();
        }

        public O ValidStates<T, C>(out IEnumerable<T> validStates) where C : Commands.CommandMultiChoice<T, O>
        {
            validStates = Extensions.GetValues<T>();
            return _owner;
        }

        public IEnumerable<bool> GetValidStates(Commands.CommandBoolean<O> command)
        {
            return new[] { true, false };
        }

        public IEnumerable<bool> GetValidStates<C>() where C : Commands.CommandBoolean<O>
        {
            return new[] { true, false };
        }

        public O ValidStates<C>(out IEnumerable<bool> validStates) where C : Commands.CommandBoolean<O>
        {
            validStates = new[] { true, false };
            return _owner;
        }

        public bool IsValid(Commands.CommandRequest<O> command)
        {
            return true;
        }

        public O Valid(Commands.CommandRequest<O> command, out bool state)
        {
            state = true;
            return _owner;
        }
    }
}
