using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SL.Controllers
{
    [Route("api/sucursal")]
    [ApiController]
    public class SucursalController : ControllerBase
    {
        // GET: api/<SucursalController>
        [HttpGet]
        [Route("getall")]
        public IActionResult GetAll()
        {
            ML.Result resultGetAll = BL.Sucursal.GetAll();
            if (resultGetAll.Correct)
            {
                return Ok(resultGetAll);
            }
            else
            {
                return NotFound(resultGetAll);
            }
        }
    }
}
