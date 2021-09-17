using System.Collections.Generic;
using System.Threading.Tasks;
using Cwi.Treinamento.TesteAutomatizado.Infra.Repositories;

namespace Cwi.Treinamento.TesteAutomatizado.Domain.Companies
{
    /// <summary>
    /// Define a interface ICompanyRepository.
    /// </summary>
    public interface ICompanyRepository : IRepository<Company>
    {
        /// <summary>
        /// Obtém todos as empresas.
        /// </summary>
        /// <returns>Empresas</returns>
        Task<IEnumerable<Company>> GetAllCompaniesAsync();

        /// <summary>
        /// Obtém as empresas por página.
        /// </summary>
        /// <param name="pagina">Número da página.</param>
        /// <param name="tamanhoPagina">Tamanho da página.</param>
        /// <returns>Empresas</returns>
        Task<IEnumerable<Company>> GetCompaniesByPageAsync(int pagina, int tamanhoPagina);

        /// <summary>
        /// Obtém a empresa filtrando por código.
        /// </summary>
        /// <param name="code">Código</param>
        /// <param name="ignoreId">Id que deve ser ignorado na consulta.</param>
        /// <returns>Empresa</returns>
        Task<Company> GetCompanyByCodeAsync(string code, long? ignoreId);
    }
}
