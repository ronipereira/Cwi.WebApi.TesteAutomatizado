using Cwi.Treinamento.TesteAutomatizado.Domain.Users.Specs;
using Cwi.Treinamento.TesteAutomatizado.Infra.Messaging;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Cwi.Treinamento.TesteAutomatizado.Domain.Users.Authenticate
{
    /// <summary>
    /// Define a classe UserAuthenticationCommandHandler.
    /// </summary>
    /// <seealso cref="Cwi.Treinamento.TesteAutomatizado.Infra.Messaging.CommandHandler{Cwi.Treinamento.TesteAutomatizado.Domain.Users.Authenticate.UserAuthenticationCommand, System.Boolean}" />
    public class UserAuthenticationCommandHandler : CommandHandler<UserAuthenticationCommand, bool>
    {
        private readonly IConfiguration configuration;

        /// <summary>
        /// Inicia uma nova instância da classe <see cref="UserAuthenticationCommandHandler" />.
        /// </summary>
        /// <param name="configuration">O configuration.</param>
        public UserAuthenticationCommandHandler(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public override async Task<CommandResponse<bool>> Handle(UserAuthenticationCommand request, CancellationToken cancellationToken)
        {
            List<User> users = new List<User>();
            configuration.GetSection("Users").Bind(users);

            var validationResult = new AuthenticationValidSpec(users).Validate(request);

            if (!validationResult.IsValid)
                return Response(validationResult);

            return Response(true);
        }
    }
}
