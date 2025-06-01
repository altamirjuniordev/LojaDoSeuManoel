using EmbaladorPedidosApi.Domain.Entities;
using EmbaladorPedidosApi.Infrastructure;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EmbaladorPedidosApi.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly PedidosDbContext _context;
        private readonly IConfiguration _configuration;

        public AuthService(PedidosDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public string GerarToken(Usuario usuario)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                new Claim(ClaimTypes.Name, usuario.Username)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Issuer"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(2),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public Usuario? Autenticar(string username, string senha)
        {
            var usuario = _context.Usuarios.FirstOrDefault(u => u.Username == username);
            if (usuario == null)
                return null;

            bool senhaValida = BCrypt.Net.BCrypt.Verify(senha, usuario.SenhaHash);
            return senhaValida ? usuario : null;
        }

        public Usuario CriarUsuario(string username, string senha)
        {
            var senhaHash = BCrypt.Net.BCrypt.HashPassword(senha);

            var usuario = new Usuario
            {
                Username = username,
                SenhaHash = senhaHash
            };

            _context.Usuarios.Add(usuario);
            _context.SaveChanges();

            return usuario;
        }
    }
}
