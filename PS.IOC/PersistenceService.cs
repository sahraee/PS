using Microsoft.Extensions.DependencyInjection.Extensions;
using PS.Domain.Interfaces;
using PS.Persistence.Repository;

namespace Microsoft.Extensions.DependencyInjection
{
  
    public static class PersistenceService
    {

        public static IServiceCollection AddPersistenceService(this IServiceCollection services)
        {
            services.TryAddEnumerable(new ServiceDescriptor[]
            {
                ServiceDescriptor.Scoped<IPersonRepository, PersonRepository>()              
             
            });

            return services;
        }
    }
}
