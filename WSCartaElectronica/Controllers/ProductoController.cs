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
    public class ProductoController : ApiController
    {
        // GET: api/Producto
        public ArrayList Get()
        {
            ProductoPersistente pp = new ProductoPersistente();
            return pp.ObtenerProductos();
        }

        // GET: api/Producto/5
        public Producto Get(int id)
        {
            ProductoPersistente pp = new ProductoPersistente();
            Producto producto = pp.ObtenerProducto(id);

            return producto;
        }

        // POST: api/Producto
        public HttpResponseMessage Post([FromBody]Producto value)
        {
            ProductoPersistente pp = new ProductoPersistente();
            long codigo = pp.GuardarProducto(value);
            value.codigo = codigo;
            HttpResponseMessage respuesta = Request.CreateResponse(HttpStatusCode.Created);
            return respuesta;
        }

        // PUT: api/Producto/5
        public HttpResponseMessage Put(int id, [FromBody]Producto value)
        {
            ProductoPersistente pp = new ProductoPersistente();
            bool existe = pp.ActualizaProducto(id, value);

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

        // DELETE: api/Producto/5
        public HttpResponseMessage Delete(int id)
        {
            ProductoPersistente pp = new ProductoPersistente();

            bool existe = pp.BorrarProducto(id);

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
    }
}

