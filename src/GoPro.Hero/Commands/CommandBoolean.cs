using System.Collections.Generic;
using GoPro.Hero.Filtering;

namespace GoPro.Hero.Commands
{
    public abstract class CommandBoolean<TO> : CommandRequest<TO> where TO : IFilterProvider<TO>
    {
        protected const string ON = "%01";
        protected const string OFF = "%00";

        public virtual bool State
        {
            get { return !string.IsNullOrEmpty(base.Parameter) && base.Parameter != OFF; }
            set { base.Parameter = value ? ON : OFF; }
        }

        public CommandBoolean<TO> Set(bool state)
        {
            State = state;
            return this;
        }

        public CommandBoolean<TO> Enable()
        {
            State = true;
            return this;
        }

        public CommandBoolean<TO> Disable()
        {
            State = false;
            return this;
        }

        public IEnumerable<bool> ValidStates()
        {
            return base.Filter.GetValidStates<CommandBoolean<TO>>(this.GetType().Name);
        }

        public CommandBoolean<TO> ValidStates(out IEnumerable<bool> validStates)
        {
            validStates = base.Filter.GetValidStates<CommandBoolean<TO>>(this.GetType().Name);
            return this;
        }
    }
}