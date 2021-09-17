using Cwi.Treinamento.TesteAutomatizado.Domain.Globalization;
using Cwi.Treinamento.TesteAutomatizado.Infra.Extensions;
using Cwi.Treinamento.TesteAutomatizado.Infra.Messaging;
using FluentValidation;

namespace Cwi.Treinamento.TesteAutomatizado.Domain.Companies.GetCompany.GetById
{
    /// <summary>
    /// Comando usado para obtenção de empresa por identificador.
    /// </summary>
    /// <seealso cref="Cwi.Treinamento.TesteAutomatizado.Infra.Messaging.Command{Cwi.Treinamento.TesteAutomatizado.Domain.Companies.GetCompany.GetById.GetCompanyByIdCommandResult}" />
    public class GetCompanyByIdCommand : Command<GetCompanyByIdCommandResult>
    {

        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="GetCompanyByIdCommand"/>.
        /// </summary>
        /// <param name="id">Identificador da empresa.</param>
        public GetCompanyByIdCommand(long id)
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
        /// Valida os dados de entrada do comando usado para obtenção de empresa por identificador.
        /// </summary>
        /// <returns>
        /// Retorna true se o comando é válido; caso contrário retorna false.
        /// </returns>
        public override bool IsValid()
        {
            var v = new InlineValidator<GetCompanyByIdCommand>();

            v.RuleFor(x => x.Id).NotNull().WithErrorCode(GlobalizationConstants.BASIC0001).WithMessage(GlobalizationConstants.BASIC0001.Resource());

            ValidationResult = v.Validate(this);

            return ValidationResult.IsValid;
        }
    }
}
