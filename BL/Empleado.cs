using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Empleado
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.RgutierrezGlobalHitssContext context = new DL.RgutierrezGlobalHitssContext())
                {
                    var query = context.Empleados.FromSqlRaw("EmpleadoGetAll").ToList();
                    if (query != null)
                    {
                        result.Objects = new List<object>();
                        foreach (var item in query)
                        {
                            ML.Empleado empleado = new ML.Empleado();
                            empleado.IdEmpleado = item.IdEmpleado;
                            empleado.Nombre = item.Nombre;
                            empleado.ApellidoPaterno = item.ApellidoPaterno;
                            empleado.ApellidoMaterno = item.ApellidoMaterno;
                            empleado.Direccion = item.Direccion;
                            empleado.Edad = item.Edad.Value;
                            empleado.Telefono = item.Telefono;
                            empleado.Sexo = item.Sexo;
                            empleado.FechaIngreso = item.FechaIngreso.Value.ToString("dd/MM/yyyy");
                            empleado.Salario = item.Salario.ToString();
                            empleado.Sucursal = new ML.Sucursal();
                            empleado.Sucursal.IdSucursal = item.IdSucursal.Value;
                            empleado.Sucursal.Nombre = item.Sucursal;

                            result.Objects.Add(empleado);
                        }
                        result.Correct = true;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Message = ex.Message;
            }
            return result;
        }

        public static ML.Result GetById(int idEmpleado)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.RgutierrezGlobalHitssContext context = new DL.RgutierrezGlobalHitssContext())
                {
                    var query = context.Empleados.FromSqlRaw("EmpleadoGetAll").AsEnumerable().FirstOrDefault();
                    if (query != null)
                    {
                        ML.Empleado empleado = new ML.Empleado();
                        empleado.IdEmpleado = query.IdEmpleado;
                        empleado.Nombre = query.Nombre;
                        empleado.ApellidoPaterno = query.ApellidoPaterno;
                        empleado.ApellidoMaterno = query.ApellidoMaterno;
                        empleado.Direccion = query.Direccion;
                        empleado.Edad = query.Edad.Value;
                        empleado.Telefono = query.Telefono;
                        empleado.Sexo = query.Sexo;
                        empleado.FechaIngreso = query.FechaIngreso.Value.ToString("dd/MM/yyyy");
                        empleado.Salario = query.Salario.ToString();
                        empleado.Sucursal = new ML.Sucursal();
                        empleado.Sucursal.IdSucursal = query.IdSucursal.Value;
                        empleado.Sucursal.Nombre = query.Sucursal;

                        result.Object = empleado;
                        result.Correct = true;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Message = ex.Message;
            }
            return result;
        }

        public static ML.Result Delete(int idEmpleado)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.RgutierrezGlobalHitssContext context = new DL.RgutierrezGlobalHitssContext())
                {
                    int query = context.Database.ExecuteSqlRaw($"EmpleadoDelete {idEmpleado}");
                    if (query > 0)
                    {
                        result.Message = "Empleado eliminado correctamente";
                        result.Correct = true;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Message = ex.Message;
            }
            return result;
        }

        public static ML.Result Add(ML.Empleado empleado)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.RgutierrezGlobalHitssContext context = new DL.RgutierrezGlobalHitssContext())
                {
                    int query = context.Database.ExecuteSqlRaw($"EmpleadoAdd '{empleado.Nombre}'," +
                        $"'{empleado.ApellidoPaterno}','{empleado.ApellidoMaterno}','{empleado.Direccion}'," +
                        $"{empleado.Edad},'{empleado.Telefono}','{empleado.Sexo}','{empleado.Salario}'," +
                        $"{empleado.Sucursal.IdSucursal}");
                    if (query > 0)
                    {
                        result.Message = "Empleado registrado correctamente";
                        result.Correct = true;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Message = ex.Message;
            }
            return result;
        }

        public static ML.Result Update(ML.Empleado empleado)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.RgutierrezGlobalHitssContext context = new DL.RgutierrezGlobalHitssContext())
                {
                    int query = context.Database.ExecuteSqlRaw($"EmpleadoUpdate {empleado.IdEmpleado}," +
                        $"'{empleado.Nombre}','{empleado.ApellidoPaterno}','{empleado.ApellidoMaterno}'," +
                        $"'{empleado.Direccion}',{empleado.Edad},'{empleado.Telefono}','{empleado.Sexo}'," +
                        $"'{empleado.Salario}',{empleado.Sucursal.IdSucursal}");
                    if (query > 0)
                    {
                        result.Message = "Información de empleado modificada correctamente";
                        result.Correct = true;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Message = ex.Message;
            }
            return result;
        }
    }
}
