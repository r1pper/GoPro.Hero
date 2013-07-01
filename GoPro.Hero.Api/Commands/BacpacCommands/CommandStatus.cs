using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GoPro.Hero.Api.Commands.BacpacCommands
{
    class CommandStatus:CommandRequest
    {
        protected override void Initialize()
        {
            base.command = HeroCommands.BACPAC_STATUS;
            base.parameter = null;
        }

        public static CommandInformation Create(string address,string passPhrase)
        {
            return CommandRequest.Create<CommandInformation>(address, HeroCommands.BACPAC_STATUS,passPhrase);
        }
    }
}
