using System;
using System.Collections.Generic;
using System.Linq;

namespace GoPro.Hero.Api.Browser
{
    public class Node
    {
        private readonly IBrowser _browser;

        private Node(ICamera camera, Uri address, IBrowser browser)
            : this(camera, address, NodeType.Root, string.Empty, browser)
        {
            Type = address.AbsolutePath == "/"
                       ? NodeType.Root
                       : _browser.IsFile(address)
                             ? NodeType.File
                             : NodeType.Folder;
        }

        internal Node(ICamera camera, Uri address, NodeType type, string size, IBrowser browser)
        {
            _browser = browser;
            Camera = camera;
            Path = address;
            Type = type;
            Size = size;
            Name = Path.AbsolutePath.Split('/').Last();
        }

        public ICamera Camera { get; private set; }
        public string Name { get; private set; }
        public Uri Path { get; private set; }
        public NodeType Type { get; private set; }
        public string Size { get; private set; }

        public IEnumerable<Node> Nodes()
        {
            return _browser.Nodes(this);
        }

        public Node Nodes(out IEnumerable<Node> nodes)
        {
            nodes = Nodes();
            return this;
        }

        public IEnumerable<Node> Folders()
        {
            return _browser.Nodes(this).Where(node => node.Type == NodeType.Folder);
        }

        public Node Folders(out IEnumerable<Node> nodes)
        {
            nodes = Folders();
            return this;
        }

        public IEnumerable<Node> Files()
        {
            return _browser.Nodes(this).Where(node => node.Type == NodeType.File);
        }

        public Node Files(out IEnumerable<Node> nodes)
        {
            nodes = Files();
            return this;
        }

        public static Node Create<T>(ICamera camera, Uri address) where T : IBrowser
        {
            var browser = Activator.CreateInstance<T>();
            browser.Initialize(camera, address);

            var node = new Node(camera, address, browser);
            return node;
        }
    }
}