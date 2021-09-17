using Cwi.Treinamento.TesteAutomatizado.Infra.Resources;
using System;

namespace Cwi.Treinamento.TesteAutomatizado.Infra.Extensions
{
    /// <summary>
    /// Define a classe StringExtensions.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Obtém a mensagem personalizada do arquivo de globalization.
        /// </summary>
        /// <param name="chave">Chave da mensagem.</param>
        /// <param name="args">Argumentos usados para formatação da mensagem.</param>
        /// <returns>
        /// A mensagem correspondente a chave encontra no arquivo de globalization.
        /// </returns>
        /// <exception cref="System.Exception">Globalization Failed!. {chave} not found.</exception>
        public static string Resource(this string chave, params object[] args)
        {
            var resource = ResourceFactory.Build("Globalization")[chave];

            if (resource.ResourceNotFound)
                throw new Exception($"Globalization Failed!. {chave} not found.");

            return args == null || args.Length == 0 ? resource.Value : string.Format(resource.Value, args);
        }

    }
}
