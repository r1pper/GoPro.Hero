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
    public sealed class Bacpac
    {
        private BacpacInformation _information;
        private BacpacStatus _status;

        public string Password { get; private set; }
        public string Address { get; private set; }

        public BacpacInformation Information
        {
            get
            {
                this.UpdateInformation();
                return _information;
            }
        }
        public BacpacStatus Status
        {
            get
            {
                this.UpdateStatus();
                return _status;
            }
        }

        public Bacpac UpdatePassword()
        {
            var request = this.CreateCommand<CommandRetrievePassword>();
            var response = request.Send();

            var length = response.RawResponse[1];
            this.Password = Encoding.UTF8.GetString(response.RawResponse, 2, length);

            return this;
        }

        private void UpdateStatus()
        {
            var request = this.CreateCommand<CommandBacpacStatus>();
            var response = request.Send();

            var stream = response.GetResponseStream();
            this._status.Update(stream);
        }

        private void UpdateInformation()
        {
            var request = this.CreateCommand<CommandBacpacInformation>();
            var response = request.Send();

            var stream = response.GetResponseStream();
            this._information.Update(stream);
        }

        public Bacpac Shutter(bool open)
        {
            var request = this.CreateCommand<CommandBacpacShutter>();
            request.Enable = open;
            var response = request.Send();

            this.UpdateStatus();
            return this;
        }

        public Bacpac Power(bool on)
        {
            var request = this.CreateCommand<CommandPowerUp>();
            request.Enable = on;
            var response = request.Send();

            this.UpdateStatus();
            return this;
        }

        private T CreateCommand<T>(string parameter = null) where T : CommandRequest
        {
            var request = CommandRequest.Create<T>(this.Address, passPhrase: this.Password, parameter: parameter);
            return request;
        }

        private Bacpac(string address)
        {
            this.Address = address;
            this._information = new BacpacInformation();
            this._status = new BacpacStatus();

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
