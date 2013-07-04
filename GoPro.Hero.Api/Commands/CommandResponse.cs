using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace GoPro.Hero.Api.Commands
{
    public class CommandResponse
    {
        public enum ResponseStatus:byte{Ok=0,Busy=2};

        public byte[] RawResponse { get; protected set; }
        public ResponseStatus Status { get { return (ResponseStatus)this.RawResponse[0]; } }

        public Stream GetResponseStream()
        {
            return new MemoryStream(this.RawResponse);
        }

        public static CommandResponse Create(byte[] response)
        {
            return new CommandResponse { RawResponse = response };
        }

    }
}
