using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.DomainServices.Common;
using Ambev.DeveloperEvaluation.Domain.DomainServices.NonRelatonal;
using Ambev.DeveloperEvaluation.Domain.DomainServices.Relational;
using Ambev.DeveloperEvaluation.Domain.Interfaces.Services.Common;
using Ambev.DeveloperEvaluation.Domain.Interfaces.Services.NonRelational;
using Ambev.DeveloperEvaluation.Domain.Interfaces.Services.Relational;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Ambev.DeveloperEvaluation.IoC.ModuleInitializers
{
    public class ServiceDomainModuleInitializer : IModuleInitializer
    {
        public void Initialize(WebApplicationBuilder builder)
        {
            builder.Services.AddScoped(typeof(IBaseRelationalService<>), typeof(BaseRelationalService<>));
            builder.Services.AddScoped<IAffiliateService, AffiliateService>();
            builder.Services.AddScoped<IUserService, UserService>();

            builder.Services.AddScoped(typeof(IBaseNonRelationalService<>), typeof(BaseNonRelationalService<>));
            builder.Services.AddScoped<IProductService, ProductService>();
            builder.Services.AddScoped<IProductSaleService, ProductSaleService>();
            builder.Services.AddScoped<ISaleService, SaleService>();
        }
    }
}
