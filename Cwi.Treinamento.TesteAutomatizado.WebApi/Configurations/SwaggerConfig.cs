using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Cwi.Treinamento.TesteAutomatizado.WebApi.Configurations.SwaggerFilters;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Serilog;

namespace Cwi.Treinamento.TesteAutomatizado.WebApi.Configurations
{
    /// <summary>
    /// Realiza configuração do swagger
    /// </summary>
    public static class SwaggerConfig
    {
        /// <summary>
        /// Configura o Swagger.
        /// </summary>
        /// <param name="services">Os serviços do container.</param>
        public static void AddSwaggerConfiguration(this IServiceCollection services)
        {
            var log = Log.ForContext(typeof(SwaggerConfig));

            log.Information("Configurando Swagger");

            if (services == null) throw new ArgumentNullException(nameof(services));

            var apiFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Assembly.GetExecutingAssembly().GetName().Name + ".xml");

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Web API",
                    Description = "Está é uma API de exemplo"
                });

                c.OperationFilter<ParameterRemoveFilter>("ValidationResult");

                c.IncludeXmlComments(apiFilePath);

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "Adicione o token de autenticação no campo abaixo \"Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
               {
                  {
                    new OpenApiSecurityScheme
                    {
                      Reference = new OpenApiReference
                      {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                      },
                      Scheme = "oauth2",
                      Name = "Bearer",
                      In = ParameterLocation.Header,
                    },
                    new List<string>()
                  }
              });
            });
        }

        /// <summary>
        /// Define que o Swagger deve ser usado na aplicaçõo.
        /// </summary>
        /// <param name="app">O builder da aplicação.</param>
        public static void UseSwaggerSetup(this IApplicationBuilder app)
        {
            if (app == null) throw new ArgumentNullException(nameof(app));

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
            });
        }
    }
}
