using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Layers.Utilities.Extensions
{
    public static class EntityFrameWorkExtensions
    {
        public static IQueryable<TEntity> IncludeAll<TEntity, TId>(this DbSet<TEntity> dbset, params Expression<Func<TEntity, object>>[] includes) where TEntity : class
        {
            IQueryable<TEntity> query = dbset;

            if (dbset != null && includes != null)
            {
                foreach (var include in includes.WhereIsNotNull())
                {
                    query = query.Include(include);
                }
            }

            return query;
        }
    }
}
