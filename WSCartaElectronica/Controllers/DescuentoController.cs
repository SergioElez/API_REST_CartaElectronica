using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WSCartaElectronica.Models;
using System.Web.Http.Cors;
using System.Collections;
using System.Timers;
using System.Web.Services.Description;

namespace WSCartaElectronica.Controllers
{
    [EnableCors(origins: "http://localhost:8100", headers: "*", methods: "*")]
    public class DescuentoController : ApiController
    {
        // ----------- CRUD ----------- \\

        // Obtener Descuentos Establecimiento
        [HttpGet]
        [Route("api/{idioma}/Descuento/establecimiento/{establecimiento}")]
        public ArrayList ObtenerDescuentosEstablecimiento(int idioma, int establecimiento)
        {
            DescuentoPersistente pp = new DescuentoPersistente();
            ArrayList descuentos = pp.ObtenerDescuentosEstablecimiento(idioma, establecimiento);
            return descuentos;
        }

        // Obtener Descuentos Usuario
        [HttpGet]
        [Route("api/{idioma}/Descuento/usuario/{usuario}")]
        public ArrayList ObtenerDescuentosUsuario(int idioma, int usuario)
        {
            DescuentoPersistente pp = new DescuentoPersistente();
            ArrayList descuentos = pp.ObtenerDescuentosUsuario(idioma, usuario);
            return descuentos;
        }


        // Añadir Descuento Usuario
        [HttpGet]
        [Route("api/Descuento/{descuento}/usuario/{usuario}")]
        public Int32 AñadirDescuentoUsuario(int usuario, int descuento)
        {
            DescuentoPersistente pp = new DescuentoPersistente();
            Int32 operacionCompletada = pp.AñadirDescuentoUsuario(usuario, descuento);
            return operacionCompletada;
        }

        // Borrar Descuento Usuario
        [HttpGet]
        [Route("api/DescuentoBorrar/{descuento}/usuario/{usuario}")]
        public void BorrarDescuentoUsuario(int usuario, int descuento)
        {
            DescuentoPersistente pp = new DescuentoPersistente();
            pp.BorrarDescuentoUsuario(usuario, descuento);
        }

    }
}
