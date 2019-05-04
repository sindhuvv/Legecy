using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Tms.ApplicationCore.Entities;
using Tms.ApplicationCore.Interfaces;

namespace Tms.Infrastructure.Data
{
	public class BaseRepository<T> : IRepository<T>, IAsyncRepository<T> where T : BaseEntity
	{
		public TmsContext _dbContext;
		public ITmsDapper _dapper;

		public BaseRepository(TmsContext baseDbContext)
		{
			_dbContext = baseDbContext;
		}

		public BaseRepository(TmsContext baseDbContext, ITmsDapper dapper)
		{
			_dbContext = baseDbContext;
			_dapper = dapper;
		}

		public T GetById(int id)
		{
			return _dbContext.Set<T>().Find(id);
		}

		public async Task<T> GetByIdAsync(int id)
		{
			return await _dbContext.Set<T>().FindAsync(id);
		}

		public T GetByExpr(Expression<Func<T, bool>> expression)
		{
			return _dbContext.Set<T>().Find(expression);
		}

		public async Task<T> GetByExprAsync(Expression<Func<T, bool>> expression)
		{
			return await _dbContext.Set<T>().FindAsync(expression);
		}

		public IReadOnlyList<T> ListAll()
		{
			return _dbContext.Set<T>().ToList();
		}

		public async Task<IReadOnlyList<T>> ListAllAsync()
		{
			return await _dbContext.Set<T>().ToListAsync();
		}

		public IReadOnlyList<T> List(Expression<Func<T, bool>> expression)
		{
			return _dbContext.Set<T>().Where(expression).ToList();
		}

		public async Task<IReadOnlyList<T>> ListAsync(Expression<Func<T, bool>> expression)
		{
			return await _dbContext.Set<T>().Where(expression).ToListAsync();
		}

		public T Add(T entity)
		{
			_dbContext.Set<T>().Add(entity);
			_dbContext.SaveChanges();

			return entity;
		}

		public async Task<T> AddAsync(T entity)
		{
			await _dbContext.Set<T>().AddAsync(entity);
			await _dbContext.SaveChangesAsync(true);

			return entity;
		}

		public IEnumerable<T> Add(IEnumerable<T> entities)
		{
			foreach (var entity in entities)
				_dbContext.Set<T>().Add(entity);
			_dbContext.SaveChanges();
			return entities;
		}

		public async Task<IEnumerable<T>> AddAsync(IEnumerable<T> entities)
		{
			foreach (var entity in entities)
				await _dbContext.Set<T>().AddAsync(entity);
			await _dbContext.SaveChangesAsync(true);
			return entities;
		}

		public void Update(T entity)
		{
			_dbContext.Entry(entity).State = EntityState.Modified;
			_dbContext.SaveChanges();
		}

		public async Task UpdateAsync(T entity)
		{
			_dbContext.Entry(entity).State = EntityState.Modified;
			await _dbContext.SaveChangesAsync(true);
		}

		public void Delete(T entity)
		{
			_dbContext.Set<T>().Remove(entity);
			_dbContext.SaveChanges();
		}

		public async Task DeleteAsync(T entity)
		{
			_dbContext.Set<T>().Remove(entity);
			await _dbContext.SaveChangesAsync(true);
		}

		public void Delete(IEnumerable<T> entities)
		{
			foreach (var entity in entities)
				_dbContext.Set<T>().Remove(entity);
			_dbContext.SaveChanges();
		}

		public async Task DeleteAsync(IEnumerable<T> entities)
		{
			foreach (var entity in entities)
				_dbContext.Set<T>().Remove(entity);
			await _dbContext.SaveChangesAsync();
		}

		public int Count(Expression<Func<T, bool>> expression)
		{
			return _dbContext.Set<T>().Where(expression).Count();
		}

		public async Task<int> CountAsync(Expression<Func<T, bool>> expression)
		{
			return await _dbContext.Set<T>().Where(expression).CountAsync();
		}
	}
}
