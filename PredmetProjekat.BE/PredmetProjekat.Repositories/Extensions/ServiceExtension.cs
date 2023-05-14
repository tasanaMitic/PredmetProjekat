using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using PredmetProjekat.Common.Interfaces;
using PredmetProjekat.Models.Models;
using PredmetProjekat.Repositories.Context;
using PredmetProjekat.Services.Services;

namespace PredmetProjekat.Repositories.Extensions
{
    public static class ServiceExtension
    {
        public static void ConfigureIdentity(this IServiceCollection services)
        {
            var builder = services.AddIdentityCore<Account>(q => q.User.RequireUniqueEmail = true);

            builder = new IdentityBuilder(builder.UserType, typeof(IdentityRole), services);
            builder.AddEntityFrameworkStores<StoreContext>().AddDefaultTokenProviders();
        }

        public static void ConfigureServices(this IServiceCollection services)
        {
            services.AddScoped<IBrandService>(serviceProvider => new BrandService(serviceProvider.GetService<IUnitOfWork>()));
            services.AddScoped<ICategoryService>(serviceProvider => new CategoryService(serviceProvider.GetService<IUnitOfWork>()));
            services.AddScoped<IProductService>(serviceProvider => new ProductService(serviceProvider.GetService<IUnitOfWork>()));
            services.AddScoped<IRegisterService>(serviceProvider => new RegisterService(serviceProvider.GetService<IUnitOfWork>()));
            services.AddScoped<IAdminService>(serviceProvider => new AdminService(serviceProvider.GetService<IUnitOfWork>()));
            services.AddScoped<IEmployeeService>(serviceProvider => new EmployeeService(serviceProvider.GetService<IUnitOfWork>()));
        }
    }
}
