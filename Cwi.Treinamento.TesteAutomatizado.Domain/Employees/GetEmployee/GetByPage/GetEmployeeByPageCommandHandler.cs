using Cwi.Treinamento.TesteAutomatizado.Infra.Messaging;
using Cwi.Treinamento.TesteAutomatizado.Infra.Paging;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Cwi.Treinamento.TesteAutomatizado.Domain.Employees.GetEmployee.GetByPage
{
    /// <summary>
    /// Define a classe GetEmployeeByPageCommandHandler.
    /// </summary>
    /// <seealso cref="Cwi.Treinamento.TesteAutomatizado.Infra.Messaging.CommandHandler{Cwi.Treinamento.TesteAutomatizado.Domain.Employees.GetEmployee.GetAll.GetAllEmployeesCommand, System.Collections.Generic.IEnumerable{Cwi.Treinamento.TesteAutomatizado.Domain.Employees.GetEmployee.GetAll.GetAllEmployeesCommandResult}}" />
    public class GetEmployeeByPageCommandHandler : CommandHandler<GetEmployeeByPageCommand, PagedResult<GetEmployeeByPageCommandResult>>
    {
        private readonly IEmployeeRepository employeeRepository;

        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="GetEmployeeByPageCommandHandler"/>.
        /// </summary>
        /// <param name="employeeRepository">Repositório de funcionários.</param>
        public GetEmployeeByPageCommandHandler(IEmployeeRepository employeeRepository) : base()
        {
            this.employeeRepository = employeeRepository;
        }

        public override async Task<CommandResponse<PagedResult<GetEmployeeByPageCommandResult>>> Handle(GetEmployeeByPageCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var employees = await employeeRepository.GetEmployeesByPageAsync(request.Filter.Page, request.Filter.Limit);

                var totalEmployees = employees.FirstOrDefault()?.TotalEmployees ?? 0;

                return Response(
                    new PagedResult<GetEmployeeByPageCommandResult>(totalEmployees, request.Filter.Limit, employees.Select(c => new GetEmployeeByPageCommandResult
                    {
                        Id = c.Id,
                        Name = c.Name,
                        Email = c.Email,
                        Active = c.Active
                    })));
            }
            catch
            {
                throw;
            }
        }
    }
}
