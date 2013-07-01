using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GoPro.Hero.Api.Commands.BacpacCommands
{
    class CommandInformation:CommandRequest
    {
        protected override void Initialize()
        {
            base.command = HeroCommands.BACPAC_INFORMATION;
            base.parameter = null;
            base.passPhrase = null;
        }

        public static CommandInformation Create(string address)
        {
            return CommandRequest.Create<CommandInformation>(address, HeroCommands.BACPAC_INFORMATION);
        }
    }
}
