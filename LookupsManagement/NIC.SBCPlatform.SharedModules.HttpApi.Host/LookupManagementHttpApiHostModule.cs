using Hangfire;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using NIC.SBCPlatform.SharedModules.EntityFrameworkCore;
using NIC.SBCPlatform.SharedModules.Filters;
using NIC.SBCPlatform.SharedModules.LookupManagement;
using StackExchange.Redis;
using System;
using System.IO;
using System.Linq;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Autofac;
using Volo.Abp.Caching;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Volo.Abp.Settings;
using Volo.Abp.VirtualFileSystem;

namespace NIC.SBCPlatform.SharedModules
{
    [DependsOn(
        typeof(LookupManagementHttpApiModule),
        typeof(AbpAutofacModule),
        typeof(LookupManagementApplicationModule),
        typeof(EntityFrameworkCoreDbMigrationsModule),
        typeof(AbpAspNetCoreMvcModule)

        )]
    public class LookupManagementHttpApiHostModule : AbpModule
    {
        private const string DefaultCorsPolicyName = "Default";

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var configuration = context.Services.GetConfiguration();
            var hostingEnvironment = context.Services.GetHostingEnvironment();

            ConfigureConventionalControllers();
            ConfigureAuthentication(context, configuration);
            ConfigureAuthorization(context, configuration);
            ConfigureLocalization();
            ConfigureSettings(configuration);
            ConfigureCache(configuration);
            ConfigureVirtualFileSystem(context);
            //ConfigureRedis(context, configuration, hostingEnvironment);
            ConfigureCors(context, configuration);
            ConfigureSwaggerServices(context);
            ConfigureHangfire(context, configuration);

            ConfigureAutoApiControllers();


        }

        private void ConfigureAutoApiControllers()
        {
            Configure<AbpAspNetCoreMvcOptions>(options =>
            {
                options.ConventionalControllers.Create(typeof(LookupManagementApplicationModule).Assembly);
            });
        }
        private void ConfigureHangfire(ServiceConfigurationContext context, IConfiguration configuration)
        {
            context.Services.AddHangfire(config =>
            {
                config.UseSqlServerStorage(configuration.GetConnectionString("LookupManagementHangfireDb"));
            });
        }
        private void ConfigureSettings(IConfiguration configuration)
        {
            Configure<AbpSettingOptions>(options =>
            {
                options.ValueProviders.Add<ConfigurationSettingValueProvider>();
            });
        }
        private void ConfigureCache(IConfiguration configuration)
        {
            Configure<AbpDistributedCacheOptions>(options =>
            {
                options.KeyPrefix = "LookupManagement:";
            });
        }

