using Cwi.Treinamento.TesteAutomatizado.Infra.Paging;

namespace Cwi.Treinamento.TesteAutomatizado.Domain.Employees.GetEmployee.GetByPage
{
    /// <summary>
    /// Comando usado para obtenção de funcionários com paginação.
    /// </summary>
    /// <seealso cref="Cwi.Treinamento.TesteAutomatizado.Infra.Paging.FilteredCommand{Cwi.Treinamento.TesteAutomatizado.Domain.Employees.GetEmployee.GetByPage.GetEmployeeByPageCommandFilter, Cwi.Treinamento.TesteAutomatizado.Domain.Employees.GetEmployee.GetByPage.GetEmployeeByPageCommandResult}" />
    public class GetEmployeeByPageCommand : FilteredCommand<GetEmployeeByPageCommandFilter, GetEmployeeByPageCommandResult>
    {
    }
}
