using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Tms.ApplicationCore.Entities;

namespace Tms.ApplicationCore.Interfaces
{
	public interface IRepository<T> where T : BaseEntity
	{
		T GetById(int id);
		T GetByExpr(Expression<Func<T, bool>> expression);
		IReadOnlyList<T> ListAll();
		IReadOnlyList<T> List(Expression<Func<T, bool>> expression);
		T Add(T entity);
		IEnumerable<T> Add(IEnumerable<T> entities);
		void Update(T entity);
		void Delete(T entity);
		void Delete(IEnumerable<T> entities);
		int Count(Expression<Func<T, bool>> expression);
	}
}
