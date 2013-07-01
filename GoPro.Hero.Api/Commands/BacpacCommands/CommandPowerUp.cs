using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GoPro.Hero.Api.Commands.BacpacCommands
{
    class CommandPowerUp:CommandRequest
    {
        private const string ON="%01";
        private const string OFF="%00";

        protected override void Initialize()
        {
            base.command = HeroCommands.BACPAC_POWER;
        }

        public bool PowerUp
        {
            get
            {
                return string.IsNullOrEmpty(base.parameter) ? false : base.parameter == OFF ? false : true;
            }
            set
            {
                base.parameter = value ? ON : OFF;
            }
        }

        public static CommandInformation Create(string address, string passPhrase,bool powerUp)
        {
            return CommandRequest.Create<CommandInformation>(address, HeroCommands.BACPAC_POWER, passPhrase, powerUp ? ON : OFF);
        }
    }
}
