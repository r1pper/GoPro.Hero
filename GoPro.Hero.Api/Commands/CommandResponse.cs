using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GoPro.Hero.Api.Commands
{
    class CommandResponse
    {
        public byte[] RawResponse { get; protected set; }

        public static CommandResponse Create(byte[] response)
        {
            return new CommandResponse { RawResponse = response };
        }
    }
}
