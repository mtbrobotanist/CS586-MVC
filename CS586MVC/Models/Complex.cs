﻿using System;
using System.Collections.Generic;

namespace CS586MVC.Models
{
    public partial class Complex
    {
        public Complex()
        {
            ComplexUnit = new HashSet<ComplexUnit>();
        }

        public int Id { get; set; }
        public string Address { get; set; }

        public ICollection<ComplexUnit> ComplexUnit { get; set; }
    }
}