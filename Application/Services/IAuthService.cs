using EmbaladorPedidosApi.Domain.Entities;

namespace EmbaladorPedidosApi.Application.Services
{
    public interface IAuthService
    {
        string GerarToken(Usuario usuario);
        Usuario? Autenticar(string username, string senha);
        Usuario CriarUsuario(string username, string senha);
    }
}
