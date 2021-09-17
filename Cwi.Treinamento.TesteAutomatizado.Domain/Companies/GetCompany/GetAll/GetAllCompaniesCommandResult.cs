namespace Cwi.Treinamento.TesteAutomatizado.Domain.Companies.GetCompany.GetAll
{
    /// <summary>
    /// Define a classe GetAllCompaniesCommandResult.
    /// </summary>
    public class GetAllCompaniesCommandResult
    {
        /// <summary>
        /// Obtém ou define Id.
        /// </summary>
        /// <value>
        /// Id.
        /// </value>
        public long Id { get; set; }

        /// <summary>
        /// Obtém ou define Name.
        /// </summary>
        /// <value>
        /// Nome.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Obtém ou define Code.
        /// </summary>
        /// <value>
        /// Code.
        /// </value>
        public string Code { get; set; }

        /// <summary>
        /// Obtém ou define Active.
        /// </summary>
        /// <value>
        /// Email.
        /// </value>
        public bool Active { get; set; }

    }

}