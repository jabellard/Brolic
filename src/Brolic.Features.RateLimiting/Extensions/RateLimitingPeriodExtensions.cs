using System;

namespace Brolic.Features.RateLimiting.Extensions
{
    public static class RateLimitingPeriodExtensions
    {
        public static TimeSpan ToTimeSpan(this string rateLimitingPeriod)
        {
            var valueLength = rateLimitingPeriod.Length - 1;
            var value = rateLimitingPeriod.Substring(0, valueLength);
            var timeUnit = rateLimitingPeriod.Substring(valueLength, 1);

            return timeUnit switch
            {
                "d" => TimeSpan.FromDays(double.Parse(value)),
                "h" => TimeSpan.FromHours(double.Parse(value)),
                "m" => TimeSpan.FromMinutes(double.Parse(value)),
                "s" => TimeSpan.FromSeconds(double.Parse(value)),
                _ => throw new ArgumentOutOfRangeException($"Unsupported time unit of value {timeUnit}.")
            };
        }
    }
}