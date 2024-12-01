using ApiBarberApp.Models;
namespace ApiBarberApp.Repositories.Interfaces
{
    public interface IRolesRepository
    {
        Task<List<Rol>> ConsultarRoles();
        Task<Rol> ConsultarRolPorId(Guid id);
        Task ActualizarDescripcionRol(Rol rol);
    }
}
