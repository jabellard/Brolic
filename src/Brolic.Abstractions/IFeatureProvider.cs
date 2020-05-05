using System.Collections.Generic;

namespace Brolic.Abstractions
{
    public interface IFeatureProvider
    {
        List<IFeature> GetFeatures();
    }
}