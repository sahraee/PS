using Microsoft.Extensions.DependencyInjection.Extensions;
using PS.Application.Interfaces;
using PS.Application.Services;


namespace Microsoft.Extensions.DependencyInjection
{
    public static class AppService
    {
        public static IServiceCollection AddAppService(this IServiceCollection services)
        {
            services.TryAddEnumerable(new ServiceDescriptor[]
            {
                ServiceDescriptor.Scoped<IPersonService, PersonService>(),
               
            });

            return services;
        }
    }
}
