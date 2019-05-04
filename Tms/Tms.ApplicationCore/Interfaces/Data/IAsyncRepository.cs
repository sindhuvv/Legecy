using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Tms.ApplicationCore.Entities;

namespace Tms.ApplicationCore.Interfaces
{
	public interface IAsyncRepository<T> where T : BaseEntity
	{
		Task<T> GetByIdAsync(int id);
		Task<T> GetByExprAsync(Expression<Func<T, bool>> expression);
		Task<IReadOnlyList<T>> ListAllAsync();
		Task<IReadOnlyList<T>> ListAsync(Expression<Func<T, bool>> expression);
		Task<T> AddAsync(T entity);
		Task<IEnumerable<T>> AddAsync(IEnumerable<T> entities);
		Task UpdateAsync(T entity);
		Task DeleteAsync(T entity);
		Task DeleteAsync(IEnumerable<T> entities);
		Task<int> CountAsync(Expression<Func<T, bool>> expression);
	}
}
