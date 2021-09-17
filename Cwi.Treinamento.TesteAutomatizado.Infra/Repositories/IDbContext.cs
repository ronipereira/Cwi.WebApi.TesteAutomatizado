
using System.Data;

namespace Cwi.Treinamento.TesteAutomatizado.Infra.Repositories
{
    public interface IDbContext
    {
        IDbConnection GetConnection();
    }
}
