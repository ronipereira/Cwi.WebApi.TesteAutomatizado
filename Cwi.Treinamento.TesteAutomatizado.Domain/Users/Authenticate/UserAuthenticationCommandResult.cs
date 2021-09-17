using System;

namespace Cwi.Treinamento.TesteAutomatizado.Domain.Users.Authenticate
{
    /// <summary>
    /// Define a classe UserAuthenticationResult.
    /// </summary>
    public class UserAuthenticationCommandResult
    {
        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="UserAuthenticationCommandResult"/>.
        /// </summary>
        /// <param name="created">Data de criação.</param>
        /// <param name="expiration">Data de expiração.</param>
        /// <param name="accessToken">Token de acesso.</param>
        public UserAuthenticationCommandResult(DateTime created, DateTime expiration, string accessToken)
        {
            Created = created;
            Expiration = expiration;
            AccessToken = accessToken;
        }

        /// <summary>
        /// Obtém ou define Created.
        /// </summary>
        /// <value>
        /// Created.
        /// </value>
        public DateTime Created { get; set; }

        /// <summary>
        /// Obtém ou define Created.
        /// </summary>
        /// <value>
        /// Created.
        /// </value>
        public DateTime Expiration { get; set; }

        /// <summary>
        /// Obtém ou define Created.
        /// </summary>
        /// <value>
        /// Created.
        /// </value>
        public string AccessToken { get; set; }
    }
}
