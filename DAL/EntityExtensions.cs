using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DAL.EntityModels;

namespace DAL
{
    public static class EntityExtensions
    {

        public static IQueryable<T> IncludeRange<T>(this DbSet<T> query, params Expression<Func<T, object>>[] includes) where T : class
        {
            IQueryable<T> result = includes
                .Aggregate(
                    query.AsQueryable(),
                    (current, include) => current.Include(include)
                );
            return result;
        }
        
    }
}