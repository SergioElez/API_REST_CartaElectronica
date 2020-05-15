using System;


namespace WSCartaElectronica.Models
{
    public class Plato
    {
        public long id { get; set; }
        public String nombre { get; set; }
        public String descripcion { get; set; }
        public String imagen { get; set; }
        public Double precio { get; set; }
        public long id_familia { get; set; }

    }
}