namespace Brolic.Abstractions
{
    public interface IFeatureOptionsProvider
    {
        TOptions GetFeatureOptions<TOptions>(string key)
            where TOptions: class;
    }
}