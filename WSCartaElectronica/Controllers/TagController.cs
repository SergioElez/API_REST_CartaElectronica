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
    public class TagController : ApiController
    {
        // ----------- CRUD ----------- \\

        // GET: api/Tag
        [HttpGet]
        [Route("api/{idioma}/Tag/plato/{plato}")]
        public ArrayList Get(byte idioma, int plato)
        {
            TagPersistente pp = new TagPersistente();
            return pp.ObtenerTagsDeUnPlato(idioma, plato);

        }


        // GET: api/Tag/5
        [HttpGet]
        [Route("api/{idioma}/Tag/{id}")]
        public Tag Get(int idioma, int id)
        {
            TagPersistente pp = new TagPersistente();
            Tag Tag = pp.ObtenerTag(idioma, id);
            return Tag;
        }

        /* DE MOMENTO NO VAMOS A UTILIZAR POST, PUT NI DELETE        

        // POST: api/Tag
        public HttpResponseMessage Post([FromBody]Tag tag)
        {
            TagPersistente pp = new TagPersistente();
            long codigo = pp.GuardarTag(tag);
            HttpResponseMessage respuesta = Request.CreateResponse(HttpStatusCode.Created);
            return respuesta;
        }




        // PUT: api/Tag/5
        public HttpResponseMessage Put([FromBody]Tag value)
        {
            TagPersistente pp = new TagPersistente();
            bool existe = pp.ActualizarTag(1, value);

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

        //// DELETE: api/Tag/5
        //public HttpResponseMessage Delete(int id)
        //{
        //    TagPersistente pp = new TagPersistente();

        //    bool existe = pp.BorrarTag(id);

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

      */
      
    }
}
