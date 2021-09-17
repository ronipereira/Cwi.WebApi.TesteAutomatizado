using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Cwi.Treinamento.TesteAutomatizado.WebApi.Configurations
{
    /// <summary>
    /// Define a classe responsável por configurar o versionamento de API.
    /// </summary>
    public static class ApiVersioningConfig
    {
        /// <summary>
        /// Configura o versionamento da API.
        /// </summary>
        /// <param name="services">A lista de serviços.</param>
        public static void AddVersioning(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.AddApiVersioning(p =>
            {
                p.DefaultApiVersion = new ApiVersion(1, 0);
                p.ReportApiVersions = true;
                p.AssumeDefaultVersionWhenUnspecified = true;
            });

            services.AddVersionedApiExplorer(p =>
            {
                p.GroupNameFormat = "'v'VVV";
                p.SubstituteApiVersionInUrl = true;
            });
        }
    }
}