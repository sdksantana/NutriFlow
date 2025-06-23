using System.Reflection;
using FluentValidation;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ApplicationServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            var assembly = Assembly.GetExecutingAssembly();

            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblies(assembly);
            });

            services.AddValidatorsFromAssembly(assembly);

            return services;
        }
    }
}
