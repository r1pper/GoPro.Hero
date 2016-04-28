using System;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using GoPro.Hero.Exceptions;
using GoPro.Hero.Filtering;

namespace GoPro.Hero.Commands
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
            var task = SendAsync(checkStatus);
            task.Wait();
            return task.Result;
        }

        public async Task<CommandResponse> SendAsync(bool checkStatus = true)
        {
            var response = await SendRequestAsync();

            if (!checkStatus) return response;

            if (response.Status != CommandResponse.ResponseStatus.Ok)
                throw new CommandFailedException();

            return response;
        }

        public TO Execute(bool checkStatus = true, bool nonBlocking = false)
        {
            var task = SendAsync(checkStatus);

            if (!nonBlocking)
                task.Wait();

            return Owner;
        }

        public async Task<TO> ExecuteAsync(bool checkStatus = true)
        {
            await SendAsync(checkStatus);
            return Owner;
        }

        public CommandRequest<TO> ExecuteSelf(bool checkStatus = true, bool nonBlocking = false)
        {
            var task = SendAsync(checkStatus);

            if (!nonBlocking)
                task.Wait();

            return this;
        }

        public async Task<CommandRequest<TO>> ExecuteSelfAsync(bool checkStatus = true)
        {
            await SendAsync(checkStatus);
            return this;
        }

        protected virtual void Initialize()
        {
            Filter = (Owner as IFilterProvider).Filter() as IFilter<TO>;

            var type = GetType().GetTypeInfo();
            var commandAtt = type.GetCustomAttribute<CommandAttribute>(true);
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

        protected CommandResponse SendRequest()
        {
            var task = SendRequestAsync();
            task.Wait();
            return task.Result;
        }

        protected virtual async Task<CommandResponse> SendRequestAsync()
        {
            return Configuration.CommandRequestMode == Configuration.HttpRequestMode.Async ?
                await SendRequestAsynchronous() :
                SendRequestSynchronous();
        }

        private async Task<CommandResponse> SendRequestAsynchronous()
        {
            var request = WebRequest.Create(GetUri()) as HttpWebRequest;
            //request.KeepAlive=true;
            //request.ProtocolVersion=HttpVersion.Version11;
            //request.SendChunked = true;
            //request.TransferEncoding="ISO-8859-1";

            if (request != null)
            {
                using (var response = await request.GetResponseAsync() as HttpWebResponse)
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

        private CommandResponse SendRequestSynchronous()
        { //NOTE: this method does not work in WP and WinRT devices
            var request = WebRequest.Create(GetUri()) as HttpWebRequest;

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