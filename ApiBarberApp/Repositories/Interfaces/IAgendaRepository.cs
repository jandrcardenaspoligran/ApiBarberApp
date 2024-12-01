using ApiBarberApp.Models;

namespace ApiBarberApp.Repositories.Interfaces
{
    public interface IAgendaRepository
    {
        Task<List<Agenda>> ConsultarAgendas();
        Task<Agenda?> ConsultarAgendaPorId(Guid id);
        Task<List<Agenda>> ConsultarAgendasConFiltros(string barber, string estado);
        Task<List<Agenda>> ConsultarCitasAgendadasPorIdBarber(Guid idBarber);
        Task<List<Agenda>> ConsultarCitasDisponiblesPorIdBarber(Guid idBarber);
        Task<List<Agenda>> ConsultarHistorialCitas(Guid idBarber);
        Task<List<Agenda>> ConsultarMisCitas(Guid id);
        Task RegistrarCita(Agenda agenda);
        Task ActualizarCita(Agenda agenda);
    }
}
