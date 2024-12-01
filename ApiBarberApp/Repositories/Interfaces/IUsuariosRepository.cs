using ApiBarberApp.Models;

namespace ApiBarberApp.Repositories.Interfaces
{
    public interface IUsuariosRepository
    {
        Task<List<Usuario>> ConsultarUsuarios();
        Task<List<Usuario>> ConsultarUsuariosConRol();
        Task<Usuario> ConsultarUsuarioPorId(Guid id);
        Task<Usuario> ConsultarUsuarioPorIdConRol(Guid id);
        Task<List<Usuario>> ConsultarBarbersActivos();
        Task RegistrarUsuario(Usuario usuario);
        Task ActualizarUsuario(Usuario usuario);
        Task ActualizarMiUsuario(Usuario usuario);
        Task ActualizarMiClave(Usuario usuario);
        Task DesactivarUsuario(Guid id);
        Task ActivarUsuario(Guid id);
        Task<Usuario> IniciarSesion(string correo, string clave);
    }
}
