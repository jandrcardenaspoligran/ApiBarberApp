using Microsoft.EntityFrameworkCore;
using ApiBarberApp.Models;
using ApiBarberApp.Utilities;

namespace ApiBarberApp.Data
{
    public class ApiBarberAppDbContext : DbContext
    {
        public ApiBarberAppDbContext(DbContextOptions<ApiBarberAppDbContext> options)
            : base(options)
        {
        }

        // Definición de DbSets para las tablas Usuarios y Roles
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Rol> Roles { get; set; }
        public DbSet<Agenda> Agenda { get; set; }
        public DbSet<Notificacion> Notificacion { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            /*** Tabla: Usuarios ***/
            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.ToTable("Usuarios", "BarberApp");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                    .HasColumnName("id");

                entity.Property(e => e.Correo)
                    .HasColumnName("correo");

                entity.Property(e => e.Clave)
                    .HasColumnName("clave");

                entity.Property(e => e.Nombre)
                    .HasColumnName("nombre");

                entity.Property(e => e.Apellidos)
                    .HasColumnName("apellidos");

                entity.Property(e => e.Genero)
                    .HasColumnName("genero");

                entity.Property(e => e.FechaNacimiento)
                    .HasColumnName("fecha_nacimiento");

                entity.Property(e => e.IdRol)
                    .HasColumnName("id_rol");

                entity.Property(e => e.Activo)
                    .HasColumnName("activo");

                entity.Property(e => e.FechaCreacion)
                    .HasColumnName("fecha_creacion");

                entity.Property(e => e.FechaActualizacion)
                    .HasColumnName("fecha_actualizacion");

                /*** Relación con tabla "Roles" ***/
                entity.HasOne(e => e.Rol)
                      .WithMany()
                      .HasForeignKey(e => e.IdRol)
                      .HasConstraintName("FK_Usuarios_Roles");
            });

            /*** Tabla: Roles ***/
            modelBuilder.Entity<Rol>(entity =>
            {
                entity.ToTable("Roles", "BarberApp");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                    .HasColumnName("id");

                entity.Property(e => e.Nombre)
                    .HasColumnName("nombre")
                    .HasMaxLength(256);

                entity.Property(e => e.Descripcion)
                    .HasColumnName("descripcion")
                    .HasMaxLength(512);

                entity.Property(e => e.FechaCreacion)
                    .HasColumnName("fecha_creacion");

                entity.Property(e => e.FechaActualizacion)
                    .HasColumnName("fecha_actualizacion");
            });

            /*** Tabla: Agenda ***/
            modelBuilder.Entity<Agenda>(entity =>
            {
                entity.ToTable("Agenda", "BarberApp");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                    .HasColumnName("id").
                    IsRequired();

                entity.Property(e => e.IdCliente)
                    .HasColumnName("id_cliente");

                entity.Property(e => e.IdBarber)
                    .HasColumnName("id_barber");

                entity.Property(e => e.FechaHora)
                    .HasColumnName("fecha_hora");

                entity.Property(e => e.MsgCliente)
                    .HasColumnName("msg_cliente");

                entity.Property(e => e.ObsBarber)
                    .HasColumnName("obs_barber");

                entity.Property(e => e.ImgReferencia)
                    .HasColumnName("img_referencia");

                entity.Property(e => e.Estado)
                    .HasColumnName("estado");

                entity.Property(e => e.EditadoPor)
                    .HasColumnName("editado_por");

                entity.Property(e => e.FechaCreacion)
                    .HasColumnName("fecha_creacion");

                entity.Property(e => e.FechaActualizacion)
                    .HasColumnName("fecha_actualizacion");

                /*** Relaciones con tabla "Usuarios" ***/
                entity.HasOne(e => e.Cliente)
                      .WithMany()
                      .HasForeignKey(e => e.IdCliente)
                      .HasConstraintName("FK_Agenda_Usuario_Cliente");

                entity.HasOne(e => e.Barber)
                      .WithMany()
                      .HasForeignKey(e => e.IdBarber)
                      .HasConstraintName("FK_Agenda_Usuario_Barber");

                entity.HasOne(e => e.UsuarioEdicion)
                      .WithMany()
                      .HasForeignKey(e => e.EditadoPor)
                      .HasConstraintName("FK_Agenda_Usuario_Editado");
            });

            modelBuilder.Entity<Notificacion>(entity =>
            {
                entity.ToTable("Notificaciones", "BarberApp");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                    .HasColumnName("id");

                entity.Property(e => e.IdUsuario)
                    .HasColumnName("id_usuario");

                entity.Property(e => e.FechaHora)
                    .HasColumnName("fecha_hora");

                entity.Property(e => e.Titulo)
                    .HasColumnName("titulo");

                entity.Property(e => e.Mensaje)
                    .HasColumnName("mensaje");

                entity.Property(e => e.Leido)
                    .HasColumnName("leido");

                entity.Property(e => e.FechaCreacion)
                    .HasColumnName("fecha_creacion");

                /*** Relación con tabla "Usuarios" ***/
                entity.HasOne(e => e.Usuario)
                    .WithMany()
                    .HasForeignKey(e => e.IdUsuario)
                    .HasConstraintName("FK_Notificaciones_Usuario");
            });
        }
    }
}
