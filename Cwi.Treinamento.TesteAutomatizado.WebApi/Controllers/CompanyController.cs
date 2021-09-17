using Cwi.Treinamento.TesteAutomatizado.Domain.Companies.CreateCompany;
using Cwi.Treinamento.TesteAutomatizado.Domain.Companies.DeleteCompany;
using Cwi.Treinamento.TesteAutomatizado.Domain.Companies.EditCompany;
using Cwi.Treinamento.TesteAutomatizado.Domain.Companies.FireEmployee;
using Cwi.Treinamento.TesteAutomatizado.Domain.Companies.GetCompany.GetAll;
using Cwi.Treinamento.TesteAutomatizado.Domain.Companies.GetCompany.GetById;
using Cwi.Treinamento.TesteAutomatizado.Domain.Companies.GetCompany.GetByPage;
using Cwi.Treinamento.TesteAutomatizado.Domain.Companies.HireEmployee;
using Cwi.Treinamento.TesteAutomatizado.Infra.Mediator;
using Cwi.Treinamento.TesteAutomatizado.Infra.Paging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Cwi.Treinamento.TesteAutomatizado.WebApi.Controllers
{
    /// <summary>
    /// Define a classe CompanyController.
    /// </summary>
    [ApiVersion("1")]
    [Route("v{version:apiVersion}/companies")]
    [ApiController]
    [Authorize]
    public class CompanyController : ApiController
    {
        private readonly IMediatorHandler mediator;

        /// <summary>
        /// Inicia uma nova instância da classe <see cref="CompanyController"/>.
        /// </summary>
        /// <param name="mediator">O mediador.</param>
        public CompanyController(IMediatorHandler mediator)
        {
            this.mediator = mediator;
        }

        /// <summary>
        /// Obtém as empresas.
        /// </summary>
        /// <returns>As empresas.</returns>
        /// <response code="200">Se alguma empresa foi encontrada.</response>
        /// <response code="204">Se nenhuma empresa não foi encontrada.</response>
        /// <response code="401">Usuário não autenticado.</response>
        /// <response code="403">Usuário não autorizado.</response>
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(typeof(GetAllCompaniesCommandResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var response = await mediator.SendCommand(new GetAllCompaniesCommand()).ConfigureAwait(false);

            return CustomResponse(response);
        }

        /// <summary>
        /// Obtém as empresas com paginação.
        /// </summary>
        /// <returns>As empresas.</returns>
        /// <response code="200">Se alguma empresa foi encontrada.</response>
        /// <response code="204">Se nenhuma empresa não foi encontrada.</response>
        /// <response code="401">Usuário não autenticado.</response>
        /// <response code="403">Usuário não autorizado.</response>
        [HttpGet("paged")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(PagedResult<GetCompanyByPageCommandResult>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetByPage([FromQuery] GetCompanyByPageCommand getCompanyByPageCommand)
        {
            var response = await mediator.SendCommand(getCompanyByPageCommand).ConfigureAwait(false);

            return CustomResponse(response);
        }

        /// <summary>
        /// Obtém a empresa pelo identificador.
        /// </summary>
        /// <param name="id">O identificador da empresa.</param>
        /// <returns>A empresa.</returns>
        /// <response code="200">Se a empresa foi encontrada.</response>
        /// <response code="401">Usuário não autenticado.</response>
        /// <response code="403">Usuário não autorizado.</response>
        /// <response code="404">Se a empresa não foi encontrada.</response>
        [HttpGet]
        [Route("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(GetCompanyByIdCommandResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetById(long id)
        {
            var response = await mediator.SendCommand(new GetCompanyByIdCommand(id)).ConfigureAwait(false);

            return CustomResponse(response);
        }

        /// <summary>
        /// Cadastra a empresa.
        /// </summary>
        /// <param name="createCompanyCommand">O comando para cadastro da empresa.</param>
        /// <returns>O resultado da operação.</returns>
        /// <response code="201">Se a empresa foi cadastrada.</response>
        /// <response code="400">Se o formato dos dados não está adequado ou as regras de negócio não foram satisfeitas.</response>
        /// <response code="401">Usuário não autenticado.</response>
        /// <response code="403">Usuário não autorizado.</response>
        [HttpPost]
        public async Task<IActionResult> Create(CreateCompanyCommand createCompanyCommand)
        {
            var response = await mediator.SendCommand(createCompanyCommand).ConfigureAwait(false);

            return CustomCreatedResponse(response, (result) => $"/company/{result}");
        }

        /// <summary>
        /// Contrata o funcionário.
        /// </summary>
        /// <param name="id">O identificador da empresa.</param>
        /// <param name="idEmployee">O identificador do funcionário.</param>
        /// <returns>O resultado da operação.</returns>
        /// <response code="200">Se o funcionário foi contratado pela empresa.</response>
        /// <response code="400">Se o formato dos dados não está adequado ou as regras de negócio não foram satisfeitas.</response>
        /// <response code="401">Usuário não autenticado.</response>
        /// <response code="403">Usuário não autorizado.</response>
        /// <response code="404">Se a empresa ou o funcionário não foram encontrados.</response>
        [HttpPost]
        [Route("{id}/hire/{idEmployee}")]
        public async Task<IActionResult> HireEmployee(long id, long idEmployee)
        {
            var response = await mediator.SendCommand(new HireEmployeeCommand(id, idEmployee)).ConfigureAwait(false);

            return CustomResponse(response);
        }

        /// <summary>
        /// Demite o funcionário.
        /// </summary>
        /// <param name="id">O identificador da empresa.</param>
        /// <param name="idEmployee">O identificador do funcionário.</param>
        /// <returns>O resultado da operação.</returns>
        /// <response code="200">Se o funcionário foi demitido pela empresa.</response>
        /// <response code="400">Se o formato dos dados não está adequado ou as regras de negócio não foram satisfeitas.</response>
        /// <response code="401">Usuário não autenticado.</response>
        /// <response code="403">Usuário não autorizado.</response>
        /// <response code="404">Se a empresa ou o funcionário não foram encontrados.</response>
        [HttpPost]
        [Route("{id}/fire/{idEmployee}")]
        public async Task<IActionResult> FireEmployee(long id, long idEmployee)
        {
            var response = await mediator.SendCommand(new FireEmployeeCommand(id, idEmployee)).ConfigureAwait(false);

            return CustomResponse(response);
        }

        /// <summary>
        /// Atualiza a empresa.
        /// </summary>
        /// <param name="id">O identificador da empresa.</param>
        /// <param name="editCompanyCommand">O comando para edição da empresa.</param>
        /// <returns>O resultado da operação.</returns>
        /// <response code="200">Se a empresa foi atualizada.</response>
        /// <response code="400">Se o formato dos dados não está adequado ou as regras de negócio não foram satisfeitas.</response>
        /// <response code="401">Usuário não autenticado.</response>
        /// <response code="403">Usuário não autorizado.</response>
        /// <response code="404">Se a empresa não foi encontrada.</response>
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Edit(long id, [FromBody] EditCompanyCommand editCompanyCommand)
        {
            editCompanyCommand.Id = id;

            var response = await mediator.SendCommand(editCompanyCommand).ConfigureAwait(false);

            return CustomResponse(response);
        }

        /// <summary>
        /// Exclui a empresa.
        /// </summary>
        /// <param name="id">O identificador da empresa.</param>
        /// <returns>O resultado da operação.</returns>
        /// <response code="200">Se a empresa foi excluída.</response>
        /// <response code="401">Usuário não autenticado.</response>
        /// <response code="403">Usuário não autorizado.</response>
        /// <response code="404">Se a empresa não foi encontrada.</response>
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var response = await mediator.SendCommand(new DeleteCompanyCommand(id)).ConfigureAwait(false);

            return CustomResponse(response);
        }

    }
}
