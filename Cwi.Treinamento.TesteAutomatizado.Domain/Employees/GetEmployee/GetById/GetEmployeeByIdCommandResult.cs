using System.Collections.Generic;

namespace Cwi.Treinamento.TesteAutomatizado.Domain.Employees.GetEmployee.GetById
{
    /// <summary>
    /// Define a classe GetEmployeeByIdCommandResult.
    /// </summary>
    public class GetEmployeeByIdCommandResult
    {
        /// <summary>
        /// Inicializa uma nova inst�ncia da classe <see cref="GetEmployeeByIdCommandResult" />.
        /// </summary>
        /// <param name="id">Identificador.</param>
        /// <param name="name">Name.</param>
        /// <param name="email">Email.</param>
        /// <param name="active">Active.</param>
        public GetEmployeeByIdCommandResult(long id, string name, string email, bool active)
        {
            this.Id = id;
            this.Name = name;
            this.Email = email;
            this.Active = active;
            this.Companies = new List<GetEmployeeByIdCompaniesCommandResult>();
        }

        /// <summary>
        /// Obt�m ou define Id.
        /// </summary>
        /// <value>
        /// Id.
        /// </value>
        public long Id { get; set; }

        /// <summary>
        /// Obt�m ou define Name.
        /// </summary>
        /// <value>
        /// O Nome.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Obt�m ou define Email.
        /// </summary>
        /// <value>
        /// Email.
        /// </value>
        public string Email { get; set; }

        /// <summary>
        /// Obt�m ou define Active.
        /// </summary>
        /// <value>
        /// Email.
        /// </value>
        public bool Active { get; set; }

        /// <summary>
        /// Obt�m ou define Companies.
        /// </summary>
        /// <value>
        /// Companies.
        /// </value>
        public List<GetEmployeeByIdCompaniesCommandResult> Companies { get; set; }
    }

}