using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AFTER.Shared.DTOs.Pagination
{
    public class PaginationDataOut<T>
    {
        public int Count { get; set; }

        public List<T> Data { get; set; }

        public PaginationDataOut()
        {
            Data = new List<T>();
        }
    }
}
