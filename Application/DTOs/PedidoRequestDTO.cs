namespace EmbaladorPedidosApi.Application.DTOs
{
    public class PedidoRequestDTO
    {
        public List<ProdutoDTO> Produtos { get; set; } = new();
    }
}
