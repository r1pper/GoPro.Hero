using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GoPro.Hero.Api.Filtering;

namespace GoPro.Hero.Api.Filtering
{
    public interface IFilterProvider
    {
        object Filter();
    }
}
