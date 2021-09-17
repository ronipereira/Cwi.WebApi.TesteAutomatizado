using System.Collections.Generic;

namespace Cwi.Treinamento.TesteAutomatizado.Domain.Companies.GetCompany.GetById
{
    /// <summary>
    /// Define a classe GetCompanyByIdCommandResult.
    /// </summary>
    public class GetCompanyByIdCommandResult
    {
        /// <summary>
        /// Inicializa uma nova inst�ncia da classe <see cref="GetCompanyByIdCommandResult" />.
        /// </summary>
        /// <param name="id">Identificador.</param>
        /// <param name="name">Name.</param>
        /// <param name="code">Code.</param>
        /// <param name="maxEmployeesNumber">MaxEmployeesNumber.</param>
        /// <param name="active">Active.</param>
        public GetCompanyByIdCommandResult(long id, string name, string code, long maxEmployeesNumber, bool active)
        {
            this.Id = id;
            this.Name = name;
            this.Code = code;
            this.MaxEmployeesNumber = maxEmployeesNumber;
            this.Active = active;
            this.Employees = new List<GetCompanyByIdEmployeesCommandResult>();
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
        /// Obt�m ou define Code.
        /// </summary>
        /// <value>
        /// Code.
        /// </value>
        public string Code { get; set; }

        /// <summary>
        /// Obt�m ou define MaxEmployeesNumber.
        /// </summary>
        /// <value>
        /// MaxEmployeesNumber.
        /// </value>
        public long MaxEmployeesNumber { get; set; }

        /// <summary>
        /// Obt�m ou define Active.
        /// </summary>
        /// <value>
        /// Email.
        /// </value>
        public bool Active { get; set; }

        /// <summary>
        /// Obt�m ou define Employees.
        /// </summary>
        /// <value>
        /// Employees.
        /// </value>
        public List<GetCompanyByIdEmployeesCommandResult> Employees { get; set; }

    }

}