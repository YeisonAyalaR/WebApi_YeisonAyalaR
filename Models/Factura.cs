using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi_YeisonAyalaR.Models
{
    [Table("Factura")]
    public class Factura
    {
        [Key]
        public Int32 IdFactura { get; set; }
        public String NoFactura { get; set; }
        public Int32 IdCliente { get; set; }
        public DateTime Fecha { get; set; }
        public Double Valor { get; set; }
        public Int32 IdUsuario { get; set; }
 
        public List<DetalleFactura> detalleFacturas { get; set; }
    }

    public class FromFactura
    {
        [Key]
        public Int32 IdFactura { get; set; }
        public String NoFactura { get; set; }
        public Int32 IdCliente { get; set; }
        public DateTime Fecha { get; set; }
        public Double Valor { get; set; }
        public Int32 IdUsuario { get; set; }
        [NotMapped]
        public List<FromDetalleFactura> detalleFacturas { get; set; }
    }
}
