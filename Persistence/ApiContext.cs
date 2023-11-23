using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;

namespace Persistence;

public partial class ApiContext : DbContext
{
    public ApiContext()
    {
    }

    public ApiContext(DbContextOptions<ApiContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cliente> Clientes { get; set; }
    public virtual DbSet<DetallePedido> DetallesPedidos { get; set; }
    public virtual DbSet<Empleado> Empleados { get; set; }
    public virtual DbSet<GamaProducto> GamasProductos { get; set; }
    public virtual DbSet<Oficina> Oficinas { get; set; }
    public virtual DbSet<Pago> Pagos { get; set; }
    public virtual DbSet<Pedido> Pedidos { get; set; }
    public virtual DbSet<Producto> Productos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
