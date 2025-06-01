using EmbaladorPedidosApi.Application.DTOs;
using EmbaladorPedidosApi.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace EmbaladorPedidosApi.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public IActionResult Register(RegisterRequestDTO dto)
        {
            var usuarioExistente = _authService.Autenticar(dto.Username, dto.Senha);
            if (usuarioExistente != null)
                return Conflict("Usuário já existe ou senha já corresponde à anterior.");

            var novoUsuario = _authService.CriarUsuario(dto.Username, dto.Senha);
            return Created("", new { novoUsuario.Id, novoUsuario.Username });
        }

        [HttpPost("login")]
        public IActionResult Login(LoginRequestDTO dto)
        {
            var usuario = _authService.Autenticar(dto.Username, dto.Senha);
            if (usuario == null)
                return Unauthorized("Credenciais inválidas.");

            var token = _authService.GerarToken(usuario);
            return Ok(new LoginResponseDTO { Token = token });
        }
    }
}
