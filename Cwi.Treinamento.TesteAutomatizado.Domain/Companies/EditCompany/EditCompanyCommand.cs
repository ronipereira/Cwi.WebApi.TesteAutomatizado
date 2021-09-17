using System.Text.Json.Serialization;
using Cwi.Treinamento.TesteAutomatizado.Domain.Globalization;
using Cwi.Treinamento.TesteAutomatizado.Infra.Extensions;
using Cwi.Treinamento.TesteAutomatizado.Infra.Messaging;
using FluentValidation;

namespace Cwi.Treinamento.TesteAutomatizado.Domain.Companies.EditCompany
{
    /// <summary>
    /// Comando usado para edição de empresa.
    /// </summary>
    /// <seealso cref="Command{System.Int64}" />
    public class EditCompanyCommand : Command<object>
    {

        /// <summary>
        /// Obtém ou define Id.
        /// </summary>
        /// <value>
        /// Id.
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
        /// Obtém ou define Code.
        /// </summary>
        /// <value>
        /// Code.
        /// </value>
        public string Code { get; set; }

        /// <summary>
        /// Obtém ou define MaxEmployeesNumber.
        /// </summary>
        /// <value>
        /// MaxEmployeesNumber.
        /// </value>
        public int MaxEmployeesNumber { get; set; }

        /// <summary>
        /// Valida os dados de entrada do comando usado para edição de empresa.
        /// </summary>
        /// <returns>
        /// Retorna true se o comando é válido; caso contrário retorna false.
        /// </returns>
        public override bool IsValid()
        {
            var v = new InlineValidator<EditCompanyCommand>();

            v.RuleFor(x => x.Name)
                .NotEmpty().WithErrorCode(GlobalizationConstants.BASIC0001).WithMessage(GlobalizationConstants.BASIC0001.Resource())
                .MaximumLength(100).WithErrorCode(GlobalizationConstants.BASIC0003).WithMessage(GlobalizationConstants.BASIC0003.Resource(nameof(this.Name), 100));

            v.RuleFor(x => x.Code)
                .NotEmpty().WithErrorCode(GlobalizationConstants.BASIC0001).WithMessage(GlobalizationConstants.BASIC0001.Resource())
                .MaximumLength(100).WithErrorCode(GlobalizationConstants.BASIC0003).WithMessage(GlobalizationConstants.BASIC0003.Resource(nameof(this.Code), 100));

            v.RuleFor(x => x.MaxEmployeesNumber)
                .NotNull().WithErrorCode(GlobalizationConstants.BASIC0001).WithMessage(GlobalizationConstants.BASIC0001.Resource())
                .GreaterThan(0).WithErrorCode(GlobalizationConstants.BASIC0002).WithMessage(GlobalizationConstants.BASIC0002.Resource());

            ValidationResult = v.Validate(this);

            return ValidationResult.IsValid;
        }
    }
}
