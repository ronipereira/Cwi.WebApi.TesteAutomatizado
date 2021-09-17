using FluentValidation;
using FluentValidation.Results;
using System.Linq;

namespace Cwi.Treinamento.TesteAutomatizado.Infra.Validation
{
    public abstract class BaseSpec<T> : AbstractValidator<T>
    {
        public string NotSatisfiedCode { get; set; }

        public string NotSatisfiedReason { get; set; }

        /// <summary>
        /// Valida todos os erros de domínio.
        /// </summary>
        /// <param name="context">O contexto da validação</param>
        /// <returns>O resultado da validação</returns>
        public override ValidationResult Validate(ValidationContext<T> context)
        {
            var validationResult = new ValidationResult();

            if (context.InstanceToValidate != null)
            {
                validationResult = base.Validate(context);
            }

            if (!validationResult.Errors.Any() && !this.IsSatisfiedBy(context.InstanceToValidate))
            {
                validationResult.Errors.Add(new ValidationFailure(string.Empty, NotSatisfiedReason) { ErrorCode = NotSatisfiedCode });
            }

            return validationResult;
        }

        public virtual bool IsSatisfiedBy(T entity)
        {
            return true;
        }
    }
}