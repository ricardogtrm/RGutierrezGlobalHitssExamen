using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Sucursal
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.RgutierrezGlobalHitssContext context = new DL.RgutierrezGlobalHitssContext())
                {
                    var query = context.Sucursals.FromSqlRaw("SucursalGetAll");
                    if (query != null)
                    {
                        result.Objects = new List<object>();
                        foreach (var item in query)
                        {
                            ML.Sucursal sucursal = new ML.Sucursal();
                            sucursal.IdSucursal = item.IdSucursal;
                            sucursal.Nombre = item.Nombre;

                            result.Objects.Add(sucursal);
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
    }
}
