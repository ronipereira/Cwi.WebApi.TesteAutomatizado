using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Credz.Portador.Api.Configuration
{
    /// <summary>
    /// Classe usada para configurar as propriedades do Signing de autenticação.
    /// </summary>
    public class SigningConfigurations
    {
        /// <summary>
        /// Obtém ou define Secret.
        /// </summary>
        /// <value>
        /// Secret.
        /// </value>
        public string Secret { get; set; }

        /// <summary>
        /// Obtém SecurityKey.
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
        /// Obtém SigningCredentials.
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
