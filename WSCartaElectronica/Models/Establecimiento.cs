using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WSCartaElectronica.Models
{
    public class Establecimiento
    {
        public long id { get; set; }
        public String nombre { get; set; }
        public String descripcion{ get; set; }
        public String tipo{ get; set; }
        public String mapa{ get; set; }
        public String imagen{ get; set; }
        public long id_empresa{ get; set; }


    }
}