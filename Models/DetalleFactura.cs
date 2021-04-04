using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi_YeisonAyalaR.Models
{
    [Table("DetalleFactura")]
    public class DetalleFactura
    {
        [Key]
        public Int32 IdFacturaDetalle { get; set; }
        public Int32 IdFactura { get; set; }
        public Int32 IdProducto { get; set; }
        public Int32 Cantidad { get; set; }
        public Double ValorUnitario { get; set; }
 
        public Double SubTotal { get; set; }

        [ForeignKey("IdFactura")]
        public Factura factura { get; set; }

    }
    public class FromDetalleFactura
    {
        [Key]
        public Int32 IdFacturaDetalle { get; set; }
        public Int32 IdFactura { get; set; }
        public Int32 IdProducto { get; set; }
        public Int32 Cantidad { get; set; }
        public Double ValorUnitario { get; set; }
        public Double SubTotal { get; set; }

    }
}
