using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using PredmetProjekat.Common.Interfaces;
using PredmetProjekat.Models.Models;
using PredmetProjekat.Repositories.Context;
using PredmetProjekat.Services.Services;
using System.Text;

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

        public static void ConfigureJWT(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtSettings = configuration.GetSection("Jwt");
            var key = jwtSettings.GetSection("key").Value;

            services.AddAuthentication(o =>
            {
                o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(o =>
                {
                    o.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = jwtSettings.GetSection("Issuer").Value,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
                        ValidateAudience = false,
                    };
                });
        }

        public static void ConfigureServices(this IServiceCollection services)
        {
            services.AddScoped<IBrandService>(serviceProvider => new BrandService(serviceProvider.GetService<IUnitOfWork>()));
            services.AddScoped<ICategoryService>(serviceProvider => new CategoryService(serviceProvider.GetService<IUnitOfWork>()));
            services.AddScoped<IProductService>(serviceProvider => new ProductService(serviceProvider.GetService<IUnitOfWork>()));
            services.AddScoped<IRegisterService>(serviceProvider => new RegisterService(serviceProvider.GetService<IUnitOfWork>()));
            services.AddScoped<IAdminService>(serviceProvider => new AdminService(serviceProvider.GetService<UserManager<Account>>(),
                                                                                    serviceProvider.GetService<IMapper>()));
            services.AddScoped<IEmployeeService>(serviceProvider => new EmployeeService(serviceProvider.GetService<IUnitOfWork>(),
                                                                                            serviceProvider.GetService<UserManager<Account>>(),
                                                                                            serviceProvider.GetService<IMapper>()));
            services.AddScoped<IAuthManager>(serviceProvider => new AuthManager(serviceProvider.GetService<UserManager<Account>>(),
                                                                                    serviceProvider.GetService<IConfiguration>()));
        }
    }
}
