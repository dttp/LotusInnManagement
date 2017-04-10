using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusInn.Management.Model
{
    public class PaginationResult<T>
    {
        public List<T> Data;
        public int Total { get; set; }
    }
}
