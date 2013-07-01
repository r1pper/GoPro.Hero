using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GoPro.Hero.Api.Commands.BacpacCommands
{
    class BacpacCommandStatus:CommandRequest
    {
        protected override void Initialize()
        {
            base.command = HeroCommands.BACPAC_STATUS;
            base.parameter = null;
        }

        public static BacpacCommandInformation Create(string address,string passPhrase)
        {
            return CommandRequest.Create<BacpacCommandInformation>(address, HeroCommands.BACPAC_STATUS,passPhrase);
        }
    }
}
