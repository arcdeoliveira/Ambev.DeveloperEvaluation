using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ambev.DeveloperEvaluation.ORM.Contexts;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace Ambev.DeveloperEvaluation.Integration
{
    public class CustomWebApplicationFactory : WebApplicationFactory<Program>
    {
        private readonly string _connectionString;

        public CustomWebApplicationFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                // Remove o ApplicationDbContext existente
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType == typeof(DbContextOptions<PostgreContext>));

                if (descriptor != null)
                    services.Remove(descriptor);

                //// Registra o ApplicationDbContext com o container PostgreSQL
                //services.AddDbContext<PostgreContext>(options =>
                //{
                //    options.UseNpgsql(_connectionString);
                //});

                //// Executa migrations no banco de teste
                //var sp = services.BuildServiceProvider();

                //using var scope = sp.CreateScope();
                //var db = scope.ServiceProvider.GetRequiredService<PostgreContext>();
                //db.Database.Migrate();
            });
        }
    }
}
