using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Cwi.Treinamento.TesteAutomatizado.Domain.Companies
{
    /// <summary>
    /// Define a entidade Company.
    /// </summary>
    public class Company
    {
        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="Company" />.
        /// </summary>
        public Company() { }

        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="Company" />.
        /// </summary>
        /// <param name="name">Name.</param>
        /// <param name="code">Code.</param>
        /// <param name="maxEmployeesNumber">MaxEmployeesNumber</param>
        /// <param name="active">Active.</param>
        public Company(string name, string code, int maxEmployeesNumber, bool active)
        {
            this.Name = name;
            this.Code = code;
            this.MaxEmployeesNumber = maxEmployeesNumber;
            this.Active = active;
        }

        /// <summary>
        /// Obtém ou define Id.
        /// </summary>
        /// <value>
        /// Id.
        /// </value>
        public long Id { get; set; }

        /// <summary>
        /// Obtém ou define Name.
        /// </summary>
        /// <value>
        /// Name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Obtém ou define Code.
        /// </summary>
        /// <value>
        /// Code.
        /// </value>
        public string Code { get; set; }

        /// <summary>
        /// Obtém ou define MaxEmployeesNumber.
        /// </summary>
        /// <value>
        /// MaxEmployeesNumber.
        /// </value>
        public int MaxEmployeesNumber { get; set; }

        /// <summary>
        /// Obtém ou define Active.
        /// </summary>
        /// <value>
        /// Active.
        /// </value>
        public bool Active { get; set; }

        /// <summary>
        /// Obtém ou define TotalCompanies.
        /// </summary>
        /// <value>
        /// TotalCompanies.
        /// </value>
        [NotMapped]
        [JsonIgnore]
        public long TotalCompanies { get; set; }
    }
}
