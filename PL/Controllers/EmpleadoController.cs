using Microsoft.AspNetCore.Mvc;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;
using System.Configuration;

namespace PL.Controllers
{
    public class EmpleadoController : Controller
    {
        private IHostingEnvironment environment;
        private IConfiguration configuration;
        public EmpleadoController(IHostingEnvironment _enviroment, IConfiguration _configuration)
        {
            environment = _enviroment;
            configuration = _configuration;
        }

        [HttpGet]
        public ActionResult GetAll()
        {
            ML.Empleado empleado = new ML.Empleado();

            ML.Result resultEmpleado = new ML.Result();
            resultEmpleado.Objects = new List<object>();

            using (HttpClient client = new HttpClient())
            {
                string webApi = configuration["WebApiEmpleado"];
                client.BaseAddress = new Uri(webApi);

                var responseTask = client.GetAsync("getall");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<ML.Result>();
                    readTask.Wait();

                    foreach (var item in readTask.Result.Objects)
                    {
                        ML.Empleado empleadoList = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Empleado>(item.ToString());
                        resultEmpleado.Objects.Add(empleadoList);
                    }
                }
                empleado.Empleados = resultEmpleado.Objects;
            }
            return View(empleado);
        }

        [HttpGet]
        public ActionResult Form(int? idEmpleado)
        {
            ML.Result resultSucursal = new ML.Result();
            resultSucursal.Objects = new List<object>();
            ML.Empleado empleado = new ML.Empleado();
            empleado.Sucursal = new ML.Sucursal();
            empleado.Sucursal.Sucursales = new List<object>();

            using (HttpClient client = new HttpClient())
            {
                string webApi = configuration["WebApiSucursal"];
                client.BaseAddress = new Uri(webApi);

                var responseTask = client.GetAsync("getall");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<ML.Result>();
                    readTask.Wait();

                    foreach (var item in readTask.Result.Objects)
                    {
                        ML.Sucursal sucursalList = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Sucursal>(item.ToString());
                        resultSucursal.Objects.Add(sucursalList);
                    }
                }
                empleado.Sucursal.Sucursales = resultSucursal.Objects;
            }

            if (idEmpleado == null)
            {
                ViewBag.Titulo = "Agregar nuevo usuario";
                ViewBag.Accion = "Agregar";

                return View(empleado);
            }
            else
            {
                ViewBag.Titulo = "Actualizar un usuario";
                ViewBag.Accion = "Actualizar";
                //ML.Result result = BL.Usuario.GetById(idUsuario.Value);
                ML.Result results = new ML.Result();
                using (HttpClient client = new HttpClient())
                {
                    string webApi = configuration["WebApiEmpleado"];
                    client.BaseAddress = new Uri(webApi);
                    var responseTask = client.GetAsync("getbyid/" + idEmpleado);
                    responseTask.Wait();

                    var resultApi = responseTask.Result;
                    if (resultApi.IsSuccessStatusCode)
                    {
                        var readTask = resultApi.Content.ReadAsAsync<ML.Result>();
                        readTask.Wait();
                        ML.Empleado resultEmpleado = new ML.Empleado();
                        resultEmpleado = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Empleado>(readTask.Result.Object.ToString());
                        results.Object = resultEmpleado;
                        if (results.Object != null)
                        {
                            empleado = ((ML.Empleado)results.Object);
                            empleado.Sucursal.Sucursales = resultSucursal.Objects;
                            return View(empleado);
                        }
                        else
                        {
                            ViewBag.Message = "Registro no encontrado";
                            return View("Modal");
                        }
                        return View("Modal");
                    }
                    else
                    {
                        results.Correct = false;
                        ViewBag.Message = "No existen registros en la tabla empleado";
                        return View("Modal");
                    }
                }
            }
        }

        [HttpPost]
        public ActionResult Form(ML.Empleado empleado)
        {
            ML.Result results = new ML.Result();
            if (empleado.IdEmpleado == 0)
            {
                //var result = BL.Usuario.Add(usuario);
                using (HttpClient client = new HttpClient())
                {
                    string webApi = configuration["WebApiEmpleado"];
                    client.BaseAddress = new Uri(webApi);

                    var postTask = client.PostAsJsonAsync<ML.Empleado>("add", empleado);
                    postTask.Wait();

                    var result = postTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        ViewBag.Titulo = "¡Registro exitoso!";
                        ViewBag.Message = results.Message;
                        return View("Modal");
                        //return RedirectToAction("GetAll");
                    }
                    else
                    {
                        ViewBag.Titulo = "¡Error al registrar!";
                        ViewBag.Message = "Error al registrar el empleado";
                        return View("Modal");
                    }
                    return View("GetAll");
                }
            }
            else
            {
                //var result = BL.Usuario.Update(usuario);
                using (HttpClient client = new HttpClient())
                {
                    string webApi = configuration["WebApiEmpleado"];
                    client.BaseAddress = new Uri(webApi);

                    var postTask = client.PostAsJsonAsync<ML.Empleado>("update", empleado);
                    postTask.Wait();

                    var result = postTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        ViewBag.Titulo = "¡Actualizació exitosa!";
                        ViewBag.Message = "Empleado modificado";
                        return View("Modal");
                        //return RedirectToAction("GetAll");
                    }
                    else
                    {
                        ViewBag.Titulo = "¡Error al registrar!";
                        ViewBag.Message = "ocurrio un error al actualizar";
                        return View("Modal");
                    }
                }
                return View("GetAll");
            }
        }

        [HttpGet]
        public ActionResult Delete(int idEmpleado)
        {
            ML.Result resultEmpleado = new ML.Result();
            using (HttpClient client = new HttpClient())
            {
                string webApi = configuration["WebApiEmpleado"];
                client.BaseAddress = new Uri(webApi);

                var postTask = client.GetAsync("delete/" + idEmpleado);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    ViewBag.Titulo = "¡Baja exitosa!";
                    ViewBag.Message = "Se elimino el empleado";
                    return View("Modal");
                    //return RedirectToAction("GetAll");
                }
                else
                {
                    ViewBag.Titulo = "¡Error al eliminar!";
                    ViewBag.Message = "No se elimino el empleado";
                    return View("Modal");
                }
            }
            return View("Modal");
        }
    }
}
