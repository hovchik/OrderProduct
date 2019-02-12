﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entity
{
    public class Product:SuperEntity    
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }

    }
}
