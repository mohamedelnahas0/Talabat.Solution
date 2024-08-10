using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.Errors;

namespace Talabat.APIs.Controllers
{
    [Route("errors/{code}")]
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorsController : ControllerBase
    {
        [HttpGet]
        public ActionResult Error(int code)
        {
            if (code == 401)
                return Unauthorized(new Apiresponse(401));
            else if (code == 404)
            return NotFound(new Apiresponse(404));
            else
                return BadRequest(code);

        }
    }
}
