using Cwi.Treinamento.TesteAutomatizado.Domain.EmployeesCompany;
using Cwi.Treinamento.TesteAutomatizado.Domain.Globalization;
using Cwi.Treinamento.TesteAutomatizado.Infra.Extensions;
using Cwi.Treinamento.TesteAutomatizado.Infra.Messaging;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Cwi.Treinamento.TesteAutomatizado.Domain.Employees.DeleteEmployee
{
    /// <summary>
    /// Define a classe DeleteEmployeeCommandHandler.
    /// </summary>
    /// <seealso cref="Cwi.Treinamento.TesteAutomatizado.Infra.Messaging.CommandHandler{Cwi.Treinamento.TesteAutomatizado.Domain.Employees.DeleteEmployee.DeleteEmployeeCommand, System.Int64}" />
    public class DeleteEmployeeCommandHandler : CommandHandler<DeleteEmployeeCommand, long>
    {
        private readonly IEmployeeRepository employeeRepository;
        private readonly IEmployeeCompanyRepository employeeCompanyRepository;

        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="DeleteEmployeeCommandHandler"/>.
        /// </summary>
        /// <param name="employeeRepository">Repositório de funcionários.</param>
        /// <param name="employeeCompanyRepository">Repositório de vínculo de funcionário com empresa.</param>
        public DeleteEmployeeCommandHandler(IEmployeeRepository employeeRepository, IEmployeeCompanyRepository employeeCompanyRepository) : base()
        {
            this.employeeRepository = employeeRepository;
            this.employeeCompanyRepository = employeeCompanyRepository;
        }

        public override async Task<CommandResponse<long>> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid()) return Response(request.ValidationResult);

            try
            {
                var employee = await employeeRepository.FindByAsync(request.Id);

                if (employee == null)
                {
                    AddError(GlobalizationConstants.RegistroNaoEncontrado, GlobalizationConstants.RegistroNaoEncontrado.Resource());
                    return Response();
                }

                var employeeCompanies = await employeeCompanyRepository.GetByIdEmployeeAsync(employee.Id);

                if (employeeCompanies?.Any() ?? false)
                {
                    AddError(GlobalizationConstants.RegistroNaoPodeSerExcluido, GlobalizationConstants.RegistroNaoPodeSerExcluido.Resource());
                    return Response();
                }

                await employeeRepository.RemoveAsync(employee);

                return Response();
            }
            catch
            {
                throw;
            }
        }
    }
}
