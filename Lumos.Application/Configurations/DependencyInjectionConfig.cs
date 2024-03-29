using Lumos.Application.Interfaces.Management;
using Lumos.Application.Repositories;
using Lumos.Application.Services.Management;
using Lumos.Data.Models.Management;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Reflection;

namespace Lumos.Application.Configurations
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterDependencies(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<LumosSession>();

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            // Registrar automaticamente todos os tipos que implementam ITransientDependency
            RegisterTransientDependencies(services, Assembly.GetExecutingAssembly());


            #region Interface X Classe 

            AddScopedServices<IUserAppService, UserAppService>(services);

            #endregion

            #region ServiceBase X AppService

            services.AddScoped<LumosAppServiceBase<User>, UserAppService>(); 
            
            #endregion
        }

        public static void RegisterTransientDependencies(IServiceCollection services, Assembly assembly)
        {
            var transientTypes = assembly.GetTypes()
                .Where(type => type.IsClass && !type.IsAbstract &&
                               typeof(ITransientDependency).IsAssignableFrom(type));

            foreach (var type in transientTypes)
            {
                services.AddTransient(type);

                // Verifica se o tipo implementa alguma interface genérica e registra essa interface também
                var interfaces = type.GetInterfaces()
                    .Where(i => i.IsGenericType && typeof(ITransientDependency).IsAssignableFrom(i));

                foreach (var @interface in interfaces)
                {
                    services.AddTransient(@interface, type);
                }
            }
        }

        public static void AddScopedServices<TService, TImplementation>(IServiceCollection services) where TService : class where TImplementation : class, TService
        {
            services.AddScoped<TService, TImplementation>();
        }
    }
}
