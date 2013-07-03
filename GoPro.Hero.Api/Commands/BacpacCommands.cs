using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GoPro.Hero.Api.Commands.BacpacCommands
{
    [Command(HeroCommands.BACPAC_INFORMATION, InSecure = true, Parameterless = true)]
    class CommandBacpacInformation : CommandRequest
    {
    }

    [Command(HeroCommands.BACPAC_STATUS, Parameterless = true)]
    class CommandBacpacStatus : CommandRequest
    {
    }

    [Command(HeroCommands.BACPAC_POWER)]
    class CommandPowerUp : CommandBoolean
    {
    }

    [Command(HeroCommands.BACPAC_GET_PASSWORD, InSecure = true, Parameterless = true)]
    class CommandRetrievePassword : CommandRequest
    {
    }

    [Command(HeroCommands.BACPAC_SHUTTER)]
    class CommandBacpacShutter : CommandBoolean
    {
    }
}
