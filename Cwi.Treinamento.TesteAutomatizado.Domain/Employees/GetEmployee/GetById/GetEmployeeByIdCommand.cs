using Cwi.Treinamento.TesteAutomatizado.Domain.Globalization;
using Cwi.Treinamento.TesteAutomatizado.Infra.Extensions;
using Cwi.Treinamento.TesteAutomatizado.Infra.Messaging;
using FluentValidation;

namespace Cwi.Treinamento.TesteAutomatizado.Domain.Employees.GetEmployee.GetById
{
    /// <summary>
    /// Comando usado para obtenção de funcionário por identificador.
    /// </summary>
    /// <seealso cref="Command{System.Int64}" />
    public class GetEmployeeByIdCommand : Command<GetEmployeeByIdCommandResult>
    {

        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="GetEmployeeByIdCommand"/>.
        /// </summary>
        /// <param name="id">Identificador do funcionário.</param>
        public GetEmployeeByIdCommand(long id)
        {
            this.Id = id;
        }

        /// <summary>
        /// Obtém ou define Id.
        /// </summary>
        /// <value>
        /// O Identificador.
        /// </value>
        public long Id { get; private set; }

        /// <summary>
        /// Valida os dados de entrada do comando usado para obtenção de funcionário por identificador.
        /// </summary>
        /// <returns>
        /// Retorna true se o comando é válido; caso contrário retorna false.
        /// </returns>
        public override bool IsValid()
        {
            var v = new InlineValidator<GetEmployeeByIdCommand>();

            v.RuleFor(x => x.Id).NotNull().WithErrorCode(GlobalizationConstants.BASIC0001).WithMessage(GlobalizationConstants.BASIC0001.Resource());

            ValidationResult = v.Validate(this);

            return ValidationResult.IsValid;
        }
    }
}
