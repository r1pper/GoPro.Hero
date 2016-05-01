using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GoPro.Hero.Filtering;
using GoPro.Hero.Browser.FileSystem;

namespace GoPro.Hero.Browser.Media
{
    public class AmbarellaMediaBrowser<T>:MediaBrowser<T> where T :ICamera<T>, IFilterProvider<T>
    {
        private const string  GROUP_ETC="etc";
        private const string GROUP_TIMELAPSED = "timeLapsed";
        private const string DESTINATION = "100GOPRO";

        public override async Task<IEnumerable<IMedia<T>>> ContentsAsync()
        {
            var fs = Camera.FileSystem<AmbarellaBrowser<T>>();

            var videos = await fs.ChildAsync("videos");
            var dcim = await videos.ChildAsync("DCIM");
            var goPro = await dcim.ChildAsync("100GOPRO");
            var files = await goPro.NodesAsync();

            var groups=files.GroupBy(n => n.Name.StartsWith("GOPR") ? GROUP_ETC : GROUP_TIMELAPSED);
            return Convert(groups);
        }

        protected override void Initialize(T camera, Uri address)
        {
            base.Initialize(camera, address);
            Destination = DESTINATION;
        }

        private IEnumerable<IMedia<T>> Convert(IEnumerable<IGrouping<string, Node<T>>> groups)
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

            var images =imageGroup!=null? imageGroup.Select(i => Image(i)).ToArray(): new Image<T>[0];
            var videos = videoGroup != null ? videoGroup.Select(v => Video(v, lowResVideoGroup)).ToArray() : new Video<T>[0];


            var timeLapsedGroup = groups.Where(g => g.Key == GROUP_TIMELAPSED).FirstOrDefault();

            var timeLapses = timeLapsedGroup != null ? timeLapsedGroup.GroupBy(n => n.Name.Substring(0, 4)).Select(g => TimeLapse(g)).ToArray() : new TimeLapsedImage<T>[0];

            return images.Cast<IMedia<T>>().Union(videos.Cast<IMedia<T>>()).Union(timeLapses.Cast<IMedia<T>>());
        }

        private Image<T> Image(Node<T> node)
        {
            var image = new ImageParameters
            {
                Name = node.Name,
                Size = node.SizeAsBytes()
            };

            return Media<T,ImageParameters>.Create<Image<T>>(image, this);
        }

        private Video<T> Video(Node<T> node, IEnumerable<Node<T>> loweResVideo)
        {
            var lowRes = loweResVideo != null ? loweResVideo.Where(n => n.NameWithoutExtension() == node.NameWithoutExtension()).FirstOrDefault() : null;

            var video = new VideoParameters
            {
                Name = node.Name,
                Size = node.SizeAsBytes(),
                LowResolutionSize=lowRes==null?-1: lowRes.SizeAsBytes()
            };

            return Media<T,VideoParameters>.Create<Video<T>>(video, this);
        }

        private TimeLapsedImage<T> TimeLapse(IEnumerable<Node<T>> nodes)
        {
            var timeLapse=new TimeLapsedImageParameters
            {
                Name=nodes.First().Name,
                Start=int.Parse(nodes.First().Name.Substring(4, 4)),
                End=int.Parse(nodes.Last().Name.Substring(4, 4)),
                Group=int.Parse(nodes.First().Name.Substring(1,3)),
                Size=nodes.Sum(n => n.SizeAsBytes())
            };

            return Media<T,TimeLapsedImageParameters>.Create<TimeLapsedImage<T>>(timeLapse, this);
        }
    }
}
