using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WSCartaElectronica.Models;
using System.Web.Http.Cors;
using System.Collections;

namespace WSCartaElectronica.Controllers
{
    [EnableCors(origins: "http://localhost:8100", headers: "*", methods: "*")]
    public class EstablecimientoController : ApiController
    {
        // ----------- CRUD ----------- \\

        // GET: api/Establecimiento
        [HttpGet]
        [Route("api/{idioma}/Establecimiento")]
        public ArrayList Get(int idioma)
        {
            EstablecimientoPersistente pp = new EstablecimientoPersistente();
            return pp.ObtenerEstablecimientos(idioma);

        }
        

        // GET: api/Establecimiento/5
        [HttpGet]
        [Route("api/{idioma}/Establecimiento/{id}")]
        public Establecimiento Get(int idioma, int id)
        {
            EstablecimientoPersistente pp = new EstablecimientoPersistente();
            Establecimiento Establecimiento = pp.ObtenerEstablecimiento(idioma, id);
            return Establecimiento;
        }

        [HttpGet]
        [Route("api/{idioma}/mapa/Establecimiento/{id}")]
        public string ObtenerMapaEstablecimiento(int idioma, int id)
        {
            EstablecimientoPersistente pp = new EstablecimientoPersistente();
            return pp.ObtenerMapaEstablecimiento(idioma, id);
           
        }


    }
}
