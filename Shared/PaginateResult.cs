using System;
using System.Collections.Generic;
using System.Formats.Tar;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Shared
{
   public class PaginateResult<TEntity>
    {
        public PaginateResult( int pageIndex, int pageSize, int totalCount,IEnumerable<TEntity>  data)

        {
            
            PageIndex = pageIndex;
            PageSize = pageSize;
            TotalCount = totalCount;
        }

        public int PageIndex { get; set; } 
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public IEnumerable<TEntity> Data { get; set; }
    }


}
