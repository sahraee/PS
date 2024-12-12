using Microsoft.Extensions.DependencyInjection;
 

namespace PS.IOC
{
    public class DependencyContainer
    {
        public static void RegisterServices(IServiceCollection service)        {
             
            service.AddAppService().AddPersistenceService();
        }
    }
}
