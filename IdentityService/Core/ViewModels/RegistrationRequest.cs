using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ViewModels
{
    public class RegistrationRequest
    {
        public User User { get; set; }
        public string Password { get; set; }
    }
}
