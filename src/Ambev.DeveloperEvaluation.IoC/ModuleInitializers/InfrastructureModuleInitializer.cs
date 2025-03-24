using Ambev.DeveloperEvaluation.Domain.Interfaces.Context;
using Ambev.DeveloperEvaluation.Domain.Interfaces.Repositories.Common;
using Ambev.DeveloperEvaluation.Domain.Interfaces.Repositories.NonRelationalDatabase;
using Ambev.DeveloperEvaluation.Domain.Interfaces.Repositories.RelationalDatabase;
using Ambev.DeveloperEvaluation.ORM.Contexts;
using Ambev.DeveloperEvaluation.ORM.Repositories.Common;
using Ambev.DeveloperEvaluation.ORM.Repositories.NonRelationalDatabase;
using Ambev.DeveloperEvaluation.ORM.Repositories.RelationalDatabase;
using Ambev.DeveloperEvaluation.ORM.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Ambev.DeveloperEvaluation.IoC.ModuleInitializers;

public class InfrastructureModuleInitializer : IModuleInitializer
{
    public void Initialize(WebApplicationBuilder builder)
    {
        var connectionStringMongoDb = builder.Configuration.GetSection("MongoDbSettings");
        builder.Services.Configure<MongoDbSettings>(connectionStringMongoDb);
        builder.Services.AddScoped<IMongoDbContext, MongoDBContext>();

        builder.Services.AddDbContext<PostgreContext>(options =>
            options.UseNpgsql(
                builder.Configuration.GetConnectionString("PostgreConnection"),
                b => b.MigrationsAssembly("Ambev.DeveloperEvaluation.ORM")
            )
        );

        builder.Services.AddScoped(typeof(IBaseRelationalDatabaseRepository<>), typeof(BaseRelationalDatabaseRepository<>));
        //builder.Services.AddScoped<IAdressRepository, AdressRepository>();
        builder.Services.AddScoped<IAffiliateRepository, AffiliateRepository>();
        builder.Services.AddScoped<IUserRepository, UserRepository>();

        builder.Services.AddScoped(typeof(IBaseMongoDBRepository<>), typeof(BaseMongoDBRepository<>));
        builder.Services.AddScoped<IProductRepository, ProductRepository>();
        //builder.Services.AddScoped<IProductSaleRepository, ProductSaleRepository>();
        builder.Services.AddScoped<ISaleRepository, SaleRepository>();
    }
}