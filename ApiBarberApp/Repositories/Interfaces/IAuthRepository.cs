using ApiBarberApp.Models;
using System.Threading.Tasks;

namespace ApiBarberApp.Repositories.Interfaces
{
    public interface IAuthRepository
    {
        Task<Usuario> ValidarCredenciales(string correo, string clave);
    }
}
