using System.Collections.Generic;
using Cwi.Treinamento.TesteAutomatizado.Infra.Messaging;

namespace Cwi.Treinamento.TesteAutomatizado.Domain.Companies.GetCompany.GetAll
{
    /// <summary>
    /// Comando usado para obtenção de empresas.
    /// </summary>
    /// <seealso cref="Cwi.Treinamento.TesteAutomatizado.Infra.Messaging.Command{System.Collections.Generic.IEnumerable{Cwi.Treinamento.TesteAutomatizado.Domain.Companies.GetCompany.GetAll.GetAllCompaniesCommandResult}}" />
    public class GetAllCompaniesCommand : Command<IEnumerable<GetAllCompaniesCommandResult>>
    {
    }
}
