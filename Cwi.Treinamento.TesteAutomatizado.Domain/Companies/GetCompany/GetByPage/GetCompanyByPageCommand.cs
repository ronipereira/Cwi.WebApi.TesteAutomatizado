using Cwi.Treinamento.TesteAutomatizado.Infra.Paging;

namespace Cwi.Treinamento.TesteAutomatizado.Domain.Companies.GetCompany.GetByPage
{
    /// <summary>
    /// Comando usado para obtenção de empresas com paginação.
    /// </summary>
    /// <seealso cref="Cwi.Treinamento.TesteAutomatizado.Infra.Paging.FilteredCommand{Cwi.Treinamento.TesteAutomatizado.Domain.Companies.GetCompany.GetByPage.GetCompanyByPageCommandFilter, Cwi.Treinamento.TesteAutomatizado.Domain.Companies.GetCompany.GetByPage.GetCompanyByPageCommandResult}" />
    public class GetCompanyByPageCommand : FilteredCommand<GetCompanyByPageCommandFilter, GetCompanyByPageCommandResult>
    {
    }
}
