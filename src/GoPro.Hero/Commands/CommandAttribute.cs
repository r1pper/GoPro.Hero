using System;

namespace GoPro.Hero.Commands
{
    internal class CommandAttribute : Attribute
    {
        public CommandAttribute(string command)
        {
            Command = command;
        }

        public string Command { get; private set; }

        public bool Parameterless { get; set; }
        public bool InSecure { get; set; }
    }
}