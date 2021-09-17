using Cwi.Treinamento.TesteAutomatizado.Domain;
using Cwi.Treinamento.TesteAutomatizado.Domain.Companies;
using Cwi.Treinamento.TesteAutomatizado.Domain.Employees;
using Cwi.Treinamento.TesteAutomatizado.Domain.EmployeesCompany;
using Cwi.Treinamento.TesteAutomatizado.Infra.Repositories;
using Dapper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cwi.Treinamento.TesteAutomatizado.Repository.QueryRepositories
{
    /// <summary>
    /// Define a implementação de um query repositório de vínculo entre funcionário e empresa.
    /// </summary>
    /// <seealso cref="Cwi.Treinamento.TesteAutomatizado.Domain.EmployeesCompany.IEmployeeCompanyQueryRepository" />
    public class EmployeeCompanyQueryRepository : IEmployeeCompanyQueryRepository
    {
        private readonly IUnitOfWork<IEmployeeDbUnitOfWork> unitOfWork;

        /// <summary>
        /// Inicia uma nova instância da classe <see cref="EmployeeCompanyQueryRepository"/>.
        /// </summary>
        /// <param name="unitOfWork">Unit of work.</param>
        public EmployeeCompanyQueryRepository(IUnitOfWork<IEmployeeDbUnitOfWork> unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Obtém o vínculo atual do funcionário pelo identificador do funcionário.
        /// </summary>
        /// <param name="idEmployee">Identificador do funcionário.</param>
        /// <returns>Vínculo atual entre funcionário e empresa.</returns>
        public Task<EmployeeCompany> GetCurrentCompanyByEmployeeAsync(long idEmployee)
        {
            var query = @"SELECT
                             Id 
                           , IdEmployee 
                           , IdCompany
                           , FromDate
                           , ToDate
                          FROM EmployeeCompany
                          WHERE IdEmployee = @IdEmployee
                          AND ToDate IS NULL";

            return this.unitOfWork.Connection.QueryFirstOrDefaultAsync<EmployeeCompany>(query, new { IdEmployee = idEmployee }, transaction: unitOfWork.Transaction);
        }

        /// <summary>
        /// Obtém o funcionário e os vínculos com empresas pelo identificador do funcionário.
        /// </summary>
        /// <param name="idEmployee">Identificador do funcionário.</param>
        /// <returns>Funcionário e os vínculos com empresas</returns>
        public async Task<IEnumerable<EmployeeCompany>> GetByEmployeeWithCompaniesAsync(long idEmployee)
        {
            var query = @"SELECT
                             ec.Id 
                           , ec.IdEmployee 
                           , ec.IdCompany
                           , ec.FromDate
                           , ec.ToDate
                           , NULL AS SplitCompany
                           , c.Id
                           , c.Name
                           , c.Code
                           , c.MaxEmployeesNumber
                           , c.Active
                          FROM EmployeeCompany ec
                          INNER JOIN Company c
                            ON ec.IdCompany = c.Id
                          WHERE ec.IdEmployee = @IdEmployee
                          ORDER BY ec.FromDate DESC";

            return await (this.unitOfWork.Connection.QueryAsync<EmployeeCompany, Company, EmployeeCompany>(query,
                splitOn: "SplitCompany",
                param: new { IdEmployee = idEmployee },
                map: (employeeCompany, company) =>
                {
                    if (employeeCompany != null && company != null)
                        employeeCompany.Company = company;

                    return employeeCompany;
                },
                transaction: unitOfWork.Transaction));
        }

        /// <summary>
        /// Obtém a empresa e os vínculos com funcionários pelo identificador da empresa.
        /// </summary>
        /// <param name="idCompany">Identificador da empresa.</param>
        /// <returns>Empresa e os vínculos com funcionários</returns>
        public async Task<IEnumerable<EmployeeCompany>> GetByCompanyWithEmployeesAsync(long idCompany)
        {
            var query = @"SELECT
                             ec.Id 
                           , ec.IdEmployee 
                           , ec.IdCompany
                           , ec.FromDate
                           , ec.ToDate
                           , NULL AS SplitEmployee
                           , e.Id
                           , e.Name
                           , e.Email
                           , e.Active
                          FROM EmployeeCompany ec
                          INNER JOIN Employee e
                            ON ec.IdEmployee = e.Id
                          WHERE ec.IdCompany = @IdCompany
                          ORDER BY ec.FromDate DESC";

            return await (this.unitOfWork.Connection.QueryAsync<EmployeeCompany, Employee, EmployeeCompany>(query,
                splitOn: "SplitEmployee",
                param: new { IdCompany = idCompany },
                map: (employeeCompany, employee) =>
                {
                    if (employeeCompany != null && employee != null)
                        employeeCompany.Employee = employee;

                    return employeeCompany;
                },
                transaction: unitOfWork.Transaction));
        }
    }
}
