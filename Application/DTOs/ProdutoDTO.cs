namespace EmbaladorPedidosApi.Application.DTOs
{
    public class ProdutoDTO
    {
        public decimal Altura { get; set; }
        public decimal Largura { get; set; }
        public int Comprimento { get; set; }
        public decimal Volume => Largura * Altura * Comprimento;
    }
}
