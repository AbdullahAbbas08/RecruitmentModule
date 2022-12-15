using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;

namespace RecruitmentModule.Extentions
{
    public static class HttpClientExtentions
    {
        public static void AddCurrentAuthHeaders(this HttpClient http, IServiceCollection services = null, IHttpContextAccessor accessor = null)
        {
            if (accessor == null)
            {
                var serviceProvider = services.BuildServiceProvider();
                // Find the HttpContextAccessor service
                accessor = serviceProvider.GetService<IHttpContextAccessor>();
            }
            if (accessor?.HttpContext != null)
            {
                var request = accessor.HttpContext.Request;

                foreach (var headerName in request.Headers.Keys)
                {

                    try
                    {
                        http.DefaultRequestHeaders.Add(headerName, (string)request.Headers[headerName]);

                    }
                    catch (Exception)
                    {
                    }
                }


                string basUrl = string.Format("{0}://{1}", request.Scheme, request.Host.Value);
                http.BaseAddress = new Uri(basUrl);

            }
        }
    }
}
