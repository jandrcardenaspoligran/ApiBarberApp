using ApiBarberApp.Models;
using ApiBarberApp.Repositories;
using ApiBarberApp.Repositories.Interfaces;
using ApiBarberApp.Services;
using ApiBarberApp.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ApiBarberApp.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class NotificacionesController : ControllerBase
    {
        private readonly INotificacionesRepository _notificacionesRepository;
        private readonly INotificacionesService _notificacionesService;

        public NotificacionesController(INotificacionesRepository notificacionesRepository, INotificacionesService notificacionesService)
        {
            _notificacionesRepository = notificacionesRepository;
            _notificacionesService = notificacionesService;
        }

        [HttpGet("Consultar")]
        public async Task<ActionResult<RespuestaGeneral<List<Notificacion>>>> ConsultarNotificacions()
        {
            IEnumerable<Claim> claims = User.Claims;
            Guid usuario = new Guid(claims.FirstOrDefault(c => c.Type == "Id")?.Value);
            RespuestaGeneral<List<Notificacion>> res = new RespuestaGeneral<List<Notificacion>>();
            try
            {
                List<Notificacion> agendas = await _notificacionesRepository.ConsultarNotificacionesNoLeidas(usuario);
                res = new RespuestaGeneral<List<Notificacion>>()
                {
                    estado = EstadosRespuesta.SUCCESS,
                    objeto = agendas
                };
                for (int i = 0; i < agendas.Count(); i++)
                {
                    agendas[i].FechaHora = agendas[i].FechaHora.AddHours(-5);
                }
            }
            catch (Exception ex)
            {
                res = new RespuestaGeneral<List<Notificacion>>()
                {
                    mensaje = "Ocurrió un error.",
                    estado = EstadosRespuesta.ERROR,
                    error = ex.Message,
                    objeto = new List<Notificacion>()
                };
            }
            return Ok(res);
        }

        [HttpGet("Actualizar")]
        public void Actualizar()
        {
            IEnumerable<Claim> claims = User.Claims;
            Guid usuario = new Guid(claims.FirstOrDefault(c => c.Type == "Id")?.Value);
            _notificacionesRepository.MarcarNotificacionesComoLeidas(usuario);
        }
    }
}
