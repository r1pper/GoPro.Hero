namespace GoPro.Hero.Browser.Media
{
    public class Image : Media<ImageParameters>
    {
        public override string ToString()
        {
            return string.Format("image:{0}", base.Name);
        }
    }
}
