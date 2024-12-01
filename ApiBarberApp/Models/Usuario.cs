using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiBarberApp.Models
{
    [Table("Usuarios")]
    public class Usuario
    {
        [Key]
        [Column("id")]
        public Guid Id { get; set; } = Guid.Empty;

        [Column("correo")]
        public string Correo { get; set; }

        [Column("clave")]
        public string Clave { get; set; }

        [Column("nombre")]
        public string Nombre { get; set; }

        [Column("apellidos")]
        public string Apellidos { get; set; }

        [Column("genero")]
        public string Genero { get; set; }

        [Column("fecha_nacimiento")]
        public DateTime FechaNacimiento { get; set; }

        [Column("id_rol")]
        public Guid IdRol { get; set; } //No se puede actualizar

        [Column("activo")]
        public byte Activo { get; set; }

        [Column("fecha_creacion")]
        public DateTime FechaCreacion { get; set; } //No se puede actualizar

        [Column("fecha_actualizacion")]
        public DateTime FechaActualizacion { get; set; } //Siempre debe ser actualizada 

        [ForeignKey("IdRol")]
        public Rol? Rol { get; set; }
    }
    public class UsuarioConRol
    {
        public Guid Id { get; set; }
        public string Correo { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public string Genero { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public bool Activo { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaActualizacion { get; set; }

        // Propiedades de Rol
        public string RolNombre { get; set; }
        public string RolDescripcion { get; set; }
    }
}
