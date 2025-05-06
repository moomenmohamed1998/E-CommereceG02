using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Domain.Contracts;
using Domain.Models;

namespace Sevices.Specifications
{
    public abstract class BaseISpecifications<TEntity, Tkey> : ISpecifications<TEntity, Tkey>
          where TEntity : ModelBase<Tkey>
    {
        public BaseISpecifications(Expression<Func<TEntity, bool>>? PassedSpecifications)
        {
            Criteria = PassedSpecifications;
        }
        public Expression<Func<TEntity, bool>>? Criteria { get; private set; }

        public List<Expression<Func<TEntity, object>>> IncludeExpressions { get; } = new List<Expression<Func<TEntity, object>>>();

        //public Expression<Func<TEntity, object>>? OrderBy => throw new NotImplementedException();

        //public Expression<Func<TEntity, object>>? OrderByDescending => throw new NotImplementedException();

        public void AddInclude(Expression<Func<TEntity, object>> includeExpression)
        {
            IncludeExpressions.Add(includeExpression);
        }
        public Expression<Func<TEntity, object>>? OrderBy { get; private set; }
        public Expression<Func<TEntity, object>>? OrderByDescending { get; private set; }

        public int Take { get;private set; }        

        public int Skip { get; private set; }

        public bool IsPaginated { get; set; } 
        protected void ApplyPAgination(int pagesize,int pageIndex)
        {
            IsPaginated = true;
            Take = pagesize;
            Skip = (pageIndex - 1) * pagesize;
        }

        protected void ApplyOrderBy(Expression<Func<TEntity, object>> orderByExpression) => OrderBy = orderByExpression;
        protected void ApplyOrderByDescending(Expression<Func<TEntity, object>> orderByDescendingExpression) => OrderByDescending = orderByDescendingExpression;

    }
}
