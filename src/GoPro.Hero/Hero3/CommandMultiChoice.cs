using System;
using System.Collections.Generic;
using GoPro.Hero.Filtering;
using GoPro.Hero.Commands;

namespace GoPro.Hero.Hero3
{
    public abstract class CommandMultiChoice<T, TO> : CommandRequest<TO>, ICommandMultiChoice<T, TO, CommandMultiChoice<T, TO>> where TO : IFilterProvider<TO>
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
    }
}