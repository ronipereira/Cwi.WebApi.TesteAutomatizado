using Cwi.Treinamento.TesteAutomatizado.Infra.Messaging;
using MediatR;

namespace Cwi.Treinamento.TesteAutomatizado.Infra.Paging
{
    /// <summary>
    /// Define um comando para consultas paginadas.
    /// </summary>
    /// <typeparam name="TFilter">O tipo do filtro.</typeparam>
    /// <typeparam name="TResultSet">O tipo de retorno da consulta.</typeparam>
    public class FilteredCommand<TFilter, TResultSet> : Command<PagedResult<TResultSet>>, IRequest<PagedResult<TResultSet>>
      where TFilter : class, new()
      where TResultSet : class
    {
        /// <summary>
        /// Inicia uma nova instância da classe <see cref="FilteredCommand{TFilter, TResultSet}"/>.
        /// </summary>
        public FilteredCommand()
        {
            Filter = new Filter<TFilter>();
        }

        /// <summary>
        /// Obtém ou define o filtro.
        /// </summary>
        public Filter<TFilter> Filter { get; set; }
    }
}