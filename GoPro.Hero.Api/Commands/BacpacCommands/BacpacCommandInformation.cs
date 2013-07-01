using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GoPro.Hero.Api.Commands.BacpacCommands
{
    class BacpacCommandInformation:CommandRequest
    {
        protected override void Initialize()
        {
            base.command = HeroCommands.BACPAC_INFORMATION;
            base.parameter = null;
            base.passPhrase = null;
        }

        public static BacpacCommandInformation Create(string address)
        {
            return CommandRequest.Create<BacpacCommandInformation>(address, HeroCommands.BACPAC_INFORMATION);
        }
    }
}
