using PredmetProjekat.Common.AutoMapper;
using PredmetProjekat.WebApi.Extensions;

namespace PredmetProjekat.WebApi
{
    public class Startup
    {
        public Startup(IConfigurationRoot configuration)
        {
            Configuration = configuration;
        }
        public IConfigurationRoot Configuration { get; }
        public void ConfigureServices(IServiceCollection services)
        {
            services.ConfigureRepository(Configuration);
            services.ConfigureAutoMapper();          
            
            services.AddAuthentication();
            services.ConfigureIdentity();
            services.ConfigureJWT(Configuration);

            services.ConfigureServices();

            services.AddControllers();
        }
        public void Configure(IApplicationBuilder app)
        {
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(x => x.MapControllers());
        }
    }
}
