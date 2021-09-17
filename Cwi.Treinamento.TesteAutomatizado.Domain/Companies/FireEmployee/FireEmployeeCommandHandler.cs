using Cwi.Treinamento.TesteAutomatizado.Domain.Companies.FireEmployee.Specs;
using Cwi.Treinamento.TesteAutomatizado.Domain.Employees;
using Cwi.Treinamento.TesteAutomatizado.Domain.EmployeesCompany;
using Cwi.Treinamento.TesteAutomatizado.Domain.Globalization;
using Cwi.Treinamento.TesteAutomatizado.Infra.Extensions;
using Cwi.Treinamento.TesteAutomatizado.Infra.Messaging;
using System.Threading;
using System.Threading.Tasks;

namespace Cwi.Treinamento.TesteAutomatizado.Domain.Companies.FireEmployee
{
    /// <summary>
    /// Define a classe FireEmployeeCommandHandler.
    /// </summary>
    /// <seealso cref="Cwi.Treinamento.TesteAutomatizado.Infra.Messaging.CommandHandler{Cwi.Treinamento.TesteAutomatizado.Domain.Companies.FireEmployee.FireEmployeeCommand, System.Boolean}" />
    public class FireEmployeeCommandHandler : CommandHandler<FireEmployeeCommand, bool>
    {
        private readonly ICompanyRepository companyRepository;
        private readonly IEmployeeRepository employeeRepository;
        private readonly IEmployeeCompanyRepository employeeCompanyRepository;
        private readonly IEmployeeCompanyQueryRepository employeeCompanyQueryRepository;

        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="FireEmployeeCommandHandler"/>.
        /// </summary>
        /// <param name="companyRepository">Repositório de empresas.</param>
        /// <param name="companyRepository">Repositório de funcionários.</param>
        /// <param name="employeeCompanyRepository">Repositório de vínculo entre funcionário e empresa.</param>
        /// <param name="employeeCompanyQueryRepository">Query repositório de vínculo entre funcionário e empresa.</param>
        public FireEmployeeCommandHandler(ICompanyRepository companyRepository, IEmployeeRepository employeeRepository, IEmployeeCompanyRepository employeeCompanyRepository, IEmployeeCompanyQueryRepository employeeCompanyQueryRepository) : base()
        {
            this.companyRepository = companyRepository;
            this.employeeRepository = employeeRepository;
            this.employeeCompanyRepository = employeeCompanyRepository;
            this.employeeCompanyQueryRepository = employeeCompanyQueryRepository;
        }

        public override async Task<CommandResponse<bool>> Handle(FireEmployeeCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid()) return Response(request.ValidationResult);

            try
            {
                var company = await companyRepository.FindByAsync(request.Id);

                if (company == null)
                {
                    AddError(GlobalizationConstants.RegistroNaoEncontrado, GlobalizationConstants.RegistroNaoEncontrado.Resource());
                    return Response();
                }

                var employee = await employeeRepository.FindByAsync(request.IdEmployee);

                if (employee == null)
                {
                    AddError(GlobalizationConstants.RegistroNaoEncontrado, GlobalizationConstants.RegistroNaoEncontrado.Resource());
                    return Response();
                }

                var employeeCompany = await employeeCompanyQueryRepository.GetCurrentCompanyByEmployeeAsync(employee.Id);

                var validationResult = new FuncionarioNaoPossuiVinculoComEmpresaSpec().Validate(employeeCompany);

                if (!validationResult.IsValid)
                {
                    return Response(validationResult);
                }

                employeeCompany.ToFire();

                await employeeCompanyRepository.UpdateAsync(employeeCompany);

                return Response();
            }
            catch
            {
                throw;
            }
        }
    }
}
