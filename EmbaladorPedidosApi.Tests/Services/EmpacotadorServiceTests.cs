using Xunit;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using EmbaladorPedidosApi.Domain.Entities;
using EmbaladorPedidosApi.Infrastructure;
using EmbaladorPedidosApi.Application.DTOs;
using EmbaladorPedidosApi.Application.Services;

namespace EmbaladorPedidosApi.Tests.Services
{
    public class EmpacotadorServiceTests
    {
        [Fact]
        public void Deve_Empacotar_Produtos_Em_Caixas_Compatíveis()
        {
            // Arrange: configurar contexto em memória com caixas
            var options = new DbContextOptionsBuilder<PedidosDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;

            using var context = new PedidosDbContext(options);

            context.Caixas.AddRange(
                new Caixa { Id = 1, Nome = "Caixa Pequena", Largura = 20, Altura = 20, Profundidade = 20 },
                new Caixa { Id = 2, Nome = "Caixa Média", Largura = 30, Altura = 30, Profundidade = 30 },
                new Caixa { Id = 3, Nome = "Caixa Grande", Largura = 40, Altura = 40, Profundidade = 40 }
            );
            context.SaveChanges();

            var service = new EmpacotadorService(context);

            var pedidos = new List<PedidoRequestDTO>
            {
                new PedidoRequestDTO
                {
                    Produtos = new List<ProdutoDTO>
                    {
                        new ProdutoDTO { Altura = 10, Largura = 10, Comprimento = 10 },
                        new ProdutoDTO { Altura = 20, Largura = 20, Comprimento = 20 }
                    }
                }
            };

            // Act
            var resultado = service.Empacotar(pedidos);

            // Asser2t
            Assert.NotNull(resultado);
            Assert.Single(resultado); // 1 pedido
            Assert.True(resultado[0].CaixasUsadas.Count >= 1);
            Assert.Equal(2, resultado[0].CaixasUsadas.SelectMany(c => c.Produtos).Count());
        }
    }
}
