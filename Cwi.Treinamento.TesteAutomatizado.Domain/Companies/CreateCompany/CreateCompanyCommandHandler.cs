using Cwi.Treinamento.TesteAutomatizado.Domain.Companies.Specs;
using Cwi.Treinamento.TesteAutomatizado.Infra.Messaging;
using Cwi.Treinamento.TesteAutomatizado.Infra.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace Cwi.Treinamento.TesteAutomatizado.Domain.Companies.CreateCompany
{
    /// <summary>
    /// Define a classe CreateCompanyCommandHandler.
    /// </summary>
    /// <seealso cref="Cwi.Treinamento.TesteAutomatizado.Infra.Messaging.CommandHandler{Cwi.Treinamento.TesteAutomatizado.Domain.Companies.CreateCompany.CreateCompanyCommand, System.Int64}" />
    public class CreateCompanyCommandHandler : CommandHandler<CreateCompanyCommand, long>
    {
        private readonly ICompanyRepository companyRepository;

        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="CreateCompanyCommandHandler"/>.
        /// </summary>
        /// <param name="companyRepository">Repositório de empresas.</param>
        public CreateCompanyCommandHandler(ICompanyRepository companyRepository) : base()
        {
            this.companyRepository = companyRepository;
        }

        public override async Task<CommandResponse<long>> Handle(CreateCompanyCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid()) return Response(request.ValidationResult);

            try
            {
                var company = new Company(request.Name, request.Code, request.MaxEmployeesNumber, true);

                var validationResult = new CodigoEmUsoPorOutraEmpresaSpec(companyRepository).Validate(company);

                if (!validationResult.IsValid)
                {
                    return Response(validationResult);
                }

                var idEmployee = await companyRepository.InsertAsync(company);

                return Response(idEmployee);
            }
            catch
            {
                throw;
            }
        }
    }
}