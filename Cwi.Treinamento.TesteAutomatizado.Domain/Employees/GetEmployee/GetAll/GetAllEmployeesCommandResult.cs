namespace Cwi.Treinamento.TesteAutomatizado.Domain.Employees.GetEmployee.GetAll
{
    /// <summary>
    /// Define a classe GetAllEmployeesCommandResult.
    /// </summary>
    public class GetAllEmployeesCommandResult
    {
        /// <summary>
        /// Obt?m ou define Id.
        /// </summary>
        /// <value>
        /// Id.
        /// </value>
        public long Id { get; set; }

        /// <summary>
        /// Obt?m ou define Name.
        /// </summary>
        /// <value>
        /// Nome.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Obt?m ou define Email.
        /// </summary>
        /// <value>
        /// Email.
        /// </value>
        public string Email { get; set; }

        /// <summary>
        /// Obt?m ou define Active.
        /// </summary>
        /// <value>
        /// Email.
        /// </value>
        public bool Active { get; set; }

    }

}