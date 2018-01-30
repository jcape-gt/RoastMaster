using RoastMaster.Data;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using System.Web.Http.Cors;

namespace RoastMaster.Api.Controllers
{
    [EnableCors("*", "*", "*")]
    public class SpeciesController : ApiController
    {
        ISpeciesRepository repository = MemorySpeciesRepository.Instance;
        
        public IHttpActionResult Get()
        {
            IHttpActionResult result;
            IEnumerable<Species> speciesTypeList = repository.Get();
            result = Json<IEnumerable<Species>>(speciesTypeList);

            return result;
        }

        public IHttpActionResult Get(int id)
        {
            IHttpActionResult result;
            Species species = repository.GetByID((int)id);

            if (species != null)
                result = Json<Species>(species);
            else
                result = NotFound();

            return result;
        }

        public IHttpActionResult Put([FromBody]Species species)
        {
            IHttpActionResult result;
            if (species != null)
            {
                repository.Insert(species);
                result = StatusCode(HttpStatusCode.Created);
            }
            else
                result = StatusCode(HttpStatusCode.BadRequest);

            return result;
        }

        public IHttpActionResult Post([FromBody]Species species)
        {
            IHttpActionResult result;
            if (species != null)
            {
                repository.Update(species);
                result = Ok();
            }
            else
                result = NotFound();

            return result;
        }

        public IHttpActionResult Delete(int? id)
        {
            IHttpActionResult result;
            if (id != null)
            {
                repository.Delete((int)id);
                result = Ok();
            }
            else
                result = StatusCode(HttpStatusCode.BadRequest);

            return result;
        }
    }
}