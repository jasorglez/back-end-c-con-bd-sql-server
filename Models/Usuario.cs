﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WSVenta.Models
{
    public partial class Usuario
    {
        public int Id { get; set; }
        public String Email { get; set; }
        public String Password { get; set; }
        public String nombre { get; set; }

    }
}
