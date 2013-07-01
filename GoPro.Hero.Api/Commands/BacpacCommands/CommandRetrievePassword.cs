using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace GoPro.Hero.Api.Commands.BacpacCommands
{
    class CommandRetrievePassword:CommandRequest
    {
        protected override void Initialize()
        {
            base.command = HeroCommands.BACPAC_GET_PASSWORD;
            base.parameter = null;
            base.passPhrase = null;
        }

        public static CommandRetrievePassword Create(string address)
        {
            return CommandRequest.Create<CommandRetrievePassword>(address, HeroCommands.BACPAC_GET_PASSWORD);
        }
    }
}
