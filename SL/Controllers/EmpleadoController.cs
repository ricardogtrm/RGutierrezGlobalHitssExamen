using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SL.Controllers
{
    [Route("api/empleado")]
    [ApiController]
    public class EmpleadoController : ControllerBase
    {
        // GET: api/<EmpleadoController>
        [HttpGet]
        [Route("getall")]
        public IActionResult GetAll()
        {
            ML.Result resultGetAll = BL.Empleado.GetAll();
            if (resultGetAll.Correct)
            {
                return Ok(resultGetAll);
            }
            else
            {
                return NotFound();
            }
        }

        // GET api/<EmpleadoController>/5
        [HttpGet]
        [Route("getbyid/{idEmpleado}")]
        public IActionResult GetById(int idEmpleado)
        {
            ML.Result resultGetById = BL.Empleado.GetById(idEmpleado);
            if (resultGetById.Correct)
            {
                return Ok(resultGetById);
            }
            else
            {
                return NotFound();
            }
        }

        // POST api/<EmpleadoController>
        [HttpPost]
        [Route("add")]
        public IActionResult Add([FromBody] ML.Empleado empleado)
        {
            ML.Result resultAdd = BL.Empleado.Add(empleado);
            if (resultAdd.Correct)
            {
                return Ok(resultAdd);
            }
            else
            {
                return NotFound();
            }
        }

        // PUT api/<EmpleadoController>/5
        [HttpPost]
        [Route("update")]
        public IActionResult Update([FromBody] ML.Empleado empleado)
        {
            ML.Result resultUpdate = BL.Empleado.Update(empleado);
            if (resultUpdate.Correct)
            {
                return Ok(resultUpdate);
            }
            else
            {
                return NotFound(resultUpdate);
            }
        }

        // DELETE api/<EmpleadoController>/5
        [HttpGet]
        [Route("delete/{idEmpleado}")]
        public IActionResult Delete(int idEmpleado)
        {
            ML.Result resultDelete = BL.Empleado.Delete(idEmpleado);
            if (resultDelete.Correct)
            {
                return Ok(resultDelete);
            }
            else
            {
                return NotFound(resultDelete);
            }
        }
    }
}
