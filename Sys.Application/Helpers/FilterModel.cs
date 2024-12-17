using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sys.Application.Helpers
{
    public class FilterModel<T> where T : class
    {
        public Dictionary<string, object> Filters { get; set; } = new Dictionary<string, object>();
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string OrderBy { get; set; }
        public string OrderDirection { get; set; } = "asc";
    }
}
