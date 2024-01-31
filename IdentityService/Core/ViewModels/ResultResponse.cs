using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ViewModels
{
    public class ResultResponse<T>
    {
        public bool Success { get; set; }
        public T Result { get; set; }
        public string? Message { get; set; }
    }
}
