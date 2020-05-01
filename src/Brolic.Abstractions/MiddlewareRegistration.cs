namespace Brolic.Abstractions
{
    public class MiddlewareRegistration
    {
        public object Middleware { get; set; }
        public MiddlewareType Type { get; set; }
        public object[] Parameters { get; set; } = { };
    }
}