using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ViewModels
{
    public class AddUserToRolesByIdRequest
    {
        public Guid UserId { get; set; }
        public string[] Roles { get; set; }
    }
}
