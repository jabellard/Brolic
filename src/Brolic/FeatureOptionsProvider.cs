using Brolic.Abstractions;
using Microsoft.Extensions.Options;

namespace Brolic
{
    public class FeatureOptionsProvider: IFeatureOptionsProvider
    {
        private readonly BrolicOptions _brolicOptions;

        public FeatureOptionsProvider(IOptions<BrolicOptions> optionsAccessor)
        {
            _brolicOptions = optionsAccessor.Value;
        }
        
        public TOptions GetFeatureOptions<TOptions>(string key)
            where TOptions: class
        {
            return _brolicOptions.Features[key] as TOptions;
        }
    }
}