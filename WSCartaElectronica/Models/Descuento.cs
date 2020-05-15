using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WSCartaElectronica.Models
{
    public class Descuento
    {
        public long id { get; set; }
        public String nombre { get; set; }
        public String descripcion { get; set; }
        public String codigo { get; set; }

    }
}