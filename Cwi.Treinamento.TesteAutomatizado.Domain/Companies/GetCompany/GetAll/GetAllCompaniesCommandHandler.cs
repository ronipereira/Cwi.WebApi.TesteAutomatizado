using Cwi.Treinamento.TesteAutomatizado.Infra.Messaging;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Cwi.Treinamento.TesteAutomatizado.Domain.Companies.GetCompany.GetAll
{
    /// <summary>
    /// Define a classe GetAllCompaniesCommandHandler.
    /// </summary>
    /// <seealso cref="Cwi.Treinamento.TesteAutomatizado.Infra.Messaging.CommandHandler{Cwi.Treinamento.TesteAutomatizado.Domain.Companies.GetCompany.GetAll.GetAllCompaniesCommand, System.Collections.Generic.IEnumerable{Cwi.Treinamento.TesteAutomatizado.Domain.Companies.GetCompany.GetAll.GetAllCompaniesCommandResult}}" />
    public class GetAllCompaniesCommandHandler : CommandHandler<GetAllCompaniesCommand, IEnumerable<GetAllCompaniesCommandResult>>
    {
        private readonly ICompanyRepository companyRepository;

        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="GetAllCompaniesCommandHandler"/>.
        /// </summary>
        /// <param name="companyRepository">Repositório de empresas.</param>
        public GetAllCompaniesCommandHandler(ICompanyRepository companyRepository) : base()
        {
            this.companyRepository = companyRepository;
        }

        public override async Task<CommandResponse<IEnumerable<GetAllCompaniesCommandResult>>> Handle(GetAllCompaniesCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var companies = await companyRepository.GetAllCompaniesAsync();

                return Response(
                    companies.Select(c => new GetAllCompaniesCommandResult
                    {
                        Id = c.Id,
                        Name = c.Name,
                        Code = c.Code,
                        Active = c.Active
                    }
                ));
            }
            catch
            {
                throw;
            }
        }
    }
}
