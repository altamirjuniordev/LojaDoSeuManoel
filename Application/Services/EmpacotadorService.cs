using Microsoft.EntityFrameworkCore;
using EmbaladorPedidosApi.Application.DTOs;
using EmbaladorPedidosApi.Infrastructure;

namespace EmbaladorPedidosApi.Application.Services
{
    public class EmpacotadorService : IEmpacotadorService
    {
        private readonly PedidosDbContext _context;
        
        public EmpacotadorService(PedidosDbContext context)
        {
            _context = context;
        }

        public List<EmpacotamentoResponseDTO> Empacotar(List<PedidoRequestDTO > pedidos)
        {
            var resultado = new List<EmpacotamentoResponseDTO>();

            var caixasDisponiveis = _context.Caixas
                .AsNoTracking()
                .AsEnumerable()
                .OrderBy(c => c.Volume)
                .ToList();

            foreach (var pedido in pedidos)
            {
                var caixasUsadas = new List<CaixaDTO>();

                var produtosOrdenados = pedido.Produtos
                    .OrderByDescending(p => p.Volume)
                    .ToList();

                foreach (var produto in produtosOrdenados)
                {
                    var volumeProduto = produto.Volume;

                    bool encaixado = false;

                    foreach(var caixa in caixasUsadas)
                    {
                        var tipoCaixa = caixasDisponiveis.FirstOrDefault(c => c.Nome == caixa.TipoCaixa);
                        var volumeOcupado = caixa.Produtos.Sum(p => p.Volume);
                        var volumeLivre = (tipoCaixa.Volume ) - volumeOcupado;

                        if(volumeProduto <= volumeLivre)
                        {
                            caixa.Produtos.Add(produto);
                            encaixado = true;
                            break;
                        }
                    }

                    if(!encaixado)
                    {
                        var caixaTipo = caixasDisponiveis
                            .Where(c=> c.Volume >= volumeProduto)
                            .OrderBy(c => c.Volume)
                            .FirstOrDefault();   
                        
                        if(caixaTipo == null)
                            throw new Exception("Produto Excede a capacidade das caixas");

                        var novaCaixa = new CaixaDTO
                        {
                            TipoCaixa = caixaTipo.Nome,
                            Produtos = new List<ProdutoDTO> { produto }
                        };

                        caixasUsadas.Add(novaCaixa);
                    }
                }

                resultado.Add(new EmpacotamentoResponseDTO
                {
                    CaixasUsadas = caixasUsadas
                });
            }
            return resultado;
        }
    }
}
