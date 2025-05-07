using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
  public  class ProductNotFoundExceptions(int Id):NotFoundExceptions($" Proudact with Id{Id} Is not Found! ")
    {
    }
}
