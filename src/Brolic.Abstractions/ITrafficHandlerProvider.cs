namespace Brolic.Abstractions
{
    public interface ITrafficHandlerProvider
    {
        ITrafficHandler GetTrafficHandler(string key);
    }
}