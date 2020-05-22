using System;
using System.Collections.Generic;
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
    public class UsuarioController : ApiController
    {
        // ----------- CRUD ----------- \\

        // GET: api/Usuario
        [HttpGet]
        [Route("api/empresa/{empresa}/Usuario")]
        public ArrayList Get(int empresa)
        {
            UsuarioPersistente pp = new UsuarioPersistente();
            return pp.ObtenerUsuarios(empresa);

        }


        // GET: api/Usuario/5
        [HttpGet]
        [Route("api/empresa/{empresa}/Usuario/{id}")]
        public Usuario Get(int empresa, int id)
        {
            UsuarioPersistente pp = new UsuarioPersistente();
            Usuario usuario = pp.ObtenerUsuario(empresa, id);
            return usuario;
        }


        // POST: api/Usuario
        public HttpResponseMessage Post([FromBody]Usuario usuario)
        {
            UsuarioPersistente pp = new UsuarioPersistente();
            long codigo = pp.GuardarUsuario(usuario);
            HttpResponseMessage respuesta = Request.CreateResponse(HttpStatusCode.Created);
            return respuesta;
        }


        // ------ RUTAS PERSONALIZADAS ----- \\


        // OBTENER USUARIO DE UNA EMPRESA
        [HttpGet]
        [Route("api/empresa/{empresa}/correo/{correo}")]
        public ArrayList BuscarUsuariosPorEmpresaYCorreo(int empresa, string correo)
        {
            UsuarioPersistente pp = new UsuarioPersistente();
            return pp.BuscarUsuariosPorEmpresaYCorreo(empresa, correo);
        }


        // Obtener Usuario Por Correo
        [HttpGet]
        [Route("api/empresa/usuario/correo/{correo}")]
        public Usuario ObtenerUsuarioPorCorreo(string correo)
        {
            UsuarioPersistente pp = new UsuarioPersistente();
            return pp.ObtenerUsuarioPorCorreo(correo);
        }


        // INICIAR SESION
        [HttpGet]
        [Route("api/empresa/correo/{correo}/contrasena/{contrasena}")]
        public bool IniciarSesion(string correo, string contrasena)
        {
            UsuarioPersistente pp = new UsuarioPersistente();
            return pp.IniciarSesion(correo, contrasena);
        }

        // Comprobar Correo
        [HttpGet]
        [Route("api/empresa/correo/{correo}")]
        public bool ComprobarCorreo(string correo)
        {
            UsuarioPersistente pp = new UsuarioPersistente();
            return pp.ComprobarCorreo(correo);
        }

        // Activar moneda
        [HttpGet]
        [Route("api/empresa/moneda/usuario/{usuario}")]
        public bool ActivarMoneda(int usuario)
        {
            UsuarioPersistente pp = new UsuarioPersistente();
            return pp.ActivarMoneda(usuario);
        }

    }
}
