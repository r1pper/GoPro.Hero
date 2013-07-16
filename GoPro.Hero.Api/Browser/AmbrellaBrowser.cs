using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace GoPro.Hero.Api.Browser
{
    public class AmbrellaBrowser:Browser
    {
        protected override IEnumerable<Node> Parse(XElement page)
        {
            var body = page.Element("body");
            var bodyInside = body.Element("div");
            var dirList = bodyInside.Element("div");
            var table = dirList.Element("table");
            var content = table.Element("tbody");

            foreach (var row in content.Elements())
            {
                var elements = row.Elements().ToArray();

                var type = elements[0].Element("img").Attribute("alt").Value;
                var path = elements[1].Element("a").Attribute("href").Value;
                var size = (elements[2].LastNode as XElement).Value;

                yield return new Node(this.Camera, new Uri(path), type == "[DIR]" ? NodeType.Folder : NodeType.File,size, this);
            }
        }
    }
}
