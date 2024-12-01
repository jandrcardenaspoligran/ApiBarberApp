using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiBarberApp.Models
{
    [Table("Agenda")]
    public class Agenda
    {
        [Key]
        [Column("id")]
        public Guid? Id { get; set; } = Guid.Empty;
        
        [Column("id_cliente")]
        public Guid? IdCliente { get; set; } = Guid.Empty;
        
        [Column("id_barber")]
        public Guid? IdBarber { get; set; } = Guid.Empty;
        
        [Column("fecha_hora")]
        public DateTime FechaHora { get; set; }
        
        [Column("msg_cliente")]
        public string? MsgCliente { get; set; }
        
        [Column("obs_barber")]
        public string? ObsBarber { get; set; }
        
        [Column("img_referencia")]
        public string? ImgReferencia { get; set; }
        
        [Column("estado")]
        public string? Estado {  get; set; }
        
        [Column("editado_por")]
        public Guid? EditadoPor {  get; set; }
        
        [Column("fecha_creacion")]
        public DateTime FechaCreacion { get; set; }
        
        [Column("fecha_actualizacion")]
        public DateTime FechaActualizacion {  get; set; }

        [ForeignKey("IdCliente")]
        public Usuario? Cliente { get; set; } 

        [ForeignKey("IdBarber")]
        public Usuario? Barber { get; set; }

        [ForeignKey("EditadoPor")]
        public Usuario? UsuarioEdicion { get; set; }

        [NotMapped]
        public IFormFile? ImgArchivo { get; set; }
    }

    public enum EstadosAgenda
    {
        DISPONIBLE,
        BLOQUEADA,
        AGENDADA,
        REAGENDADA,
        COMPLETADA,
        CANCELADA,
        RECHAZADA
    }
}
