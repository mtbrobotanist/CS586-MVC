using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CS586MVC.Models
{
    public class PermissionRole
    {
        public int ID { get; set; }
        public int PermissionID { get; set; }
        public int RoleID { get; set; }
    }
}
