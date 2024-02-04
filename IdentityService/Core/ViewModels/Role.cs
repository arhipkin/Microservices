using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ViewModels
{
    public class Role
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? NormalizedName { get; set; }
    }
}
