using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GoPro.Hero.Api.Commands.BacpacCommands
{
    class CommandBacpacStatus:CommandRequest
    {
        protected override void Initialize()
        {
            base.command = HeroCommands.BACPAC_STATUS;
            base.parameter = null;
        }

        public static CommandBacpacInformation Create(string address,string passPhrase)
        {
            return CommandRequest.Create<CommandBacpacInformation>(address, HeroCommands.BACPAC_STATUS,passPhrase);
        }
    }
}
