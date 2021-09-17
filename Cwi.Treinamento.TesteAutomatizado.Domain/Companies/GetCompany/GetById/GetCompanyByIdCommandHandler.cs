using Cwi.Treinamento.TesteAutomatizado.Domain.EmployeesCompany;
using Cwi.Treinamento.TesteAutomatizado.Domain.Globalization;
using Cwi.Treinamento.TesteAutomatizado.Infra.Extensions;
using Cwi.Treinamento.TesteAutomatizado.Infra.Messaging;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Cwi.Treinamento.TesteAutomatizado.Domain.Companies.GetCompany.GetById
{
    /// <summary>
    /// Define a classe GetCompanyByIdCommandHandler.
    /// </summary>
    /// <seealso cref="Cwi.Treinamento.TesteAutomatizado.Infra.Messaging.CommandHandler{Cwi.Treinamento.TesteAutomatizado.Domain.Companies.GetCompany.GetById.GetCompanyByIdCommand, Cwi.Treinamento.TesteAutomatizado.Domain.Companies.GetCompany.GetById.GetCompanyByIdCommandResult}" />
    public class GetCompanyByIdCommandHandler : CommandHandler<GetCompanyByIdCommand, GetCompanyByIdCommandResult>
    {
        private readonly ICompanyRepository companyRepository;
        private readonly IEmployeeCompanyQueryRepository employeeCompanyQueryRepository;

        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="GetCompanyByIdCommandHandler"/>.
        /// </summary>
        /// <param name="companyRepository">Repositório de empresa.</param>
        /// <param name="employeeCompanyQueryRepository">Query repositório de vínculo entre funcionários e empresas.</param>
        public GetCompanyByIdCommandHandler(ICompanyRepository companyRepository, IEmployeeCompanyQueryRepository employeeCompanyQueryRepository) : base()
        {
            this.companyRepository = companyRepository;
            this.employeeCompanyQueryRepository = employeeCompanyQueryRepository;
        }

        public override async Task<CommandResponse<GetCompanyByIdCommandResult>> Handle(GetCompanyByIdCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var company = await companyRepository.FindByAsync(request.Id);

                if (company == null)
                {
                    AddError(GlobalizationConstants.RegistroNaoEncontrado, GlobalizationConstants.RegistroNaoEncontrado.Resource());
                    return Response();
                }

                var response = new GetCompanyByIdCommandResult(company.Id, company.Name, company.Code, company.MaxEmployeesNumber, company.Active);

                var companyEmployees = await employeeCompanyQueryRepository.GetByCompanyWithEmployeesAsync(request.Id);

                if (companyEmployees?.Any() ?? false)
                {
                    response.Employees.AddRange(companyEmployees.Select(ec => new GetCompanyByIdEmployeesCommandResult(ec.Employee, ec.FromDate, ec.ToDate)));
                }

                return Response(response);
            }
            catch
            {
                throw;
            }
        }
    }
}