using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GoPro.Hero.Api.Commands.BacpacCommands
{
    [Command(HeroCommands.BACPAC_SHUTTER)]
    class CommandBacpacShutter : CommandBoolean<Bacpac>
    {
    }

    [Command(HeroCommands.BACPAC_POWER)]
    class CommandBacpacPowerUp : CommandBoolean<Bacpac>
    {
    }

    [Command(HeroCommands.BACPAC_GET_PASSWORD, InSecure = true, Parameterless = true)]
    class CommandBacpacRetrievePassword : CommandRequest<Bacpac>
    {
    }

    [Command(HeroCommands.BACPAC_INFORMATION, InSecure = true, Parameterless = true)]
    class CommandBacpacInformation : CommandRequest<Bacpac>
    {
    }

    [Command(HeroCommands.BACPAC_STATUS, Parameterless = true)]
    class CommandBacpacStatus : CommandRequest<Bacpac>
    {
    }
}
