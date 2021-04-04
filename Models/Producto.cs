using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi_YeisonAyalaR.Models
{
    [Table("Producto")]
    public class Producto
    {
        [Key]
        public int IdProducto { get; set; }
        public String NombreProducto { get; set; }
        public double PrecioUnitario { get; set; }
        public Int32 StockMin { get; set; }
        public Int32 StockMax { get; set; }
        public Int32 Saldo { get; set; }
    }
}
