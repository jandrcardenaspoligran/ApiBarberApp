using ApiBarberApp.Models;
using ApiBarberApp.Repositories.Interfaces;
using ApiBarberApp.Utilities;

namespace ApiBarberApp.Services
{
    public class NotificacionesService: INotificacionesService
    {
        private INotificacionesRepository _notificacionesRepository;
        private IAgendaRepository _agendaRepository;
        private IUsuariosRepository _usuariosRepository;
        public NotificacionesService(INotificacionesRepository notificacionesRepository, IAgendaRepository agendaRepository, IUsuariosRepository usuariosRepository)
        {
            _notificacionesRepository = notificacionesRepository;
            _agendaRepository = agendaRepository;
            _usuariosRepository = usuariosRepository;
        }

        public void Notificar(Guid idUsuarioAcciona, List<Guid> usuariosANotificar, Agenda agenda, string estado, string tipoNotificacion)
        {
            Guid idAgenda = new Guid(agenda.Id.ToString());
            Agenda infoAgenda = _agendaRepository.ConsultarAgendaPorId(idAgenda).Result;
            Usuario infoUsuario = _usuariosRepository.ConsultarUsuarioPorId(idUsuarioAcciona).Result;
            Notificacion notificacion = new Notificacion()
            {
                FechaCreacion = Fecha.Actual().AddHours(-1),
                FechaHora = Fecha.Actual().AddHours(-1),
                Leido = 0
            };
            switch (tipoNotificacion)
            {
                case "ACTUALIZACION":
                    notificacion.Titulo = "¡Actualización de cita!";
                    notificacion.Mensaje = $"Se ha realizado una actualización a la cita del día {infoAgenda.FechaCreacion.AddHours(-6).ToString("g")} por {infoUsuario.Nombre + " " + infoUsuario.Apellidos}. El estado de la cita quedó definido como: {infoAgenda.Estado.ToUpper()}. Observaciones del Barber: {agenda.ObsBarber}, Mensaje del cliente: {agenda.MsgCliente}";
                    break;
                case "SOLICITUD":
                    notificacion.Titulo = "¡Cita agendada!";
                    notificacion.Mensaje = $"Agendada la cita para el día: {infoAgenda.FechaCreacion.AddHours(-6).ToString("g")} por {infoUsuario.Nombre + " " + infoUsuario.Apellidos}.";
                    break;
            }
            foreach (var item in usuariosANotificar)
            {
                notificacion.Id = Guid.NewGuid();
                notificacion.IdUsuario = item;
                _notificacionesRepository.RegistrarNotificacion(notificacion);
            }
        }
    }
}
