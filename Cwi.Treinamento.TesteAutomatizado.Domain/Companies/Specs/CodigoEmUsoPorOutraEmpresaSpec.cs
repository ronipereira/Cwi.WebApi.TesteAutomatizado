using Cwi.Treinamento.TesteAutomatizado.Domain.Globalization;
using Cwi.Treinamento.TesteAutomatizado.Infra.Extensions;
using Cwi.Treinamento.TesteAutomatizado.Infra.Validation;

namespace Cwi.Treinamento.TesteAutomatizado.Domain.Companies.Specs
{
    /// <summary>
    /// Verifica se o código esta sendo usado por outra empresa.
    /// </summary>
    /// <seealso cref="Cwi.Treinamento.TesteAutomatizado.Infra.Validation.BaseSpec{Cwi.Treinamento.TesteAutomatizado.Domain.Companies.Company}" />
    public class CodigoEmUsoPorOutraEmpresaSpec : BaseSpec<Company>
    {
        private readonly ICompanyRepository companyRepository;

        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="CodigoEmUsoPorOutraEmpresaSpec"/>.
        /// </summary>
        /// <param name="companyRepository">Repositório de empresas.</param>
        public CodigoEmUsoPorOutraEmpresaSpec(ICompanyRepository companyRepository)
        {
            this.companyRepository = companyRepository;
        }

        public override bool IsSatisfiedBy(Company entity)
        {
            NotSatisfiedCode = GlobalizationConstants.CodigoEmUsoPorOutraEmpresaSpec;
            NotSatisfiedReason = GlobalizationConstants.CodigoEmUsoPorOutraEmpresaSpec.Resource();

            var employee = companyRepository.GetCompanyByCodeAsync(entity.Code, entity.Id).GetAwaiter().GetResult();

            return employee == null;
        }

    }
}
