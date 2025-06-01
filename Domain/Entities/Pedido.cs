namespace EmbaladorPedidosApi.Domain.Entities
{
    public class Pedido
    {
        public int Id { get; set; }
        public ICollection<Produto> Produtos { get; set; } = new List<Produto>();
    }
}
