using ApiBarberApp.Models;
using ApiBarberApp.Repositories.Interfaces;
using ApiBarberApp.Utilities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiBarberApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RolesController : ControllerBase
    {
        private readonly IRolesRepository _rolesRepository;

        public RolesController (IRolesRepository rolesRepository)
        {
            _rolesRepository = rolesRepository;
        }

        [HttpGet("Roles")]
        public async Task<ActionResult<RespuestaGeneral<List<Rol>>>> GetRoles()
        {
            RespuestaGeneral<List<Rol>> res = new RespuestaGeneral<List<Rol>>();
            try
            {
                List<Rol> roles = await _rolesRepository.ConsultarRoles();
                res = new RespuestaGeneral<List<Rol>>()
                {
                    estado = EstadosRespuesta.SUCCESS,
                    objeto = roles
                };
            }
            catch (Exception ex)
            {
                res = new RespuestaGeneral<List<Rol>>()
                {
                    mensaje = "Ocurrió un error.",
                    estado = EstadosRespuesta.ERROR,
                    error = ex.Message,
                    objeto = new List<Rol>()
                };
            }
            return Ok(res);
        }

        [HttpGet("Actualizar")]
        public async Task<ActionResult<RespuestaGeneral<Rol>>> GetRolPorId(Guid id)
        {
            RespuestaGeneral<Rol> res = new RespuestaGeneral<Rol>();
            try
            {
                Rol rol = await _rolesRepository.ConsultarRolPorId(id);
                res = new RespuestaGeneral<Rol>()
                {
                    estado = EstadosRespuesta.SUCCESS,
                    objeto = rol
                };
            }
            catch (Exception ex)
            {
                res = new RespuestaGeneral<Rol>()
                {
                    mensaje = "Ocurrió un error.",
                    estado = EstadosRespuesta.ERROR,
                    error = ex.Message,
                    objeto = new Rol()
                };
            }
            return Ok(res);
        }

        [HttpPut("Actualizar")]
        public async Task<ActionResult<RespuestaGeneral<string>>> ActualizarRol([FromBody] Rol rol)
        {
            RespuestaGeneral<string> res = new RespuestaGeneral<string>();
            try
            {
                await _rolesRepository.ActualizarDescripcionRol(rol);
                res = new RespuestaGeneral<string>()
                {
                    mensaje = "El rol se ha actualizado correctamente.",
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
                    error = ex.Message
                };
            }
            return Ok(res);
        }
    }
}
