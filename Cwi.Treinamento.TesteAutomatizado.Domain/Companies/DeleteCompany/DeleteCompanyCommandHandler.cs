using Cwi.Treinamento.TesteAutomatizado.Domain.EmployeesCompany;
using Cwi.Treinamento.TesteAutomatizado.Domain.Globalization;
using Cwi.Treinamento.TesteAutomatizado.Infra.Extensions;
using Cwi.Treinamento.TesteAutomatizado.Infra.Messaging;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Cwi.Treinamento.TesteAutomatizado.Domain.Companies.DeleteCompany
{
    /// <summary>
    /// Define a classe DeleteCompanyCommandHandler.
    /// </summary>
    /// <seealso cref="Cwi.Treinamento.TesteAutomatizado.Infra.Messaging.CommandHandler{Cwi.Treinamento.TesteAutomatizado.Domain.Companies.DeleteCompany.DeleteCompanyCommand, System.Int64}" />
    public class DeleteCompanyCommandHandler : CommandHandler<DeleteCompanyCommand, long>
    {
        private readonly ICompanyRepository companyRepository;
        private readonly IEmployeeCompanyRepository employeeCompanyRepository;

        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="DeleteCompanyCommandHandler"/>.
        /// </summary>
        /// <param name="companyRepository">Repositório de empresas.</param>
        /// <param name="employeeCompanyRepository">Repositório de vínculo de funcionário com empresa.</param>
        public DeleteCompanyCommandHandler(ICompanyRepository companyRepository, IEmployeeCompanyRepository employeeCompanyRepository) : base()
        {
            this.companyRepository = companyRepository;
            this.employeeCompanyRepository = employeeCompanyRepository;
        }

        public override async Task<CommandResponse<long>> Handle(DeleteCompanyCommand request, CancellationToken cancellationToken)
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

                var companyEmployees = await employeeCompanyRepository.GetByIdCompanyAsync(company.Id);

                if (companyEmployees?.Any() ?? false)
                {
                    AddError(GlobalizationConstants.RegistroNaoPodeSerExcluido, GlobalizationConstants.RegistroNaoPodeSerExcluido.Resource());
                    return Response();
                }

                await companyRepository.RemoveAsync(company);

                return Response();
            }
            catch
            {
                throw;
            }
        }
    }
}
