using Cwi.Treinamento.TesteAutomatizado.Domain.Employees;
using Cwi.Treinamento.TesteAutomatizado.Domain.EmployeesCompany;
using Cwi.Treinamento.TesteAutomatizado.Domain.Globalization;
using Cwi.Treinamento.TesteAutomatizado.Infra.Extensions;
using Cwi.Treinamento.TesteAutomatizado.Infra.Validation;

namespace Cwi.Treinamento.TesteAutomatizado.Domain.Companies.HireEmployee.Specs
{
    /// <summary>
    /// Verifica se o funcionário pode ser contratado pela empresa.
    /// </summary>
    /// <seealso cref="Cwi.Treinamento.TesteAutomatizado.Infra.Validation.BaseSpec{Cwi.Treinamento.TesteAutomatizado.Domain.Companies.Company}" />
    public class FuncionarioNaoPodeSerContratadoSpec : BaseSpec<Employee>
    {
        private readonly IEmployeeCompanyQueryRepository employeeCompanyQueryRepository;

        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="FuncionarioNaoPodeSerContratadoSpec"/>.
        /// </summary>
        /// <param name="employeeCompanyQueryRepository">Query repositório de vínculo do funcionário com empresa.</param>
        public FuncionarioNaoPodeSerContratadoSpec(IEmployeeCompanyQueryRepository employeeCompanyQueryRepository)
        {
            this.employeeCompanyQueryRepository = employeeCompanyQueryRepository;
        }

        public override bool IsSatisfiedBy(Employee entity)
        {
            NotSatisfiedCode = GlobalizationConstants.FuncionarioNaoPodeSerContratadoSpec;
            NotSatisfiedReason = GlobalizationConstants.FuncionarioNaoPodeSerContratadoSpec.Resource();

            var employeeCompany = employeeCompanyQueryRepository.GetCurrentCompanyByEmployeeAsync(entity.Id).GetAwaiter().GetResult();

            return employeeCompany == null;
        }

    }
}
