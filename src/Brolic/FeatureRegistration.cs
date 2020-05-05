using System;
using Brolic.Abstractions;

namespace Brolic
{
    public class FeatureRegistration: IFeatureRegistration
    {
        public IFeature Feature { get; set; }
        public DateTime Timestamp { get; set; }
        
    }
}