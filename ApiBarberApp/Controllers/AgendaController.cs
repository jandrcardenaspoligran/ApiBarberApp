using ApiBarberApp.Models;
using ApiBarberApp.Repositories;
using ApiBarberApp.Repositories.Interfaces;
using ApiBarberApp.Services;
using ApiBarberApp.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ApiBarberApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class AgendaController : Controller
    {
        private IAgendaRepository _agendaRepository;
        private INotificacionesService _notificacionesService;
        public AgendaController(IAgendaRepository agendaRepository, INotificacionesService notificacionesService)
        {
            _agendaRepository = agendaRepository;
            _notificacionesService = notificacionesService;
        }

        [HttpGet("ConsultarAgendas")]
        public async Task<ActionResult<RespuestaGeneral<List<Agenda>>>> ConsultarAgendas()
        {
            RespuestaGeneral<List<Agenda>> res = new RespuestaGeneral<List<Agenda>>();
            try
            {
                List<Agenda> agendas = await _agendaRepository.ConsultarAgendas();
                res = new RespuestaGeneral<List<Agenda>>()
                {
                    estado = EstadosRespuesta.SUCCESS,
                    objeto = agendas
                };
            }
            catch (Exception ex)
            {
                res = new RespuestaGeneral<List<Agenda>>()
                {
                    mensaje = "Ocurrió un error.",
                    estado = EstadosRespuesta.ERROR,
                    error = ex.Message,
                    objeto = new List<Agenda>()
                };
            }
            return Ok(res);
        }


        [HttpGet("ConsultarMisCitas")]
        public async Task<ActionResult<RespuestaGeneral<List<Agenda>>>> ConsultarMisCitas()
        {
            IEnumerable<Claim> claims = User.Claims;
            RespuestaGeneral<List<Agenda>> res = new RespuestaGeneral<List<Agenda>>();
            try
            {
                Guid id = new Guid(claims.FirstOrDefault(c => c.Type == "Id")?.Value);
                List<Agenda> agendas = await _agendaRepository.ConsultarMisCitas(id);
                res = new RespuestaGeneral<List<Agenda>>()
                {
                    estado = EstadosRespuesta.SUCCESS,
                    objeto = agendas
                };
            }
            catch (Exception ex)
            {
                res = new RespuestaGeneral<List<Agenda>>()
                {
                    mensaje = "Ocurrió un error.",
                    estado = EstadosRespuesta.ERROR,
                    error = ex.Message,
                    objeto = new List<Agenda>()
                };
            }
            return Ok(res);
        }

        [HttpGet("ConsultarAgendaPorId/{id}")]
        public async Task<ActionResult<RespuestaGeneral<Agenda>>> ConsultarAgendas(Guid id)
        {
            RespuestaGeneral<Agenda> res = new RespuestaGeneral<Agenda>();
            try
            {
                Agenda agenda = await _agendaRepository.ConsultarAgendaPorId(id);
                res = new RespuestaGeneral<Agenda>()
                {
                    estado = EstadosRespuesta.SUCCESS,
                    objeto = agenda
                };
            }
            catch (Exception ex)
            {
                res = new RespuestaGeneral<Agenda>()
                {
                    mensaje = "Ocurrió un error.",
                    estado = EstadosRespuesta.ERROR,
                    error = ex.Message,
                    objeto = new Agenda()
                };
            }
            return Ok(res);
        }

        [HttpGet("ConsultarCitasAgendadasPorIdBarber")]
        public async Task<ActionResult<RespuestaGeneral<List<Agenda>>>> ConsultarCitasAgendadasPorIdBarber()
        {
            IEnumerable<Claim> claims = User.Claims;
            Guid idBarber = new Guid(claims.FirstOrDefault(c => c.Type == "Id")?.Value);
            RespuestaGeneral<List<Agenda>> res = new RespuestaGeneral<List<Agenda>>();
            try
            {
                List<Agenda> agendas = await _agendaRepository.ConsultarCitasAgendadasPorIdBarber(idBarber);
                res = new RespuestaGeneral<List<Agenda>>()
                {
                    estado = EstadosRespuesta.SUCCESS,
                    objeto = agendas
                };
            }
            catch (Exception ex)
            {
                res = new RespuestaGeneral<List<Agenda>>()
                {
                    mensaje = "Ocurrió un error.",
                    estado = EstadosRespuesta.ERROR,
                    error = ex.Message,
                    objeto = new List<Agenda>()
                };
            }
            return Ok(res);
        }

        [HttpGet("ConsultarAgendaFiltrada/{barber}&{estado}")]
        public async Task<ActionResult<RespuestaGeneral<List<Agenda>>>> ConsultarAgendaFiltrada(string barber = "", string estado = "")
        {
            RespuestaGeneral<List<Agenda>> res = new RespuestaGeneral<List<Agenda>>();
            try
            {
                List<Agenda> agendas = await _agendaRepository.ConsultarAgendasConFiltros(barber, estado);
                res = new RespuestaGeneral<List<Agenda>>()
                {
                    estado = EstadosRespuesta.SUCCESS,
                    objeto = agendas
                };
            }
            catch (Exception ex)
            {
                res = new RespuestaGeneral<List<Agenda>>()
                {
                    mensaje = "Ocurrió un error.",
                    estado = EstadosRespuesta.ERROR,
                    error = ex.Message,
                    objeto = new List<Agenda>()
                };
            }
            return Ok(res);
        }

        [HttpGet("ConsultarCitasDisponiblesPorIdBarber/{idBarber}")]
        public async Task<ActionResult<RespuestaGeneral<List<Agenda>>>> ConsultarCitasDisponiblesPorIdBarber(Guid idBarber)
        {
            RespuestaGeneral<List<Agenda>> res = new RespuestaGeneral<List<Agenda>>();
            try
            {
                List<Agenda> agendas = await _agendaRepository.ConsultarCitasDisponiblesPorIdBarber(idBarber);
                res = new RespuestaGeneral<List<Agenda>>()
                {
                    estado = EstadosRespuesta.SUCCESS,
                    objeto = agendas
                };
            }
            catch (Exception ex)
            {
                res = new RespuestaGeneral<List<Agenda>>()
                {
                    mensaje = "Ocurrió un error.",
                    estado = EstadosRespuesta.ERROR,
                    error = ex.Message + ((ex.InnerException != null) ? ex.InnerException.Message : ""),
                    objeto = new List<Agenda>()
                };
            }
            return Ok(res);
        }

        [HttpGet("ConsultarHistorialCitas")]
        public async Task<ActionResult<RespuestaGeneral<List<Agenda>>>> ConsultarHistorialCitas()
        {
            IEnumerable<Claim> claims = User.Claims;
            Guid idBarber = new Guid(claims.FirstOrDefault(c => c.Type == "Id")?.Value);
            RespuestaGeneral<List<Agenda>> res = new RespuestaGeneral<List<Agenda>>();
            try
            {
                List<Agenda> agendas = await _agendaRepository.ConsultarHistorialCitas(idBarber);
                res = new RespuestaGeneral<List<Agenda>>()
                {
                    estado = EstadosRespuesta.SUCCESS,
                    objeto = agendas
                };
            }
            catch (Exception ex)
            {
                res = new RespuestaGeneral<List<Agenda>>()
                {
                    mensaje = "Ocurrió un error.",
                    estado = EstadosRespuesta.ERROR,
                    error = ex.Message,
                    objeto = new List<Agenda>()
                };
            }
            return Ok(res);
        }

        [HttpPost("RegistrarCita")]
        public async Task<ActionResult<RespuestaGeneral<string>>> RegistrarCita([FromForm] Agenda agenda)
        {
            List<Guid> usuariosANotificar = new List<Guid>();
            RespuestaGeneral<string> res = new RespuestaGeneral<string>();
            IEnumerable<Claim> claims = User.Claims;
            if (agenda.ImgArchivo != null && agenda.ImgArchivo.Length > 0)
            {
                var extension = Path.GetExtension(agenda.ImgArchivo.FileName);
                var nombreArchivo = $"{Guid.NewGuid()}{extension}";

                var ruta = Path.Combine("C:\\BarberApp\\BarberApp\\src\\assets\\imgs\\", "referencias");
                Directory.CreateDirectory(ruta);

                var filePath = Path.Combine(ruta, nombreArchivo);

                // Guardar el archivo
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await agenda.ImgArchivo.CopyToAsync(stream);
                }
                agenda.ImgReferencia = nombreArchivo;
            }
            agenda.IdCliente = new Guid(claims.FirstOrDefault(c => c.Type == "Id")?.Value);
            agenda.EditadoPor = new Guid(claims.FirstOrDefault(c => c.Type == "Id")?.Value);
            agenda.FechaActualizacion = Fecha.Actual();
            agenda.Estado = EstadosAgenda.AGENDADA.ToString();
            usuariosANotificar = new List<Guid>() { new Guid(agenda.IdCliente.ToString()), new Guid(agenda.IdBarber.ToString()) };
            try
            {
                await _agendaRepository.RegistrarCita(agenda);
                _notificacionesService.Notificar(new Guid(agenda.IdCliente.ToString()), usuariosANotificar.Distinct().ToList(), agenda, agenda.Estado, "SOLICITUD");
                res = new RespuestaGeneral<string>()
                {
                    mensaje = "Registro de cita exitoso.",
                    estado = EstadosRespuesta.SUCCESS
                };
            }
            catch (GeneralException ex)
            {
                res = new RespuestaGeneral<string>()
                {
                    mensaje = ex.Message,
                    estado = EstadosRespuesta.FAILURE
                };
            }
            catch (Exception ex)
            {
                res = new RespuestaGeneral<string>()
                {
                    mensaje = "Ocurrió un error.",
                    estado = EstadosRespuesta.ERROR,
                    error = ex.Message + ((ex != null & ex.InnerException != null) ? ex.InnerException.Message : "")
                };
            }
            return Ok(res);
        }

        [HttpPost("ActualizarCita")]
        public async Task<ActionResult<RespuestaGeneral<string>>> ActualizarCita([FromForm] Agenda agenda)
        {
            List<Guid> usuariosANotificar = new List<Guid>();
            RespuestaGeneral<string> res = new RespuestaGeneral<string>();
            IEnumerable<Claim> claims = User.Claims;
            Guid idUsuario = new Guid(claims.FirstOrDefault(c => c.Type == "Id")?.Value);
            try
            {
                string estadoParaNotificacion = "";
                Agenda agendaActual = await _agendaRepository.ConsultarAgendaPorId(new Guid(agenda.Id.ToString()));
                if (agendaActual != null)
                {
                    if (agendaActual.IdCliente != null)
                        usuariosANotificar.Add(new Guid(agendaActual.IdCliente.ToString()));
                    usuariosANotificar.Add(new Guid(agendaActual.IdBarber.ToString()));
                }
                if(agenda.IdCliente != null) usuariosANotificar.Add(new Guid(agenda.IdCliente.ToString()));
                if (agenda.Estado != EstadosAgenda.CANCELADA.ToString() && agenda.Estado != EstadosAgenda.RECHAZADA.ToString())
                {
                    Agenda agendaNueva = new Agenda()
                    {
                        Id = agenda.Id,
                        EditadoPor = idUsuario,
                        FechaActualizacion = Fecha.Actual(),
                        Estado = agenda.Estado,
                        ObsBarber = agenda.ObsBarber,
                        MsgCliente = agenda.MsgCliente,
                        IdBarber = agenda.IdBarber,
                        IdCliente = agenda.IdCliente
                    };
                    if (agenda.ImgArchivo != null && agenda.ImgArchivo.Length > 0)
                    {
                        var extension = Path.GetExtension(agenda.ImgArchivo.FileName);
                        var nombreArchivo = $"{Guid.NewGuid()}{extension}";

                        var ruta = Path.Combine("C:\\BarberApp\\BarberApp\\src\\assets\\imgs\\", "referencias");
                        Directory.CreateDirectory(ruta);

                        var filePath = Path.Combine(ruta, nombreArchivo);

                        // Guardar el archivo
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await agenda.ImgArchivo.CopyToAsync(stream);
                        }
                        agendaNueva.ImgReferencia = nombreArchivo;
                    }
                    await _agendaRepository.ActualizarCita(agendaNueva);
                    res = new RespuestaGeneral<string>()
                    {
                        mensaje = "Actualización de cita exitosa.",
                        estado = EstadosRespuesta.SUCCESS
                    };
                    estadoParaNotificacion = agenda.Estado;
                }
                else if(agenda.Estado == EstadosAgenda.CANCELADA.ToString())
                {
                    Agenda agendaNueva = new Agenda()
                    {
                        Id = agenda.Id,
                        EditadoPor = idUsuario,
                        FechaActualizacion = Fecha.Actual(),
                        Estado = EstadosAgenda.DISPONIBLE.ToString(),
                        ObsBarber = null,
                        MsgCliente = null,
                        IdCliente = null,
                    };
                    await _agendaRepository.ActualizarCita(agendaNueva);
                    res = new RespuestaGeneral<string>()
                    {
                        mensaje = "La cita fue cancelada y eliminada de la agenda del cliente y se encuentra en el listado de citas disponibles..",
                        estado = EstadosRespuesta.SUCCESS
                    };
                    estadoParaNotificacion = "CANCELADA";
                }else if(agenda.Estado != EstadosAgenda.RECHAZADA.ToString())
                {
                    Agenda agendaNueva = new Agenda()
                    {
                        Id = agenda.Id,
                        EditadoPor = idUsuario,
                        FechaActualizacion = Fecha.Actual(),
                        Estado = EstadosAgenda.DISPONIBLE.ToString(),
                        ObsBarber = null,
                        MsgCliente = null,
                        IdCliente = null,
                    };
                    await _agendaRepository.ActualizarCita(agendaNueva);
                    res = new RespuestaGeneral<string>()
                    {
                        mensaje = "La cita ha sido rechazada, ya no se mostrará en las citas disponibles y se elimina de la agenda del cliente. Si desea habilitar la cita nuevamente, puede hacerlo desde el historial de citas siempre y cuando la fecha sea a futuro.",
                        estado = EstadosRespuesta.SUCCESS
                    };
                    estadoParaNotificacion = "FECHA RECHAZADA";
                }
                _notificacionesService.Notificar(idUsuario, usuariosANotificar.Distinct().ToList(), agenda, estadoParaNotificacion, "ACTUALIZACION");
            }
            catch (GeneralException ex)
            {
                res = new RespuestaGeneral<string>()
                {
                    mensaje = ex.Message,
                    estado = EstadosRespuesta.FAILURE
                };
            }
            catch (Exception ex)
            {
                res = new RespuestaGeneral<string>()
                {
                    mensaje = "Ocurrió un error.",
                    estado = EstadosRespuesta.ERROR,
                    error = ex.Message + ((ex != null & ex.InnerException != null) ? ex.InnerException.Message : "")
                };
            }
            return Ok(res);
        }
    }
}
