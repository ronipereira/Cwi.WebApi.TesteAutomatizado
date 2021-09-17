using System.Collections.Generic;

namespace Cwi.Treinamento.TesteAutomatizado.Infra.Paging
{
    /// <summary>
    /// Define o resultado de uma consulta paginada.
    /// </summary>
    /// <typeparam name="T">O tipo de retorno da consulta.</typeparam>
    public class PagedResult<T>
      where T : class
    {
        /// <summary>
        /// Inicia uma nova instância da classe <see cref="PagedResult{T}"/>.
        /// </summary>
        /// <param name="totalSize">O total de registros.</param>
        /// <param name="pageSize">O total de registros por página.</param>
        /// <param name="resultSet">O resultado da consulta.</param>
        public PagedResult(long totalSize, int pageSize, IEnumerable<T> resultSet)
        {
            PageSize = pageSize;
            TotalSize = totalSize;
            ResultSet = resultSet;
        }

        /// <summary>
        /// Obtém ou define o total de registros por página.
        /// </summary>
        public int PageSize { get; private set; }

        /// <summary>
        /// Obtém ou define o total de registros.
        /// </summary>
        public long TotalSize { get; private set; }

        /// <summary>
        /// Obtém ou define o resultado da consulta.
        /// </summary>
        public IEnumerable<T> ResultSet { get; private set; }
    }
}