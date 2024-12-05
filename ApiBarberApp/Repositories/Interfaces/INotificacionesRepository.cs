using ApiBarberApp.Models;

namespace ApiBarberApp.Repositories.Interfaces
{
    public interface INotificacionesRepository
    {
        void RegistrarNotificacion(Notificacion notificacion);
        Task<List<Notificacion>> ConsultarNotificacionesNoLeidas(Guid idUsuario);
        void MarcarNotificacionesComoLeidas(Guid idUsuario);
    }
}
