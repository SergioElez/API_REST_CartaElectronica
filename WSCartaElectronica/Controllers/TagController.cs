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

        // GET: api/Tag/5
        [HttpGet]
        [Route("api/{idioma}/{Tag}")]
        public ArrayList Get(int idioma)
        {
            TagPersistente pp = new TagPersistente();
            ArrayList TagArrayList = pp.ObtenerTags(idioma);
            return TagArrayList;
        }



    }
}
