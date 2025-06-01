namespace EmbaladorPedidosApi.Application.DTOs
{
    public class CaixaDTO
    {
        public string TipoCaixa { get; set; } = string.Empty;
        public List<ProdutoDTO> Produtos { get; set; } = new();
    }
}
