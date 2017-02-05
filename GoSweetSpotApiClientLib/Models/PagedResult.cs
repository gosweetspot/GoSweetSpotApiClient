using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoSweetSpotApiClientLib.Models
{
    public class PagedResult<T>
    {
        public int Page { get; set; }
        public int Pages { get; set; }
        public int PageSize { get; set; }
        public List<T> Results { get; set; }
    }
}
