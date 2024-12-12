using PS.IOC;
using PS.Persistence.Contexts;
using PS.PBService.MiddleWares;
using PS.APIServer.Service;

namespace PS.APIServer
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();


            services.AddDbContext<PSDBContext>();
            services.AddEndpointsApiExplorer();

            services.AddGrpc();

            //Call Inject My Services
            RegisterMyServices(services);



        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
             
            }
          
            app.UseStatusCodePages();
            app.UseHttpsRedirection();
            app.UseRouting();       
            //app.UseTransactionMiddleware();



         
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGrpcService<PersonAPIService>();

            });
                  
        }

        public static void RegisterMyServices(IServiceCollection services)
        {        
            DependencyContainer.RegisterServices(services);
        }
    }
}
