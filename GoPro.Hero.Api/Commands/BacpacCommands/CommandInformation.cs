using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GoPro.Hero.Api.Commands.BacpacCommands
{
    class CommandInformation:CommandRequest
    {
        public static CommandInformation Create(string address)
        {
            return new CommandInformation() { commandUri = CommandRequest.CreateUri(address, HeroCommands.BACPAC_INFORMATION) };
        }
    }
}
