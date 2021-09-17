namespace Cwi.Treinamento.TesteAutomatizado.Domain.Companies.GetCompany.GetByPage
{
    /// <summary>
    /// Define a classe GetCompanyByPageCommandResult.
    /// </summary>
    public class GetCompanyByPageCommandResult
    {
        /// <summary>
        /// Obt�m ou define Id.
        /// </summary>
        /// <value>
        /// Id.
        /// </value>
        public long Id { get; set; }

        /// <summary>
        /// Obt�m ou define Name.
        /// </summary>
        /// <value>
        /// O Nome.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Obt�m ou define Code.
        /// </summary>
        /// <value>
        /// Code.
        /// </value>
        public string Code { get; set; }

        /// <summary>
        /// Obt�m ou define Active.
        /// </summary>
        /// <value>
        /// Email.
        /// </value>
        public bool Active { get; set; }
    }

}