using ApiBarberApp.Data;
using ApiBarberApp.Models;
using ApiBarberApp.Repositories.Interfaces;

namespace ApiBarberApp.Repositories
{
    public class NotificacionesRepository: INotificacionesRepository
    {
        private readonly ApiBarberAppDbContext _context;
        public NotificacionesRepository(ApiBarberAppDbContext context)
        {
            _context = context;
        }

        public async Task RegistrarNotificacion(Notificacion notificacion)
        {
            await _context.AddAsync(notificacion);
            await _context.SaveChangesAsync();
        }
    }
}
