using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WSCartaElectronica.Models
{
    public class Producto
    {
        public long codigo { get; set; }
        public String nombre { get; set; }
        public String grupo { get; set; }
        public String especificaciones { get; set; }
        public Double precio { get; set; }
        public String imagen{ get; set; }


    }
}