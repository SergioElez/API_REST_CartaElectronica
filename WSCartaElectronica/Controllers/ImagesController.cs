using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace WSCartaElectronica.Controllers
{
    [EnableCors(origins: "http://localhost:8100", headers: "*", methods: "*")]
    public class ImagesController : ApiController
    {
        // https://localhost:44365/api/images/plato/2/emperador
        [HttpGet]
        [Route("api/images/plato/{familia}/{nombreImg}")]
        public HttpResponseMessage Get(int familia, string nombreImg)
        {
            var response = Request.CreateResponse(HttpStatusCode.OK);

            var path = "~/images/default.jpg";

            path = System.Web.Hosting.HostingEnvironment.MapPath(path);

            //var ext = System.IO.Path.GetExtension(path);

            var contents = System.IO.File.ReadAllBytes(path);

            try
            {
                path = "~/images/Carta/" + familia + "/" + nombreImg + ".jpg";

                path = System.Web.Hosting.HostingEnvironment.MapPath(path);

                //ext = System.IO.Path.GetExtension(path);


                contents = System.IO.File.ReadAllBytes(path);
            }
            catch (Exception)
            {
                Console.WriteLine("Error, no se encuentra esa imagen");
            }


            System.IO.MemoryStream ms = new System.IO.MemoryStream(contents);

            response.Content = new StreamContent(ms);
            response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("image/jpg");

            return response;

        }



        // https://localhost:44365/api/images/empresa/1/Establecimiento/canfali
        [HttpGet]
        [Route("api/images/empresa/{empresa}/Establecimiento/{nombre}")]
        public HttpResponseMessage Get( string nombre, int empresa)
        {
            var response = Request.CreateResponse(HttpStatusCode.OK);

            var path = "~/images/default.jpg";

            path = System.Web.Hosting.HostingEnvironment.MapPath(path);

            var contents = System.IO.File.ReadAllBytes(path);

            try
            {
                path = "~/images/empresa/" + empresa + "/Establecimiento/" + nombre + ".jpg";

                path = System.Web.Hosting.HostingEnvironment.MapPath(path);

                contents = System.IO.File.ReadAllBytes(path);
            }
            catch (Exception)
            {
                Console.WriteLine("Error, no se encuentra esa imagen");
            }


            System.IO.MemoryStream ms = new System.IO.MemoryStream(contents);

            response.Content = new StreamContent(ms);
            response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("image/jpg");

            return response;

        }



    }
}
