using ApiBarberApp.Data;
using ApiBarberApp.Models;
using ApiBarberApp.Repositories.Interfaces;
using ApiBarberApp.Utilities;
using Microsoft.EntityFrameworkCore;

namespace ApiBarberApp.Repositories
{
    public class AgendaRepository: IAgendaRepository
    {
        private readonly ApiBarberAppDbContext _context;

        public AgendaRepository(ApiBarberAppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Agenda>> ConsultarAgendas()
        {
            return await _context.Agenda
                .Include(item => item.Cliente)
                .Include(item => item.Barber)
                .Include(item => item.UsuarioEdicion)
                .OrderByDescending(item => item.FechaHora)
                .ToListAsync();
        }

        public async Task<Agenda?> ConsultarAgendaPorId(Guid id)
        {
            return await _context.Agenda
                .Include(item => item.Cliente)
                .Include(item => item.Barber)
                .Include(item => item.UsuarioEdicion)
                .Where(item => item.Id == id)
                .OrderByDescending(item => item.FechaHora)
                .FirstOrDefaultAsync();
        }

        public async Task<List<Agenda>> ConsultarAgendasConFiltros(string barber = "", string estado = "")
        {
            return await _context.Agenda
                .Include(item => item.Cliente)
                .Include(item => item.Barber)
                .Include(item => item.UsuarioEdicion)
                .Where(item => (item.Barber.Nombre+" "+item.Barber.Apellidos).Contains(barber) || item.Estado.Contains(estado))
                .OrderByDescending(item => item.FechaHora)
                .ToListAsync();
        }

        public async Task<List<Agenda>> ConsultarCitasAgendadasPorIdBarber(Guid idBarber)
        {
            DateTime fecha = Fecha.Actual();
            List<Agenda> agenda = await _context.Agenda
                .Include(item => item.Cliente)
                .Include(item => item.Barber)
                .Include(item => item.UsuarioEdicion)
                .Where(item => item.IdBarber == idBarber && (item.Estado == EstadosAgenda.AGENDADA.ToString() || item.Estado == EstadosAgenda.REAGENDADA.ToString()) && item.Barber.Activo == 1 && item.FechaHora >= fecha)
                .OrderByDescending(item => item.FechaHora)
                .ToListAsync();
            return agenda == null ? new List<Agenda>() : agenda;
        }

        public async Task<List<Agenda>> ConsultarCitasDisponiblesPorIdBarber(Guid idBarber)
        {
            DateTime fecha = Fecha.Actual();
            List<Agenda> agenda = await _context.Agenda
                .Include(item => item.Barber)
                .Where(item => item.IdBarber == idBarber && item.Estado == "DISPONIBLE" && item.Barber.Activo == 1 && item.FechaHora > fecha)
                .OrderBy(item => item.FechaHora)
                .ToListAsync();
            return agenda == null ? new List<Agenda>() : agenda;
        }

        public async Task<List<Agenda>> ConsultarHistorialCitas(Guid idBarber)
        {
            List<Agenda> agenda = await _context.Agenda
                .Include(item => item.Cliente)
                .Include(item => item.UsuarioEdicion)
                .Include(item => item.Barber)
                .Where(item => item.IdBarber == idBarber && item.Barber.Activo == 1)
                .OrderByDescending(item => item.FechaHora)
                .ToListAsync();
            return agenda == null ? new List<Agenda>() : agenda;
        }

        public async Task<List<Agenda>> ConsultarMisCitas(Guid id)
        {
            List<Agenda> agenda = await _context.Agenda
                .Include(item => item.Cliente)
                .Include(item => item.Barber)
                .Include(item => item.UsuarioEdicion)
                .Where(item => item.IdCliente == id)
                .OrderBy(item => item.FechaHora)
                .ToListAsync();
            return agenda == null ? new List<Agenda>() : agenda;
        }

        public async Task RegistrarCita(Agenda agenda)
        {
            _context.Agenda.Update(agenda);
            await _context.SaveChangesAsync();
        }

        public async Task ActualizarCita(Agenda agenda)
        {
            Agenda citaActual = await _context.Agenda.FindAsync(agenda.Id);
            citaActual.MsgCliente = agenda.MsgCliente;
            citaActual.EditadoPor = agenda.EditadoPor;
            citaActual.Estado = agenda.Estado;
            citaActual.ObsBarber = agenda.ObsBarber;
            citaActual.FechaActualizacion = agenda.FechaActualizacion;
            citaActual.IdCliente = agenda.IdCliente;
            _context.Agenda.Update(citaActual);
            await _context.SaveChangesAsync();
        }
    }
}
