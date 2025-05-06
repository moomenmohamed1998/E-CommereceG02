using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;
using Domain.Models.Products;

namespace Domain.Contracts
{
    public interface IGenericRepository<TEntity, Tkey> where TEntity : ModelBase<Tkey>
    {

        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity> GetByAsynce(Tkey id);
        Task<IEnumerable<TEntity>> GetAllAsync(ISpecifications<TEntity, Tkey> spec);
        Task<TEntity> GetByAsynce(ISpecifications<TEntity, Tkey> spec);

        void Add(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        Task<Product> GetByIdAsync(int id);
    }
}
