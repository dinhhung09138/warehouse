using System;
using System.Text;
using System.Threading.Tasks;
using Common.API;
using Core.Common.Services;
using Core.Common.Services.Interfaces;
using Warehouse.DataAccess;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Admin.Service;
using Admin.Service.Interfaces;
using Authentication.Service;
using Authentication.Service.Interfaces;
using WareHouse.Service;
using WareHouse.Service.Interfaces;
using Customer.Service;
using Customer.Service.Interfaces;

namespace WareHouseApplication.Extensions
{
    /// <summary>
    /// Service collection extension class.
    /// </summary>
    public static class ServiceCollectionExtension
    {
        /// <summary>
        /// Common configuration method.
        /// </summary>
        /// <param name="services">IServiceCollection object.</param>
        /// <param name="config">Configuration object.</param>
        /// <returns>IServiceCollection.</returns>
        public static IServiceCollection CommonConfiguration(this IServiceCollection services, IConfiguration config)
        {
            // services.AddCors();
            services.AddCors(o => o.AddPolicy("WarehouseApplicationPolicy", builder =>
            {
                builder.WithOrigins(config["CORS"])
                       .AllowAnyMethod()
                       .SetIsOriginAllowed((host) => true)
                       .SetIsOriginAllowedToAllowWildcardSubdomains()
                       .AllowAnyHeader();
            }));

            services.AddMvc(option => option.EnableEndpointRouting = false)
                    .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            return services;
        }

        /// <summary>
        /// Database configuration for application.
        /// </summary>
        /// <param name="services">IServiceCollection object.</param>
        /// <param name="config">Configuration object.</param>
        /// <returns>IServiceCollection.</returns>
        public static IServiceCollection DatabaseConfiguration(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<WareHouseContext>(
                options =>
                options.UseSqlServer(config.GetConnectionString("WareHouseConnection")),
                ServiceLifetime.Scoped);

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            return services;
        }

        /// <summary>
        /// Authentication configuration method.
        /// </summary>
        /// <param name="services">IServiceCollection object.</param>
        /// <param name="config">IConfiguration object.</param>
        /// <returns>IServiceCollection.</returns>
        public static IServiceCollection AuthenticationConfiguration(this IServiceCollection services, IConfiguration config)
        {
            services.AddAuthentication(m =>
            {
                m.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                m.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    SaveSigninToken = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = config[Core.Common.Constants.JwtConstant.ISSUER],
                    ValidAudience = config[Core.Common.Constants.JwtConstant.AUDIENCE],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config[Core.Common.Constants.JwtConstant.SECRET_KEY])),
                };
                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        if (context.Request.Path.ToString().StartsWith("/hubs/notification"))
                        {
                            context.Token = context.Request.Query["token"];
                        }

                        return Task.CompletedTask;
                    },
                };
            });

            return services;
        }

        /// <summary>
        /// api version configuration method.
        /// </summary>
        /// <param name="services">IServiceCollection object.</param>
        /// <returns>IServiceCollection.</returns>
        public static IServiceCollection ConfigApiVersion(this IServiceCollection services)
        {
            services.AddApiVersioning(options =>
            {
                // Flag used to add the version of API in the response header
                options.ReportApiVersions = true;

                // Flag used to set the default version when client has not specified any versions.
                options.AssumeDefaultVersionWhenUnspecified = true;

                // Default api version.
                options.DefaultApiVersion = new ApiVersion(1, 0);
            });

            return services;
        }

        /// <summary>
        /// signalR configuration.
        /// </summary>
        /// <param name="services">IServiceCollection object.</param>
        /// <returns>IServiceCollection.</returns>
        public static IServiceCollection ConfigSignalR(this IServiceCollection services)
        {
            services.AddSignalR(hubOptions =>
            {
                hubOptions.KeepAliveInterval = TimeSpan.FromSeconds(10);
                hubOptions.EnableDetailedErrors = true;
            });

            return services;
        }

        /// <summary>
        /// Inject application method.
        /// </summary>
        /// <param name="services">IServiceCollection object.</param>
        /// <returns>IServiceCollection.</returns>
        public static IServiceCollection InjectApplicationService(this IServiceCollection services)
        {
            services.AddSingleton<ConnectionMapping, ConnectionMapping>();

            services.AddScoped<IWareHouseUnitOfWork, WareHouseUnitOfWork>();
            services.AddScoped<IJwtTokenSecurityService, JwtTokenSecurityService>();
            services.AddScoped<ILoggerService, LoggerService>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<ISessionLogService, SessionLogService>();
            services.AddScoped<IUserService, UserService>();
            //
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<ICustomerEmployeeService, CustomerEmployeeService>();
            services.AddScoped<ICustomerStoreService, CustomerStoreService>();
            //
            services.AddScoped<IGoodsUnitService, GoodsUnitService>();
            services.AddScoped<IGoodsCategoryService, GoodsCategoryService>();
            services.AddScoped<IGoodsService, GoodsService>();
            services.AddScoped<IFeeService, FeeService>();
            services.AddScoped<IServerUploadFileService, ServerUploadFileService>();
            //
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IDepartmentService, DepartmentService>();
            services.AddScoped<ICountryService, CountryService>();
            services.AddScoped<ICityService, CityService>();
            return services;
        }
    }
}
