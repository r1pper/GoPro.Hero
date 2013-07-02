using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using GoPro.Hero.Api.Exceptions;
using System.IO;

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

        public CommandResponse Send()
        {
            var response = Commando.Send(this);

            if (response.Status != CommandResponse.ResponseStatus.Ok)
                throw new CommandFailedException();

            return response;
        }

        protected CommandRequest() { }

        protected virtual void Initialize()
        {
            var type = this.GetType();
            var att = type.GetCustomAttributes(typeof(CommandAttribute), true);
            if (att.Length == 0) return;
            var commandAtt = att[0] as CommandAttribute;
            this.command = commandAtt.Command;
            if (commandAtt.InSecure) this.passPhrase = null;
            if (commandAtt.Parameterless) this.parameter = null;
        }

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

        public override string ToString()
        {
            return this.GetUri().ToString();
        }
    }
}
