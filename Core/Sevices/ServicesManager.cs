using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abstraction;
using AutoMapper;
using Domain.Contracts;

namespace Sevices
{
    public class ServicesManager(IUnitOfWork unitOfWork,IMapper mapper) : IServicesManager
    {
        private readonly Lazy<IProductServices> _LazyproductServices=new Lazy<IProductServices>
            (()=> new ProductService(unitOfWork,mapper));
        public IProductServices ProductServices => _LazyproductServices.Value;
    }
}
