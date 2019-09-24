using Abilities.Persistence;
using Abilities.Persistence.DataSeeding;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Abilities.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var env = host.Services.GetRequiredService<IWebHostEnvironment>();
                if (env.IsDev())
                {
                    var context = scope.ServiceProvider.GetService<AbilitiesDbContext>();
                    context.Database.Migrate();

                    AbilitiesInitializer.Initialize(context);
                }
            }

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
