namespace Credz.Portador.Api.Configuration
{
    /// <summary>
    /// Classe usada para configurar as propriedades do Token de autenticação.
    /// </summary>
    public class TokenConfigurations
	{
        /// <summary>
        /// Obtém ou define Audience.
        /// </summary>
        /// <value>
        /// Audience.
        /// </value>
        public string Audience { get; set; }

        /// <summary>
        /// Obtém ou define Issuer.
        /// </summary>
        /// <value>
        /// Issuer.
        /// </value>
        public string Issuer { get; set; }

        /// <summary>
        /// Obtém ou define Seconds.
        /// </summary>
        /// <value>
        /// Seconds.
        /// </value>
        public int Seconds { get; set; }
	}
}
