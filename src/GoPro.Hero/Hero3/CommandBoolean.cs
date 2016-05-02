using System.Collections.Generic;
using GoPro.Hero.Filtering;
using GoPro.Hero.Commands;

namespace GoPro.Hero.Hero3
{
    public abstract class CommandBoolean<TO> : CommandRequest<TO>, ICommandBoolean<TO,CommandBoolean<TO>> where TO : IFilterProvider<TO>
    {
        protected const string ON = "%01";
        protected const string OFF = "%00";

        public virtual bool State
        {
            get { return !string.IsNullOrEmpty(Parameter) && Parameter != OFF; }
            set { Parameter = value ? ON : OFF; }
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
    }
}