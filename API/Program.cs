using System;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using Persistence;

namespace API
{
    public class Program
    {
        public async static Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using var scope = host.Services.CreateScope();

            var services = scope.ServiceProvider;

            try
            {
                // programmatically run migration every time app is loaded
                var context = services.GetRequiredService<DataContext>();
                await context.Database.MigrateAsync( );
                await Seed.SeedData( context );
            }
            catch ( Exception ex )
            {
                var logger = services.GetRequiredService<ILogger<Program>>();
                logger.LogError( ex, "An error occurred during migration" );
            }


            await host.RunAsync( );
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
