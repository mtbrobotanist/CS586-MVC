using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage.Blob.Protocol;

namespace CS586MVC.Models
{
    public class Role
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int RoleTypeID { get; set; }
    }
}
