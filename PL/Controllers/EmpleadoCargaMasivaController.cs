using Microsoft.AspNetCore.Mvc;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;
using System.Configuration;

namespace PL.Controllers
{
    public class EmpleadoCargaMasivaController : Controller
    {
        private IHostingEnvironment environment;
        private IConfiguration configuration;
        public EmpleadoCargaMasivaController(IHostingEnvironment _enviroment, IConfiguration _configuration)
        {
            environment = _enviroment;
            configuration = _configuration;
        }

        public IActionResult GetCargaMasiva()
        {
            ML.Result result = new ML.Result();
            return View(result);
        }

        [HttpPost]
        public IActionResult PostCargaMasiva(IFormFile file)
        {
            if (file == null || file.Length <= 0)
            {
                return RedirectToAction("GetCargaMasiva");
            }
            using (var reader = new StreamReader(file.OpenReadStream()))
            {
                string linea = reader.ReadLine();
                while (!reader.EndOfStream)
                {
                    linea = reader.ReadLine();
                    var valores = linea.Split('|');

                    ML.Empleado empleado = new ML.Empleado();
                    empleado.IdEmpleado = int.Parse(valores[0]);
                    empleado.Nombre = valores[1];
                    empleado.ApellidoPaterno = valores[2];
                    empleado.ApellidoMaterno = valores[3];
                    empleado.Direccion = valores[4];
                    empleado.Edad = int.Parse(valores[5]);
                    empleado.Telefono = valores[6];
                    empleado.Sexo = valores[7];
                    empleado.FechaIngreso = valores[8];
                    empleado.Salario = valores[9];
                    empleado.Sucursal = new ML.Sucursal();
                    empleado.Sucursal.IdSucursal = int.Parse(valores[11]);

                    using (HttpClient client = new HttpClient())
                    {
                        string webApi = configuration["WebApiEmpleado"];
                        client.BaseAddress = new Uri(webApi);

                        var postTask = client.PostAsJsonAsync<ML.Empleado>("add", empleado);
                        postTask.Wait();

                        var result1 = postTask.Result;
                        if (result1.IsSuccessStatusCode)
                        {
                            ViewBag.Titulo = "¡Registro exitoso!";
                            ViewBag.Message = "Empleado registrado";
                            return View("Modal");
                            //return RedirectToAction("GetAll");
                        }
                        else
                        {
                            ViewBag.Titulo = "¡Error al registrar!";
                            ViewBag.Message = Response.StatusCode = 400;
                            return View("Modal");
                        }
                    }
                }
                return RedirectToAction("Empleado/GetAll");
            }
        }
    }
}
