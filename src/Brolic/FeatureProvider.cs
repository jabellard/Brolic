using System.Collections.Generic;
using System.Linq;
using Brolic.Abstractions;

namespace Brolic
{
    public class FeatureProvider: IFeatureProvider
    {
        private readonly IEnumerable<IFeatureRegistration> _featureRegistrations;

        public FeatureProvider(IEnumerable<IFeatureRegistration> featureRegistrations)
        {
            _featureRegistrations = featureRegistrations;
        }
        
        public List<IFeature> GetFeatures()
        {
            return _featureRegistrations
                .OrderBy(fr => fr.Timestamp)
                .Select(fr => fr.Feature)
                .ToList();
        }
    }
}