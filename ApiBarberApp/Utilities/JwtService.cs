using ApiBarberApp.Models;
using Microsoft.IdentityModel.Tokens;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ApiBarberApp.Utilities
{
    public interface IJwtService
    {
        string GenerarToken(Usuario usuario);
    }

    public class JwtService : IJwtService
    {
        private readonly IConfiguration _configuration;

        public JwtService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerarToken(Usuario usuario)
        {
            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));
            var credentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim("Id", usuario.Id.ToString()),
                new Claim("Nombre", usuario.Nombre + usuario.Apellidos),
                new Claim("Genero", usuario.Genero),
                new Claim("FechaNacimiento", usuario.FechaNacimiento.ToString("dd/MM/yyyy", new CultureInfo("es-ES"))),
                new Claim("FechaCreacion", usuario.FechaCreacion.ToString("dd/MM/yyyy", new CultureInfo("es-ES"))),
                new Claim("FechaActualizacion", usuario.FechaActualizacion.ToString("dd/MM/yyyy", new CultureInfo("es-ES"))),
                new Claim("Correo", usuario.Correo),
                new Claim("IdRol", usuario.IdRol.ToString()),
                new Claim("NombreRol", usuario.Rol?.Nombre ?? "Usuario")
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:Issuer"],
                audience: _configuration["JWT:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(double.Parse(_configuration["JWT:TokenExpirationInMinutes"])),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
