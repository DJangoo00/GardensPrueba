using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configuration
{
    public class DetallePedidoConfiguration : IEntityTypeConfiguration<DetallePedido>
    {
        public void Configure(EntityTypeBuilder<DetallePedido> builder)
        {
            // Aquí puedes configurar las propiedades de la entidad Marca
            // utilizando el objeto 'builder'.
            builder.HasKey(e => new { e.IdPedido, e.IdProducto })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

            builder.ToTable("detalle_pedido");

            builder.HasIndex(e => e.IdProducto, "id_producto");

            builder.Property(e => e.IdPedido).HasColumnName("id_pedido");
            builder.Property(e => e.IdProducto)
                .HasMaxLength(15)
                .HasColumnName("id_producto");
                
            builder.Property(e => e.Cantidad).HasColumnName("cantidad");
            builder.Property(e => e.NumeroLinea).HasColumnName("numero_linea");
            builder.Property(e => e.PrecioUnidad)
                .HasPrecision(15, 2)
                .HasColumnName("precio_unidad");

            builder.HasOne(d => d.IdPedidoNavigation).WithMany(p => p.DetallePedidos)
                .HasForeignKey(d => d.IdPedido)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("detalle_pedido_ibfk_1");

            builder.HasOne(d => d.IdProductoNavigation).WithMany(p => p.DetallePedidos)
                .HasForeignKey(d => d.IdProducto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("detalle_pedido_ibfk_2");
        }
    }
}