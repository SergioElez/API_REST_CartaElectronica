using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WSCartaElectronica.Models
{
    public class Usuario
    {
        public long id { get; set; }
        public String nombre { get; set; }
        public String correo { get; set; }
        public String contraseña { get; set; }
        public String imagen { get; set; }
        public int puntos { get; set; }
        public long id_empresa{ get; set; }


    }
}