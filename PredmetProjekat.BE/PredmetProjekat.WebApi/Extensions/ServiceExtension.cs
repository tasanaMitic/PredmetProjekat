﻿using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PredmetProjekat.Common.AutoMapper;
using PredmetProjekat.Common.Interfaces;
using PredmetProjekat.Common.Interfaces.IService;
using PredmetProjekat.Models.Models;
using PredmetProjekat.Repositories.Context;
using PredmetProjekat.Repositories.UnitOfWork;
using PredmetProjekat.Services.Services;
using PredmetProjekat.Services.Services.AccountServices;
using System.Text;

namespace PredmetProjekat.WebApi.Extensions
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
            services.AddScoped<IBrandService>(serviceProvider => new BrandService(serviceProvider.GetService<IUnitOfWork>(), serviceProvider.GetService<IMapper>()));
            services.AddScoped<ICategoryService>(serviceProvider => new CategoryService(serviceProvider.GetService<IUnitOfWork>(), serviceProvider.GetService<IMapper>()));
            services.AddScoped<IProductService>(serviceProvider => new ProductService(serviceProvider.GetService<IUnitOfWork>(), serviceProvider.GetService<IMapper>()));
            services.AddScoped<IRegisterService>(serviceProvider => new RegisterService(serviceProvider.GetService<IUnitOfWork>(), serviceProvider.GetService<IMapper>()));
            services.AddScoped<IAdminService>(serviceProvider => new AdminService(serviceProvider.GetService<UserManager<Account>>(), serviceProvider.GetService<IMapper>()));
            services.AddScoped<IEmployeeService>(serviceProvider => new EmployeeService(serviceProvider.GetService<UserManager<Account>>(), serviceProvider.GetService<IMapper>()));
            services.AddScoped<IAuthManager>(serviceProvider => new AuthManager(serviceProvider.GetService<UserManager<Account>>(), serviceProvider.GetService<IConfiguration>()));
            services.AddScoped<IAccountService>(serviceProvider => new AccountService(serviceProvider.GetService<IMapper>(), serviceProvider.GetService<UserManager<Account>>()));
        }

        public static void ConfigureRepository(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<StoreContext>(options => options.UseSqlServer(configuration.GetConnectionString("DbConnectionString")));
            services.AddTransient<IUnitOfWork, UnitOfWork>();
        }

        public static void ConfigureAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MappingProfile));
        }
    }
}
