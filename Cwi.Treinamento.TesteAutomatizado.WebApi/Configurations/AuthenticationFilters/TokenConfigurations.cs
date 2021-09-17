namespace Credz.Portador.Api.Configuration
{
    /// <summary>
    /// Classe usada para configurar as propriedades do Token de autentica��o.
    /// </summary>
    public class TokenConfigurations
	{
        /// <summary>
        /// Obt�m ou define Audience.
        /// </summary>
        /// <value>
        /// Audience.
        /// </value>
        public string Audience { get; set; }

        /// <summary>
        /// Obt�m ou define Issuer.
        /// </summary>
        /// <value>
        /// Issuer.
        /// </value>
        public string Issuer { get; set; }

        /// <summary>
        /// Obt�m ou define Seconds.
        /// </summary>
        /// <value>
        /// Seconds.
        /// </value>
        public int Seconds { get; set; }
	}
}
