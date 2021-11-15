using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace BadNews.Elevation
{
    public class ElevationMiddleware
    {
        private readonly RequestDelegate next;
    
        public ElevationMiddleware(RequestDelegate next)
        {
            this.next = next;
        }
    
        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Request.Path == "/elevation")
            {
                Elevate(context.Response, context.Request.Query.ContainsKey("up"));
                context.Response.Redirect("/");
            }
            else
            {
                await next(context);
            }
        }

        private static void Elevate(HttpResponse response, bool up)
        {
            if (up)
            {
                response.Cookies.Append(ElevationConstants.CookieName, ElevationConstants.CookieValue,
                    new CookieOptions
                    {
                        HttpOnly = true
                    });
            }
            else
                response.Cookies.Delete(ElevationConstants.CookieName);
        }
    }
}
