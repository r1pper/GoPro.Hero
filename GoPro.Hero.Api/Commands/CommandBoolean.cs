using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GoPro.Hero.Api.Filtering;

namespace GoPro.Hero.Api.Commands
{
    public abstract class CommandBoolean<O>:CommandRequest<O> where O:IFilterProvider
    {
        protected const string ON = "%01";
        protected const string OFF = "%00";

        public virtual bool State
        {
            get
            {
                return string.IsNullOrEmpty(base.parameter) ? false : base.parameter == OFF ? false : true;
            }
            set
            {
                base.parameter = value ? ON : OFF;
            }
        }

        public CommandBoolean<O> Set(bool state)
        {
            this.State = state;
            return this;
        }

        public CommandBoolean<O> Enable()
        {
            this.State = true;
            return this;
        }

        public CommandBoolean<O> Disable()
        {
            this.State = false;
            return this;
        }

        public IEnumerable<bool> ValidStates()
        {
            return base.filter.GetValidStates<CommandBoolean<O>>();
        }

        public CommandBoolean<O> ValidStates(out IEnumerable<bool> validStates)
        {
            validStates = base.filter.GetValidStates<CommandBoolean<O>>();
            return this;
        }
    }
}
