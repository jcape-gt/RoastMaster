using RoastMaster.Data;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using System.Web.Http.Cors;

namespace RoastMaster.Api.Controllers
{
    [EnableCors("*", "*", "*")]
    public class RoastTypeController : ApiController
    {
        IRoastTypeRepository repository = MemoryRoastTypeRepository.Instance;

        public IHttpActionResult Get()
        {
            IHttpActionResult result;
            IEnumerable<RoastType> roastTypeList = repository.Get();
            result = Json<IEnumerable<RoastType>>(roastTypeList);

            return result;
        }

        public IHttpActionResult Get(int id)
        {
            IHttpActionResult result;
            RoastType roastType = repository.GetByID((int)id);

            if (roastType != null)
                result = Json<RoastType>(roastType);
            else
                result = NotFound();

            return result;
        }

        public IHttpActionResult Put([FromBody]RoastType roastType)
        {
            IHttpActionResult result;
            if (roastType != null)
            {
                repository.Insert(roastType);
                result = StatusCode(HttpStatusCode.Created);
            }
            else
                result = StatusCode(HttpStatusCode.BadRequest);

            return result;
        }

        public IHttpActionResult Post([FromBody]RoastType roastType)
        {
            IHttpActionResult result;
            if (roastType != null)
            {
                repository.Update(roastType);
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