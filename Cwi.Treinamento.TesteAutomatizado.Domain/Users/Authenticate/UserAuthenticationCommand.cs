using Cwi.Treinamento.TesteAutomatizado.Infra.Messaging;

namespace Cwi.Treinamento.TesteAutomatizado.Domain.Users.Authenticate
{
    /// <summary>
    /// Define a entidade UserAuthenticationCommand.
    /// </summary>
    public class UserAuthenticationCommand : Command<bool>
    {
        /// <summary>
        /// Obtém ou define Username.
        /// </summary>
        /// <value>
        /// O Username.
        /// </value>
        public string Username { get; set; }

        /// <summary>
        /// Obtém ou define Password.
        /// </summary>
        /// <value>
        /// O Password.
        /// </value>
        public string Password { get; set; }

    }
}
