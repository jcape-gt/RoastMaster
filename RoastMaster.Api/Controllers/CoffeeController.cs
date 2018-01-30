using RoastMaster.Data;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;

namespace RoastMaster.Api.Controllers
{
    public class CoffeeController : ApiController
    {
        ICoffeeRepository repository = MemoryCoffeeRepository.Instance;

        public IHttpActionResult Get()
        {
            IHttpActionResult result;
            IEnumerable<Coffee> coffeeList = repository.Get();
            result = Json<IEnumerable<Coffee>>(coffeeList);

            return result;
        }

        public IHttpActionResult Get(int id)
        {
            IHttpActionResult result;
            Coffee coffee = repository.GetByID((int)id);

            if (coffee != null)
                result = Json<Coffee>(coffee);
            else
                result = NotFound();

            return result;
        }

        public IHttpActionResult Put(Coffee coffee)
        {
            IHttpActionResult result;
            if (coffee != null)
            {
                repository.Insert(coffee);
                result =  StatusCode(HttpStatusCode.Created);
            }
            else
                result =  StatusCode(HttpStatusCode.BadRequest);

            return result;
        }

        public IHttpActionResult Post(Coffee coffee)
        {
            IHttpActionResult result;
            if (coffee != null)
            {
                repository.Update(coffee);
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
