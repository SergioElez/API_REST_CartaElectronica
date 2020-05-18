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
    public class IngredienteController : ApiController
    {
        // ----------- CRUD ----------- \\

        // GET: api/Ingrediente
        [HttpGet]
        [Route("api/{idioma}/Ingrediente/plato/{plato}")]
        public ArrayList Get(byte idioma, int plato)
        {
            IngredientePersistente pp = new IngredientePersistente();
            return pp.ObtenerIngredientesDeUnPlato(idioma, plato);

        }


        // GET: api/Ingrediente/5
        [HttpGet]
        [Route("api/{idioma}/Ingrediente/{id}")]
        public Ingrediente Get(int idioma, int id)
        {
            IngredientePersistente pp = new IngredientePersistente();
            Ingrediente Ingrediente = pp.ObtenerIngrediente(idioma, id);
            return Ingrediente;
        }

        /* DE MOMENTO NO VAMOS A UTILIZAR POST, PUT NI DELETE        

        // POST: api/Ingrediente
        public HttpResponseMessage Post([FromBody]Ingrediente Ingrediente)
        {
            IngredientePersistente pp = new IngredientePersistente();
            long codigo = pp.GuardarIngrediente(Ingrediente);
            HttpResponseMessage respuesta = Request.CreateResponse(HttpStatusCode.Created);
            return respuesta;
        }




        // PUT: api/Ingrediente/5
        public HttpResponseMessage Put([FromBody]Ingrediente value)
        {
            IngredientePersistente pp = new IngredientePersistente();
            bool existe = pp.ActualizarIngrediente(1, value);

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

        //// DELETE: api/Ingrediente/5
        //public HttpResponseMessage Delete(int id)
        //{
        //    IngredientePersistente pp = new IngredientePersistente();

        //    bool existe = pp.BorrarIngrediente(id);

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
