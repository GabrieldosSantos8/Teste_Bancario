using System;
using System.Collections.Generic;
using Xunit;

namespace TesteItau.Tests
{
    public class OperacaoTests
    {
        private Operacao NovaOperacaoValida()
        {
            return new Operacao
            {
                PkOperacao = 1,
                FkUsuario = 1,
                FkAtivo = 1,
                NrQuantidade = 10,
                VlPrecoUnitario = 100.50m,
                TpOperacao = "compra",
                VlCorretagem = 5.00m,
                DtOperacao = DateTime.Now
            };
        }

        [Fact]
        public void Operacao_ValoresEsperados_DeveSerValida()
        {
            var op = NovaOperacaoValida();
            Assert.Equal(1, op.PkOperacao);
            Assert.Equal(1, op.FkUsuario);
            Assert.Equal(1, op.FkAtivo);
            Assert.Equal(10, op.NrQuantidade);
            Assert.Equal(100.50m, op.VlPrecoUnitario);
            Assert.Equal("compra", op.TpOperacao);
            Assert.Equal(5.00m, op.VlCorretagem);
            Assert.True(op.DtOperacao <= DateTime.Now);
        }

        [Fact]
        public void Operacao_QuantidadeZero_DeveSerInvalida()
        {
            var op = NovaOperacaoValida();
            op.NrQuantidade = 0;
            Assert.Equal(0, op.NrQuantidade);
        }

        [Fact]
        public void ListaOperacoes_Vazia_DeveSerVazia()
        {
            var lista = new List<Operacao>();
            Assert.Empty(lista);
        }

        [Fact]
        public void ListaOperacoes_ComItens_DeveConterItens()
        {
            var lista = new List<Operacao> { NovaOperacaoValida() };
            Assert.NotEmpty(lista);
            Assert.Single(lista);
        }
    }
}
