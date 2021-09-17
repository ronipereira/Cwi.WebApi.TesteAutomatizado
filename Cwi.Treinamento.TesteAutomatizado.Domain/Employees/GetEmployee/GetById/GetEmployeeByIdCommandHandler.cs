using Cwi.Treinamento.TesteAutomatizado.Domain.EmployeesCompany;
using Cwi.Treinamento.TesteAutomatizado.Domain.Globalization;
using Cwi.Treinamento.TesteAutomatizado.Infra.Extensions;
using Cwi.Treinamento.TesteAutomatizado.Infra.Messaging;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Cwi.Treinamento.TesteAutomatizado.Domain.Employees.GetEmployee.GetById
{
    /// <summary>
    /// Define a classe GetEmployeeByIdCommandHandler.
    /// </summary>
    /// <seealso cref="Cwi.Treinamento.TesteAutomatizado.Infra.Messaging.CommandHandler{Cwi.Treinamento.TesteAutomatizado.Domain.Employees.GetEmployee.GetById.GetEmployeeByIdCommand, Cwi.Treinamento.TesteAutomatizado.Domain.Employees.GetEmployee.GetById.GetEmployeeByIdCommandResult}" />
    public class GetEmployeeByIdCommandHandler : CommandHandler<GetEmployeeByIdCommand, GetEmployeeByIdCommandResult>
    {
        private readonly IEmployeeRepository employeeRepository;
        private readonly IEmployeeCompanyQueryRepository employeeCompanyQueryRepository;

        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="GetEmployeeByIdCommandHandler"/>.
        /// </summary>
        /// <param name="employeeRepository">Repositório de funcionários.</param>
        /// <param name="employeeCompanyQueryRepository">Query repositório de vínculo entre funcionários e empresas.</param>
        public GetEmployeeByIdCommandHandler(IEmployeeRepository employeeRepository, IEmployeeCompanyQueryRepository employeeCompanyQueryRepository) : base()
        {
            this.employeeRepository = employeeRepository;
            this.employeeCompanyQueryRepository = employeeCompanyQueryRepository;
        }

        public override async Task<CommandResponse<GetEmployeeByIdCommandResult>> Handle(GetEmployeeByIdCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var employee = await employeeRepository.FindByAsync(request.Id);

                if (employee == null)
                {
                    AddError(GlobalizationConstants.RegistroNaoEncontrado, GlobalizationConstants.RegistroNaoEncontrado.Resource());
                    return Response();
                }

                var response = new GetEmployeeByIdCommandResult(employee.Id, employee.Name, employee.Email, employee.Active);

                var employeeCompanies = await employeeCompanyQueryRepository.GetByEmployeeWithCompaniesAsync(request.Id);

                if (employeeCompanies?.Any() ?? false)
                {
                    response.Companies.AddRange(employeeCompanies.Select(ec => new GetEmployeeByIdCompaniesCommandResult(ec.Company, ec.FromDate, ec.ToDate)));
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