        private void ConfigureVirtualFileSystem(ServiceConfigurationContext context)
        {
            var hostingEnvironment = context.Services.GetHostingEnvironment();

            if (hostingEnvironment.IsDevelopment())
            {
                Configure<AbpVirtualFileSystemOptions>(options =>
                {
                    //options.FileSets.AddEmbedded<LookupManagementDomainSharedModule>();
                    //options.FileSets.AddEmbedded<LookupManagementDomainModule>();
                    //options.FileSets.AddEmbedded<LookupManagementApplicationContractsModule>();
                    //options.FileSets.AddEmbedded<LookupManagementApplicationModule>();
                    // var d = Path.Combine(hostingEnvironment.ContentRootPath,
                    //     $"..{Path.DirectorySeparatorChar}NIC.SBCPlatform.SharedModules.LookupManagement.Domain.Shared");
                    // options.FileSets.ReplaceEmbeddedByPhysical<LookupManagementDomainSharedModule>(
                    //     Path.Combine(hostingEnvironment.ContentRootPath,
                    //     $"..{Path.DirectorySeparatorChar}NIC.SBCPlatform.SharedModules.LookupManagement.Domain.Shared"));

                    // d = Path.Combine(hostingEnvironment.ContentRootPath,
                    //     $"..{Path.DirectorySeparatorChar}NIC.SBCPlatform.SharedModules.LookupManagement.Domain");
                    // options.FileSets.ReplaceEmbeddedByPhysical<LookupManagementDomainModule>(Path.Combine(hostingEnvironment.ContentRootPath,
                    //     $"..{Path.DirectorySeparatorChar}NIC.SBCPlatform.SharedModules.LookupManagement.Domain"));

                    // d = Path.Combine(hostingEnvironment.ContentRootPath,
                    //     $"..{Path.DirectorySeparatorChar}NIC.SBCPlatform.SharedModules.LookupManagement.Application.Contracts");

                    //options.FileSets.ReplaceEmbeddedByPhysical<LookupManagementApplicationContractsModule>(
                    //     Path.Combine(hostingEnvironment.ContentRootPath,
                    //     $"..{Path.DirectorySeparatorChar}NIC.SBCPlatform.SharedModules.LookupManagement.Application.Contracts"));

                    // d = Path.Combine(hostingEnvironment.ContentRootPath,
                    //     $"..{Path.DirectorySeparatorChar}NIC.SBCPlatform.SharedModules.LookupManagement.Application");

                    // options.FileSets.ReplaceEmbeddedByPhysical<LookupManagementApplicationModule>(Path.Combine(hostingEnvironment.ContentRootPath,
                    //     $"..{Path.DirectorySeparatorChar}NIC.SBCPlatform.SharedModules.LookupManagement.Application"));

                    //options.FileSets.ReplaceEmbeddedByPhysical<LookupManagementDomainSharedModule>(Path.Combine(hostingEnvironment.ContentRootPath, $"..{Path.DirectorySeparatorChar}NIC.SBCPlatform.SharedModules.LookupManagement.Domain.Shared"));
                    //options.FileSets.ReplaceEmbeddedByPhysical<LookupManagementDomainModule>(Path.Combine(hostingEnvironment.ContentRootPath, $"..{Path.DirectorySeparatorChar}NIC.SBCPlatform.SharedModules.LookupManagement.Domain"));
                    //options.FileSets.ReplaceEmbeddedByPhysical<LookupManagementApplicationContractsModule>(Path.Combine(hostingEnvironment.ContentRootPath, $"..{Path.DirectorySeparatorChar}NIC.SBCPlatform.SharedModules.LookupManagement.Application.Contracts"));
                    //options.FileSets.ReplaceEmbeddedByPhysical<LookupManagementApplicationModule>(Path.Combine(hostingEnvironment.ContentRootPath, $"..{Path.DirectorySeparatorChar}NIC.SBCPlatform.SharedModules.LookupManagement.Application"));

                    //options.FileSets.ReplaceEmbeddedByPhysical<LookupManagementDomainSharedModule>(Path.Combine(hostingEnvironment.ContentRootPath, $"..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}modules{Path.DirectorySeparatorChar}issue{Path.DirectorySeparatorChar}src{Path.DirectorySeparatorChar}NIC.SBCPlatform.SharedModules.HttpApi.Host.Domain.Shared"));
                    //options.FileSets.ReplaceEmbeddedByPhysical<LookupManagementDomainModule>(Path.Combine(hostingEnvironment.ContentRootPath, $"..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}modules{Path.DirectorySeparatorChar}issue{Path.DirectorySeparatorChar}src{Path.DirectorySeparatorChar}NIC.SBCPlatform.SharedModules.HttpApi.Host.Domain"));
                    //options.FileSets.ReplaceEmbeddedByPhysical<LookupManagementApplicationContractsModule>(Path.Combine(hostingEnvironment.ContentRootPath, $"..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}modules{Path.DirectorySeparatorChar}issue{Path.DirectorySeparatorChar}src{Path.DirectorySeparatorChar}NIC.SBCPlatform.SharedModules.HttpApi.Host.Application.Contracts"));
                    //options.FileSets.ReplaceEmbeddedByPhysical<LookupManagementApplicationModule>(Path.Combine(hostingEnvironment.ContentRootPath, $"..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}modules{Path.DirectorySeparatorChar}issue{Path.DirectorySeparatorChar}src{Path.DirectorySeparatorChar}NIC.SBCPlatform.SharedModules.HttpApi.Host.Application"));
                });
            }
        }

