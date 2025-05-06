using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
   public class ProductQueryParams
    {
        public  const int DefaultPageSize = 5;
        private const int maxPageSize = 10;

        public int? BrandId { get; set; }
        public int? TypeId { get; set; }
        public ProductSortingOptions SortingOptions { get; set; }
        public string? SearchValue { get; set; }
        public int PageIndex { get; set; } = 1;

        private int pageSize = DefaultPageSize;
        public int PageSize
        {
            get { return pageSize; }
            set => pageSize = (value > maxPageSize) ? maxPageSize : value;
        }



        //public int PageSize { get; set; } 
        //public string? SortBy { get; set; } = "NameAsc";
        //public bool IsAscending { get; set; } = true;
        //public ProductQueryParams(int? brandId, int? typeId, string? search, int pageIndex, int pageSize, string? sortBy, bool isAscending)
        //{
        //    BrandId = brandId;
        //    TypeId = typeId;
        //    Search = search;
        //    PageIndex = pageIndex;
        //    PageSize = pageSize;
        //    SortBy = sortBy;
        //    IsAscending = isAscending;
        //}
    }
}
