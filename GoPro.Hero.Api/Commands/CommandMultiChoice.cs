using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GoPro.Hero.Api.Filtering;

namespace GoPro.Hero.Api.Commands
{
    public abstract class CommandMultiChoice<T, O> : CommandRequest<O> where O:IFilterProvider
    {
        public T Selection
        {
            get
            {
                if (string.IsNullOrEmpty(base.parameter))
                    return default(T);

                var num = int.Parse(base.parameter.Substring(1));
                return (T)Enum.ToObject(typeof(T), num);
            }

            set
            {
                base.parameter = "%" + Convert.ToInt32(value).ToString("x2");
            }
        }

        public CommandMultiChoice<T, O> Select(T mode)
        {
            this.Selection = mode;
            return this;
        }

        public IEnumerable<T> ValidStates()
        {
            return base.filter.GetValidStates<T,CommandMultiChoice<T, O>>();
        }

        public CommandMultiChoice<T, O> ValidStates(out IEnumerable<T> validStates)
        {
            validStates = base.filter.GetValidStates<T,CommandMultiChoice<T, O>>();
            return this;
        }
    }
}
