using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GoPro.Hero.Api.Commands
{
    public abstract class CommandMultiChoice<T>:CommandRequest
    {
       public T Select
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
                base.parameter = string.Format("%0{0}", Convert.ToString(Convert.ToByte(value), 16));
            }
        }
    }
}
