using Microsoft.EntityFrameworkCore;
using PredmetProjekat.Common.Interfaces;
using PredmetProjekat.Repositories.Context;
using PredmetProjekat.Repositories.UnitOfWork;
using PredmetProjekat.Services.Services;

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

            services.AddScoped<IBrandService>(serviceProvider => new BrandService(serviceProvider.GetService<IUnitOfWork>()));
            services.AddScoped<ICategoryService>(serviceProvider => new CategoryService(serviceProvider.GetService<IUnitOfWork>()));
            services.AddScoped<IProductService>(serviceProvider => new ProductService(serviceProvider.GetService<IUnitOfWork>()));
            services.AddScoped<IRegisterService>(serviceProvider => new RegisterService(serviceProvider.GetService<IUnitOfWork>()));

            services.AddControllers();
        }
        public void Configure(IApplicationBuilder app)
        {
            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseEndpoints(x => x.MapControllers());
        }
    }
}
