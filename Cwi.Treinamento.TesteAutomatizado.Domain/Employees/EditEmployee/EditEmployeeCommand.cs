using System.Text.Json.Serialization;
using Cwi.Treinamento.TesteAutomatizado.Domain.Globalization;
using Cwi.Treinamento.TesteAutomatizado.Infra.Extensions;
using Cwi.Treinamento.TesteAutomatizado.Infra.Messaging;
using FluentValidation;

namespace Cwi.Treinamento.TesteAutomatizado.Domain.Employees.EditEmployee
{
    /// <summary>
    /// Comando usado para edição de funcionário.
    /// </summary>
    /// <seealso cref="Command{System.Int64}" />
    public class EditEmployeeCommand : Command<object>
    {

        /// <summary>
        /// Obtém ou define Id.
        /// </summary>
        /// <value>
        /// O Identificador.
        /// </value>
        [JsonIgnore]
        public long Id { get; set; }

        /// <summary>
        /// Obtém ou define Name.
        /// </summary>
        /// <value>
        /// Name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Obtém ou define Email.
        /// </summary>
        /// <value>
        /// Email.
        /// </value>
        public string Email { get; set; }

        /// <summary>
        /// Valida os dados de entrada do comando usado para edição de funcionário.
        /// </summary>
        /// <returns>
        /// Retorna true se o comando é válido; caso contrário retorna false.
        /// </returns>
        public override bool IsValid()
        {
            var v = new InlineValidator<EditEmployeeCommand>();

            v.RuleFor(x => x.Name)
                .NotEmpty().WithErrorCode(GlobalizationConstants.BASIC0001).WithMessage(GlobalizationConstants.BASIC0001.Resource())
                .MaximumLength(100).WithErrorCode(GlobalizationConstants.BASIC0003).WithMessage(GlobalizationConstants.BASIC0003.Resource(nameof(this.Name), 100));

            v.RuleFor(x => x.Email)
             .NotEmpty().WithErrorCode(GlobalizationConstants.BASIC0001).WithMessage(GlobalizationConstants.BASIC0001.Resource())
             .EmailAddress().WithErrorCode(GlobalizationConstants.BASIC0002).WithMessage(GlobalizationConstants.BASIC0002.Resource())
             .MaximumLength(100).WithErrorCode(GlobalizationConstants.BASIC0003).WithMessage(GlobalizationConstants.BASIC0003.Resource(nameof(this.Email), 100));

            ValidationResult = v.Validate(this);

            return ValidationResult.IsValid;
        }
    }
}
