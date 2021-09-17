using Cwi.Treinamento.TesteAutomatizado.Domain.Employees.Specs;
using Cwi.Treinamento.TesteAutomatizado.Infra.Messaging;
using System.Threading;
using System.Threading.Tasks;

namespace Cwi.Treinamento.TesteAutomatizado.Domain.Employees.CreateEmployee
{
    /// <summary>
    /// Define a classe CreateEmployeeCommandHandler.
    /// </summary>
    /// <seealso cref="Cwi.Treinamento.TesteAutomatizado.Infra.Messaging.CommandHandler{Cwi.Treinamento.TesteAutomatizado.Domain.Employees.CreateEmployee.CreateEmployeeCommand, System.Int64}" />
    public class CreateCompanyCommandHandler : CommandHandler<CreateEmployeeCommand, long>
    {
        private readonly IEmployeeRepository employeeRepository;

        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="CreateCompanyCommandHandler"/>.
        /// </summary>
        /// <param name="employeeRepository">Repositório de funcionários.</param>
        public CreateCompanyCommandHandler(IEmployeeRepository employeeRepository) : base()
        {
            this.employeeRepository = employeeRepository;
        }

        public override async Task<CommandResponse<long>> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid()) return Response(request.ValidationResult);

            try
            {
                var employee = new Employee(request.Name, request.Email, true);

                var validationResult = new EmailEmUsoPorOutroFuncionarioSpec(employeeRepository).Validate(employee);

                if (!validationResult.IsValid)
                {
                    return Response(validationResult);
                }

                var idEmployee = await employeeRepository.InsertAsync(employee);

                return Response(idEmployee);
            }
            catch
            {
                throw;
            }
        }
    }
}
