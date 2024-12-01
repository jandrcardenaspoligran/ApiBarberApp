using ApiBarberApp.Data;
using ApiBarberApp.Models;
using ApiBarberApp.Repositories.Interfaces;
using ApiBarberApp.Utilities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiBarberApp.Repositories
{
    public class RolesRepository : IRolesRepository
    {
        private readonly ApiBarberAppDbContext _context;

        public RolesRepository(ApiBarberAppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Rol>> ConsultarRoles()
        {
            return await _context.Roles.ToListAsync();
        }

        public async Task<Rol> ConsultarRolPorId(Guid id)
        {
            Rol? rol = await _context.Roles.FindAsync(id);
            return rol == null ? new Rol() : rol;
        }

        public async Task ActualizarDescripcionRol(Rol rol)
        {
            Rol? rolActual = await _context.Roles.FindAsync(rol.Id);
            if (rolActual == null)
            {
                throw new GeneralException("Ocurrió un problema al intentar la descripción del rol. Intente nuevamente, si el error persiste contacte al Super Administrador", StatusCodes.Status400BadRequest);
            }
            rolActual.Descripcion = rol.Descripcion;
            rolActual.FechaActualizacion = Fecha.Actual();
            _context.Roles.Update(rolActual);
            await _context.SaveChangesAsync();
        }
    }
}
