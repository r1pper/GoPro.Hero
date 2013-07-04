using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GoPro.Hero.Api.Commands
{
    public abstract class CommandMultiChoice<T, O> : CommandRequest<O>
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
    }
}
