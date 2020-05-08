using System.Collections.Generic;

namespace Brolic.Features.RateLimiting
{
    public class RateLimitingRule
    {
        public long Limit { get; set; }
        public string Period { get; set; }
        public List<string> Methods { get; set; }
    }
}