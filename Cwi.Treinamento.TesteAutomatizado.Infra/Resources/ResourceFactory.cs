using Microsoft.Extensions.Localization;
using System.Collections.Generic;
using System.Linq;

namespace Cwi.Treinamento.TesteAutomatizado.Infra.Resources
{
    /// <summary>
    /// Define a classe ResourceFactory.
    /// </summary>
    public static class ResourceFactory
    {
        private static string _asm;

        public static void SetAssembly(string asm)
        {
            _asm = asm;
        }

        /// <summary>
        /// Obtém ou Define Factory.
        /// </summary>
        public static IStringLocalizerFactory Factory { get; set; }

        private static Dictionary<string, IStringLocalizer> localizers = new Dictionary<string, IStringLocalizer>();

        /// <summary>
        /// Inicializa uma nova instancia de IStringLocalizer.
        /// </summary>
        /// <param name="resource"></param>
        /// <returns></returns>
        public static IStringLocalizer Build(string resource)
        {
            if (!localizers.Any(k => k.Key == resource))
                localizers[resource] = Factory.Create(resource, _asm);

            return localizers[resource];
        }
    }
}
