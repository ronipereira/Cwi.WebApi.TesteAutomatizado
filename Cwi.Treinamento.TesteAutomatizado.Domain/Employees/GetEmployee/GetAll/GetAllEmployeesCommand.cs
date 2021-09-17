using System.Collections.Generic;
using Cwi.Treinamento.TesteAutomatizado.Infra.Messaging;

namespace Cwi.Treinamento.TesteAutomatizado.Domain.Employees.GetEmployee.GetAll
{
    /// <summary>
    /// Comando usado para obtenção de funcionários.
    /// </summary>
    /// <seealso cref="Cwi.Treinamento.TesteAutomatizado.Infra.Messaging.Command{System.Collections.Generic.IEnumerable{Cwi.Treinamento.TesteAutomatizado.Domain.Employees.GetEmployee.GetAll.GetAllEmployeesCommandResult}}" />
    public class GetAllEmployeesCommand : Command<IEnumerable<GetAllEmployeesCommandResult>>
    {
    }
}
