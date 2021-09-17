using Cwi.Treinamento.TesteAutomatizado.Domain.Employees.Specs;
using Cwi.Treinamento.TesteAutomatizado.Domain.Globalization;
using Cwi.Treinamento.TesteAutomatizado.Infra.Extensions;
using Cwi.Treinamento.TesteAutomatizado.Infra.Messaging;
using System.Threading;
using System.Threading.Tasks;

namespace Cwi.Treinamento.TesteAutomatizado.Domain.Employees.EditEmployee
{
    /// <summary>
    /// Define a classe EditEmployeeCommandHandler.
    /// </summary>
    /// <seealso cref="Cwi.Treinamento.TesteAutomatizado.Infra.Messaging.CommandHandler{Cwi.Treinamento.TesteAutomatizado.Domain.Employees.EditEmployee.EditEmployeeCommand, System.Object}" />
    public class EditEmployeeCommandHandler : CommandHandler<EditEmployeeCommand, object>
    {
        private readonly IEmployeeRepository employeeRepository;

        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="EditEmployeeCommandHandler"/>.
        /// </summary>
        /// <param name="employeeRepository">Repositório de funcionários.</param>
        public EditEmployeeCommandHandler(IEmployeeRepository employeeRepository) : base()
        {
            this.employeeRepository = employeeRepository;
        }

        public override async Task<CommandResponse<object>> Handle(EditEmployeeCommand request, CancellationToken cancellationToken)
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

                employee.Name = request.Name;
                employee.Email = request.Email;

                var validationResult = new EmailEmUsoPorOutroFuncionarioSpec(employeeRepository).Validate(employee);

                if (!validationResult.IsValid)
                {
                    return Response(validationResult);
                }

                await employeeRepository.UpdateAsync(employee);

                return Response();
            }
            catch
            {
                throw;
            }
        }
    }
}
