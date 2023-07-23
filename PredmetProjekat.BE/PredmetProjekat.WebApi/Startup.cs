using Microsoft.EntityFrameworkCore;
using PredmetProjekat.Common.AutoMapper;
using PredmetProjekat.Common.Interfaces;
using PredmetProjekat.Repositories.Context;
using PredmetProjekat.Repositories.Extensions;
using PredmetProjekat.Repositories.UnitOfWork;

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
            services.AddDbContext<StoreContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DbConnectionString")));
            services.AddTransient<IUnitOfWork, UnitOfWork>();

            services.AddAutoMapper(typeof(MappingProfile));

            
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
