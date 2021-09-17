using Cwi.Treinamento.TesteAutomatizado.Domain.EmployeesCompany;
using Cwi.Treinamento.TesteAutomatizado.Domain.Globalization;
using Cwi.Treinamento.TesteAutomatizado.Infra.Extensions;
using Cwi.Treinamento.TesteAutomatizado.Infra.Validation;
using System.Linq;

namespace Cwi.Treinamento.TesteAutomatizado.Domain.Companies.HireEmployee.Specs
{
    /// <summary>
    /// Verifica se a empresa não excedeu o número máximo de funcionários ativos.
    /// </summary>
    /// <seealso cref="Cwi.Treinamento.TesteAutomatizado.Infra.Validation.BaseSpec{Cwi.Treinamento.TesteAutomatizado.Domain.Companies.Company}" />
    public class EmpresaNaoPodeContratarSpec : BaseSpec<Company>
    {
        private readonly IEmployeeCompanyRepository employeeCompanyRepository;

        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="EmpresaNaoPodeContratarSpec"/>.
        /// </summary>
        /// <param name="employeeCompanyRepository">Repositório de vínculo do funcionário com empresa.</param>
        public EmpresaNaoPodeContratarSpec(IEmployeeCompanyRepository employeeCompanyRepository)
        {
            this.employeeCompanyRepository = employeeCompanyRepository;
        }

        public override bool IsSatisfiedBy(Company entity)
        {
            NotSatisfiedCode = GlobalizationConstants.EmpresaNaoPodeContratarSpec;
            NotSatisfiedReason = GlobalizationConstants.EmpresaNaoPodeContratarSpec.Resource();

            var companyEmployees = employeeCompanyRepository.GetByIdCompanyAsync(entity.Id).GetAwaiter().GetResult();

            return companyEmployees == null || companyEmployees.Where(ce=> !ce.ToDate.HasValue).Count() < entity.MaxEmployeesNumber;
        }

    }
}
