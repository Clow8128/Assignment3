//Cameron Low
//Distributed Applications
//Assignment 2
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace RESTAPI.Middleware
{
    public class CustomMiddleware
    {
        private readonly RequestDelegate _next;

        public CustomMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            Debug.WriteLine($"---> Request for {httpContext.Request.Path}");

            await _next.Invoke(httpContext);
        }

    }
}
