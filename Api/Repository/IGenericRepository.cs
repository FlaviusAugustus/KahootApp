﻿using System.Linq.Expressions;
using KahootBackend.Models;

namespace KahootBackend.Repository;
public interface IGenericRepository<TEntity> 
    where TEntity : class, IEntity
{
    void Add(TEntity entity);

    Task AddRange(IEnumerable<TEntity> entities);
    void Update(TEntity entity);
    void Remove(TEntity entity);
    Task RemoveByIdAsync(Guid id);
    Task SaveAsync();
    Task<TEntity?> GetByIdAsync(Guid id);
    Task<IEnumerable<TEntity>> GetAllAsync();
    Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> expression);

    Task<IEnumerable<TEntity>> GetPage(int pageSize, int pageNumber);
    Task<IEnumerable<TEntity>> GetVirtualize(int startIndex, int count);

    Task<int> GetItemCount();
}