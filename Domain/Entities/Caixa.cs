namespace EmbaladorPedidosApi.Domain.Entities
{
    public class Caixa
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public decimal Largura  { get; set; }
        public decimal Altura { get; set; }
        public decimal Profundidade { get; set; }

        public decimal Volume => Largura * Altura * Profundidade;
    }
}
