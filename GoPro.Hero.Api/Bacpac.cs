using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using GoPro.Hero.Api.Commands.BacpacCommands;
using GoPro.Hero.Api.Commands;
using GoPro.Hero.Api.Exceptions;
using System.IO;

namespace GoPro.Hero.Api
{
    public class Bacpac
    {
        public string Password { get; private set; }
        public string Address { get; private set; }

        public BacpacInformation Information { get; private set; }
        public BacpacStatus Status { get; private set; }

        public void UpdatePassword()
        {
            var request = this.CreateCommand<CommandRetrievePassword>();
            var response = Commando.Send(request);

            if (response.Status != CommandResponse.ResponseStatus.Ok)
                throw new BacpacException();

            var length = response.RawResponse[1];
            this.Password = Encoding.UTF8.GetString(response.RawResponse, 2, length);
        }

        public void UpdateStatus()
        {
            var request = this.CreateCommand<CommandStatus>();
            var response = Commando.Send(request);

            if (response.Status != CommandResponse.ResponseStatus.Ok)
                throw new BacpacException();

            var stream = response.GetResponseStream();
            this.Status.Update(stream);
        }

        public void UpdateInformation()
        {
            var request = this.CreateCommand<CommandInformation>();
            var response = Commando.Send(request);

            if (response.Status != CommandResponse.ResponseStatus.Ok)
                throw new BacpacException();

            var stream = response.GetResponseStream();
            this.Information.Update(stream);
        }

        public void Shoot()
        {
        }

        public void Power(bool on)
        {
            var request = this.CreateCommand<CommandPowerUp>();
            request.PowerUp = on;
            var response=Commando.Send(request);

            if (response.Status != CommandResponse.ResponseStatus.Ok)
                throw new BacpacException();
        }

        private T CreateCommand<T>(string parameter = null) where T : CommandRequest
        {
            var request = CommandRequest.Create<T>(this.Address, passPhrase: this.Password, parameter: parameter);
            return request;
        }

        private Bacpac(string address)
        {
            this.Address = address;
            this.Information = new BacpacInformation();
            this.Status = new BacpacStatus();

            this.UpdatePassword();
            this.UpdateInformation();
            this.UpdateStatus();
        }

        public static Bacpac Create(string address)
        {
            var bacpac = new Bacpac(address);
            return bacpac;
        }
    }
}
