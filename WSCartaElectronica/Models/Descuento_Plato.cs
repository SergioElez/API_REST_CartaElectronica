using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WSCartaElectronica.Models
{
    public class Descuento_Plato
    {
        public long id_descuento { get; set; }
        public String nombre_descuento { get; set; }
        public String descripcion_descuento { get; set; }
        public String codigo_descuento { get; set; }
        public int precio_descuento{ get; set; }
        public Double precio_final_descuento { get; set; }

        public long id_plato { get; set; }
        public String nombre_plato { get; set; }
        public String imagen_plato { get; set; }
        public Double precio_plato { get; set; }
        public String nombre_familia { get; set; }
        public long id_familia_plato { get; set; }

    }
}