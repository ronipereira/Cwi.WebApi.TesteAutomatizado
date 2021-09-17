using Credz.Portador.Api.Configuration;
using Cwi.Treinamento.TesteAutomatizado.Domain.Users.Authenticate;
using Cwi.Treinamento.TesteAutomatizado.Infra.Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace Cwi.Treinamento.TesteAutomatizado.WebApi.Controllers
{
    /// <summary>
    /// Define a classe AuthenticationController.
    /// </summary>
    [ApiVersion("1")]
    [Route("v{version:apiVersion}/public")]
    [ApiController]
    public class AuthenticationController : ApiController
    {
        private readonly SigningConfigurations signingConfigurations;
        private readonly TokenConfigurations tokenConfigurations;
        private readonly IMediatorHandler mediator;

        /// <summary>
        /// Inicia uma nova instância da classe <see cref="AuthenticationController" />.
        /// </summary>
        /// <param name="mediator">O mediador.</param>
        /// <param name="signingConfigurations">O SigningConfigurations.</param>
        /// <param name="tokenConfigurations">O TokenConfigurations.</param>
        public AuthenticationController(IMediatorHandler mediator, SigningConfigurations signingConfigurations, TokenConfigurations tokenConfigurations)
        {
            this.mediator = mediator;
            this.signingConfigurations = signingConfigurations;
            this.tokenConfigurations = tokenConfigurations;
        }


        /// <summary>
        /// Autentica o usuário.
        /// </summary>
        /// <param name="userAuthenticationCommand">Comando usado para autenticar o usuário.</param>
        /// <returns></returns>
        /// <response code="200">Se o usuário foi autenticado.</response>
        /// <response code="400">Se o usuário ou a senha estiverem inválidos.</response>
        [HttpPost("auth")]
        [AllowAnonymous]
        public async Task<IActionResult> AuthenticateUser([FromBody] UserAuthenticationCommand userAuthenticationCommand)
        {
            var response = await mediator.SendCommand(userAuthenticationCommand).ConfigureAwait(false);

            if (response.IsValid && response.Result)
            {
                DateTime dataCriacao = DateTime.Now;
                DateTime dataExpiracao = dataCriacao.AddSeconds(this.tokenConfigurations.Seconds);

                var handler = new JwtSecurityTokenHandler();
                var securityToken = handler.CreateToken(new SecurityTokenDescriptor
                {
                    Issuer = this.tokenConfigurations.Issuer,
                    Audience = this.tokenConfigurations.Audience,
                    SigningCredentials = this.signingConfigurations.SigningCredentials,
                    Subject = new ClaimsIdentity(new GenericIdentity(userAuthenticationCommand.Username, "Login"), new[] {
                                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                                    new Claim(ClaimTypes.NameIdentifier, userAuthenticationCommand.Username),
                                }),
                    NotBefore = dataCriacao,
                    Expires = dataExpiracao
                });

                return Ok(new UserAuthenticationCommandResult(dataCriacao, dataExpiracao, handler.WriteToken(securityToken)));
            }

            return CustomResponse(response);
        }
    }

}
