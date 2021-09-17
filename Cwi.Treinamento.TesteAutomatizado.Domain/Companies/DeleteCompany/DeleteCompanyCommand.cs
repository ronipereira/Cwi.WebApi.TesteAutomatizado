using Cwi.Treinamento.TesteAutomatizado.Domain.Globalization;
using Cwi.Treinamento.TesteAutomatizado.Infra.Extensions;
using Cwi.Treinamento.TesteAutomatizado.Infra.Messaging;
using FluentValidation;

namespace Cwi.Treinamento.TesteAutomatizado.Domain.Companies.DeleteCompany
{
    /// <summary>
    /// Comando usado para exclusão de empresa.
    /// </summary>
    /// <seealso cref="Command{System.Int64}" />
    public class DeleteCompanyCommand : Command<long>
    {
        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="DeleteCompanyCommand"/>.
        /// </summary>
        /// <param name="id">Identificador da empresa.</param>
        public DeleteCompanyCommand(long id)
        {
            this.Id = id;
        }

        /// <summary>
        /// Obtém ou define Id.
        /// </summary>
        /// <value>
        /// Id.
        /// </value>
        public long Id { get; private set; }

        /// <summary>
        /// Valida os dados de entrada do comando usado para exclusão de empresa.
        /// </summary>
        /// <returns>
        /// Retorna true se o comando é válido; caso contrário retorna false.
        /// </returns>
        public override bool IsValid()
        {
            var v = new InlineValidator<DeleteCompanyCommand>();

            v.RuleFor(x => x.Id)
                .NotNull().WithErrorCode(GlobalizationConstants.BASIC0001).WithMessage(GlobalizationConstants.BASIC0001.Resource())
                .GreaterThan(0).WithErrorCode(GlobalizationConstants.BASIC0002).WithMessage(GlobalizationConstants.BASIC0002.Resource());

            ValidationResult = v.Validate(this);

            return ValidationResult.IsValid;
        }
    }
}
