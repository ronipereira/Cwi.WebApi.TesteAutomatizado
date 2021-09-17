using System.Threading.Tasks;

namespace Cwi.Treinamento.TesteAutomatizado.Infra.Repositories
{
	public abstract class RepositoryBase<TEntity, TType> : IRepository<TEntity>
    {
		private IUnitOfWork<TType> unitOfWork;

		/// <summary>
		/// Initializes a new instance of the Entity class.
		/// </summary>
		/// <param name="unitOfWork">Unit of work.</param>
		protected RepositoryBase(IUnitOfWork<TType> unitOfWork)
		{
			this.unitOfWork = unitOfWork;
		}

		/// <summary>
		/// Sets the unit of work.
		/// </summary>
		/// <param name="unitOfWork">Unit of work.</param>
		public virtual void SetUnitOfWork(IUnitOfWork<TType> unitOfWork)
		{
			this.unitOfWork = unitOfWork;
		}

		/// <summary>
		/// Finds an entity by the key.
		/// </summary>
		/// <returns>The entity.</returns>
		/// <param name="key">Key.</param>
		public abstract Task<TEntity> FindByAsync(object key);

		/// <summary>
		/// Insert the specified item.
		/// </summary>
		/// <param name="item">Item.</param>
		public abstract Task<long> InsertAsync(TEntity item);

		/// <summary>
		/// Update the specified item.
		/// </summary>
		/// <param name="item">Item.</param>
		public abstract Task<int> UpdateAsync(TEntity item);

		/// <summary>
		/// Remove the specified entity.
		/// </summary>
		/// <param name="item">The entity.</param>
		public abstract Task RemoveAsync(TEntity item);
	}
}
