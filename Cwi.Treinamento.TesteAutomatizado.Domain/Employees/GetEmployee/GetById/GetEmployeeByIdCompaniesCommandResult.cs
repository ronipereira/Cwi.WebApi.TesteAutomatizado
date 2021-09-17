using Cwi.Treinamento.TesteAutomatizado.Domain.Companies;
using System;

namespace Cwi.Treinamento.TesteAutomatizado.Domain.Employees.GetEmployee.GetById
{
    /// <summary>
    /// Define a classe GetEmployeeByIdCompaniesCommandResult.
    /// </summary>
    public class GetEmployeeByIdCompaniesCommandResult
    {
        /// <summary>
        /// Inicializa uma nova inst�ncia da classe <see cref="GetEmployeeByIdCompaniesCommandResult" />.
        /// </summary>
        /// <param name="company">Company.</param>
        /// <param name="fromDate">FromDate.</param>
        /// <param name="toDate">ToDate.</param>
        public GetEmployeeByIdCompaniesCommandResult(Company company, DateTime fromDate, Nullable<DateTime> toDate)
        {
            this.Id = company.Id;
            this.Name = company.Name;
            this.Code = company.Code;
            this.MaxEmployeesNumber = company.MaxEmployeesNumber;
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