using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace GoPro.Hero.Api.Commands.BacpacCommands
{
    class CommandRetrievePassword:CommandRequest
    {
        public static CommandRetrievePassword Create(string address)
        {
            return new CommandRetrievePassword() { commandUri = CommandRequest.CreateUri(address, HeroCommands.BACPAC_GET_PASSWORD) };
        }
    }
}
