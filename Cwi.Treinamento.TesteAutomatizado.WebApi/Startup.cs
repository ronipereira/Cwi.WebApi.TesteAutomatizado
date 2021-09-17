using Cwi.Treinamento.TesteAutomatizado.WebApi.Configurations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Cwi.Treinamento.TesteAutomatizado
{
    /// <summary>
    /// Define a classe que configura a aplica��o.
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Obt�m as configura��es.
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Inicia uma inst�ncia da classe <see cref="Startup"/>.
        /// </summary>
        /// <param name="configuration">As configura��es.</param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// M�todos respons�vel por adicionar o servi�os ao container.
        /// </summary>
        /// <param name="services">A lista de servi�os.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddLocalization(o => o.ResourcesPath = "Globalization");
            services.AddDependencyInjectionConfiguration(Configuration);
            services.AddSwaggerConfiguration();
            services.AddJwtAuthentication(Configuration);
            services.AddVersioning();
        }

        /// <summary>
        /// M�todo respons�vel por configurar o pipeline de requisi��es HTTP.
        /// </summary>
        /// <param name="app">A classe que configura o pipeline de requisi��es.</param>
        /// <param name="env">A classe que fornece informa��es sobre o ambiente de execu��o da aplica��o.</param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseCors();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwaggerSetup();
        }
    }
}