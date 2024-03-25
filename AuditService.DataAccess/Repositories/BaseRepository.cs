﻿using AuditService.DataAccess.DatabaseContexts;
using AuditService.DataAccess.Repositories.Interfaces;

namespace AuditService.DataAccess.Repositories;

internal class BaseRepository<TEntity> : IRepositoryBase<TEntity> where TEntity : class
{
    public IUnitOfWork UnitOfWork => Context;

    protected readonly AuditDbContext Context;

    protected BaseRepository(AuditDbContext context)
    {
        Context = context;
    }

    public virtual TEntity Add(TEntity entity)
    {
        return Context.Set<TEntity>().Add(entity).Entity;
    }

    public virtual void AddRange(ICollection<TEntity> entities)
    {
        Context.Set<TEntity>().AddRange(entities);
    }

    public virtual void Delete(TEntity entity)
    {
        Context.Set<TEntity>().Remove(entity);
    }
    
    public virtual void DeleteRange(ICollection<TEntity> entities)
    {
        Context.Set<TEntity>().RemoveRange(entities);
    }
    
    public virtual void Update(TEntity entity)
    {
        Context.Set<TEntity>().Update(entity);
    }
    
    public virtual void Update(ICollection<TEntity> entities)
    {
        Context.Set<TEntity>().UpdateRange(entities);
    }
}
