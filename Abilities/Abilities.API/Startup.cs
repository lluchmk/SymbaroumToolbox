using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Abilities.Application.Abilities.Queries.SearchAbilities;
using Abilities.Application.Infrastructure.Automapper;
using Abilities.Application.Interfaces.Repositories;
using Abilities.Persistence.EFInMemory;
using Abilities.Persistence.EFInMemory.Repositories;
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

namespace Abilities.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthorization();

            services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
                .AddIdentityServerAuthentication(options =>
                {
                    options.Authority = "http://localhost:5000"; // TODO: To configuration
                    options.ApiName = "abilities";
                    //options.ApiSecret = "abilitiesSecret"; // TODO: To configuration
                    options.RequireHttpsMetadata = false;
                });

            services.AddAutoMapper(typeof(AutoMapperProfile).GetTypeInfo().Assembly);

            // Add repositories
            services.AddTransient<IAbilitiesRepository, AbilitiesRepository>();

            // Add MediatR
            // TODO: Add pipeline behaviors
            services.AddMediatR(typeof(SearchAbilitiesQueryHandler).GetTypeInfo().Assembly);

            // Add DbContext using InMemory provider
            services.AddDbContext<AbilitiesDbContext>(options =>
                options.UseInMemoryDatabase("SymbaroumAbilities"));

            services.
                AddMvc(/* TODO: Add custom filters */)
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            // TODO: Register FluentValidation
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
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
}
