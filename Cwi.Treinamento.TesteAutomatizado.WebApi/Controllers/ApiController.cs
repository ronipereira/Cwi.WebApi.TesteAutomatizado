using Cwi.Treinamento.TesteAutomatizado.Infra.Messaging;
using Cwi.Treinamento.TesteAutomatizado.Domain.Globalization;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Cwi.Treinamento.TesteAutomatizado.WebApi.Controllers
{
    /// <summary>
    /// Define uma classe base para controllers.
    /// </summary>
    public class ApiController : ControllerBase
    {
        /// <summary>
        /// Cria uma resposta HTTP traduzindo a resposta de um comando.
        /// </summary>
        /// <typeparam name="TResult">A resposta do comando.</typeparam>
        /// <param name="response">O tipo de resposta do comando.</param>
        /// <returns>Retorna uma resposta HTTP.</returns>
        protected ActionResult CustomResponse<TResult>(CommandResponse<TResult> response)
        {
            if (response == null)
            {
                return Ok();
            }

            if (!response.IsValid)
            {
                return InvalidResponse(response);
            }

            if (object.Equals(response.Result, default(TResult)))
            {
                return Ok();
            }

            if (IsResultAnEmptyList(response))
            {
                return NoContent();
            }

            return Ok(response.Result);
        }

        /// <summary>
        /// Cria uma resposta HTTP do tipo Created a partir de uma resposta de um comando.
        /// </summary>
        /// <typeparam name="TResult">O tipo de resposta do comando.</typeparam>
        /// <param name="response">A resposta do comando.</param>
        /// <param name="getUri">A URL para consulta do recurso criado.</param>
        /// <returns>Retorna uma resposta HTTP.</returns>
        protected ActionResult CustomCreatedResponse<TResult>(CommandResponse<TResult> response, Func<TResult, string> getUri)
        {
            if (response == null) throw new ArgumentNullException(nameof(response));
            if (getUri == null) throw new ArgumentNullException(nameof(getUri));

            if (!response.IsValid)
            {
                return InvalidResponse(response);
            }

            return Created(getUri(response.Result), response.Result);
        }

        private static bool IsResultAnEmptyList<TResult>(CommandResponse<TResult> response)
        {
            var list = response.Result as IEnumerable;
            return list != null && !list.Cast<object>().Any();
        }

        private ActionResult InvalidResponse<TResult>(CommandResponse<TResult> response)
        {
            if (response.Errors.Any(e => string.Equals(e.ErrorCode, GlobalizationConstants.RegistroNaoEncontrado)))
            {
                return NotFound();
            }

            var errors = FormatErrorMessages(response.Errors);

            return BadRequest(new ValidationProblemDetails
            (
                new Dictionary<string, string[]>
                {
                    {
                      "Messages", errors
                    }
                })
            );
        }

        private static string[] FormatErrorMessages(IEnumerable<ValidationFailure> errors)
        {
            return errors
              .Select(e => string.IsNullOrWhiteSpace(e.ErrorCode) ? e.ErrorMessage : $"{e.ErrorCode} - {e.ErrorMessage}")
              .ToArray();
        }
    }
}