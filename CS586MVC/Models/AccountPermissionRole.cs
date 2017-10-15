using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CS586MVC.Models
{
    public class AccountPermissionRole
    {
        public int ID { get; set; }
        public int AccountID { get; set; }
        public int PermissionRoleID { get; set; }
    }
}
