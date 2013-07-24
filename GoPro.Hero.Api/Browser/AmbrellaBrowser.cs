using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace GoPro.Hero.Api.Browser
{
    public class AmbrellaBrowser : Browser
    {
        protected override IEnumerable<Node> Parse(XElement page)
        {
            var body = page.Element("body");
            if (body == null) yield break;
            var bodyInside = body.Element("div");
            if (bodyInside == null) yield break;
            var dirList = bodyInside.Element("div");
            if (dirList == null) yield break;
            var table = dirList.Element("table");
            if (table == null) yield break;
            var content = table.Element("tbody");

            if (content != null)
                foreach (var row in content.Elements())
                {
                    var elements = row.Elements().ToArray();

                    var xElement = elements[0].Element("img");
                    if (xElement == null) continue;
                    var xAttribute = xElement.Attribute("alt");
                    if (xAttribute == null) continue;
                    var type = xAttribute.Value;
                    var element = elements[1].Element("a");
                    if (element == null) continue;
                    var attribute = element.Attribute("href");
                    if (attribute == null) continue;
                    var path = attribute.Value;
                    var xElement1 = elements[2].LastNode as XElement;
                    if (xElement1 == null) continue;
                    var size = xElement1.Value;

                    yield return
                        new Node(Camera, new Uri(path), type == "[DIR]" ? NodeType.Folder : NodeType.File, size, this);
                }
        }
    }
}