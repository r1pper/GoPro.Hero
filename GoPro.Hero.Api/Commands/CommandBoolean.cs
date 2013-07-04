using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GoPro.Hero.Api.Commands
{
    public abstract class CommandBoolean:CommandRequest
    {
        protected const string ON = "%01";
        protected const string OFF = "%00";

        public virtual bool Enable
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
    }
}