        private void ConfigureConventionalControllers()
        {
            //TODO : validate it works on excluding services
            Configure<AbpAspNetCoreMvcOptions>(options =>
            {
                options.ConventionalControllers.Create(typeof(LookupManagementApplicationModule).Assembly, opts =>
                {
                    opts.TypePredicate = type => { return true; };
                });
            });
        }

        private void ConfigureAuthentication(ServiceConfigurationContext context, IConfiguration configuration)
        {
            context.Services.AddAuthentication("Bearer").AddIdentityServerAuthentication(options =>
            {
                options.Authority = configuration.GetValue<string>("IdentityServer:URL");// "https://nicidpdevapp.azurewebsites.net/";
                options.ApiName = configuration.GetValue<string>("IdentityServer:ApiName");//"clientapi1";
                options.ApiSecret = configuration.GetValue<string>("IdentityServer:ApiSecret"); //"apisecret";
                options.SaveToken = true;
            });
        }


        private void ConfigureAuthorization(ServiceConfigurationContext context, IConfiguration configuration)
        {
            context.Services.AddAuthorization();

           

            
            //context.Services.AddAuthentication("Bearer")
            // .AddIdentityServerAuthentication(options =>
            // {
            //     options.Authority = "https://nicidpdevapp.azurewebsites.net";
            //     options.ApiName = "clientapi1";
            //     options.ApiSecret = "apisecret";
            //     options.SaveToken = true;
            // });
        }

        private static void ConfigureSwaggerServices(ServiceConfigurationContext context)
        {
            context.Services.AddSwaggerGen(
                options =>
                {
                    options.SwaggerDoc("v1", new OpenApiInfo { Title = "LookupManagement API", Version = "v1" });
                    options.DocInclusionPredicate((docName, description) => true);
                    options.DocumentFilter<RemoveUnusedEndpointFilter>();
                    options.SchemaFilter<RemoveUnusedSchemaFilter>();
                });
        }

        private void ConfigureLocalization()
        {
            Configure<AbpLocalizationOptions>(options =>
            {
                options.Languages.Add(new LanguageInfo("ar", "ar", "العربية"));
                options.Languages.Add(new LanguageInfo("en", "en", "English"));
            });
        }



        private void ConfigureRedis(
            ServiceConfigurationContext context,
            IConfiguration configuration,
            IWebHostEnvironment hostingEnvironment)
        {
            context.Services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = configuration["Redis:Configuration"];
            });

            if (!hostingEnvironment.IsDevelopment())
            {
                var redis = ConnectionMultiplexer.Connect(configuration["Redis:Configuration"]);
                context.Services
                    .AddDataProtection()
                    .PersistKeysToStackExchangeRedis(redis, "LookupManagement-Protection-Keys");
            }
        }

        private void ConfigureCors(ServiceConfigurationContext context, IConfiguration configuration)
        {
            context.Services.AddCors(options =>
            {
                options.AddPolicy(DefaultCorsPolicyName, builder =>
                {
                    builder
                        .WithOrigins(
                            configuration["App:CorsOrigins"]
                                .Split(",", StringSplitOptions.RemoveEmptyEntries)
                                .Select(o => o.RemovePostFix("/"))
                                .ToArray()
                        )
                        .WithAbpExposedHeaders()
                        .SetIsOriginAllowedToAllowWildcardSubdomains()
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials();
                });
            });
        }

        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            var app = context.GetApplicationBuilder();
            var env = context.GetEnvironment();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // app.UseErrorPage();
            }

            app.UseCorrelationId();
            app.UseVirtualFiles();
            app.UseRouting();
            app.UseCors(DefaultCorsPolicyName);

            app.UseAbpRequestLocalization();
            app.UseAuthentication();

            app.UseAuthorization();

            //app.UseSwagger(c =>
            //{
            //    c.SerializeAsV2 = true;
            //});
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "LookupManagement API");
            });

            app.UseAuditing();

            app.UseConfiguredEndpoints();

            app.UseHangfireServer();
            app.UseHangfireDashboard();
        }
    }
}
