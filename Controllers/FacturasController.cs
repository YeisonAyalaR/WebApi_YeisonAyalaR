using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi_YeisonAyalaR.AcessoDatos;
using WebApi_YeisonAyalaR.Models;

namespace WebApi_YeisonAyalaR.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacturasController : ControllerBase
    {
        private readonly BdFacturacion_DbContext _context;

        public FacturasController(BdFacturacion_DbContext context)
        {
            _context = context;
        }

        //api/Facturas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Factura>>> GetFactura()
        {
            return await _context.Facturas.ToListAsync();
        }

        //api/Facturas/5
        [HttpGet("{id}")]
        public ActionResult<FromFactura> GetFactura(int id)
        {
            try
            {
                FromFactura factura = new FromFactura();
                factura = (from f in _context.Facturas
                           where f.IdFactura == id
                           select new FromFactura
                           {
                               IdFactura = f.IdFactura,
                               IdCliente = f.IdCliente,
                               IdUsuario = f.IdUsuario,
                               Fecha = f.Fecha,
                               Valor = f.Valor,
                               NoFactura = f.NoFactura
                           }).FirstOrDefault();



                var detfactura = (from df in _context.DetalleFacturas
                                  where df.IdFactura == id
                                  select new FromDetalleFactura
                                  {
                                      IdFacturaDetalle = df.IdFacturaDetalle,
                                      IdFactura = df.IdFactura,
                                      IdProducto = df.IdProducto,
                                      Cantidad = df.Cantidad,
                                      ValorUnitario = df.ValorUnitario,
                                      SubTotal = df.SubTotal

                                  }).ToList();
                if (factura == null)
                {
                    throw new Exception("Factura no encontrada !!!", new Exception("Factura no encontrada !!!"));
                }

                factura.detalleFacturas = new List<FromDetalleFactura>();

                foreach (var item in detfactura)
                {
                    factura.detalleFacturas.Add(item);
                }

                return factura;
            }
            catch (Exception ex)
            {

                return BadRequest(new { success = false, message = ex.InnerException.Message });
            }

        }

        //api/CrearFactura
        [Route("CrearFactura")]
        [HttpPost]
        public async Task<ActionResult<Factura>> CrearFactura(Factura factura)
        {
            try
            {
                if (string.IsNullOrEmpty(factura.NoFactura) || factura.Fecha == null || factura.IdCliente == 0 || factura.Valor == 0 || factura.IdUsuario == 0)
                {
                    throw new Exception("Todos los campos son obligatorios!!!!", new Exception("Todos los campos son obligatorios!!!!"));
                }
                else if (factura.detalleFacturas.Count == 0)
                {
                    throw new Exception("La factura por lo menos debe contener un producto !!!", new Exception("La factura por lo menos debe contener un producto !!!"));
                }
                else
                {
                    _context.Facturas.Add(factura);

                    foreach (var item in factura.detalleFacturas)
                    {
                        if (item.IdProducto == 0)
                        {
                            throw new Exception("Producto no valido o no ha selecionado ninguno producto del sistema !!!", new Exception("Producto no valido o no ha selecionado ninguno producto del sistema !!!"));
                        }
                        else
                        {
                            _context.DetalleFacturas.Add(item);
                        }
                    }
                    
                    await _context.SaveChangesAsync();

                    return Ok(new { success = true, message = "Factura creada con exito!!!" }); ;
                }
            }
            catch (Exception ex)
            {

                return BadRequest(new { success = false, message = ex.InnerException.Message });
            }
        }

        //api/EliminarFactura
        [Route("EliminarFactura")]
        [HttpPost]
        public async Task<ActionResult<string>> EliminarFactura(int id)
        {
            try
            {
                var factura = await _context.Facturas.FindAsync(id);

                if (factura == null)
                {
                    throw new Exception("Factura no encontrada !!!", new Exception("Factura no encontrada !!!"));
                }

                _context.Facturas.Remove(factura);
                await _context.SaveChangesAsync();

                return Ok(new { success = true, message = "Factura Eliminado con exito!!!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.InnerException.Message });
            }
        }

    }
}
