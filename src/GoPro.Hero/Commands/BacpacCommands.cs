namespace GoPro.Hero.Commands
{
    [Command(HeroCommands.BACPAC_SHUTTER)]
    internal class CommandBacpacShutter : CommandBoolean<Bacpac>
    {
    }

    [Command(HeroCommands.BACPAC_POWER)]
    internal class CommandBacpacPowerUp : CommandBoolean<Bacpac>
    {
    }

    [Command(HeroCommands.BACPAC_GET_PASSWORD, InSecure = true, Parameterless = true)]
    internal class CommandBacpacRetrievePassword : CommandRequest<Bacpac>
    {
    }

    [Command(HeroCommands.BACPAC_INFORMATION, InSecure = true, Parameterless = true)]
    internal class CommandBacpacInformation : CommandRequest<Bacpac>
    {
    }

    [Command(HeroCommands.BACPAC_STATUS, Parameterless = true)]
    internal class CommandBacpacStatus : CommandRequest<Bacpac>
    {
    }
}