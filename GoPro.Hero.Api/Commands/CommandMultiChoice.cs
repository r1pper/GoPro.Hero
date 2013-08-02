using System;
using System.Collections.Generic;
using GoPro.Hero.Api.Filtering;

namespace GoPro.Hero.Api.Commands
{
    public abstract class CommandMultiChoice<T, TO> : CommandRequest<TO> where TO : IFilterProvider
    {
        public T Selection
        {
            get
            {
                if (string.IsNullOrEmpty(base.Parameter))
                    return default(T);

                var num = int.Parse(base.Parameter.Substring(1));
                return (T) Enum.ToObject(typeof (T), num);
            }

            set { base.Parameter = "%" + Convert.ToInt32(value).ToString("x2"); }
        }

        public CommandMultiChoice<T, TO> Select(T mode)
        {
            Selection = mode;
            return this;
        }

        public IEnumerable<T> ValidStates()
        {
            return base.Filter.GetValidStates<T, CommandMultiChoice<T, TO>>(this.GetType().Name);
        }

        public CommandMultiChoice<T, TO> ValidStates(out IEnumerable<T> validStates)
        {
            validStates = base.Filter.GetValidStates<T, CommandMultiChoice<T, TO>>(this.GetType().Name);
            return this;
        }
    }
}