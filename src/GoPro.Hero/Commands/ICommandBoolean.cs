using System.Collections.Generic;
using GoPro.Hero.Filtering;

namespace GoPro.Hero.Commands
{
    public interface ICommandBoolean<TO,TS> where TO : IFilterProvider<TO> where TS :ICommandBoolean<TO,TS>
    {
        bool State { get; set; }

        TS Disable();
        TS Enable();
        TS Set(bool state);
        IEnumerable<bool> ValidStates();
    }
}