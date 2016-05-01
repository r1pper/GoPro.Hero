using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using GoPro.Hero.Filtering;

namespace GoPro.Hero.Browser.FileSystem
{
    public class Node<T> where T :ICamera<T>,IFilterProvider<T>
    {
        private readonly IFileSystemBrowser<T> _browser;

        private Node(T camera, Uri address, IFileSystemBrowser<T> browser)
            : this(camera, address, NodeType.Root, string.Empty, browser)
        {
            Type = address.AbsolutePath == "/"
                       ? NodeType.Root
                       : _browser.IsFile(address)
                             ? NodeType.File
                             : NodeType.Folder;
        }

        internal Node(T camera, Uri address, NodeType type, string size, IFileSystemBrowser<T> browser)
        {
            _browser = browser;
            Camera = camera;
            Path = address;
            Type = type;
            Size = size;

            var segments = Path.AbsolutePath.Split('/');
            Name = string.IsNullOrEmpty(segments.Last()) ? segments[segments.Length - 2] : segments.Last();
        }

        public T Camera { get; private set; }
        public string Name { get; private set; }
        public Uri Path { get; private set; }
        public NodeType Type { get; private set; }
        public string Size { get; private set; }

        public Node<T> this[string name]
        {
            get { return Child(name); }
        }

        public long SizeAsBytes()
        {
            var type = Size[Size.Length - 1];
            var value = double.Parse(Size.Substring(0, Size.Length - 1));
            switch (type)
            {
                case 'M':
                    return (long)(value * 1024 * 1024);
                case 'K':
                    return (long)(value * 1024);
                default:
                    return -1;
            }
        }

        public string Extension()
        {
            return Name.Split('.').LastOrDefault();
        }

        public string NameWithoutExtension()
        {
            return Name.Split('.').First();
        }

        public IEnumerable<Node<T>> Children(string name)
        {
            return ChildrenAsync(name).Result;
        }

        public async Task<IEnumerable<Node<T>>> ChildrenAsync(string name)
        {
            return (await NodesAsync()).Where(n => n.Name == name);
        }

        public Node<T> Child(string name)
        {
            return ChildAsync(name).Result;
        }

        public async Task<Node<T>> ChildAsync(string name)
        {
            return (await this.ChildrenAsync(name)).FirstOrDefault();
        }

        public IEnumerable<Node<T>> Nodes()
        {
            return _browser.Nodes(this);
        }

        public async Task<IEnumerable<Node<T>>> NodesAsync()
        {
            return await _browser.NodesAsync(this);
        }

        public IEnumerable<Node<T>> Folders()
        {
            return FoldersAsync().Result;
        }

        public async Task<IEnumerable<Node<T>>> FoldersAsync()
        {
            return (await _browser.NodesAsync(this)).Where(node => node.Type == NodeType.Folder);
        }

        public IEnumerable<Node<T>> Files()
        {
            return FilesAsync().Result;
        }

        public async Task<IEnumerable<Node<T>>> FilesAsync()
        {
            return (await _browser.NodesAsync(this)).Where(node => node.Type == NodeType.File);
        }

        public WebResponse DownloadContent()
        {
            return _browser.DownloadContent(this);
        }

        public async Task<WebResponse> DownloadContentAsync()
        {
            return await _browser.DownloadContentAsync(this);
        }

        public override string ToString()
        {
            return Name;
        }

        public static Node<T> Create<TB>(T camera, Uri address) where TB : IFileSystemBrowser<T>
        {
            var browser = Activator.CreateInstance<TB>();
            browser.Initialize(camera, address);

            var node = new Node<T>(camera, address, browser);
            return node;
        }

        public static Node<T> Create(IFileSystemBrowser<T> browser)
        {
            var node = new Node<T>(browser.Camera, browser.Address, browser);
            return node;
        }
    }
}