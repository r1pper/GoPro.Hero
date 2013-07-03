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

        public Bacpac UpdatePassword()
        {
            var request = this.CreateCommand<CommandRetrievePassword>();
            var response = request.Send();

            var length = response.RawResponse[1];
            this.Password = Encoding.UTF8.GetString(response.RawResponse, 2, length);

            return this;
        }

        public Bacpac UpdateStatus()
        {
            var request = this.CreateCommand<CommandBacpacStatus>();
            var response = request.Send();

            var stream = response.GetResponseStream();
            this.Status.Update(stream);

            return this;
        }

        public Bacpac UpdateInformation()
        {
            var request = this.CreateCommand<CommandBacpacInformation>();
            var response = request.Send();

            var stream = response.GetResponseStream();
            this.Information.Update(stream);

            return this;
        }

        public Bacpac Shutter(bool trigger)
        {
            var request = this.CreateCommand<CommandBacpacShutter>();
            request.Enable = trigger;
            var response = request.Send();
            return this.UpdateStatus();
        }

        public Bacpac Power(bool on)
        {
            var request = this.CreateCommand<CommandPowerUp>();
            request.Enable = on;
            var response = request.Send();

            return this.UpdateStatus();
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
