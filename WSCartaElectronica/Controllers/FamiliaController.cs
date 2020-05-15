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
    public class FamiliaController : ApiController
    {
        // ----------- CRUD ----------- \\

        // GET: api/Familia
        [HttpGet]
        [Route("api/{idioma}/Familia")]
        public ArrayList Get(int idioma)
        {
            FamiliaPersistente pp = new FamiliaPersistente();
            return pp.ObtenerFamilias(idioma);

        }


        // GET: api/Familia/5
        [HttpGet]
        [Route("api/{idioma}/Familia/{id}")]
        public Familia Get(int idioma, int id)
        {
            FamiliaPersistente pp = new FamiliaPersistente();
            Familia Familia = pp.ObtenerFamilia(idioma, id);
            return Familia;
        }


        //// POST: api/Familia
        //public HttpResponseMessage Post([FromBody]FamiliaTraducido Familia)
        //{
        //    FamiliaPersistente pp = new FamiliaPersistente();
        //    long codigo = pp.GuardarFamilia(Familia);
        //    HttpResponseMessage respuesta = Request.CreateResponse(HttpStatusCode.Created);
        //    return respuesta;
        //}




        //// PUT: api/Familia/5
        //public HttpResponseMessage Put([FromBody]FamiliaTraducido value)
        //{
        //    FamiliaPersistente pp = new FamiliaPersistente();
        //    bool existe = pp.ActualizarFamilia(1, value);

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

        //// DELETE: api/Familia/5
        //public HttpResponseMessage Delete(int id)
        //{
        //    FamiliaPersistente pp = new FamiliaPersistente();

        //    bool existe = pp.BorrarFamilia(id);

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

        // OBTENER FAMILIAS DE UN ESTABLECIMIENTO
        [HttpGet]
        [Route("api/{idioma}/familia/establecimiento/{establecimiento}")]
        public ArrayList BuscarPorEstablecimiento(int idioma, int establecimiento)
        {
            FamiliaPersistente pp = new FamiliaPersistente();
            return pp.BuscarFamiliaPorEstablecimiento(idioma, establecimiento);
        }



        // OBTENER FAMILIAS DE UN ESTABLECIMIENTO POR NOMBRE
        [HttpGet]
        [Route("api/{idioma}/familia/{establecimiento}/busqueda/{busqueda}")]
        public ArrayList BuscarPorNombre(int idioma, int establecimiento, string busqueda)
        {
            FamiliaPersistente pp = new FamiliaPersistente();
            return pp.BuscarFamiliasPorNombre(idioma, establecimiento, busqueda);
        }


    }
}
