using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Abilities.Application.Abilities.Queries.SearchAbilities;
using Abilities.Application.Infrastructure.Automapper;
using Abilities.Application.Interfaces.Repositories;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using IdentityServer4.AccessTokenValidation;
using Abilities.API.Options;
using Microsoft.IdentityModel.Logging;
using Abilities.Persistence.Repositories;
using Abilities.Persistence;

namespace Abilities.API
{
    public class Startup
    {
        private readonly IConfiguration _config;

        public Startup(IConfiguration configuration)
        {
            _config = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            IdentityModelEventSource.ShowPII = true;

            services.AddAuthorization();

            var authConfig = _config.GetSection("Authentication").Get<Authentication>();
            services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
                .AddIdentityServerAuthentication(options =>
                {
                    options.Authority = authConfig.Authority;
                    options.ApiName = authConfig.ApiName;
                    options.ApiSecret = authConfig.ApiSecret;
                    options.RequireHttpsMetadata = authConfig.RequiredHttpsMetadata;
                });

            services.AddAutoMapper(typeof(AutoMapperProfile).GetTypeInfo().Assembly);

            // Add repositories
            services.AddTransient<IAbilitiesRepository, AbilitiesRepository>();

            // Add MediatR
            // TODO: Add pipeline behaviors
            services.AddMediatR(typeof(SearchAbilitiesQueryHandler).GetTypeInfo().Assembly);

            var connectionString = _config.GetConnectionString("Abilities");
            // Add DbContext using Postgress provider
            services.AddDbContext<AbilitiesDbContext>(options =>
                options.UseNpgsql(connectionString));

            services.
                AddMvc(/* TODO: Add custom filters */)
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            // TODO: Register FluentValidation

            services.AddHttpClient();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDev())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseAuthentication();
            //app.UseHttpsRedirection();
            app.UseMvc();
        }
    }

    public static class EnvironmentExtesions
    {
        public static bool IsDev(this IHostingEnvironment env)
        {
            return env.IsDevelopment() || env.IsEnvironment("DevelopmentDocker");
        }
    }
}
