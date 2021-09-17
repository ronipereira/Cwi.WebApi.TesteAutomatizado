using Cwi.Treinamento.TesteAutomatizado.Domain.Companies;
using Cwi.Treinamento.TesteAutomatizado.Domain.Employees;
using System;

namespace Cwi.Treinamento.TesteAutomatizado.Domain.EmployeesCompany
{
    /// <summary>
    /// Define a entidade EmployeeCompany.
    /// </summary>
    public class EmployeeCompany
    {
        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="EmployeeCompany" />.
        /// </summary>
        public EmployeeCompany() { }

        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="EmployeeCompany" />.
        /// </summary>
        /// <param name="idEmployee">Identificador do funcionário.</param>
        /// <param name="idCompany">Identificador da empresa.</param>
        public EmployeeCompany(long idEmployee, long idCompany)
        {
            this.IdEmployee = idEmployee;
            this.IdCompany = idCompany;
            this.FromDate = DateTime.Now;
        }

        /// <summary>
        /// Obtém ou define Id.
        /// </summary>
        /// <value>
        /// Id.
        /// </value>
        public long Id { get; set; }

        /// <summary>
        /// Obtém ou define IdEmployee.
        /// </summary>
        /// <value>
        /// IdEmployee.
        /// </value>
        public long IdEmployee { get; set; }

        /// <summary>
        /// Obtém ou define Employee.
        /// </summary>
        /// <value>
        /// Employee.
        /// </value>
        public Employee Employee { get; set; }

        /// <summary>
        /// Obtém ou define IdCompany.
        /// </summary>
        /// <value>
        /// IdCompany.
        /// </value>
        public long IdCompany { get; set; }

        /// <summary>
        /// Obtém ou define Company.
        /// </summary>
        /// <value>
        /// Company.
        /// </value>
        public Company Company { get; set; }

        /// <summary>
        /// Obtém ou define FromDate.
        /// </summary>
        /// <value>
        /// FromDate.
        /// </value>
        public DateTime FromDate { get; set; }

        /// <summary>
        /// Obtém ou define ToDate.
        /// </summary>
        /// <value>
        /// ToDate.
        /// </value>
        public Nullable<DateTime> ToDate { get; set; }

        /// <summary>
        /// Configura a demissão do funcionário.
        /// </summary>
        public void ToFire()
        {
            this.ToDate = DateTime.Now;
        }

    }
}
