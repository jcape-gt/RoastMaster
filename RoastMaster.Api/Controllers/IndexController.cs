using System.Web.Http;

namespace RoastMaster.Api.Controllers
{
    public class IndexController : ApiController
    {
        public IHttpActionResult Get()
        {
            string[] endpointList = { "roastmaster/coffee,", "roastmaster/roasttype", "roastmaster/species" };
            return Ok(new { Endpoints = endpointList});
        }
    }
}