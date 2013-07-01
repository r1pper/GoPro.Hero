using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace GoPro.Hero.Api.Commands
{
    class CommandRequest
    {
        protected Uri commandUri;

        public virtual Uri GetUri()
        {
            return commandUri;
        }

        public static CommandRequest Create(string address, string command, string passPhrase = null, string parameter = null)
        {
            var uri = CreateUri(address, command, passPhrase, parameter);

            return new CommandRequest { commandUri = uri };
        }

        protected static Uri CreateUri(string address, string command, string passPhrase = null, string parameter = null)
        {
            var builder = new UriBuilder();
            builder.Host = address;
            builder.Path = command;

            if (!string.IsNullOrEmpty(passPhrase))
                builder.Query = string.Format("t={0}", passPhrase);

            if (!string.IsNullOrEmpty(parameter))
                builder.Query = string.IsNullOrEmpty(builder.Query)
                    ? string.Format("p={0}", parameter)
                    : string.Format("{0}&p={1}", builder.Query, parameter);
            return builder.Uri;
        }
    }
}
