using Cwi.Treinamento.TesteAutomatizado.Domain.Employees;
using System;

namespace Cwi.Treinamento.TesteAutomatizado.Domain.Companies.GetCompany.GetById
{
    /// <summary>
    /// Define a classe GetCompanyByIdEmployeesCommandResult.
    /// </summary>
    public class GetCompanyByIdEmployeesCommandResult
    {
        /// <summary>
        /// Inicializa uma nova inst�ncia da classe <see cref="GetCompanyByIdEmployeesCommandResult" />.
        /// </summary>
        /// <param name="company">Company.</param>
        /// <param name="fromDate">FromDate.</param>
        /// <param name="toDate">ToDate.</param>
        public GetCompanyByIdEmployeesCommandResult(Employee employee, DateTime fromDate, Nullable<DateTime> toDate)
        {
            this.Id = employee.Id;
            this.Name = employee.Name;
            this.Email = employee.Email;
            this.FromDate = fromDate;
            this.ToDate = toDate;
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
        /// Name.
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
        /// Obt�m ou define FromDate.
        /// </summary>
        /// <value>
        /// FromDate.
        /// </value>
        public DateTime FromDate { get; set; }

        /// <summary>
        /// Obt�m ou define ToDate.
        /// </summary>
        /// <value>
        /// ToDate.
        /// </value>
        public Nullable<DateTime> ToDate { get; set; }
    }

}