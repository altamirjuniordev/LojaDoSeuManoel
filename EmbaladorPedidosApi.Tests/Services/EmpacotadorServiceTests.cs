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

        [Fact]
        public void Deve_Lancar_Excecao_Se_Produto_Nao_Caber_Em_Nenhuma_Caixa()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<PedidosDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb_Excessivo")
                .Options;

            using var context = new PedidosDbContext(options);
            context.Caixas.Add(new Caixa { Id = 1, Nome = "Caixa Pequena", Largura = 20, Altura = 20, Profundidade = 20 });
            context.SaveChanges();

            var service = new EmpacotadorService(context);

            var pedidos = new List<PedidoRequestDTO>
            {
                new PedidoRequestDTO
                {
                    Produtos = new List<ProdutoDTO>
                    {
                        new ProdutoDTO { Altura = 100, Largura = 100, Comprimento = 100 }
                    }
                }
            };

            // Act & Assert
            Assert.Throws<Exception>(() => service.Empacotar(pedidos));
        }
        [Fact]
        public void Deve_Empacotar_Produto_Que_Cabe_Exatamente_Na_Caixa()
        {
            var options = new DbContextOptionsBuilder<PedidosDbContext>()
                .UseInMemoryDatabase("TestDb_Exato")
                .Options;

            using var context = new PedidosDbContext(options);
            context.Caixas.Add(new Caixa { Id = 1, Nome = "Caixa Exata", Largura = 10, Altura = 10, Profundidade = 10 });
            context.SaveChanges();

            var service = new EmpacotadorService(context);

            var pedidos = new List<PedidoRequestDTO>
            {
                new PedidoRequestDTO
                {
                    Produtos = new List<ProdutoDTO>
                    {
                        new ProdutoDTO { Altura = 10, Largura = 10, Comprimento = 10 }
                    }
                }
            };

            var resultado = service.Empacotar(pedidos);

            Assert.Single(resultado);
            Assert.Single(resultado[0].CaixasUsadas);
            Assert.Single(resultado[0].CaixasUsadas[0].Produtos);
        }
        [Fact]
        public void Deve_Usar_Multiplas_Caixas_Se_Necessario()
        {
            var options = new DbContextOptionsBuilder<PedidosDbContext>()
                .UseInMemoryDatabase("TestDb_MultiplasCaixas")
                .Options;

            using var context = new PedidosDbContext(options);
            context.Caixas.Add(new Caixa { Id = 1, Nome = "Caixa Média", Largura = 20, Altura = 20, Profundidade = 20 });
            context.SaveChanges();

            var service = new EmpacotadorService(context);

            var pedidos = new List<PedidoRequestDTO>
            {
                new PedidoRequestDTO
                {
                    Produtos = new List<ProdutoDTO>
                    {
                        new ProdutoDTO { Altura = 20, Largura = 20, Comprimento = 20 },
                        new ProdutoDTO { Altura = 20, Largura = 20, Comprimento = 20 }
                    }
                }
            };

            var resultado = service.Empacotar(pedidos);

            Assert.Single(resultado);
            Assert.Equal(2, resultado[0].CaixasUsadas.Count); // Um produto por caixa
        }
        [Fact]
        public void Deve_Empacotar_Corretamente_Multiplos_Pedidos()
        {
            var options = new DbContextOptionsBuilder<PedidosDbContext>()
                .UseInMemoryDatabase("TestDb_MultiplosPedidos")
                .Options;

            using var context = new PedidosDbContext(options);
            context.Caixas.AddRange(
                new Caixa { Id = 1, Nome = "Caixa Pequena", Largura = 10, Altura = 10, Profundidade = 10 },
                new Caixa { Id = 2, Nome = "Caixa Média", Largura = 20, Altura = 20, Profundidade = 20 }
            );
            context.SaveChanges();

            var service = new EmpacotadorService(context);

            var pedidos = new List<PedidoRequestDTO>
            {
                new PedidoRequestDTO
                {
                    Produtos = new List<ProdutoDTO>
                    {
                        new ProdutoDTO { Altura = 10, Largura = 10, Comprimento = 10 }
                    }
                },
                new PedidoRequestDTO
                {
                    Produtos = new List<ProdutoDTO>
                    {
                        new ProdutoDTO { Altura = 20, Largura = 20, Comprimento = 20 }
                    }
                }
            };

            var resultado = service.Empacotar(pedidos);

            Assert.Equal(2, resultado.Count); // dois pedidos processados
            Assert.Single(resultado[0].CaixasUsadas);
            Assert.Single(resultado[1].CaixasUsadas);
        }

    }
}
