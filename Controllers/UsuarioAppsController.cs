using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi_YeisonAyalaR.Models;
using WebApi_YeisonAyalaR.AcessoDatos;

namespace WebApi_YeisonAyalaR.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioAppsController : ControllerBase
    {
        private readonly BdFacturacion_DbContext  _context;

        public UsuarioAppsController(BdFacturacion_DbContext context)
        {
            _context = context;
        }

        // GET: api/UsuarioApps
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UsuarioApp>>> GetUsuarioApp()
        {
            return await _context.Usuarios.ToListAsync();
        }

        // GET: api/UsuarioApps/1
        [HttpGet("{id}")]
        public async Task<ActionResult<UsuarioApp>> GetUsuarioApp(int id)
        {
            var usuarioApp = await _context.Usuarios.FindAsync(id);

            if (usuarioApp == null)
            {
                return NotFound();
            }

            return usuarioApp;
        }



        // POST: api/UsuarioApps
        [HttpPost]
        public async Task<ActionResult<string>> PostUsuarioApp(UsuarioApp usuarioApp)
        {
            try
            {
                if (string.IsNullOrEmpty(usuarioApp.Nombres) 
                    || string.IsNullOrEmpty(usuarioApp.Apellidos) 
                    || string.IsNullOrEmpty(usuarioApp.Usuario)
                    || string.IsNullOrEmpty(usuarioApp.Clave)
                    || usuarioApp.Estado == 0)
                {
                    throw new Exception("Todos los campos son obligatorios!!!!", new Exception("Todos los campos son obligatorios!!!!"));
                }
                else 
                {
                    usuarioApp.Clave = Ultilidades.MD5Hash(usuarioApp.Clave);
                
                    _context.Usuarios.Add(usuarioApp);
                    await _context.SaveChangesAsync();

                    return Ok(new { success = true, message = "Datos Guardas con exito!!!" }); ;
                
                }
            }
            catch (Exception ex)
            {

                return BadRequest(new { success = false, message = ex.InnerException.Message });
            }

        }

    }
}
