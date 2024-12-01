using ApiBarberApp.Data;
using ApiBarberApp.Models;
using ApiBarberApp.Repositories.Interfaces;
using ApiBarberApp.Utilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ApiBarberApp.Repositories
{
    public class UsuariosRepository : IUsuariosRepository
    {
        private readonly ApiBarberAppDbContext _context;

        public UsuariosRepository(ApiBarberAppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Usuario>> ConsultarUsuarios()
        {
            return await _context.Usuarios.
                Select(item => new Usuario()
                {
                    Id = item.Id,
                    Correo = item.Correo,
                    Clave = string.Empty,
                    Nombre = item.Nombre,
                    Apellidos = item.Apellidos,
                    Genero = item.Genero,
                    FechaNacimiento = item.FechaNacimiento,
                    Activo = item.Activo,
                    FechaCreacion = item.FechaCreacion,
                    FechaActualizacion = item.FechaActualizacion,
                    IdRol = item.IdRol
                }).ToListAsync();
        }

        public async Task<List<Usuario>> ConsultarUsuariosConRol()
        {
            return await _context.Usuarios
                .Include(item => item.Rol)
                .Select(item => new Usuario
                {
                    Id = item.Id,
                    Correo = item.Correo,
                    Clave = string.Empty,
                    Nombre = item.Nombre,
                    Apellidos = item.Apellidos,
                    Genero = item.Genero,
                    FechaNacimiento = item.FechaNacimiento,
                    Activo = item.Activo,
                    FechaCreacion = item.FechaCreacion,
                    FechaActualizacion = item.FechaActualizacion,
                    IdRol = item.IdRol,
                    Rol = item.Rol
                })
                .ToListAsync();
        }

        public async Task<Usuario> ConsultarUsuarioPorId(Guid id)
        {
            Usuario? usuario = await _context.Usuarios.FindAsync(id);
            return usuario == null ? new Usuario() : usuario;
        }

        public async Task<Usuario> ConsultarUsuarioPorIdConRol(Guid id)
        {
            Usuario? usuarioConRol = await _context.Usuarios
                .Include(item => item.Rol)
                .Where(item => item.Id == id)
                .Select(item => new Usuario
                {
                    Id = item.Id,
                    Correo = item.Correo,
                    Nombre = item.Nombre,
                    Apellidos = item.Apellidos,
                    Genero = item.Genero,
                    FechaNacimiento = item.FechaNacimiento,
                    Activo = item.Activo,
                    FechaCreacion = item.FechaCreacion,
                    FechaActualizacion = item.FechaActualizacion,
                    Rol = item.Rol
                })
                .FirstOrDefaultAsync();
            return usuarioConRol == null ? new Usuario() : usuarioConRol;
        }

        public async Task<List<Usuario>> ConsultarBarbersActivos()
        {
            List<Usuario>? usuarios = await _context.Usuarios
                .Include(item => item.Rol)
                .Where(item => item.Rol.Nombre.ToUpper().Contains("BARBER") && item.Activo == 1)
                .Select(item => new Usuario
                {
                    Id = item.Id,
                    Correo = item.Correo,
                    Nombre = item.Nombre,
                    Apellidos = item.Apellidos,
                    Genero = item.Genero,
                    FechaNacimiento = item.FechaNacimiento,
                    Activo = item.Activo,
                    FechaCreacion = item.FechaCreacion,
                    FechaActualizacion = item.FechaActualizacion,
                    Rol = item.Rol
                })
                .ToListAsync();
            return usuarios == null ? new List<Usuario>() : usuarios;
        }

        public async Task RegistrarUsuario(Usuario usuario)
        {
            Rol? rol = await _context.Roles.Where(item => item.Nombre.ToUpper().Contains("CLIENTE")).FirstOrDefaultAsync();
            List<Usuario> usuariosMismoCorreo = await _context.Usuarios.Where(item => item.Correo.Equals(usuario.Correo)).ToListAsync();
            if (usuariosMismoCorreo == null) usuariosMismoCorreo = new List<Usuario>();
            if (usuariosMismoCorreo.Any())
            {
                if (usuariosMismoCorreo.FirstOrDefault().Activo == 1)
                {
                    throw new GeneralException("Ya existe una cuenta con el correo ingresado.", StatusCodes.Status400BadRequest);
                }
                else
                {
                    throw new GeneralException("Ya existe una cuenta con el correo ingresado, pero se encuentra desactivado. Comuniquese con un administrador", StatusCodes.Status400BadRequest);
                }
            }
            usuario.Id = Guid.NewGuid();
            usuario.FechaCreacion = Fecha.Actual();
            usuario.FechaActualizacion = new DateTime(2000, 1, 1);
            usuario.IdRol = rol.Id;
            await _context.Usuarios.AddAsync(usuario);
            await _context.SaveChangesAsync();
        }

        public async Task ActualizarUsuario(Usuario usuario)
        {
            Usuario? usuarioActual = await _context.Usuarios.FindAsync(usuario.Id);
            if (usuarioActual == null)
            {
                throw new GeneralException("El usuario que se intenta actualizar no se encuentra registrado en la aplicación.", StatusCodes.Status400BadRequest);
            }
            usuario.Clave = usuarioActual.Clave;
            usuario.FechaCreacion = usuarioActual.FechaCreacion;
            usuario.FechaActualizacion = Fecha.Actual();
            _context.Usuarios.Update(usuario);
            await _context.SaveChangesAsync();
        }

        public async Task ActualizarMiUsuario(Usuario usuario)
        {
            Usuario? usuarioActual = await _context.Usuarios.FindAsync(usuario.Id);
            if (usuarioActual == null)
            {
                throw new GeneralException("El usuario que se intenta actualizar no se encuentra registrado en la aplicación.", StatusCodes.Status400BadRequest);
            }
            usuarioActual.Nombre = usuario.Nombre;
            usuarioActual.Apellidos = usuario.Apellidos;
            usuarioActual.Genero = usuario.Genero;
            usuarioActual.FechaNacimiento = usuario.FechaNacimiento;
            usuarioActual.FechaActualizacion = Fecha.Actual();
            if (usuario.Clave.Length == 4)
            {
                usuarioActual.Clave = usuario.Clave;
            }
            _context.Usuarios.Update(usuarioActual);
            await _context.SaveChangesAsync();
        }

        public async Task ActualizarMiClave(Usuario usuario)
        {
            Usuario? usuarioActual = await _context.Usuarios.FindAsync(usuario.Id);
            if (usuarioActual == null)
            {
                throw new GeneralException("El usuario que se intenta actualizar no se encuentra registrado en la aplicación.", StatusCodes.Status400BadRequest);
            }
            usuarioActual.Activo = 1;
            usuarioActual.Clave = usuario.Clave;
            _context.Usuarios.Update(usuarioActual);
            await _context.SaveChangesAsync();
        }

        /*** Eliminación lógica del usuario ***/
        public async Task DesactivarUsuario(Guid id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                throw new GeneralException("El usuario que se intenta desactivar no se encuentra registrado en la aplicación.", StatusCodes.Status400BadRequest);
            }
            usuario.Activo = 0;
            await _context.SaveChangesAsync();
        }

        /*** Activación del usuario ***/
        public async Task ActivarUsuario(Guid id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                throw new GeneralException("El usuario que se intenta activar no se encuentra registrado en la aplicación.", StatusCodes.Status400BadRequest);
            }
            usuario.Activo = 1;
            await _context.SaveChangesAsync();
        }
        public async Task<Usuario> IniciarSesion(string correo, string clave)
        {

            Usuario? usuario = new Usuario() { Id = Guid.Empty }; 
            usuario = await _context.Usuarios
                .Include(item => item.Rol)
                .Where(item => item.Correo == correo && item.Clave == clave)
                .Select(item => new Usuario
                {
                    Id = item.Id,
                    Correo = item.Correo,
                    Clave = string.Empty,
                    Nombre = item.Nombre,
                    Apellidos = item.Apellidos,
                    Genero = item.Genero,
                    FechaNacimiento = item.FechaNacimiento,
                    Activo = item.Activo,
                    FechaCreacion = item.FechaCreacion,
                    FechaActualizacion = item.FechaActualizacion,
                    IdRol = item.IdRol,
                    Rol = item.Rol
                }).FirstOrDefaultAsync();
            if (usuario == null)
            {
                throw new GeneralException("Ha ocurrido un error al buscar la información del usuario con el que intenta iniciar sesión.", StatusCodes.Status400BadRequest);
            }
            return usuario;
        }
    }
}
