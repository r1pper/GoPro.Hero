using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GoPro.Hero.Api.Commands
{
    class CommandAttribute:Attribute
    {
        public string Command{get;private set;}

        public bool Parameterless { get; set; }
        public bool InSecure { get; set; }

        public CommandAttribute(string command)
        {
            this.Command = command;
        }
    }
}
