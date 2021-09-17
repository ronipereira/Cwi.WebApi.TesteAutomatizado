using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Credz.Portador.Api.Configuration
{
    /// <summary>
    /// Classe usada para configurar as propriedades do Signing de autentica��o.
    /// </summary>
    public class SigningConfigurations
    {
        /// <summary>
        /// Obt�m ou define Secret.
        /// </summary>
        /// <value>
        /// Secret.
        /// </value>
        public string Secret { get; set; }

        /// <summary>
        /// Obt�m SecurityKey.
        /// </summary>
        /// <value>
        /// SecurityKey.
        /// </value>
        public SecurityKey SigningKey
        {
            get
            {
                return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Secret));
            }
        }

        /// <summary>
        /// Obt�m SigningCredentials.
        /// </summary>
        /// <value>
        /// SigningCredentials.
        /// </value>
        public SigningCredentials SigningCredentials
        {
            get
            {
                return new SigningCredentials(SigningKey, SecurityAlgorithms.HmacSha256Signature);
            }
        }
    }
}
