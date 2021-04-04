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
    public class ProductosController : ControllerBase
    {
        private readonly BdFacturacion_DbContext _context;

        public ProductosController(BdFacturacion_DbContext context)
        {
            _context = context;
        }

        //api/Productos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Producto>>> GetProducto()
        {
            return await _context.Productos.ToListAsync();
        }

        //api/CrearProducto
        [Route("CrearProducto")]
        [HttpPost]
        public async Task<ActionResult<string>> CrearProducto(Producto producto)
        {
            try
            {
                if (string.IsNullOrEmpty(producto.NombreProducto) || producto.PrecioUnitario == 0 )
                {
                    throw new Exception("Los campos NombreProducto,  PrecioUnitario son obligatorios!!!!", new Exception("Los campos NombreProducto,  PrecioUnitario son obligatorios!!!!"));
                }
                else if (producto.StockMin <= 5 || producto.StockMax == 0 || producto.Saldo == 0)
                {
                    throw new Exception("El campo StockMin no puede ser menor a 5 !!!", new Exception("El campo StockMin no puede ser menor a 5 !!!"));
                }
                else if ( producto.StockMax <= 5)
                {
                    throw new Exception("El campo StockMax debe ser mayor a 5 !!!", new Exception("El campo StockMax debe ser mayor a 5 !!!"));
                }
                else if (producto.Saldo == 0)
                {
                    throw new Exception("El campo Saldo no puede ser igual a cero !!!", new Exception("El campo Saldo no puede ser igual a cero !!!"));
                }
                else
                {
                    _context.Productos.Add(producto);
                    await _context.SaveChangesAsync();

                    return Ok(new { success = true, message = "Producto creado con exito!!!" }); ;
                }
            }
            catch (Exception ex)
            {

                return BadRequest(new { success = false, message = ex.InnerException.Message });
            }
        }

        //api/CrearProducto
        [Route("ModificarProducto")]
        [HttpPost]
        public async Task<ActionResult<string>> ModificarProducto(Producto producto)
        {
            try
            {
                var exitos = await _context.Productos.FindAsync(producto.IdProducto);

                if(exitos == null)
                {
                    throw new Exception("El producto " + producto.NombreProducto +" no existe en el sistemas !!!", new Exception("El producto " + producto.NombreProducto + " no existe en el sistemas !!!"));
                }
                else if (string.IsNullOrEmpty(producto.NombreProducto) || producto.PrecioUnitario == 0)
                {
                    throw new Exception("Los campos NombreProducto,  PrecioUnitario son obligatorios!!!!", new Exception("Los campos NombreProducto,  PrecioUnitario son obligatorios!!!!"));
                }
                else if (producto.StockMin <= 5 || producto.StockMax == 0 || producto.Saldo == 0)
                {
                    throw new Exception("El campo StockMin no puede ser menor a 5 !!!", new Exception("El campo StockMin no puede ser menor a 5 !!!"));
                }
                else if (producto.StockMax <= 5)
                {
                    throw new Exception("El campo StockMax debe ser mayor a 5 !!!", new Exception("El campo StockMax debe ser mayor a 5 !!!"));
                }
                else if (producto.Saldo == 0)
                {
                    throw new Exception("El campo Saldo no puede ser igual a cero !!!", new Exception("El campo Saldo no puede ser igual a cero !!!"));
                }
                else
                {
                    exitos.NombreProducto = producto.NombreProducto;
                    exitos.PrecioUnitario = producto.PrecioUnitario;
                    exitos.StockMin = producto.StockMin;
                    exitos.StockMax = producto.StockMax;
                    exitos.Saldo = producto.Saldo;

                    await _context.SaveChangesAsync();

                    return Ok(new { success = true, message = "Producto Modificado con exito!!!" }); ;
                }
            }
            catch (Exception ex)
            {

                return BadRequest(new { success = false, message = ex.InnerException.Message });
            }
        }

        //api/EliminaProducto
        [Route("EliminaProducto")]
        [HttpPost]
        public async Task<ActionResult<string>> EliminaProducto(int id)
        {
            try
            {
                var producto = await _context.Productos.FindAsync(id);
                var producFactura = (from fd in _context.DetalleFacturas
                                     where fd.IdProducto == id
                                     select fd).FirstOrDefault();
                if (producto == null)
                {
                    throw new Exception("Producto no encontrado !!!", new Exception("Producto no encontrado !!!"));
                }
                else if (producFactura != null)
                {
                    throw new Exception("Producto no puede ser eliminado por que se encuentra en facturas !!!", new Exception("Producto no puede ser eliminado por que se encuentra en facturas !!!"));
                }
                else
                {

                    _context.Productos.Remove(producto);
                    await _context.SaveChangesAsync();

                    return Ok(new { success = true, message = "Producto Eliminado con exito!!!" }); 
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.InnerException.Message });
            }
        }
    }
}
