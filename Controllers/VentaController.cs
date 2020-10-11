using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WSVenta.Models;
using WSVenta.Models.Request;
using WSVenta.Models.Response;

namespace WSVenta.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class VentaController : ControllerBase
    {
        [HttpPost]
        public IActionResult Add(VentaRequest tablaventa)
        {
            Respuesta respuesta = new Respuesta();
            try
            {
                using (VentaRealContext db = new VentaRealContext())
                {

                    using (var transaction = db.Database.BeginTransaction()){
                        try{
                            var venta = new Venta();
                            venta.Total = tablaventa.Conceptos.Sum(b => b.Cantidad * b.PrecioUnitario);
                            venta.Fecha = DateTime.Now;
                            venta.IdCliente = tablaventa.IdCliente;
                            db.Venta.Add(venta);
                            db.SaveChanges();

                            foreach (var tablaconcepto in tablaventa.Conceptos)
                            {
                                var concepto = new Models.Concepto();
                                concepto.Cantidad = tablaconcepto.Cantidad;
                                concepto.IdProducto = tablaconcepto.IdProducto;
                                concepto.PrecioUnitario = tablaconcepto.PrecioUnitario;
                                concepto.Importe = tablaconcepto.Importe;
                                concepto.IdVenta = venta.Id;
                                db.Concepto.Add(concepto);
                                db.SaveChanges();
                            }
                            transaction.Commit();
                            respuesta.Exito = 1;
                        }catch(Exception)
                        {
                            transaction.Rollback();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                respuesta.Mensaje = ex.Message;
            }

            return Ok(respuesta);
        }
    }
}
