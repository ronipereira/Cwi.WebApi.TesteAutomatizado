using System.Collections.Generic;
using System.Threading.Tasks;
using Cwi.Treinamento.TesteAutomatizado.Infra.Repositories;

namespace Cwi.Treinamento.TesteAutomatizado.Domain.Employees
{
    /// <summary>
    /// Define a interface IEmployeeRepository.
    /// </summary>
    public interface IEmployeeRepository : IRepository<Employee>
    {
        /// <summary>
        /// Obtém todos os funcionários.
        /// </summary>
        /// <returns>Funcionários</returns>
        Task<IEnumerable<Employee>> GetAllEmployeesAsync();

        /// <summary>
        /// Obtém os funcionários por página.
        /// </summary>
        /// <param name="pagina">Número da página.</param>
        /// <param name="tamanhoPagina">Tamanho da página.</param>
        /// <returns>Funcionários</returns>
        Task<IEnumerable<Employee>> GetEmployeesByPageAsync(int pagina, int tamanhoPagina);

        /// <summary>
        /// Obtém o funcionário filtrando por e-mail.
        /// </summary>
        /// <param name="email">E-mail</param>
        /// <param name="ignoreId">Id que deve ser ignorado na consulta.</param>
        /// <returns>Funcionário</returns>
        Task<Employee> GetEmployeeByEmailAsync(string email, long? ignoreId);
    }
}
