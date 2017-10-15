using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CS586MVC.Models
{
    public class Lease
    {
        public int ID { get; set; }
        public int Duration { get; set; }
        public bool Pets { get; set; }
    }
}
