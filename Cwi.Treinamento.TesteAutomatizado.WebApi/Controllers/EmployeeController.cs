using Cwi.Treinamento.TesteAutomatizado.Domain.Employees.DeleteEmployee;
using Cwi.Treinamento.TesteAutomatizado.Domain.Employees.EditEmployee;
using Cwi.Treinamento.TesteAutomatizado.Domain.Employees.GetEmployee.GetAll;
using Cwi.Treinamento.TesteAutomatizado.Domain.Employees.GetEmployee.GetById;
using Cwi.Treinamento.TesteAutomatizado.Domain.Employees.GetEmployee.GetByPage;
using Cwi.Treinamento.TesteAutomatizado.Domain.Employees.CreateEmployee;
using Cwi.Treinamento.TesteAutomatizado.Infra.Mediator;
using Cwi.Treinamento.TesteAutomatizado.Infra.Paging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Cwi.Treinamento.TesteAutomatizado.WebApi.Controllers
{
    /// <summary>
    /// Define a classe EmployeeController.
    /// </summary>
    [ApiVersion("1")]
    [Route("v{version:apiVersion}/employees")]
    [ApiController]
    [Authorize]
    public class EmployeeController : ApiController
    {
        private readonly IMediatorHandler mediator;

        /// <summary>
        /// Inicia uma nova instância da classe <see cref="EmployeeController"/>.
        /// </summary>
        /// <param name="mediator">O mediador.</param>
        public EmployeeController(IMediatorHandler mediator)
        {
            this.mediator = mediator;
        }

        /// <summary>
        /// Obtém os funcionários.
        /// </summary>
        /// <returns>Os funcionários.</returns>
        /// <response code="200">Se algum funcionário foi encontrado.</response>
        /// <response code="204">Se nenhum funcionário não foi encontrado.</response>
        /// <response code="401">Usuário não autenticado.</response>
        /// <response code="403">Usuário não autorizado.</response>
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(typeof(GetAllEmployeesCommandResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var response = await mediator.SendCommand(new GetAllEmployeesCommand()).ConfigureAwait(false);

            return CustomResponse(response);
        }

        /// <summary>
        /// Obtém os funcionários com paginação.
        /// </summary>
        /// <returns>Os funcionários.</returns>
        /// <response code="200">Se algum funcionário foi encontrado.</response>
        /// <response code="204">Se nenhum funcionário não foi encontrado.</response>
        /// <response code="401">Usuário não autenticado.</response>
        /// <response code="403">Usuário não autorizado.</response>
        [HttpGet("paged")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(PagedResult<GetAllEmployeesCommandResult>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetByPage([FromQuery] GetEmployeeByPageCommand getEmployeeByPageCommand)
        {
            var response = await mediator.SendCommand(getEmployeeByPageCommand).ConfigureAwait(false);

            return CustomResponse(response);
        }

        /// <summary>
        /// Obtém o funcionário pelo identificador.
        /// </summary>
        /// <param name="id">O identificador do funcionário.</param>
        /// <returns>O funcionário.</returns>
        /// <response code="200">Se o funcionário foi encontrado.</response>
        /// <response code="401">Usuário não autenticado.</response>
        /// <response code="403">Usuário não autorizado.</response>
        /// <response code="404">Se o funcionário não foi encontrado.</response>
        [HttpGet]
        [Route("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(GetEmployeeByIdCommandResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetById(long id)
        {
            var response = await mediator.SendCommand(new GetEmployeeByIdCommand(id)).ConfigureAwait(false);

            return CustomResponse(response);
        }

        /// <summary>
        /// Cadastra o funcionário.
        /// </summary>
        /// <param name="createEmployeeCommand">O comando para cadastro do funcionário.</param>
        /// <returns>O resultado da operação.</returns>
        /// <response code="201">Se o funcionário foi cadastrado.</response>
        /// <response code="400">Se o formato dos dados não está adequado ou as regras de negócio não foram satisfeitas.</response>
        /// <response code="401">Usuário não autenticado.</response>
        /// <response code="403">Usuário não autorizado.</response>
        [HttpPost]
        public async Task<IActionResult> Create(CreateEmployeeCommand createEmployeeCommand)
        {
            var response = await mediator.SendCommand(createEmployeeCommand).ConfigureAwait(false);

            return CustomCreatedResponse(response, (result) => $"/employee/{result}");
        }

        /// <summary>
        /// Atualiza o funcionário.
        /// </summary>
        /// <param name="id">O identificador do funcionário.</param>
        /// <param name="editEmployeeCommand">O comando para edição do funcionário.</param>
        /// <returns>O resultado da operação.</returns>
        /// <response code="200">Se o funcionário foi atualizado.</response>
        /// <response code="400">Se o formato dos dados não está adequado ou as regras de negócio não foram satisfeitas.</response>
        /// <response code="401">Usuário não autenticado.</response>
        /// <response code="403">Usuário não autorizado.</response>
        /// <response code="404">Se o funcionário não foi encontrado.</response>
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Edit(long id, [FromBody] EditEmployeeCommand editEmployeeCommand)
        {
            editEmployeeCommand.Id = id;

            var response = await mediator.SendCommand(editEmployeeCommand).ConfigureAwait(false);

            return CustomResponse(response);
        }

        /// <summary>
        /// Exclui o funcionário.
        /// </summary>
        /// <param name="id">O identificador do funcionário.</param>
        /// <returns>O resultado da operação.</returns>
        /// <response code="200">Se o funcionário foi excluído.</response>
        /// <response code="401">Usuário não autenticado.</response>
        /// <response code="403">Usuário não autorizado.</response>
        /// <response code="404">Se o funcionário não foi encontrado.</response>
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var response = await mediator.SendCommand(new DeleteEmployeeCommand(id)).ConfigureAwait(false);

            return CustomResponse(response);
        }

    }
}
