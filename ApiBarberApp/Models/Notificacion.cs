using ApiBarberApp.Utilities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiBarberApp.Models
{
    public class Notificacion
    {
        [Key]
        [Column("id")]
        public Guid Id { get; set; } = Guid.Empty;

        [Column("id_usuario")]
        public Guid IdUsuario { get; set; } = Guid.Empty;

        [Column("fecha_hora")]
        public DateTime FechaHora { get; set; } = Fecha.Actual();

        [Column("mensaje")]
        public string? Mensaje { get; set; }

        [Column("leido")]
        public int Leido { get; set; }

        [Column("fecha_creacion")]
        public DateTime FechaCreacion { get; set; } = Fecha.Actual();

        [ForeignKey("IdUsuario")]
        public Usuario? Usuario { get; set; }

    }
}
