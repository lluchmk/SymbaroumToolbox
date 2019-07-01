using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Abilities.Persistence.DataSeeding;
using Abilities.Persistence;

namespace Abilities.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateWebHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var env = host.Services.GetRequiredService<IHostingEnvironment>();
                if (env.IsDev())
                {
                    var context = scope.ServiceProvider.GetService<AbilitiesDbContext>();
                    context.Database.Migrate();

                    AbilitiesInitializer.Initialize(context);
                }
            }

            host.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
