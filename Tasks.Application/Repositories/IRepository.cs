using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Tasks.Domain.Entities;

namespace Tasks.Application.Repositories
{
    public interface IRepository<TEntity> where TEntity : EntityBase
    {
        void Delete(TEntity entityToDelete);

        void DeleteByExpression(Expression<Func<TEntity, bool>> expression);
        void Delete(object id);
        IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "");
        TEntity GetByID(object id);
        IEnumerable<TEntity> GetWithRawSql(string query,
            params object[] parameters);
        void Insert(TEntity entity);
        void Update(TEntity entityToUpdate);

        bool IsInserted(TEntity entity);
    }
}
