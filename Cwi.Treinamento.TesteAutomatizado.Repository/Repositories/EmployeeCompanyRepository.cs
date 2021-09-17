using Cwi.Treinamento.TesteAutomatizado.Domain;
using Cwi.Treinamento.TesteAutomatizado.Domain.EmployeesCompany;
using Cwi.Treinamento.TesteAutomatizado.Infra.Repositories;
using Dapper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cwi.Treinamento.TesteAutomatizado.Repository.Repositories
{

    /// <summary>
    /// Define a implementação de um repositório de vínculo entre funcionário e empresa.
    /// </summary>
    /// <seealso cref="Cwi.Treinamento.TesteAutomatizado.Infra.Repositories.RepositoryBase{Cwi.Treinamento.TesteAutomatizado.Domain.EmployeesCompany.EmployeeCompany, Cwi.Treinamento.TesteAutomatizado.Domain.IEmployeeDbUnitOfWork}" />
    /// <seealso cref="Cwi.Treinamento.TesteAutomatizado.Domain.EmployeesCompany.IEmployeeCompanyRepository" />
    public class EmployeeCompanyRepository : RepositoryBase<EmployeeCompany, IEmployeeDbUnitOfWork>, IEmployeeCompanyRepository
    {
        private readonly IUnitOfWork<IEmployeeDbUnitOfWork> unitOfWork;

        /// <summary>
        /// Inicia uma nova instância da classe <see cref="EmployeeCompanyRepository"/>.
        /// </summary>
        /// <param name="unitOfWork">Unit of work.</param>
        public EmployeeCompanyRepository(IUnitOfWork<IEmployeeDbUnitOfWork> unitOfWork) : base(unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Obtém os vínculos pelo identificador do funcionário.
        /// </summary>
        /// <param name="idEmployee">Identificador do funcionário.</param>
        /// <returns>Vínculos entre funcionário e empresas.</returns>
        public Task<IEnumerable<EmployeeCompany>> GetByIdEmployeeAsync(long idEmployee)
        {
            var query = @"SELECT
                             Id 
                           , IdEmployee 
                           , IdCompany
                           , FromDate
                           , ToDate
                          FROM EmployeeCompany
                          WHERE IdEmployee = @IdEmployee";

            return this.unitOfWork.Connection.QueryAsync<EmployeeCompany>(query, new { IdEmployee = idEmployee }, transaction: unitOfWork.Transaction);
        }

        /// <summary>
        /// Obtém os vínculos pelo identificador da empresa.
        /// </summary>
        /// <param name="idCompany">Identificador do empresa.</param>
        /// <returns>Vínculos entre empresa e funcionários.</returns>
        public Task<IEnumerable<EmployeeCompany>> GetByIdCompanyAsync(long idCompany)
        {
            var query = @"SELECT
                             Id 
                           , IdEmployee 
                           , IdCompany
                           , FromDate
                           , ToDate
                          FROM EmployeeCompany
                          WHERE IdCompany = @IdCompany";

            return this.unitOfWork.Connection.QueryAsync<EmployeeCompany>(query, new { IdCompany = idCompany }, transaction: unitOfWork.Transaction);
        }

        /// <summary>
        /// Obtém o vínculo de funcionário e empresa pelo identificador.
        /// </summary>
        /// <returns>Vínculo de funcionário e empresa.</returns>
        public override Task<EmployeeCompany> FindByAsync(object key)
        {
            var query = @"SELECT
                             Id 
                           , IdEmployee
                           , IdCompany
                           , FromDate
                           , ToDate 
                          FROM EmployeeCompany 
                          WHERE Id = @Id";

            return this.unitOfWork.Connection.QueryFirstOrDefaultAsync<EmployeeCompany>(query, new { Id = key }, unitOfWork.Transaction);
        }

        /// <summary>
        /// Insere o vínculo de funcionário e empresa.
        /// </summary>
        /// <returns>Identificador do novo vínculo de funcionário e empresa.</returns>
        public override Task<long> InsertAsync(EmployeeCompany item)
        {
            var query = @"INSERT INTO
                             EmployeeCompany
                            (
                              IdEmployee
                            , IdCompany
                            , FromDate 
                            , ToDate) 
                          VALUES
                            (
                              @IdEmployee
                            , @IdCompany
                            , @FromDate
                            , @ToDate)
                          RETURNING Id";

            return this.unitOfWork.Connection.QueryFirstOrDefaultAsync<long>(query, item, unitOfWork.Transaction);
        }

        /// <summary>
        /// Exclui o vínculo de funcionário e empresa.
        /// </summary>
        public override Task RemoveAsync(EmployeeCompany item)
        {
            var query = @"DELETE FROM EmployeeCompany WHERE Id = @Id";

            return this.unitOfWork.Connection.ExecuteAsync(query, new { Id = item.Id }, unitOfWork.Transaction);
        }

        /// <summary>
        /// Atualiza o vínculo de funcionário e empresa.
        /// </summary>
        public override Task<int> UpdateAsync(EmployeeCompany item)
        {
            var query = @"UPDATE EmployeeCompany
                            SET
                             IdEmployee = @IdEmployee,
                             IdCompany = @IdCompany,
                             FromDate = @FromDate,
                             ToDate = @ToDate
                          WHERE Id = @Id";

            return this.unitOfWork.Connection.ExecuteAsync(query, item, unitOfWork.Transaction);
        }
    }
}
