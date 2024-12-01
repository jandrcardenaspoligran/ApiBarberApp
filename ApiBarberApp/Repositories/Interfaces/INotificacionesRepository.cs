using ApiBarberApp.Models;

namespace ApiBarberApp.Repositories.Interfaces
{
    public interface INotificacionesRepository
    {
        Task RegistrarNotificacion(Notificacion notificacion);
    }
}
