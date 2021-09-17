using System.Threading.Tasks;

namespace Cwi.Treinamento.TesteAutomatizado.Infra.Repositories
{
	public interface IRepository<TEntity>
	{
		/// <summary>
		/// Finds the entity by the key.
		/// </summary>
		/// <returns>The found entity.</returns>
		/// <param name="key">Key.</param>
		Task<TEntity> FindByAsync(object key);

		/// <summary>
		/// Insert the specified entity.
		/// </summary>
		/// <param name="item">The entity.</param>
		Task<long> InsertAsync(TEntity item);

		/// <summary>
		/// Update the specified entity.
		/// </summary>
		/// <param name="item">The entity.</param>
		Task<int> UpdateAsync(TEntity item);

		/// <summary>
		/// Remove the specified entity.
		/// </summary>
		/// <param name="item">The entity.</param>
		Task RemoveAsync(TEntity item);
	}
}
