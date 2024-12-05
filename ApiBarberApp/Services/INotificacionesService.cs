using ApiBarberApp.Models;

namespace ApiBarberApp.Services
{
    public interface INotificacionesService
    {
        void Notificar(Guid idUsuarioAcciona, List<Guid> usuariosANotificar, Agenda agenda, string estado, string tipoNotificacion);
    }
}
