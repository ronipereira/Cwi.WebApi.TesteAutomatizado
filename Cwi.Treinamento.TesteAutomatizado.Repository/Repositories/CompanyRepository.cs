using Cwi.Treinamento.TesteAutomatizado.Domain;
using Cwi.Treinamento.TesteAutomatizado.Domain.Companies;
using Cwi.Treinamento.TesteAutomatizado.Infra.Repositories;
using Dapper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cwi.Treinamento.TesteAutomatizado.Repository.Repositories
{

    /// <summary>
    /// Define a implementação de um repositório de empresa.
    /// </summary>
    /// <seealso cref="Cwi.Treinamento.TesteAutomatizado.Infra.Repositories.RepositoryBase{Cwi.Treinamento.TesteAutomatizado.Domain.Companies.Company, Cwi.Treinamento.TesteAutomatizado.Domain.IEmployeeDbUnitOfWork}" />
    /// <seealso cref="Cwi.Treinamento.TesteAutomatizado.Domain.Companies.ICompanyRepository" />
    public class CompanyRepository : RepositoryBase<Company, IEmployeeDbUnitOfWork>, ICompanyRepository
    {
        private readonly IUnitOfWork<IEmployeeDbUnitOfWork> unitOfWork;

        /// <summary>
        /// Inicia uma nova instância da classe <see cref="CompanyRepository"/>.
        /// </summary>
        /// <param name="unitOfWork">Unit of work.</param>
        public CompanyRepository(IUnitOfWork<IEmployeeDbUnitOfWork> unitOfWork) : base(unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Obtém todos as empresas.
        /// </summary>
        /// <returns>Empresas</returns>
        public Task<IEnumerable<Company>> GetAllCompaniesAsync()
        {
            var query = @"SELECT
                             Id 
                           , Name 
                           , Code
                           , MaxEmployeesNumber
                           , Active
                          FROM Company";

            return this.unitOfWork.Connection.QueryAsync<Company>(query, unitOfWork.Transaction);
        }

        /// <summary>
        /// Obtém as empresas por página.
        /// </summary>
        /// <param name="pagina">Número da página.</param>
        /// <param name="tamanhoPagina">Tamanho da página.</param>
        /// <returns>Empresas</returns>
        public Task<IEnumerable<Company>> GetCompaniesByPageAsync(int pagina, int tamanhoPagina)
        {
            var query = @"SELECT
                             Id 
                           , Name 
                           , Code
                           , MaxEmployeesNumber
                           , Active
                           , COUNT(1) OVER() AS TotalCompanies
                          FROM Company
                        ORDER BY 
                            Id ASC
                        LIMIT 
                            @TamanhoPagina
                        OFFSET 
                            (@Pagina - 1) * @TamanhoPagina";

            return this.unitOfWork.Connection.QueryAsync<Company>(query, new { Pagina = pagina, TamanhoPagina = tamanhoPagina }, unitOfWork.Transaction);
        }

        /// <summary>
        /// Obtém a empresa filtrando por código.
        /// </summary>
        /// <param name="code">Código</param>
        /// <param name="ignoreId">Id que deve ser ignorado na consulta.</param>
        /// <returns>Empresa</returns>
        public Task<Company> GetCompanyByCodeAsync(string code, long? ignoreId)
        {
            var query = @"SELECT
                             Id 
                           , Name 
                           , Code
                           , MaxEmployeesNumber
                           , Active
                          FROM Company 
                          WHERE Code = @Code
                          AND (@IgnoreId IS NULL OR Id <> @IgnoreId)";

            return this.unitOfWork.Connection.QueryFirstOrDefaultAsync<Company>(query, new { Code = code, IgnoreId = ignoreId }, unitOfWork.Transaction);
        }

        /// <summary>
        /// Obtém empresa pelo identificador.
        /// </summary>
        /// <returns>Empresa</returns>
        public override Task<Company> FindByAsync(object key)
        {
            var query = @"SELECT
                             Id 
                           , Name 
                           , Code
                           , MaxEmployeesNumber
                           , Active
                          FROM Company 
                          WHERE Id = @Id";

            return this.unitOfWork.Connection.QueryFirstOrDefaultAsync<Company>(query, new { Id = key }, unitOfWork.Transaction);
        }

        /// <summary>
        /// Insere Empresa.
        /// </summary>
        /// <returns>Identificador da nova empresa.</returns>
        public override Task<long> InsertAsync(Company item)
        {
            var query = @"INSERT INTO
                             Company
                            (
                              Name
                            , Code
                            , MaxEmployeesNumber
                            , Active) 
                          VALUES
                            (
                              @Name
                            , @Code
                            , @MaxEmployeesNumber
                            , @Active)
                          RETURNING Id";

            return this.unitOfWork.Connection.QueryFirstOrDefaultAsync<long>(query, item, unitOfWork.Transaction);
        }

        /// <summary>
        /// Exclui a empresa pelo identificador.
        /// </summary>
        public override Task RemoveAsync(Company item)
        {
            var query = @"DELETE FROM Company WHERE Id = @Id";

            return this.unitOfWork.Connection.ExecuteAsync(query, new { Id = item.Id }, unitOfWork.Transaction);
        }

        /// <summary>
        /// Atualiza a Empresa.
        /// </summary>
        public override Task<int> UpdateAsync(Company item)
        {
            var query = @"UPDATE Company
                            SET
                             Name = @Name,
                             Code = @Code,
                             MaxEmployeesNumber = @MaxEmployeesNumber,
                             Active = @Active
                          WHERE Id = @Id";

            return this.unitOfWork.Connection.ExecuteAsync(query, item, unitOfWork.Transaction);
        }
    }
}
