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

    [Command(HeroCommands.BACPAC_RESET, Parameterless = true)]
    internal class CommandBacpacReset : CommandRequest<Bacpac>
    {
    }

    [Command(HeroCommands.BACPAC_WIFI_CONFIG)]
    internal class CommandBacpacWifiConfigure : CommandRequest<Bacpac>
    {
        private string _password=string.Empty;
        private string _name=string.Empty;

        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                UpdateParameter();
            }
        }

        public string Name
        {
            get{return _name;}
            set
            {
                _name = value;
                UpdateParameter();
            }
        }

        private void UpdateParameter()
        {
            var nameLength = _name.Length.ToString("x2");
            var passwordLength = _password.Length.ToString("x2");

            base.Parameter = string.Format("%{0}{1}%{2}{3}", passwordLength, _password, nameLength, _name);
        }

        public CommandBacpacWifiConfigure SetName(string name)
        {
            Name = name;
            return this;
        }

        public CommandBacpacWifiConfigure SetPassword(string password)
        {
            Password = password;
            return this;
        }
    }
}