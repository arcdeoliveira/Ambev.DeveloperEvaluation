using Ambev.DeveloperEvaluation.Domain.DomainServices;
using Ambev.DeveloperEvaluation.Domain.DomainServices.Common;
using Ambev.DeveloperEvaluation.Domain.Interfaces.Services;
using Ambev.DeveloperEvaluation.Domain.Interfaces.Services.Common;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Ambev.DeveloperEvaluation.IoC.ModuleInitializers
{
    public class ServiceDomainModuleInitializer : IModuleInitializer
    {
        public void Initialize(WebApplicationBuilder builder)
        {
            builder.Services.AddScoped(typeof(IBaseNonRelationalService<>), typeof(BaseNonRelationalService<>));
            builder.Services.AddScoped<IProductService, ProductService>();
        }
    }
}
