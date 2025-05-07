using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Shared.ErrorModelse
{
public    class ValidationErrorToRuturn
    {

        public int StatusCode { get; set; } =(int) HttpStatusCode.BadRequest;
        public string Message { get; set; } = "validation Error";
        public IEnumerable<ValidayionError> ValidtionErrors { get; set; } = [];
    }
}
