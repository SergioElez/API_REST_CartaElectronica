using System;


namespace WSCartaElectronica.Models
{
    public class PlatoTraducido
    {
        public long id { get; set; }
        public String nombre_ES { get; set; }
        public String descripcion_ES { get; set; }
        public String nombre_EN { get; set; }
        public String descripcion_EN { get; set; }
        public String imagen { get; set; }
        public Double precio { get; set; }
        public long id_familia { get; set; }

    }
}