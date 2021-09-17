using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Cwi.Treinamento.TesteAutomatizado.Domain.Employees
{
    /// <summary>
    /// Define a entidade Employee.
    /// </summary>
    public class Employee
    {
        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="Employee" />.
        /// </summary>
        public Employee() { }

        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="Employee" />.
        /// </summary>
        /// <param name="name">Name.</param>
        /// <param name="email">Email.</param>
        /// <param name="active">Active.</param>
        public Employee(string name, string email, bool active)
        {
            this.Name = name;
            this.Email = email;
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
        /// Obtém ou define Email.
        /// </summary>
        /// <value>
        /// Email.
        /// </value>
        public string Email { get; set; }

        /// <summary>
        /// Obtém ou define Active.
        /// </summary>
        /// <value>
        /// Active.
        /// </value>
        public bool Active { get; set; }

        /// <summary>
        /// Obtém ou define TotalEmployees.
        /// </summary>
        /// <value>
        /// TotalEmployees.
        /// </value>
        [NotMapped]
        [JsonIgnore]
        public long TotalEmployees { get; set; }
    }
}
