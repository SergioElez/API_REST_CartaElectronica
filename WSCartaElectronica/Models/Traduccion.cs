using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WSCartaElectronica.Models
{
    public class Traduccion
    {
        public long id { get; set; }
        public long id_idioma { get; set; }
        public String id_tipo{ get; set; }
        public long id_item { get; set; }
        public String texto { get; set; }

    }
}