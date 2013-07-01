using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using GoPro.Hero.Api.Commands.BacpacCommands;
using GoPro.Hero.Api.Commands;
using GoPro.Hero.Api.Exceptions;

namespace GoPro.Hero.Api
{
    public class Bacpac
    {
        public string Password { get; private set; }
        public string Address { get; private set; }

        public void UpdatePassword()
        {
            var request = CommandRetrievePassword.Create(this.Address);
            var response=Commando.Send(request);

            var failed = response.RawResponse[0] != 0;
            if (failed)
                throw new BacpacException();

            var length = response.RawResponse[1];
            this.Password =Encoding.UTF8.GetString(response.RawResponse,2,length);
        }

        public void UpdateStatus()
        {
        }

        public void UpdateInformation()
        {
            var request = CommandInformation.Create(this.Address);
            var response = Commando.Send(request);
        }

        public void Shoot()
        {
        }

        private Bacpac(string address)
        {
            this.Address = address;
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
