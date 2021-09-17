using Credz.Portador.Api.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Serilog;
using System;

namespace Cwi.Treinamento.TesteAutomatizado.WebApi.Configurations
{
    /// <summary>
    /// Realiza configuração da autenticação.
    /// </summary>
    public static class AuthenticationConfig
    {
        /// <summary>
        /// Configura a autenticação.
        /// </summary>
        /// <param name="services">Os serviços do container.</param>
        /// <param name="configuration">A configuração da aplicação.</param>
        public static void AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            var log = Log.ForContext(typeof(AuthenticationConfig));

            log.Information("Configurando Autenticação");

            ConfigureCors(services);

            var tokenConfigurations = GetTokenConfiguration(configuration);
            services.AddSingleton(tokenConfigurations);

            var signingConfigurations = GetSigningConfiguration(configuration);
            services.AddSingleton(signingConfigurations);

            services.AddAuthentication(authOptions =>
            {
                authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(bearerOptions =>
            {
                bearerOptions.TokenValidationParameters.IssuerSigningKey = signingConfigurations.SigningKey;
                bearerOptions.TokenValidationParameters.ValidAudience = tokenConfigurations.Audience;
                bearerOptions.TokenValidationParameters.ValidIssuer = tokenConfigurations.Issuer;
                bearerOptions.TokenValidationParameters.ValidateIssuerSigningKey = true;
                bearerOptions.TokenValidationParameters.ValidateLifetime = true;
                bearerOptions.TokenValidationParameters.ClockSkew = TimeSpan.Zero;
            });

            services.AddAuthorization(auth =>
            {
                auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser().Build());
            });
        }

        /// <summary>
        /// Configura o Cors.
        /// </summary>
        /// <param name="services">Os serviços do container.</param>
        private static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors((options) =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder
                      .AllowAnyOrigin()
                      .AllowAnyHeader()
                      .AllowAnyMethod()
                      .SetIsOriginAllowedToAllowWildcardSubdomains();
                });
            });
        }

        /// <summary>
        /// Obtém a configuração utilizada no token de autenticação.
        /// </summary>
        /// <param name="configuration">Configuration.</param>
        private static TokenConfigurations GetTokenConfiguration(IConfiguration configuration)
        {
            var tokenConfigurations = new TokenConfigurations();

            new ConfigureFromConfigurationOptions<TokenConfigurations>(configuration.GetSection("TokenConfigurations")).Configure(tokenConfigurations);

            return tokenConfigurations;
        }

        /// <summary>
        /// Obtém a configuração utilizada no signing de autenticação.
        /// </summary>
        /// <param name="configuration">Configuration.</param>
        private static SigningConfigurations GetSigningConfiguration(IConfiguration configuration)
        {
            var signingConfigurations = new SigningConfigurations();

            new ConfigureFromConfigurationOptions<SigningConfigurations>(configuration.GetSection("SigningConfigurations")).Configure(signingConfigurations);

            return signingConfigurations;
        }
    }
}
