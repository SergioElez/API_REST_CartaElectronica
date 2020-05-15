using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WSCartaElectronica.Models
{
    public class Maridaje
    {
        public long id { get; set; }
        public long id_plato { get; set; }
        public long id_plato_recomendado { get; set; }
        public String descripcion { get; set; }

    }
}