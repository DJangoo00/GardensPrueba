using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configuration
{
    public class PagoConfiguration : IEntityTypeConfiguration<Pago>
    {
        public void Configure(EntityTypeBuilder<Pago> builder)
        {
            // Aquí puedes configurar las propiedades de la entidad Marca
            // utilizando el objeto 'builder'.
            builder.HasKey(e => new { e.IdCliente, e.Id })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

            builder.ToTable("pago");

            builder.Property(e => e.IdCliente).HasColumnName("id_cliente");
            builder.Property(e => e.Id)
                .HasMaxLength(50)
                .HasColumnName("id_transaccion");

            builder.Property(e => e.FechaPago).HasColumnName("fecha_pago");
            
            builder.Property(e => e.FormaPago)
                .IsRequired()
                .HasMaxLength(48)
                .HasColumnName("forma_pago");

            builder.Property(e => e.Total)
                .HasPrecision(15, 2)
                .HasColumnName("total");

            builder.HasOne(d => d.IdClienteNavigation).WithMany(p => p.Pagos)
                .HasForeignKey(d => d.IdCliente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("pago_ibfk_1");
        }
    }
}