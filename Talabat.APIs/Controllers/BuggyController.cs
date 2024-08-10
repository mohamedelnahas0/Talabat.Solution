using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.Errors;
using Talabat.Repository.Generic_Repository.Data;

namespace Talabat.APIs.Controllers
{
   
    public class BuggyController : BaseApiController

    {
        private readonly StoreContext _dbcontext;

        public BuggyController(StoreContext dbcontext)
        {
            _dbcontext = dbcontext;
        }


        [HttpGet("NotFound")]

        public ActionResult GetNotFoundRequest()
        {
            var product = _dbcontext.Products.Find(100);
            if (product is null) 
            { 
                return NotFound(new Apiresponse(404));
            }
            return Ok(product);
        }


        [HttpGet("ServerError")]
        public ActionResult GetServerError()
        {
            var product = _dbcontext.Products.Find(100);
            var producttoreturn = product.ToString();
             return Ok(producttoreturn);
        }


        [HttpGet("BadRequest")]
        public ActionResult GetbadRequest()
        {
            return BadRequest();
        }

        [HttpGet("BadRequest/{id}")]
        public ActionResult GetbadRequest(int id)
        {
            return Ok();
        }

    }
}
