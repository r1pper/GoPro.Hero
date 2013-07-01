using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace GoPro.Hero.Api.Commands
{
    class CommandRequest
    {
        protected string address;
        protected string command;
        protected string passPhrase;
        protected string parameter;

        public virtual Uri GetUri()
        {
            return CreateUri(address, command, passPhrase, parameter);
        }

        protected CommandRequest() { }

        protected virtual void Initialize() { }

        public static CommandRequest Create(string address, string command, string passPhrase = null, string parameter = null)
        {
            return new CommandRequest { address = address, command = command, passPhrase = passPhrase, parameter = parameter };
        }

        public static T Create<T>(string address=null, string command=null, string passPhrase = null, string parameter = null) where T : CommandRequest
        {
            var request = Activator.CreateInstance<T>();
            request.address = address;
            request.command = command;
            request.parameter = parameter;
            request.passPhrase = passPhrase;
            request.Initialize();
            
            return request;
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
                    : string.Format("t={0}&p={1}", passPhrase, parameter);
            return builder.Uri;
        }
    }
}
