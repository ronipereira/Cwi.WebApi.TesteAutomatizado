namespace Cwi.Treinamento.TesteAutomatizado.Domain.Employees.GetEmployee.GetByPage
{
    /// <summary>
    /// Define a classe GetEmployeeByPageCommandResult.
    /// </summary>
    public class GetEmployeeByPageCommandResult
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
        /// O Nome.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Obtém ou define Email.
        /// </summary>
        /// <value>
        /// Email.
        /// </value>
        public string Email { get; set; }

        /// <summary>
        /// Obtém ou define Active.
        /// </summary>
        /// <value>
        /// Email.
        /// </value>
        public bool Active { get; set; }
    }

}