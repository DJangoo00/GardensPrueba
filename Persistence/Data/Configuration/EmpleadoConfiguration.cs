using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configuration
{
    public class EmpleadoConfiguration : IEntityTypeConfiguration<Empleado>
    {
        public void Configure(EntityTypeBuilder<Empleado> builder)
        {
            // Aquí puedes configurar las propiedades de la entidad Marca
            // utilizando el objeto 'builder'.
            builder.HasKey(e => e.Id).HasName("PRIMARY");

            builder.ToTable("empleado");

            builder.HasIndex(e => e.IdJefe, "id_jefe");

            builder.HasIndex(e => e.IdOficina, "id_oficina");

            builder.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
                
            builder.Property(e => e.Apellido2)
                .HasMaxLength(58)
                .HasColumnName("apellido2");

            builder.Property(e => e.Apellidol)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("apellidol");

            builder.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("email");

            builder.Property(e => e.Extension)
                .IsRequired()
                .HasMaxLength(10)
                .HasColumnName("extension");

            builder.Property(e => e.IdJefe).HasColumnName("id_jefe");
            builder.Property(e => e.IdOficina)
                .IsRequired()
                .HasMaxLength(10)
                .HasColumnName("id_oficina");

            builder.Property(e => e.Nombre)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("nombre");

            builder.Property(e => e.Puesto)
                .HasMaxLength(50)
                .HasColumnName("puesto");

            builder.HasOne(d => d.IdJefeNavigation).WithMany(p => p.InverseIdJefeNavigation)
                .HasForeignKey(d => d.IdJefe)
                .HasConstraintName("empleado_ibfk_2");

            builder.HasOne(d => d.IdOficinaNavigation).WithMany(p => p.Empleados)
                .HasForeignKey(d => d.IdOficina)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("empleado_ibfk_1");
        }
    }
}