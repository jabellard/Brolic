namespace Brolic.Features.RateLimiting
{
    public class RateLimitingValidationContext
    {
        public string TrafficInitiator { get; set; }
        public string Path { get; set; }
        public string Method { get; set; }
        public long Limit { get; set; }
        public string Period { get; set; }
    }
}