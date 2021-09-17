using System;
using Cwi.Treinamento.TesteAutomatizado.IoC;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cwi.Treinamento.TesteAutomatizado.WebApi.Configurations
{
    /// <summary>
    /// Define a classe responsável por configurar a injeção de dependência.
    /// </summary>
    public static class DependencyInjectionConfig
    {
        /// <summary>
        /// Configura a injeção de dependência.
        /// </summary>
        /// <param name="services">A lista de serviços.</param>
        /// <param name="configuration">A configuração da aplicação.</param>
        public static void AddDependencyInjectionConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            NativeInjectorBootStrapper.RegisterServices(services, configuration);
        }
    }
}
