using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Brolic
{
    public class BrolicMiddleware
    {
        private readonly RequestDelegate _next;

        public BrolicMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        
        public Task Invoke(HttpContext context)
        {
            throw new NotImplementedException();
        }
    }
}