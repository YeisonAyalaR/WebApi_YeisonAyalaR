using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi_YeisonAyalaR.Models
{
    [Table("UsuarioApp")]
    public class UsuarioApp
    {
        [Key]
        public int IdUsuario { get; set; }
        public String Nombres { get; set; }
        public String Apellidos { get; set; }
        public String Usuario { get; set; }
        public String Clave { get; set; }
        public Int32 Estado { get; set; }

    }
}
