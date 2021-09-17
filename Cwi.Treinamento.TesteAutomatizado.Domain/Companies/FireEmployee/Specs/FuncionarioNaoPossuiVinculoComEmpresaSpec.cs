using Cwi.Treinamento.TesteAutomatizado.Domain.EmployeesCompany;
using Cwi.Treinamento.TesteAutomatizado.Domain.Globalization;
using Cwi.Treinamento.TesteAutomatizado.Infra.Extensions;
using Cwi.Treinamento.TesteAutomatizado.Infra.Validation;
using System.Linq;

namespace Cwi.Treinamento.TesteAutomatizado.Domain.Companies.FireEmployee.Specs
{
    /// <summary>
    /// Verifica se o funcionário possui um vínculo ativo com a empresa.
    /// </summary>
    /// <seealso cref="Cwi.Treinamento.TesteAutomatizado.Infra.Validation.BaseSpec{Cwi.Treinamento.TesteAutomatizado.Domain.EmployeesCompany.EmployeeCompany}" />
    public class FuncionarioNaoPossuiVinculoComEmpresaSpec : BaseSpec<EmployeeCompany>
    {
        public override bool IsSatisfiedBy(EmployeeCompany entity)
        {
            NotSatisfiedCode = GlobalizationConstants.FuncionarioNaoPossuiVinculoComEmpresaSpec;
            NotSatisfiedReason = GlobalizationConstants.FuncionarioNaoPossuiVinculoComEmpresaSpec.Resource();

            return entity != null;
        }

    }
}
