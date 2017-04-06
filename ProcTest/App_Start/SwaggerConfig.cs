using System.Web.Http;
using WebActivatorEx;
using ProcTest;
using Swashbuckle.Application;

[assembly: PreApplicationStartMethod(typeof(SwaggerConfig), "Register")]

namespace ProcTest
{
    public class SwaggerConfig
    {
        public static void Register()
        {
            var thisAssembly = typeof(SwaggerConfig).Assembly;

            GlobalConfiguration.Configuration 
                .EnableSwagger(c =>
                    {
                        
                        
                        c.SingleApiVersion("v1", "ProcTest");

                        
                    })
                .EnableSwaggerUi(c =>
                    {
                        
                    });
        }
    }
}
