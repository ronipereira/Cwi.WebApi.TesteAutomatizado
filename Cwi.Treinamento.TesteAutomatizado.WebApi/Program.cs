using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Sinks.SystemConsole.Themes;
using System;
using System.IO;

namespace Cwi.Treinamento.TesteAutomatizado.WebApi
{
    /// <summary>
    /// Define a classe que inicializa a aplicação.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// Método principal da classe Program.
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var isDevelopment = environmentName.Equals("Development", StringComparison.OrdinalIgnoreCase);

            var configuration = new ConfigurationBuilder()
              .SetBasePath(Directory.GetCurrentDirectory())
              .AddJsonFile("appsettings.json")
              .AddJsonFile($"appsettings.{environmentName}.json", true)
              .AddEnvironmentVariables()
              .Build();

            SetupLogger(configuration, isDevelopment);

            try
            {
                CreateWebHostBuilder(args, configuration)
                  .Build()
                  .Run();
            }
            catch (Exception ex)
            {
                Log.ForContext(typeof(Program))
                  .Fatal(ex, "Host terminated unexpectedly");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        private static void SetupLogger(IConfiguration configuration, bool isDevelopment)
        {
            var outputTemplate = isDevelopment ?
              "[{Timestamp:HH:mm:ss} {Level:u3}] [{SourceContext}] {Message:lj}{NewLine}{Exception}" :
              "[{Timestamp:HH:mm:ss} {Level:u3}] [{SourceContext}] {Message:lj}\r{CloudWatchFormatedException}{NewLine}";

            Log.Logger = new LoggerConfiguration()
              .ReadFrom.Configuration(configuration)
              .Enrich.FromLogContext()
              .WriteTo.Console(outputTemplate: outputTemplate, theme: AnsiConsoleTheme.Literate)
              .CreateLogger();
        }

        /// <summary>
        /// Construtor do IWebHostBuilder.
        /// </summary>
        /// <param name="args">Os argumentos.</param>
        /// <param name="configuration">A configuração.</param>
        /// <returns></returns>
        public static IWebHostBuilder CreateWebHostBuilder(string[] args, IConfiguration configuration)
        {
            return WebHost.CreateDefaultBuilder(args)
              .UseConfiguration(configuration)
              .UseIISIntegration()
              .UseSerilog()
              .UseStartup<Startup>();
        }
    }
}
