using Cwi.Treinamento.TesteAutomatizado.WebApi.Configurations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Cwi.Treinamento.TesteAutomatizado
{
    /// <summary>
    /// Define a classe que configura a aplicação.
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Obtém as configurações.
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Inicia uma instância da classe <see cref="Startup"/>.
        /// </summary>
        /// <param name="configuration">As configurações.</param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Métodos responsável por adicionar o serviços ao container.
        /// </summary>
        /// <param name="services">A lista de serviços.</param>
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
        /// Método responsável por configurar o pipeline de requisições HTTP.
        /// </summary>
        /// <param name="app">A classe que configura o pipeline de requisições.</param>
        /// <param name="env">A classe que fornece informações sobre o ambiente de execução da aplicação.</param>
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