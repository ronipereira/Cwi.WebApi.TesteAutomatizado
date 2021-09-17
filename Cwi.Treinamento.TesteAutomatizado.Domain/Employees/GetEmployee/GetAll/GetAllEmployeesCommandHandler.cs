using Cwi.Treinamento.TesteAutomatizado.Infra.Messaging;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Cwi.Treinamento.TesteAutomatizado.Domain.Employees.GetEmployee.GetAll
{
    /// <summary>
    /// Define a classe GetAllEmployeesCommandHandler.
    /// </summary>
    /// <seealso cref="Cwi.Treinamento.TesteAutomatizado.Infra.Messaging.CommandHandler{Cwi.Treinamento.TesteAutomatizado.Domain.Employees.GetEmployee.GetAll.GetAllEmployeesCommand, System.Collections.Generic.IEnumerable{Cwi.Treinamento.TesteAutomatizado.Domain.Employees.GetEmployee.GetAll.GetAllEmployeesCommandResult}}" />
    public class GetAllEmployeesCommandHandler : CommandHandler<GetAllEmployeesCommand, IEnumerable<GetAllEmployeesCommandResult>>
    {
        private readonly IEmployeeRepository employeeRepository;

        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="GetAllEmployeesCommandHandler"/>.
        /// </summary>
        /// <param name="employeeRepository">Repositório de funcionários.</param>
        public GetAllEmployeesCommandHandler(IEmployeeRepository employeeRepository) : base()
        {
            this.employeeRepository = employeeRepository;
        }

        public override async Task<CommandResponse<IEnumerable<GetAllEmployeesCommandResult>>> Handle(GetAllEmployeesCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var employees = await employeeRepository.GetAllEmployeesAsync();

                return Response(
                    employees.Select(c => new GetAllEmployeesCommandResult
                    {
                        Id = c.Id,
                        Name = c.Name,
                        Email = c.Email,
                        Active = c.Active
                    }
                ));
            }
            catch
            {
                throw;
            }
        }
    }
}
