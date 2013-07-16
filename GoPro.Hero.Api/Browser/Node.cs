using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GoPro.Hero.Api.Browser
{
    public class Node
    {
        private IBrowser _browser;

        public ICamera Camera { get; private set; }
        public string Name { get; private set; }
        public Uri Path { get; private set; }
        public NodeType Type { get; private set; }

        public IEnumerable<Node> Nodes()
        {
            return _browser.Nodes(this);
        }

        public Node Nodes(out IEnumerable<Node> nodes)
        {
            nodes = this.Nodes();
            return this;
        }

        public IEnumerable<Node> Folders()
        {
            return _browser.Nodes(this).Where(node => node.Type == NodeType.Folder);
        }

        public Node Folders(out IEnumerable<Node> nodes)
        {
            nodes = this.Folders();
            return this;
        }

        public IEnumerable<Node> Files()
        {
            return _browser.Nodes(this).Where(node => node.Type == NodeType.File);
        }

        public Node Files(out IEnumerable<Node> nodes)
        {
            nodes = this.Files();
            return this;
        }

        private Node(ICamera camera, Uri address, IBrowser browser)
            : this(camera, address, NodeType.Root, browser)
        {
            this.Type = address.AbsolutePath == "/"
                ? NodeType.Root : this._browser.IsFile(address)
                ? NodeType.File : NodeType.Folder;
        }

        private Node(ICamera camera, Uri address, NodeType type, IBrowser browser)
        {
            _browser = browser;
            this.Path = address;
            this.Type = type;
            this.Name = this.Path.AbsolutePath.Split('/').Last();
        }

        public static Node Create<T>(ICamera camera, Uri address) where T : IBrowser
        {
            var browser = Activator.CreateInstance<T>();
            browser.Initialize(camera, address);

            var node = new Node(camera,address,browser);
            return node;
        }

        internal static Node Create<T>(ICamera camera, Uri address,NodeType type) where T : IBrowser
        {
            var browser = Activator.CreateInstance<T>();
            browser.Initialize(camera, address);

            var node = new Node(camera, address,type,browser);
            return node;
        }
    }
}
