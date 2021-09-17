using Cwi.Treinamento.TesteAutomatizado.Infra.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cwi.Treinamento.TesteAutomatizado.Domain.EmployeesCompany
{
    /// <summary>
    /// Define a interface IEmployeeCompanyRepository.
    /// </summary>
    public interface IEmployeeCompanyRepository : IRepository<EmployeeCompany>
    {

        /// <summary>
        /// Obtém os vínculos pelo identificador do funcionário.
        /// </summary>
        /// <param name="idEmployee">Identificador do funcionário.</param>
        /// <returns>Vínculos entre funcionário e empresas.</returns>
        Task<IEnumerable<EmployeeCompany>> GetByIdEmployeeAsync(long idEmployee);

        /// <summary>
        /// Obtém os vínculos pelo identificador da empresa.
        /// </summary>
        /// <param name="idCompany">Identificador do empresa.</param>
        /// <returns>Vínculos entre empresa e funcionários.</returns>
        Task<IEnumerable<EmployeeCompany>> GetByIdCompanyAsync(long idCompany);
    }
}
