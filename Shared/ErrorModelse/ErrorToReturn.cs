using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.ErrorModelse
{
   public class ErrorToReturn
    {
        public int statusCode { get; set; }
        public string ErrorMassage { get; set; } = null;
    }
}
