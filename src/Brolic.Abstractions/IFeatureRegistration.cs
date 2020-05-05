using System;

namespace Brolic.Abstractions
{
    public interface IFeatureRegistration
    {
        public IFeature Feature { get;}
        public DateTime Timestamp { get;}
    }
}