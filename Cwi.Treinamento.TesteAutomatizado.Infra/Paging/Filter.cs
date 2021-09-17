namespace Cwi.Treinamento.TesteAutomatizado.Infra.Paging
{
    /// <summary>
    /// Define o filtro para uma consulta paginada.
    /// </summary>
    /// <typeparam name="T">O tipo do filtro.</typeparam>
    public class Filter<T> where T : class, new()
    {
        /// <summary>
        /// Inicia uma nova instância da classe <see cref="Filter{T}"/>.
        /// </summary>
        public Filter()
        {
            Data = new T();
        }

        /// <summary>
        /// Obtém ou define os dados utilizados como filtro.
        /// </summary>
        public T Data { get; set; }

        /// <summary>
        /// Obtém ou define a quantidade de registros (Padrão: 3).
        /// </summary>
        public int Limit { get; set; } = 3;

        /// <summary>
        /// Obtém ou define a página (Padrão: 1).
        /// </summary>
        public int Page { get; set; } = 1;
    }
}