using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {

        IEnumerable<TEntity> GetEntities();
        TEntity GetEntityById(int entityId);
        TEntity AddEntity(TEntity entity);
        void UpdateEntity(TEntity entity);
        void DeleteEntity(int entityId);
    }
}
