using System;
using System.Net;
using System.Reflection;
using GoPro.Hero.Api.Exceptions;
using GoPro.Hero.Api.Filtering;

namespace GoPro.Hero.Api.Commands
{
    public class CommandRequest<TO> where TO : IFilterProvider
    {
        protected string Address;
        protected string Command;
        protected IFilter<TO> Filter;
        protected string Parameter;
        protected string PassPhrase;

        protected CommandRequest()
        {
        }

        public TO Owner { get; protected set; }

        public virtual Uri GetUri()
        {
            return CreateUri(Address, Command, PassPhrase, Parameter);
        }

        public CommandResponse Send(bool checkStatus = true)
        {
            var response = Send(this);

            if (!checkStatus) return response;

            if (response.Status != CommandResponse.ResponseStatus.Ok)
                throw new CommandFailedException();

            return response;
        }

        public TO Execute(bool checkStatus = true)
        {
            Send(checkStatus);
            return Owner;
        }

        public CommandRequest<TO> ExecuteSelf(bool checkStatus = true)
        {
            Send(checkStatus);
            return this;
        }

        protected virtual void Initialize()
        {
            Filter = (Owner as IFilterProvider).Filter() as IFilter<TO>;

            var type = GetType();
            var att = type.GetCustomAttributes(typeof (CommandAttribute), true);
            if (att.Length == 0) return;
            var commandAtt = att[0] as CommandAttribute;
            if (commandAtt == null) return;
            Command = commandAtt.Command;
            if (commandAtt.InSecure) PassPhrase = null;
            if (commandAtt.Parameterless) Parameter = null;
        }

        public static CommandRequest<TO> Create(TO owner, string address, string command, string passPhrase = null,
                                               string parameter = null)
        {
            return new CommandRequest<TO>
                {
                    Owner = owner,
                    Address = address,
                    Command = command,
                    PassPhrase = passPhrase,
                    Parameter = parameter
                };
        }

        public static T Create<T>(TO owner, string address = null, string command = null, string passPhrase = null,
                                  string parameter = null) where T : CommandRequest<TO>
        {
            var request = Activator.CreateInstance<T>();
            request.Address = address;
            request.Command = command;
            request.Parameter = parameter;
            request.PassPhrase = passPhrase;
            request.Owner = owner;
            request.Initialize();

            return request;
        }

        protected static Uri CreateUri(string address, string command, string passPhrase = null, string parameter = null)
        {
            var addressParts = address.Split(':');

            var builder = addressParts.Length == 1
               ? new UriBuilder { Host = address, Path = command }
               : new UriBuilder { Host = addressParts[0], Port = int.Parse(addressParts[1]), Path = command };
                

            if (!string.IsNullOrEmpty(passPhrase))
                builder.Query = string.Format("t={0}", passPhrase);

            if (!string.IsNullOrEmpty(parameter))
                builder.Query = string.IsNullOrEmpty(builder.Query)
                                    ? string.Format("p={0}", parameter)
                                    : string.Format("t={0}&p={1}", passPhrase, parameter);
            return builder.Uri;
        }

        private static CommandResponse Send(CommandRequest<TO> command)
        {
            var request = WebRequest.Create(command.GetUri()) as HttpWebRequest;
            //request.KeepAlive=true;
            //request.ProtocolVersion=HttpVersion.Version11;
            //request.SendChunked = true;
            //request.TransferEncoding="ISO-8859-1";

            if (request != null)
            {
                var asyncResponse = request.BeginGetResponse(null, null);
                asyncResponse.AsyncWaitHandle.WaitOne();
          
                using (var response = request.EndGetResponse(asyncResponse) as HttpWebResponse)
                {
                    if (response != null)
                    {
                        var stream = response.GetResponseStream();
                        var buffer = new byte[response.ContentLength];
                        stream.Read(buffer, 0, buffer.Length);
                        stream.Dispose();
                        return CommandResponse.Create(buffer);
                    }
                }
            }
            throw new GoProException();
        }

        public override string ToString()
        {
            return GetUri().ToString();
        }
    }
}