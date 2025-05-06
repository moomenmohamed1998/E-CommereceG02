using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Contracts;
using Domain.Models;
using Domain.Models.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Persistence.Data;

namespace Persistence.Repositories
{
    public class GenericRepositorycs<TEntity, Tkey>(StoreDBContext context) : IGenericRepository<TEntity, Tkey>
        where TEntity : ModelBase<Tkey>
    {

        public async Task<IEnumerable<TEntity>> GetAllAsync()
            => await context.Set<TEntity>().ToListAsync();

        public async Task<TEntity> GetByAsynce(Tkey id)
            => await context.Set<TEntity>().FindAsync(id);

        public void Add(TEntity entity)
            => context.Set<TEntity>().Add(entity);

        public void Update(TEntity entity)
            => context.Set<TEntity>().Update(entity);

        public void Delete(TEntity entity)
            => context.Set<TEntity>().Remove(entity);


        public async Task<IEnumerable<TEntity>> GetAllAsync(ISpecifications<TEntity, Tkey> spec)
        {
            return await SpacificationEvaluator.CreateQuery(context.Set<TEntity>(), spec)
                 .ToListAsync();
        }


        public async Task<TEntity> GetBySpecAsync(ISpecifications<TEntity, Tkey> spec)
        {
            return await SpacificationEvaluator.CreateQuery(context.Set<TEntity>(), spec)
                         .FirstOrDefaultAsync();
        }
        public Task<Product> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<TEntity> GetByAsynce(ISpecifications<TEntity, Tkey> spec)
        {
            throw new NotImplementedException();
        }

        public async Task<int> CountAsync(ISpecifications<TEntity, Tkey> spec)
     => await SpacificationEvaluator.CreateQuery(context.Set<TEntity>(), spec).CountAsync();
    }
}
