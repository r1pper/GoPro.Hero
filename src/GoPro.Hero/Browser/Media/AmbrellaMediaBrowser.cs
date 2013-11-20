using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GoPro.Hero.Browser.FileSystem;

namespace GoPro.Hero.Browser.Media
{
    public class AmbrellaMediaBrowser:MediaBrowser
    {
        private const string  GROUP_ETC="etc";
        private const string GROUP_TIMELAPSED = "timeLapsed";

        public override async Task<IEnumerable<IMedia>> ContentsAsync()
        {
            var heroCamera = base.Camera as Camera;
            var fsBrowser = heroCamera.FileSystem<AmbrellaBrowser>();
            var nodes= await fsBrowser.NodesAsync();

            var groups=nodes.GroupBy(n => n.Name.StartsWith("GOPR") ? GROUP_ETC : GROUP_TIMELAPSED);
            return Convert(groups);
        }

        private IEnumerable<IMedia> Convert(IEnumerable<IGrouping<string, Node>> groups)
        {
            var etc = groups.Where(g => g.Key == GROUP_ETC).FirstOrDefault();
            if (etc == null) 
                return null;
            var etcGroups =  etc.GroupBy(n => n.Extension());
            if (etcGroups == null) 
                return null;

            var imageGroup = etcGroups.Where(g => g.Key == "JPG").FirstOrDefault();
            var videoGroup = etcGroups.Where(g => g.Key == "MP4").FirstOrDefault();
            var lowResVideoGroup = etcGroups.Where(g => g.Key == "LRV").FirstOrDefault();

            var images =imageGroup!=null? imageGroup.Select(i => Image(i)): new Image[0];
            var videos = videoGroup != null ? videoGroup.Select(v => Video(v, lowResVideoGroup)) : new Video[0];


            var timeLapsedGroup = groups.Where(g => g.Key == GROUP_TIMELAPSED).FirstOrDefault();

            var timeLapses = timeLapsedGroup != null ? timeLapsedGroup.GroupBy(n => n.Name.Substring(0, 4)).Select(g => TimeLapse(g)) : new TimeLapsedImage[0];

            return images.Cast<IMedia>().Union(videos.Cast<IMedia>()).Union(timeLapses.Cast<IMedia>());
        }

        private Image Image(Node node)
        {
            var image = new ImageParameters
            {
                Name = node.Name,
                Size = 0 //NOTE: Check size format
            };

            return Media<ImageParameters>.Create<Image>(image, this);
        }

        private Video Video(Node node, IEnumerable<Node> loweResVideo)
        {
            var lowRes = loweResVideo != null ? loweResVideo.Where(n => n.NameWithoutExtension() == node.NameWithoutExtension()).FirstOrDefault() : null;

            var video = new VideoParameters
            {
                Name = node.Name,
                Size = node.SizeAsBytes(),
                LowResolutionSize=lowRes==null?-1: lowRes.SizeAsBytes()
            };

            return Media<VideoParameters>.Create<Video>(video, this);
        }

        private TimeLapsedImage TimeLapse(IEnumerable<Node> nodes)
        {
            var timeLapse=new TimeLapsedImageParameters
            {
                Name=nodes.First().Name,
                Start=int.Parse(nodes.First().Name.Substring(4, 4)),
                End=int.Parse(nodes.Last().Name.Substring(4, 4)),
                Size=nodes.Sum(n => n.SizeAsBytes())
            };

            return Media<TimeLapsedImageParameters>.Create<TimeLapsedImage>(timeLapse, this);
        }
    }
}
