using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WSVenta.Models.Request
{
    public class ClienteRequest
    {
        public long Id { get; set; }
        public String Nombre { get; set; }
        public string Direccion { get; set; }
    }
}
