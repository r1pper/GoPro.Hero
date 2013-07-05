using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using GoPro.Hero.Api.Exceptions;
using System.IO;

namespace GoPro.Hero.Api.Commands
{
    public class CommandRequest<O>
    {
        protected string address;
        protected string command;
        protected string passPhrase;
        protected string parameter;

        public O Owner { get; protected set; }

        public virtual Uri GetUri()
        {
            return CreateUri(address, command, passPhrase, parameter);
        }

        public CommandResponse Send(bool checkStatus=true)
        {
            var response = Send(this);

            if (!checkStatus) return response;

            if (response.Status != CommandResponse.ResponseStatus.Ok)
                throw new CommandFailedException();

            return response;
        }

        public O Execute(bool checkStatus = true)
        {
            this.Send(checkStatus);
            return this.Owner;
        }

        public CommandRequest<O> ExecuteSelf(bool checkStatus = true)
        {
            this.Send(checkStatus);
            return this;
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

        public static CommandRequest<O> Create(O owner,string address, string command, string passPhrase = null, string parameter = null)
        {
            return new CommandRequest<O> {Owner=owner, address = address, command = command, passPhrase = passPhrase, parameter = parameter };
        }

        public static T Create<T>(O owner,string address=null, string command=null, string passPhrase = null, string parameter = null) where T : CommandRequest<O>
        {
            var request = Activator.CreateInstance<T>();
            request.address = address;
            request.command = command;
            request.parameter = parameter;
            request.passPhrase = passPhrase;
            request.Owner = owner;
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

        private static CommandResponse Send(CommandRequest<O> command)
        {
            var request = HttpWebRequest.Create(command.GetUri()) as HttpWebRequest;
            //request.KeepAlive=true;
            //request.ProtocolVersion=HttpVersion.Version11;
            //request.SendChunked = true;
            //request.TransferEncoding="ISO-8859-1";

            var asyncResponse = request.BeginGetResponse(null, null);
            asyncResponse.AsyncWaitHandle.WaitOne();

            using (var response = request.EndGetResponse(asyncResponse) as HttpWebResponse)
            {
                var stream = response.GetResponseStream();
                var buffer = new byte[response.ContentLength];
                stream.Read(buffer, 0, buffer.Length);
                stream.Dispose();
                return CommandResponse.Create(buffer);
            }
        }

        public override string ToString()
        {
            return this.GetUri().ToString();
        }
    }
}
