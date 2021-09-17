using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Cwi.Treinamento.TesteAutomatizado.Infra.Messaging
{
    /// <summary>
    /// Define a classe base para resposta de um comando.
    /// </summary>
    /// <typeparam name="TResult">O tipo de resultado.</typeparam>
    public class CommandResponse<TResult>
    {
        /// <summary>
        /// Inicia uma nova instância da classe <see cref="CommandResponse{TResult}"/>
        /// </summary>
        /// <param name="result">O resultado retornado pelo comando.</param>
        /// <param name="validationResult">A lista de erros de validação.</param>
        public CommandResponse(TResult result, ValidationResult validationResult)
        {
            this.Result = result;

            this.Errors = validationResult == null ?
              Array.Empty<ValidationFailure>() :
              validationResult.Errors.ToArray();
        }

        /// <summary>
        /// Obtém um valor indicando se o comando está válido.
        /// </summary>
        public bool IsValid => !Errors.Any();

        /// <summary>
        /// Obtém o resultado retornado pelo comando.
        /// </summary>
        public TResult Result { get; private set; }

        /// <summary>
        /// Obtém a lista de erros de validação.
        /// </summary>
        public IEnumerable<ValidationFailure> Errors { get; private set; }
    }
}