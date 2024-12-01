using ApiBarberApp.Data;
using ApiBarberApp.Models;
using ApiBarberApp.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace ApiBarberApp.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly ApiBarberAppDbContext _context;

        public AuthRepository(ApiBarberAppDbContext context)
        {
            _context = context;
        }

        public async Task<Usuario> ValidarCredenciales(string correo, string clave)
        {
            return await _context.Usuarios
                .Include(u => u.Rol)
                .FirstOrDefaultAsync(u => u.Correo == correo && u.Clave == clave && u.Activo == 1);
        }
    }
}
