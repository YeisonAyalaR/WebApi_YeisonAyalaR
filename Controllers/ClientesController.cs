using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi_YeisonAyalaR.AcessoDatos;
using WebApi_YeisonAyalaR.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi_YeisonAyalaR.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly BdFacturacion_DbContext _context;

        public ClientesController(BdFacturacion_DbContext context)
        {
            _context = context;
        }

        //api/Clientes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cliente>>> GetCliente()
        {
            return await _context.Clientes.ToListAsync();
        }

        //api/CrearCliente
        [Route("CrearCliente")]
        [HttpPost]
        public async Task<ActionResult<string>> CrearCliente(Cliente cliente)
        {
            try
            {
                if (string.IsNullOrEmpty(cliente.Nombres) 
                    || string.IsNullOrEmpty(cliente.Apellidos)
                    || string.IsNullOrEmpty(cliente.Direccion)
                    || string.IsNullOrEmpty(cliente.Celular)
                    || string.IsNullOrEmpty(cliente.Correo)
                    || cliente.FechaNacimiento == null)
                {
                    throw new Exception("Todos los campos son obligatorios!!!!", new Exception("Todos los campos son obligatorios!!!!"));
                }
                else
                {
                    _context.Clientes.Add(cliente);
                    await _context.SaveChangesAsync();

                    return Ok(new { success = true, message = "Cliente creado con exito!!!" }); ;
                }
            }
            catch (Exception ex)
            {

                return BadRequest(new { success = false, message = ex.InnerException.Message });
            }
        }

        //api/CrearProducto
        [Route("ModificarCliente")]
        [HttpPost]
        public async Task<ActionResult<string>> ModificarCliente(Cliente cliente)
        {
            try
            {
                var exitos = await _context.Clientes.FindAsync(cliente.IdCliente);

                if (exitos == null)
                {
                    throw new Exception("El cliente con id " + cliente.IdCliente + " no existe en el sistemas !!!", new Exception("El cliente con id " + cliente.IdCliente + " no existe en el sistemas !!!"));
                }
                else if (string.IsNullOrEmpty(cliente.Nombres)
                      || string.IsNullOrEmpty(cliente.Apellidos)
                      || string.IsNullOrEmpty(cliente.Direccion)
                      || string.IsNullOrEmpty(cliente.Celular)
                      || string.IsNullOrEmpty(cliente.Correo)
                      || cliente.FechaNacimiento == null)
                {
                    throw new Exception("Todos los campos son obligatorios!!!!", new Exception("Todos los campos son obligatorios!!!!"));
                }
                else
                {
                    exitos.Nombres = cliente.Nombres;
                    exitos.Apellidos = cliente.Apellidos;
                    exitos.Direccion = cliente.Direccion;
                    exitos.Celular = cliente.Celular;
                    exitos.Correo = cliente.Correo;
                    exitos.FechaNacimiento = cliente.FechaNacimiento;

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
        public async Task<ActionResult<string>> EliminaCliente(int id)
        {
            try
            {
                var cliente = await _context.Clientes.FindAsync(id);
                var clienteFactura = (from f in _context.Facturas
                                     where f.IdCliente == id
                                     select f).FirstOrDefault();
                if (cliente == null)
                {
                    throw new Exception("Cliente no encontrado !!!", new Exception("Clienete no encontrado !!!"));
                }
                else if (clienteFactura != null)
                {
                    throw new Exception("El cliente no puede ser eliminado por que se encuentra en facturas !!!", new Exception("El cliente no puede ser eliminado por que se encuentra en facturas !!!"));
                }
                else
                {

                    _context.Clientes.Remove(cliente);
                    await _context.SaveChangesAsync();

                    return Ok(new { success = true, message = "Cliente Eliminado con exito!!!" });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.InnerException.Message });
            }
        }
    }
}
