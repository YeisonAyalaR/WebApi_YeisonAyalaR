using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebApi_YeisonAyalaR.Models;
using System.Data.Entity.Infrastructure;

namespace WebApi_YeisonAyalaR.AcessoDatos
{
    public class BdFacturacion_DbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var builder = new ConfigurationBuilder()
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.json");

            var configution = builder.Build();

            optionsBuilder.UseSqlServer(configution["ConnectionStrings:bdfacturacion"]);

        }
        public BdFacturacion_DbContext(DbContextOptions<BdFacturacion_DbContext> o) : base(o) { }

        public DbSet<UsuarioApp> Usuarios { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<DetalleFactura> DetalleFacturas { get; set; }
        public DbSet<WebApi_YeisonAyalaR.Models.Factura> Factura { get; set; }
    }
}
