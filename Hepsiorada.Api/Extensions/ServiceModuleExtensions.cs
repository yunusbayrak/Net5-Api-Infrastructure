using Hangfire;
using Hangfire.PostgreSql;
using Hepsiorada.Api.BackgroundJobs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hepsiorada.Api.Extensions
{
    public static class ServiceModuleExtensions
    {
        public static void ConfigureAuthorization(this IServiceCollection services)
        {
            var securityKey = "hepsiorada_api_security_key_for_token_validation$hepsiorada";
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(securityKey));

            services.AddAuthorization(auth =>
            {
                auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser().Build());
            });

            services
                .AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = symmetricSecurityKey,
                        ValidateIssuer = true,
                        ValidIssuer = "hepsiorada.api",
                        ValidateAudience = true,
                        ValidAudience = "HepsioradaApps"
                    };
                });
        }

        public static void ConfigureHangfire(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ITransferDataToMongo, TransferDataToMongo>();
            services.AddHangfire(config => config
                    .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                    .UseSimpleAssemblyNameTypeSerializer()
                    .UseRecommendedSerializerSettings()
                    .UsePostgreSqlStorage(configuration.GetConnectionString("PostgreSQL"), new PostgreSqlStorageOptions
                    {
                        SchemaName = "jobs"
                    }));

            // Add the processing server as IHostedService
            services.AddHangfireServer();
        }

        public static void UseHangfire(this IApplicationBuilder app)
        {
            app.UseHangfireDashboard(); //Will be available under http://localhost:5000/hangfire"
        }
    }
}
