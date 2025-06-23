using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using NutriFlow.Application;
using NutriFlow.Application.Requests.Patient;
using NutriFlow.Domain.Queries.Interfaces;
using NutriFlow.Domain.Repositories;
using NutriFlow.Domain.Services;
using NutriFlow.Domain.Settings;
using NutriFlow.Infrastructure;
using NutriFlow.Infrastructure.Identity;
using NutriFlow.Infrastructure.Queries;
using NutriFlow.Infrastructure.Repositories;
using NutriFlow.Infrastructure.Services;
using System.Text;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class InfrastructureServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration config)
        {
            int.TryParse(Environment.GetEnvironmentVariable("JWT_EXPIRATION"), out var expiration);

            services.Configure<JwtSettings>(opts =>
            {
                opts.Secret = Environment.GetEnvironmentVariable("JWT_SECRET")!;
                opts.Issuer = Environment.GetEnvironmentVariable("JWT_ISSUER")!;
                opts.Audience = Environment.GetEnvironmentVariable("JWT_AUDIENCE")!;
                opts.ExpirationMinutes = expiration > 0 ? expiration : 120;
            });

            var jwtSettings = new JwtSettings
            {
                Secret = Environment.GetEnvironmentVariable("JWT_SECRET")!,
                Issuer = Environment.GetEnvironmentVariable("JWT_ISSUER")!,
                Audience = Environment.GetEnvironmentVariable("JWT_AUDIENCE")!,
                ExpirationMinutes = expiration > 0 ? expiration : 120
            };

            //var jwtSettings = config.GetSection("JwtSettings").Get<JwtSettings>();

            services.AddDbContext<NutriFlowDbContext>(opts => opts
                        .UseNpgsql(Environment.GetEnvironmentVariable("RAILWAY_CONNECTION_STRING") ?? config.GetConnectionString("DefaultConnection"))
                        .LogTo(Console.WriteLine, LogLevel.Information)
                        .EnableSensitiveDataLogging());

            services.AddIdentity<User, IdentityRole<Guid>>()
                    .AddEntityFrameworkStores<NutriFlowDbContext>()
                    .AddDefaultTokenProviders();

            services.AddScoped<HashService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IJwtTokenService, JwtTokenService>();
            services.AddScoped<IPatientService, PatientService>();
            services.AddScoped<INutritionistPatientService, NutritionistPatientService>();

            services.AddScoped<INutritionistPatientRepository, NutritionistPatientRepository>();
            services.AddScoped<INutritionistRepository, NutritionistRepository>();
            services.AddScoped<IPatientRepository, PatientRepository>();
            services.AddScoped<IPatientQuery, PatientQuery>();
            services.AddScoped<INutritionistPatientQuery, NutritionistPatientQuery>();
            services.AddScoped<INutritionistQuery, NutritionistQuery>();


            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                var keyBytes = Encoding.UTF8.GetBytes(jwtSettings.Secret);
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = jwtSettings.Issuer,
                    ValidateAudience = true,
                    ValidAudience = jwtSettings.Audience,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(keyBytes),
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
                options.Events = new JwtBearerEvents
                {
                    OnChallenge = context =>
                    {
                        context.HandleResponse();
                        context.Response.StatusCode = 401;
                        context.Response.ContentType = "application/json";
                        return context.Response.WriteAsync(
                            "{\"error\":\"Não autorizado - token inválido ou ausente\"}"
                        );
                    }
                };
            });

            services.AddAuthorization();

            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblyContaining<AddPatientRecipeRequest>();
            });

            services.AddValidatorsFromAssemblyContaining<AddRecipeRequestRequestValidator>();
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            return services;
        }
    }
}
