using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Contracts;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    public static class SpacificationEvaluator
    {
        public static IQueryable<TEntity> CreateQuery<TEntity, Tkey>(IQueryable<TEntity> IputQuery, ISpecifications<TEntity, Tkey> spec)
           where TEntity : ModelBase<Tkey>
        {
            var query = IputQuery;
            if (spec.Criteria != null)
                query = query.Where(spec.Criteria);
            if (spec.IncludeExpressions != null && spec.IncludeExpressions.Count > 0)
                query = spec.IncludeExpressions.Aggregate(query, (currentQuery, Exp) => currentQuery.Include(Exp));
            return query;
        }
    }
}
