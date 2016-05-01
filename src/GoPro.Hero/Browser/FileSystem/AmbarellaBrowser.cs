using GoPro.Hero.Filtering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace GoPro.Hero.Browser.FileSystem
{
    public class AmbarellaBrowser<T> : FileSystemBrowser<T>where T :ICamera<T>,IFilterProvider<T>
    {
        private const string DEFAULT = "http://10.5.5.9:8080";

        protected override IEnumerable<Node<T>> Parse(XElement page, Node<T> parent)
        {
            if (parent == null)
                parent = new Node<T>(default(T), new Uri(DEFAULT), NodeType.Root, string.Empty, this);

            var xmlns = page.GetDefaultNamespace().NamespaceName;
            var body = page.Element(XName.Get("body", xmlns));
            if (body == null) yield break;
            var bodyInside = body.Element(XName.Get("div", xmlns));
            if (bodyInside == null) yield break;
            var dirList = bodyInside.Element(XName.Get("div", xmlns));
            if (dirList == null) yield break;
            var table = dirList.Element(XName.Get("table", xmlns));
            if (table == null) yield break;
            var content = table.Element(XName.Get("tbody", xmlns));

            if (content != null)
                foreach (var row in content.Elements())
                {
                    var elements = row.Elements().ToArray();

                    var xElement = elements[0].Element(XName.Get("img", xmlns));
                    if (xElement == null) continue;
                    var xAttribute = xElement.Attribute("alt");
                    if (xAttribute == null) continue;
                    var type = xAttribute.Value;
                    var element = elements[1].Element(XName.Get("a", xmlns));
                    if (element == null) continue;
                    var attribute = element.Attribute("href");
                    if (attribute == null) continue;
                    var path = attribute.Value;
                    var sizeElement = elements[2] as XElement;
                    var size = sizeElement.Elements().InDocumentOrder().Select(e=>e.Value).Aggregate((c, n) => c + n);

                    var uri = path.StartsWith(parent.Path.ToString()) ? new Uri(path) : new Uri(parent.Path, path);

                    yield return 
                        new Node<T>(Camera, uri, type == "[DIR]" ? NodeType.Folder : NodeType.File, size, this);
                }
        }

        public Node<T> Root()
        {
            return Node<T>.Create(this);
        }
    }
}
