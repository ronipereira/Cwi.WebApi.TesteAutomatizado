using Cwi.Treinamento.TesteAutomatizado.Infra.Messaging;
using Cwi.Treinamento.TesteAutomatizado.Infra.Paging;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Cwi.Treinamento.TesteAutomatizado.Domain.Companies.GetCompany.GetByPage
{
    /// <summary>
    /// Define a classe GetCompanyByPageCommandHandler.
    /// </summary>
    /// <seealso cref="Cwi.Treinamento.TesteAutomatizado.Infra.Messaging.CommandHandler{Cwi.Treinamento.TesteAutomatizado.Domain.Companies.GetCompany.GetByPage.GetCompanyByPageCommand, Cwi.Treinamento.TesteAutomatizado.Infra.Paging.PagedResult{Cwi.Treinamento.TesteAutomatizado.Domain.Companies.GetCompany.GetByPage.GetCompanyByPageCommandResult}}" />
    public class GetCompanyByPageCommandHandler : CommandHandler<GetCompanyByPageCommand, PagedResult<GetCompanyByPageCommandResult>>
    {
        private readonly ICompanyRepository companyRepository;

        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="GetCompanyByPageCommandHandler"/>.
        /// </summary>
        /// <param name="companyRepository">Repositório de empresas.</param>
        public GetCompanyByPageCommandHandler(ICompanyRepository companyRepository) : base()
        {
            this.companyRepository = companyRepository;
        }

        public override async Task<CommandResponse<PagedResult<GetCompanyByPageCommandResult>>> Handle(GetCompanyByPageCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var companies = await companyRepository.GetCompaniesByPageAsync(request.Filter.Page, request.Filter.Limit);

                var totalCompanies = companies.FirstOrDefault()?.TotalCompanies ?? 0;

                return Response(
                    new PagedResult<GetCompanyByPageCommandResult>(totalCompanies, request.Filter.Limit, companies.Select(c => new GetCompanyByPageCommandResult
                    {
                        Id = c.Id,
                        Name = c.Name,
                        Code = c.Code,
                        Active = c.Active
                    })));
            }
            catch
            {
                throw;
            }
        }
    }
}
