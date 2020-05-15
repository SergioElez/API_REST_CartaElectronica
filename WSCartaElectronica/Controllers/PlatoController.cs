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
    public class PlatoController : ApiController
    {
        // ----------- CRUD ----------- \\

        // GET: api/Plato
        [HttpGet]
        [Route("api/{idioma}/plato")]
        public ArrayList Get(int idioma)
        {
            PlatoPersistente pp = new PlatoPersistente();
            return pp.ObtenerPlatos(idioma);

        }


        // GET: api/Plato/5
        [HttpGet]
        [Route("api/{idioma}/plato/{id}")]
        public Plato Get(int idioma, int id)
        {
            PlatoPersistente pp = new PlatoPersistente();
            Plato Plato = pp.ObtenerPlato(idioma, id);
            return Plato;
        }


        // POST: api/Plato
        public HttpResponseMessage Post([FromBody]PlatoTraducido plato)
        {
            PlatoPersistente pp = new PlatoPersistente();
            long codigo = pp.GuardarPlato(plato);
            HttpResponseMessage respuesta = Request.CreateResponse(HttpStatusCode.Created);
            return respuesta;
        }




        // PUT: api/Plato/5
        public HttpResponseMessage Put([FromBody]PlatoTraducido value)
        {
            PlatoPersistente pp = new PlatoPersistente();
            bool existe = pp.ActualizarPlato(1, value);

            HttpResponseMessage respuesta;
            
            if (existe)
            {
                respuesta = Request.CreateResponse(HttpStatusCode.OK);
            }
            else
            {
                respuesta = Request.CreateResponse(HttpStatusCode.NotFound);
            }

            return respuesta;
        }

        //// DELETE: api/Plato/5
        //public HttpResponseMessage Delete(int id)
        //{
        //    PlatoPersistente pp = new PlatoPersistente();

        //    bool existe = pp.BorrarPlato(id);

        //    HttpResponseMessage respuesta;

        //    if (existe)
        //    {
        //        respuesta = Request.CreateResponse(HttpStatusCode.OK);
        //    }
        //    else
        //    {
        //        respuesta = Request.CreateResponse(HttpStatusCode.NotFound);
        //    }

        //    return respuesta;
        //}

        // ------ RUTAS PERSONALIZADAS ----- \\

        //Buscar plato por familia
        [HttpGet]
        [Route("api/{idioma}/Plato/familia/{familia}")]
        public ArrayList BuscarPorFamilia(int idioma, int familia)
        {
            PlatoPersistente pp = new PlatoPersistente();
            return pp.BuscarPlatosPorFamilia(idioma, familia);
        }


        //Buscar por nombre de Plato
        [HttpGet]
        [Route("api/{idioma}/Plato/{familia}/busqueda/{busqueda}")]
        public ArrayList BuscarPorNombre(int idioma, int familia, string busqueda)
        {
            PlatoPersistente pp = new PlatoPersistente();
            return pp.BuscarPlatosPorNombre(idioma, familia, busqueda);
        }


        //Buscar por tag
        [HttpGet]
        [Route("api/{idioma}/Plato/tag/{tag}")]
        public ArrayList BuscarPorTag(int idioma, string tag)
        {
            PlatoPersistente pp = new PlatoPersistente();
            return pp.BuscarPlatosPorTag(idioma, tag);
        }

    }
}
