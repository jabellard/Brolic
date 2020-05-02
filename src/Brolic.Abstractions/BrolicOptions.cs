using System.Collections.Generic;

namespace Brolic.Abstractions
{
    public class BrolicOptions
    {
        public List<BrolicRoute> Routes { get; set; }
        public Dictionary<string, Downstream> Downstreams { get; set; }
        public Dictionary<string, object> Features { get; set; }
    }
}