namespace CSharpApp.Core.Interfaces
{
    public interface IMapper<TSource, TDestination>
    {
        TDestination Map(TSource source);

        List<TDestination> Map(IReadOnlyCollection<TSource> source);
    }
}
