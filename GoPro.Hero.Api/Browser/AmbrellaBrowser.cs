using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace GoPro.Hero.Api.Browser
{
    public class AmbrellaBrowser : Browser
    {
        private const string DEFAULT = "http://10.5.5.9:8080";

        protected override IEnumerable<Node> Parse(XElement page, Node parent)
        {
            if (parent == null)
                parent = new Node(null, new Uri(DEFAULT), NodeType.Root, string.Empty, this);

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
                    var xElement1 = elements[2].LastNode as XElement;
                    if (xElement1 == null) continue;
                    var size = xElement1.Value;

                    var uri = path.StartsWith(parent.Path.ToString()) ? new Uri(path) : new Uri(parent.Path, path);

                    yield return 
                        new Node(Camera, uri, type == "[DIR]" ? NodeType.Folder : NodeType.File, size, this);
                }
        }
    }
}