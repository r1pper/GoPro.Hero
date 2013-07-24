using System;
using System.Collections.Generic;

namespace GoPro.Hero.Api.Browser
{
    public interface IBrowser
    {
        Uri Address { get; }
        ICamera Camera { get; }

        void Initialize(ICamera camera, Uri address);
        bool IsFile(Uri address);
        IEnumerable<Node> Nodes();
        IEnumerable<Node> Nodes(Node node);
    }
}