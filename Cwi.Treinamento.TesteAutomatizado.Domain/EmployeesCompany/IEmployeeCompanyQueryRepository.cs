using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cwi.Treinamento.TesteAutomatizado.Domain.EmployeesCompany
{
    /// <summary>
    /// Define a interface IEmployeeCompanyQueryRepository.
    /// </summary>
    public interface IEmployeeCompanyQueryRepository
    {

        /// <summary>
        /// Obtém o vínculo atual do funcionário pelo identificador do funcionário.
        /// </summary>
        /// <param name="idEmployee">Identificador do funcionário.</param>
        /// <returns>Vínculo atual entre funcionário e empresa.</returns>
        Task<EmployeeCompany> GetCurrentCompanyByEmployeeAsync(long idEmployee);

        /// <summary>
        /// Obtém o funcionário e os vínculos com empresas pelo identificador do funcionário.
        /// </summary>
        /// <param name="idEmployee">Identificador do funcionário.</param>
        /// <returns>Funcionário e os vínculos com empresas</returns>
        Task<IEnumerable<EmployeeCompany>> GetByEmployeeWithCompaniesAsync(long idEmployee);

        /// <summary>
        /// Obtém a empresa e os vínculos com funcionários pelo identificador da empresa.
        /// </summary>
        /// <param name="idCompany">Identificador da empresa.</param>
        /// <returns>Empresa e os vínculos com funcionários</returns>
        Task<IEnumerable<EmployeeCompany>> GetByCompanyWithEmployeesAsync(long idCompany);
    }
}
