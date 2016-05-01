namespace GoPro.Hero.Filtering
{
    public interface IFilterProvider<T>where T :IFilterProvider<T>
    {
        IFilter<T> Filter();
    }
}