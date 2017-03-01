using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace LiuFangJwtAuth.Extensions
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class TokenAuthMiddleware
    {
        private readonly RequestDelegate _next;

        public TokenAuthMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext httpContext)
        {
            //检测是否包含'Authorization'请求头，如果不包含返回context进行下一个中间件，用于访问不需要认证的API
            if (!httpContext.Request.Headers.ContainsKey("Authorization"))
                return _next(httpContext);
            else
            {
                var tokenHeader = httpContext.Request.Headers["Authorization"];
                try
                {
                    tokenHeader = tokenHeader.ToString().Substring("Bearer ".Length).Trim();
                    var result = Config.StoreSignedUser.list.Where(m => m.userToken == tokenHeader).FirstOrDefault();
                    if (result == null)
                        return httpContext.Response.WriteAsync("非法请求");
                    else
                    {
                        ClaimsIdentity identity = new ClaimsIdentity(result.userClaims);
                        ClaimsPrincipal principal = new ClaimsPrincipal(identity);
                        httpContext.User = principal;//构建authorize认证
                        return _next(httpContext);
                    }
                }
                catch(Exception e)
                {
                    return httpContext.Response.WriteAsync("token值长度不够");
                }
            }
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class TokenAuthMiddlewareExtensions
    {
        public static IApplicationBuilder UseTokenAuthMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<TokenAuthMiddleware>();
        }
    }
}
