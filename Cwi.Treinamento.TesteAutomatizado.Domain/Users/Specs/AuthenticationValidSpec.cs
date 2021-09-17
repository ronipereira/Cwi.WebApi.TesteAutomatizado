using Cwi.Treinamento.TesteAutomatizado.Domain.Globalization;
using Cwi.Treinamento.TesteAutomatizado.Domain.Users.Authenticate;
using Cwi.Treinamento.TesteAutomatizado.Infra.Extensions;
using Cwi.Treinamento.TesteAutomatizado.Infra.Validation;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;

namespace Cwi.Treinamento.TesteAutomatizado.Domain.Users.Specs
{
    public class AuthenticationValidSpec : BaseSpec<UserAuthenticationCommand>
    {
        private readonly List<User> users;

        public AuthenticationValidSpec(List<User> users)
        {
            this.users = users;
        }

        public override bool IsSatisfiedBy(UserAuthenticationCommand entity)
        {
            var user = users?.FirstOrDefault(u => u.Username == entity.Username);

            if (user == null)
            {
                NotSatisfiedCode = GlobalizationConstants.UsuarioNaoEncontradoSpec;
                NotSatisfiedReason = GlobalizationConstants.UsuarioNaoEncontradoSpec.Resource();
                return false;
            }

            NotSatisfiedCode = GlobalizationConstants.UsuarioSenhaInvalidoSpec;
            NotSatisfiedReason = GlobalizationConstants.UsuarioSenhaInvalidoSpec.Resource();
            return new PasswordHasher<string>().VerifyHashedPassword(user.Username, user.Password, entity.Password) == PasswordVerificationResult.Success;
        }
    }
}
