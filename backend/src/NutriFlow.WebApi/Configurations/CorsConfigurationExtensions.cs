namespace NutriFlow.WebApi.Configurations
{
    public static class CorsConfigurationExtensions
    {
        public static IServiceCollection AddCorsPolicy(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("NutriflowCors", policy =>
                {
                    policy.WithOrigins(
                        "https://nutriflow-frontend.up.railway.app"
                    )
                    .AllowAnyHeader()
                    .AllowAnyMethod();
                });
            });

            return services;
        }
    }
}
