namespace EmbaladorPedidosApi.Domain.Entities
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string SenhaHash { get; set; } = string.Empty;
    }
}
