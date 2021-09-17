using Cwi.Treinamento.TesteAutomatizado.Domain.Companies.Specs;
using Cwi.Treinamento.TesteAutomatizado.Domain.Globalization;
using Cwi.Treinamento.TesteAutomatizado.Infra.Extensions;
using Cwi.Treinamento.TesteAutomatizado.Infra.Messaging;
using System.Threading;
using System.Threading.Tasks;

namespace Cwi.Treinamento.TesteAutomatizado.Domain.Companies.EditCompany
{
    /// <summary>
    /// Define a classe EditCompanyCommandHandler.
    /// </summary>
    /// <seealso cref="Cwi.Treinamento.TesteAutomatizado.Infra.Messaging.CommandHandler{Cwi.Treinamento.TesteAutomatizado.Domain.Companies.EditCompany.EditCompanyCommand, System.Object}" />
    public class EditCompanyCommandHandler : CommandHandler<EditCompanyCommand, object>
    {
        private readonly ICompanyRepository companyRepository;

        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="EditCompanyCommandHandler"/>.
        /// </summary>
        /// <param name="companyRepository">Repositório de empresas.</param>
        public EditCompanyCommandHandler(ICompanyRepository companyRepository) : base()
        {
            this.companyRepository = companyRepository;
        }

        public override async Task<CommandResponse<object>> Handle(EditCompanyCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid()) return Response(request.ValidationResult);

            try
            {
                var company = await companyRepository.FindByAsync(request.Id);

                if (company == null)
                {
                    AddError(GlobalizationConstants.RegistroNaoEncontrado, GlobalizationConstants.RegistroNaoEncontrado.Resource());
                    return Response();
                }

                company.Name = request.Name;
                company.Code = request.Code;
                company.MaxEmployeesNumber = request.MaxEmployeesNumber;

                var validationResult = new CodigoEmUsoPorOutraEmpresaSpec(companyRepository).Validate(company);

                if (!validationResult.IsValid)
                {
                    return Response(validationResult);
                }

                await companyRepository.UpdateAsync(company);

                return Response();
            }
            catch
            {
                throw;
            }
        }
    }
}