using System.Text.Json.Serialization;
using Cwi.Treinamento.TesteAutomatizado.Infra.Messaging;

namespace Cwi.Treinamento.TesteAutomatizado.Domain.Companies.FireEmployee
{
    /// <summary>
    /// Comando usado para contratação de um funcionário pela empresa.
    /// </summary>
    /// <seealso cref="Cwi.Treinamento.TesteAutomatizado.Infra.Messaging.Command{System.Boolean}" />
    public class FireEmployeeCommand : Command<bool>
    {
        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="FireEmployeeCommand" />.
        /// </summary>
        /// <param name="id">Identificador da empresa.</param>
        /// <param name="idEmployee">Identificador do funcionário.</param>
        public FireEmployeeCommand(long id, long idEmployee)
        {
            this.Id = id;
            this.IdEmployee = idEmployee;
        }

        /// <summary>
        /// Obtém ou define Id.
        /// </summary>
        /// <value>
        /// Id.
        /// </value>
        [JsonIgnore]
        public long Id { get; set; }

        /// <summary>
        /// Obtém ou define IdEmployee
        /// </summary>
        /// <value>
        /// IdEmployee.
        /// </value>
        [JsonIgnore]
        public long IdEmployee { get; set; }

    }
}
