using Cwi.Treinamento.TesteAutomatizado.Domain.Globalization;
using Cwi.Treinamento.TesteAutomatizado.Infra.Extensions;
using Cwi.Treinamento.TesteAutomatizado.Infra.Validation;

namespace Cwi.Treinamento.TesteAutomatizado.Domain.Employees.Specs
{
    /// <summary>
    /// Verifica se o e-mail esta sendo usado por outro funcionário.
    /// </summary>
    /// <seealso cref="Cwi.Treinamento.TesteAutomatizado.Infra.Validation.BaseSpec{Cwi.Treinamento.TesteAutomatizado.Domain.Employees.Employee}" />
    public class EmailEmUsoPorOutroFuncionarioSpec : BaseSpec<Employee>
    {
        private readonly IEmployeeRepository employeeRepository;

        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="EmailEmUsoPorOutroFuncionarioSpec"/>.
        /// </summary>
        /// <param name="employeeRepository">Repositório de funcionários.</param>
        public EmailEmUsoPorOutroFuncionarioSpec(IEmployeeRepository employeeRepository)
        {
            this.employeeRepository = employeeRepository;
        }

        public override bool IsSatisfiedBy(Employee entity)
        {
            NotSatisfiedCode = GlobalizationConstants.EmailEmUsoPorOutroFuncionarioSpec;
            NotSatisfiedReason = GlobalizationConstants.EmailEmUsoPorOutroFuncionarioSpec.Resource();

            var employee = employeeRepository.GetEmployeeByEmailAsync(entity.Email, entity.Id).GetAwaiter().GetResult();

            return employee == null;
        }

    }
}
