using System;
using System.Reflection;
using System.Threading.Tasks;
using GoPro.Hero.Exceptions;
using GoPro.Hero.Filtering;
using System.Net.Http;
using System.IO;
using GoPro.Hero.Utilities;

namespace GoPro.Hero.Commands
{
    public interface ICommandRequest
    {
        CommandResponse Send(bool checkStatus = true);
        Task<CommandResponse> SendAsync(bool checkStatus = true);
        void Execute(bool checkStatus = true);
        Task ExecuteAsync(bool checkStatus = true);
    }

    public class CommandRequest : ICommandRequest
    {
        protected string Address;
        protected string Command;
        protected string Parameter;
        protected string PassPhrase;

        protected virtual void Initialize()
        {
            var type = GetType().GetTypeInfo();
            var commandAtt = type.GetCustomAttribute<CommandAttribute>(true);
            if (commandAtt == null) return;
            Command = commandAtt.Command;
            if (commandAtt.InSecure) PassPhrase = null;
            if (commandAtt.Parameterless) Parameter = null;
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
            return AsyncHelpers.RunSync(SendRequestAsync);
        }

        protected virtual async Task<CommandResponse> SendRequestAsync()
        {
            var uri = GetUri();
            var request = new HttpClient();
            var response = await request.GetAsync(uri);

            using (var ms = new MemoryStream())
            {
                await response.Content.CopyToAsync(ms);
                return CommandResponse.Create(ms.ToArray());
            }
            throw new GoProException();
        }

        public CommandResponse Send(bool checkStatus = true)
        {
            var response = SendRequest();

            if (!checkStatus) return response;

            if (response.Status != CommandResponse.ResponseStatus.Ok)
                throw new CommandFailedException();

            return response;
        }

        public async Task<CommandResponse> SendAsync(bool checkStatus = true)
        {
            var response = await SendRequestAsync();

            if (!checkStatus) return response;

            if (response.Status != CommandResponse.ResponseStatus.Ok)
                throw new CommandFailedException();

            return response;
        }

        void ICommandRequest.Execute(bool checkStatus)
        {
            Send(checkStatus);
        }

        public async Task ExecuteAsync(bool checkStatus = true)
        {
            await SendAsync(checkStatus);
        }

        public virtual Uri GetUri()
        {
            return CreateUri(Address, Command, PassPhrase, Parameter);
        }

        public override string ToString()
        {
            return GetUri().ToString();
        }


        public static CommandRequest Create( string address, string command, string passPhrase = null,
                                       string parameter = null)
        {
            return new CommandRequest
            {
                Address = address,
                Command = command,
                PassPhrase = passPhrase,
                Parameter = parameter
            };
        }

        public static T Create<T>(string address = null, string command = null, string passPhrase = null,
                                  string parameter = null) where T : CommandRequest
        {
            var request = Activator.CreateInstance<T>();
            request.Address = address;
            request.Command = command;
            request.Parameter = parameter;
            request.PassPhrase = passPhrase;
            request.Initialize();

            return request;
        }
    }

    public class CommandRequest<TO>:CommandRequest,ICommandRequest where TO : IFilterProvider<TO>
    {     
        protected IFilter<TO> Filter;

        protected CommandRequest()
        {
        }

        public TO Owner { get; private set; }

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

        public async Task<CommandRequest<TO>> ExecuteSelfAsync(bool checkStatus = true)
        {
            await SendAsync(checkStatus);
            return this;
        }

        protected override void Initialize()
        {
            Filter = (Owner as IFilterProvider<TO>).Filter();
            base.Initialize();
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

        public override string ToString()
        {
            return GetUri().ToString();
        }

    }
}