using ApiBarberApp.Models;
using ApiBarberApp.Repositories.Interfaces;
using ApiBarberApp.Utilities;
using Azure.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ApiBarberApp.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuariosRepository _usuariosRepository;
        private readonly IJwtService _jwtService;

        public UsuariosController(IUsuariosRepository usuariosRepository, IJwtService jwtService)
        {
            _usuariosRepository = usuariosRepository;
            _jwtService = jwtService;   
        }

        [HttpGet("Usuarios")]
        public async Task<ActionResult<RespuestaGeneral<List<Usuario>>>> GetUsuarios()
        {
            RespuestaGeneral<List<Usuario>> res = new RespuestaGeneral<List<Usuario>>();
            try
            {
                List<Usuario> usuarios = await _usuariosRepository.ConsultarUsuarios();
                res = new RespuestaGeneral<List<Usuario>>()
                {
                    estado = EstadosRespuesta.SUCCESS,
                    objeto = usuarios
                };
            }
            catch (Exception ex)
            {
                res = new RespuestaGeneral<List<Usuario>>()
                {
                    mensaje = "Ocurrió un error.",
                    estado = EstadosRespuesta.ERROR,
                    error = ex.Message,
                    objeto = new List<Usuario>()
                };
            }
            return Ok(res);
        }

        [HttpGet("UsuariosConRol")]
        public async Task<ActionResult<RespuestaGeneral<List<Usuario>>>> GetUsuariosConRol()
        {
            RespuestaGeneral<List<Usuario>> res = new RespuestaGeneral<List<Usuario>>();
            try
            {
                List<Usuario> usuarios = await _usuariosRepository.ConsultarUsuariosConRol();
                res = new RespuestaGeneral<List<Usuario>>()
                {
                    estado = EstadosRespuesta.SUCCESS,
                    objeto = usuarios
                };
            }
            catch (Exception ex)
            {
                res = new RespuestaGeneral<List<Usuario>>()
                {
                    mensaje = "Ocurrió un error.",
                    estado = EstadosRespuesta.ERROR,
                    error = ex.Message,
                    objeto = new List<Usuario>()
                };
            }
            return Ok(res);
        }

        [HttpGet("MiPerfil")]   
        public async Task<ActionResult<RespuestaGeneral<Usuario>>> GetUsuarioPorId()
        {
            IEnumerable < Claim > claims = User.Claims;
            Guid id = new Guid(claims.FirstOrDefault(c => c.Type == "Id")?.Value);
            RespuestaGeneral<Usuario> res = new RespuestaGeneral<Usuario>();
            try
            {
                Usuario usuario = await _usuariosRepository.ConsultarUsuarioPorIdConRol(id);
                if (usuario.Id == Guid.Empty)
                {
                    res = new RespuestaGeneral<Usuario>()
                    {
                        mensaje = "No se encontró el usuario.",
                        estado = EstadosRespuesta.UNSUCCESSFUL
                    };
                }
                else
                {
                    res = new RespuestaGeneral<Usuario>()
                    {
                        mensaje = "Usuario encontrado.",
                        estado = EstadosRespuesta.SUCCESS,
                        objeto = usuario
                    };
                }
            }
            catch (Exception ex)
            {
                res = new RespuestaGeneral<Usuario>()
                {
                    mensaje = "Ocurrió un error.",
                    estado = EstadosRespuesta.ERROR,
                    error = ex.Message,
                    objeto = new Usuario()
                };
            }
            return Ok(res);
        }
        [HttpGet("Consultar/{id}")]
        public async Task<ActionResult<RespuestaGeneral<Usuario>>> GetUsuarioPorId(Guid id)
        {
            RespuestaGeneral<Usuario> res = new RespuestaGeneral<Usuario>();
            try
            {
                Usuario usuario = await _usuariosRepository.ConsultarUsuarioPorId(id);
                if (usuario.Id == Guid.Empty)
                {
                    res = new RespuestaGeneral<Usuario>()
                    {
                        mensaje = "No se encontró el usuario.",
                        estado = EstadosRespuesta.UNSUCCESSFUL
                    };
                }
                else
                {
                    res = new RespuestaGeneral<Usuario>()
                    {
                        mensaje = "Usuario encontrado.",
                        estado = EstadosRespuesta.SUCCESS,
                        objeto = usuario
                    };
                }
            }
            catch (Exception ex)
            {
                res = new RespuestaGeneral<Usuario>()
                {
                    mensaje = "Ocurrió un error.",
                    estado = EstadosRespuesta.ERROR,
                    error = ex.Message,
                    objeto = new Usuario()
                };
            }
            return Ok(res);
        }

        [HttpGet("ConsultarConRol/{id}")]
        public async Task<ActionResult<RespuestaGeneral<Usuario>>> GetUsuarioPorIdConRol(Guid id)
        {
            RespuestaGeneral<Usuario> res = new RespuestaGeneral<Usuario>();
            try
            {
                Usuario usuario = await _usuariosRepository.ConsultarUsuarioPorIdConRol(id);
                if (usuario.Id == Guid.Empty)
                {
                    res = new RespuestaGeneral<Usuario>()
                    {
                        mensaje = "No se encontró el usuario.",
                        estado = EstadosRespuesta.UNSUCCESSFUL
                    };
                }
                else
                {
                    res = new RespuestaGeneral<Usuario>()
                    {
                        mensaje = "Usuario encontrado.",
                        estado = EstadosRespuesta.SUCCESS,
                        objeto = usuario
                    };
                }
            }
            catch (Exception ex)
            {
                res = new RespuestaGeneral<Usuario>()
                {
                    mensaje = "Ocurrió un error.",
                    estado = EstadosRespuesta.ERROR,
                    error = ex.Message,
                    objeto = new Usuario()
                };
            }
            return Ok(res);
        }

        [HttpGet("ConsultarBarbers")]
        public async Task<ActionResult<RespuestaGeneral<List<Usuario>>>> ConsultarBarbers()
        {
            RespuestaGeneral<List<Usuario>> res = new RespuestaGeneral<List<Usuario>>();
            try
            {
                List<Usuario> usuarios = await _usuariosRepository.ConsultarBarbersActivos();
                res = new RespuestaGeneral<List<Usuario>>()
                {
                    estado = EstadosRespuesta.SUCCESS,
                    objeto = usuarios
                };
            }
            catch (Exception ex)
            {
                res = new RespuestaGeneral<List<Usuario>>()
                {
                    mensaje = "Ocurrió un error.",
                    estado = EstadosRespuesta.ERROR,
                    error = ex.Message,
                    objeto = new List<Usuario>()
                };
            }
            return Ok(res);
        }

        [AllowAnonymous]
        [HttpPost("Registrar")]
        public async Task<ActionResult<RespuestaGeneral<string>>> RegistrarUsuario([FromBody] Usuario usuario)
        {
            RespuestaGeneral<string> res = new RespuestaGeneral<string>();
            try
            {
                await _usuariosRepository.RegistrarUsuario(usuario);
                res = new RespuestaGeneral<string>()
                {
                    mensaje = "Registro de usuario exitoso.",
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

        [HttpPost("Actualizar")]
        public async Task<ActionResult<RespuestaGeneral<string>>> ActualizarUsuario([FromBody] Usuario usuario)
        {
            RespuestaGeneral<string> res = new RespuestaGeneral<string>();
            try
            {
                await _usuariosRepository.ActualizarUsuario(usuario);
                res = new RespuestaGeneral<string>()
                {
                    mensaje = "Actualización de usuario exitosa.",
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

        [HttpPost("ActualizarMiPerfil")]
        public async Task<ActionResult<RespuestaGeneral<string>>> ActualizarMiPerfil([FromBody] Usuario usuario)
        {
            RespuestaGeneral<string> res = new RespuestaGeneral<string>();
            try
            {
                await _usuariosRepository.ActualizarMiUsuario(usuario);
                res = new RespuestaGeneral<string>()
                {
                    mensaje = "Su perfil se ha actualizado correctamente.",
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

        [HttpPost("ActualizarClave")]
        public async Task<ActionResult<RespuestaGeneral<string>>> ActualizarClave([FromBody] Usuario usuario)
        {
            RespuestaGeneral<string> res = new RespuestaGeneral<string>();
            try
            {
                await _usuariosRepository.ActualizarMiClave(usuario);
                res = new RespuestaGeneral<string>()
                {
                    mensaje = "Registro de usuario exitoso.",
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

        [HttpPost("Activar/{id}")]
        public async Task<ActionResult<RespuestaGeneral<string>>> ActivarUsuario(Guid id)
        {
            RespuestaGeneral<string> res = new RespuestaGeneral<string>();
            try
            {
                await _usuariosRepository.ActivarUsuario(id);
                res = new RespuestaGeneral<string>()
                {
                    mensaje = "El usuario fue activado.",
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

        [HttpPost("Desactivar/{id}")]
        public async Task<ActionResult<RespuestaGeneral<string>>> DesactivarUsuario(Guid id)
        {
            RespuestaGeneral<string> res = new RespuestaGeneral<string>();
            try
            {
                await _usuariosRepository.DesactivarUsuario(id);
                res = new RespuestaGeneral<string>()
                {
                    mensaje = "El usuario fue desactivado.",
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

        [AllowAnonymous]
        [HttpPost("IniciarSesion")]
        public async Task<ActionResult<UsuarioConRol>> IniciarSesion([FromBody] LoginDTO loginDTO)
        {
            RespuestaGeneral<Usuario> res = new RespuestaGeneral<Usuario>();
            try
            {
                Usuario usuario = await _usuariosRepository.IniciarSesion(loginDTO.Correo, loginDTO.Clave);
                if (usuario.Id == Guid.Empty)
                {
                    res = new RespuestaGeneral<Usuario>()
                    {
                        mensaje = "Usuario y/o contraseña incorrectos.",
                        estado = EstadosRespuesta.UNSUCCESSFUL
                    };
                }
                else
                {
                    var token = _jwtService.GenerarToken(usuario);
                    res = new RespuestaGeneral<Usuario>()
                    {
                        mensaje = "Inicio de sesión exitoso.",
                        estado = EstadosRespuesta.SUCCESS,
                        token = token,
                        objeto = usuario
                    };
                }
            }
            catch (GeneralException ex)
            {
                res = new RespuestaGeneral<Usuario>()
                {
                    mensaje = ex.Message,
                    estado = EstadosRespuesta.FAILURE
                };
            }
            catch (Exception ex)
            {
                res = new RespuestaGeneral<Usuario>()
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
