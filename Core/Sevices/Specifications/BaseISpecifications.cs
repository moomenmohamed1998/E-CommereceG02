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

        public List<Expression<Func<TEntity, object>>> IncludeExpressions  { get; } = new List<Expression<Func<TEntity, object>>>();
        public void AddInclude(Expression<Func<TEntity, object>> includeExpression)
        {
            IncludeExpressions.Add(includeExpression);
        }
    
    }
}
