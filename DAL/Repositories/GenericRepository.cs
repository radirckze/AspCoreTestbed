using DAL.Interfaces;
using DAL.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DAL.Repositories
{
    class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        internal MovieBuffContext context;
        internal DbSet<TEntity> dbSet;

        public GenericRepository(MovieBuffContext context)
        {
            this.context = context;
            this.dbSet = context.Set<TEntity>();
        }

        public virtual IEnumerable<TEntity> GetEntities()
        {
            return dbSet.AsEnumerable();
        }

        public virtual TEntity GetEntityById(int entityId)
        {
            return dbSet.Find(entityId);
        }

        public virtual TEntity AddEntity(TEntity entity)
        {
            dbSet.Add(entity);
            return entity;
        }

        public virtual void DeleteEntity(int entityId)
        {
            TEntity entityToDelete = dbSet.Find(entityId);
            if(context.Entry(entityToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }
            dbSet.Remove(entityToDelete);
        }

        public virtual void UpdateEntity(TEntity entityToUpdate)
        {
            dbSet.Attach(entityToUpdate);
            context.Entry(entityToUpdate).State = EntityState.Modified;
        }
    }
}
