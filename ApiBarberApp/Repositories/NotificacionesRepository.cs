using ApiBarberApp.Data;
using ApiBarberApp.Models;
using ApiBarberApp.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ApiBarberApp.Repositories
{
    public class NotificacionesRepository : INotificacionesRepository
    {
        private readonly ApiBarberAppDbContext _context;
        public NotificacionesRepository(ApiBarberAppDbContext context)
        {
            _context = context;
        }

        public void RegistrarNotificacion(Notificacion notificacion)
        {
            _context.Add(notificacion);
            _context.SaveChanges();
        }

        public async Task<List<Notificacion>> ConsultarNotificacionesNoLeidas(Guid idUsuario)
        {
            List<Notificacion> notificacionesNoLeidas = await _context.Notificacion
                .Include(item => item.Usuario)
                .Where(item => item.Leido == 0 && item.Usuario.Id == idUsuario)
                .OrderByDescending(item => item.FechaHora)
                .ToListAsync();

            List<Notificacion> notificacionesLeidas = await _context.Notificacion
                .Include(item => item.Usuario)
                .Where(item => item.Leido == 1 && item.Usuario.Id == idUsuario)
                .OrderByDescending(item => item.FechaHora)
                .Take(10)
                .ToListAsync();

            var notificaciones = notificacionesNoLeidas
                .Concat(notificacionesLeidas)
                .OrderByDescending(n => n.FechaHora) // Reordenar si es necesario
                .ToList();
            return notificaciones;
        }

        public void MarcarNotificacionesComoLeidas(Guid idUsuario)
        {
            _context.Notificacion
                .Where(n => n.IdUsuario == idUsuario && n.Leido == 0)
                .ExecuteUpdate(s => s.SetProperty(n => n.Leido, 1));
        }
    }
}
