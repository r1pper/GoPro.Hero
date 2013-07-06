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

        bool IsAvailable(CommandRequest<O> command);
        bool IsAvailable<C>() where C : CommandRequest<O>;
        O Available<C>(out bool state) where C : CommandRequest<O>;

        IEnumerable<T> GetValidStates<T>(CommandMultiChoice<T, O> command);
        IEnumerable<T> GetValidStates<T, C>() where C : CommandMultiChoice<T, O>;
        O ValidStates<T, C>(out IEnumerable<T> validStates) where C : CommandMultiChoice<T, O>;

        IEnumerable<bool> GetValidStates(CommandBoolean<O> command);
        IEnumerable<bool> GetValidStates<C>() where C : CommandBoolean<O>;
        O ValidStates<C>(out IEnumerable<bool> validStates) where C : CommandBoolean<O>;

        bool IsValid(CommandRequest<O> command);
        O Valid(CommandRequest<O> command, out bool state);
    }
}
