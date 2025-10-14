using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charipay.Application.Common.Pagination
{
    public class PagedRequest
    {
        private int _pageSize = 10;
        public int PageNumber { get; set; } = 1;

        public int PageSize { 
         get => _pageSize;
         set => _pageSize = (value>100 ? 100 : _pageSize);
        }
    }
}
