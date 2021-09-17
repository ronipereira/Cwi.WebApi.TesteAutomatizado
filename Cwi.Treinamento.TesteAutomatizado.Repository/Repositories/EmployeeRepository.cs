using Cwi.Treinamento.TesteAutomatizado.Domain;
using Cwi.Treinamento.TesteAutomatizado.Domain.Employees;
using Cwi.Treinamento.TesteAutomatizado.Infra.Repositories;
using Dapper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cwi.Treinamento.TesteAutomatizado.Repository.Repositories
{

    /// <summary>
    /// Define a implementação de um repositório de funcionário.
    /// </summary>
    /// <seealso cref="Cwi.Treinamento.TesteAutomatizado.Infra.Repositories.RepositoryBase{Cwi.Treinamento.TesteAutomatizado.Domain.Employees.Employee, Cwi.Treinamento.TesteAutomatizado.Domain.IEmployeeDbUnitOfWork}" />
    /// <seealso cref="Cwi.Treinamento.TesteAutomatizado.Domain.Employees.IEmployeeRepository" />
    public class EmployeeRepository : RepositoryBase<Employee, IEmployeeDbUnitOfWork>, IEmployeeRepository
    {
        private readonly IUnitOfWork<IEmployeeDbUnitOfWork> unitOfWork;

        /// <summary>
        /// Inicia uma nova instância da classe <see cref="EmployeeRepository"/>.
        /// </summary>
        /// <param name="unitOfWork">Unit of work.</param>
        public EmployeeRepository(IUnitOfWork<IEmployeeDbUnitOfWork> unitOfWork) : base(unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Obtém todos os funcionários.
        /// </summary>
        /// <returns>Funcionários</returns>
        public Task<IEnumerable<Employee>> GetAllEmployeesAsync()
        {
            var query = @"SELECT
                             Id 
                           , Name 
                           , Email
                           , Active
                          FROM Employee";

            return this.unitOfWork.Connection.QueryAsync<Employee>(query, unitOfWork.Transaction);
        }

        /// <summary>
        /// Obtém os funcionários por página.
        /// </summary>
        /// <param name="pagina">Número da página.</param>
        /// <param name="tamanhoPagina">Tamanho da página.</param>
        /// <returns>Funcionários</returns>
        public Task<IEnumerable<Employee>> GetEmployeesByPageAsync(int pagina, int tamanhoPagina)
        {
            var query = @"SELECT
                             Id 
                           , Name 
                           , Email
                           , Active
                           , COUNT(1) OVER() AS TotalEmployees
                          FROM Employee
                        ORDER BY 
                            Id ASC
                        LIMIT 
                            @TamanhoPagina
                        OFFSET 
                            (@Pagina - 1) * @TamanhoPagina";

            return this.unitOfWork.Connection.QueryAsync<Employee>(query, new { Pagina = pagina, TamanhoPagina = tamanhoPagina }, unitOfWork.Transaction);
        }

        /// <summary>
        /// Obtém o funcionário filtrando por e-mail.
        /// </summary>
        /// <param name="email">E-mail</param>
        /// <param name="ignoreId">Id que deve ser ignorado na consulta.</param>
        /// <returns>Funcionário</returns>
        public Task<Employee> GetEmployeeByEmailAsync(string email, long? ignoreId = null)
        {
            var query = @"SELECT
                             Id 
                           , Name 
                           , Email
                           , Active
                          FROM Employee 
                          WHERE Email = @Email
                          AND (@IgnoreId IS NULL OR Id <> @IgnoreId)";

            return this.unitOfWork.Connection.QueryFirstOrDefaultAsync<Employee>(query, new { Email = email, IgnoreId = ignoreId }, unitOfWork.Transaction);
        }

        /// <summary>
        /// Obtém funcionário pelo identificador.
        /// </summary>
        /// <returns>Funcionário</returns>
        public override Task<Employee> FindByAsync(object key)
        {
            var query = @"SELECT
                             Id 
                           , Name 
                           , Email
                           , Active
                          FROM Employee 
                          WHERE Id = @Id";

            return this.unitOfWork.Connection.QueryFirstOrDefaultAsync<Employee>(query, new { Id = key }, unitOfWork.Transaction);
        }

        /// <summary>
        /// Insere funcionário.
        /// </summary>
        /// <returns>Identificador do novo funcionário.</returns>
        public override Task<long> InsertAsync(Employee item)
        {
            var query = @"INSERT INTO
                             Employee
                            (
                              Name
                            , Email
                            , Active) 
                          VALUES
                            (
                              @Name
                            , @Email
                            , @Active)
                          RETURNING Id";

            return this.unitOfWork.Connection.QueryFirstOrDefaultAsync<long>(query, item, unitOfWork.Transaction);
        }

        /// <summary>
        /// Exclui o funcionário pelo identificador.
        /// </summary>
        public override Task RemoveAsync(Employee item)
        {
            var query = @"DELETE FROM Employee WHERE Id = @Id";

            return this.unitOfWork.Connection.ExecuteAsync(query, new { Id = item.Id }, unitOfWork.Transaction);
        }

        /// <summary>
        /// Atualiza o funcionário.
        /// </summary>
        public override Task<int> UpdateAsync(Employee item)
        {
            var query = @"UPDATE Employee
                            SET
                             Name = @Name,
                             Email = @Email,
                             Active = @Active
                          WHERE Id = @Id";

            return this.unitOfWork.Connection.ExecuteAsync(query, item, unitOfWork.Transaction);
        }
    }
}
